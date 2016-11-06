using ComputersFactory.Models;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Reflection;

namespace ComputersFactory.Logic
{
    public class ExcelHandler
    {
        /// <summary>
        /// Reads an excel file
        /// </summary>
        /// <param name="filePath">The excel file path and name</param>
        /// <param name="sheetName">The sheet name to be read</param>
        /// <returns>Returns a dictionary where the key is the column name and the value is a list of the columns values</returns>
        public static Dictionary<string, List<string>> ReadFromFile(string filePath, string sheetName)
        {
            var result = new Dictionary<string, List<string>>();

            string connectionString = $@"Provider=Microsoft.ACE.OLEDB.12.0;
                                        Data Source={filePath};
                                        Extended Properties='Excel 12.0;HDR=Yes;'";

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand($"SELECT * FROM [{sheetName}$]", connection);

                using (OleDbDataReader dataReader = command.ExecuteReader())
                {
                    var columnNames = dataReader.GetSchemaTable();
                    List<List<string>> data = new List<List<string>>();

                    for (int i = 0; i < columnNames.Rows.Count; i++)
                    {
                        data.Add(new List<string>());
                    }

                    while (dataReader.Read())
                    {
                        for (int i = 0; i < columnNames.Rows.Count; i++)
                        {
                            data[i].Add(dataReader[i].ToString());
                        }
                    }

                    for (int i = 0; i < columnNames.Rows.Count; i++)
                    {
                        result[columnNames.Rows[i]["ColumnName"].ToString()] = data[i];
                    }
                }
            }

            return result;
        }

        public void TransferToExcel(Dictionary<string, List<string>> data, string filePath, string sheetName)
        {
            string connectionString = $@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = {filePath}; Extended Properties = ""Excel 12.0 Macro;HDR=YES""";

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();

                int length = data.Values.FirstOrDefault().Count;
                for (int i = 0; i < length; i++)
                {
                    string values = "";
                    string cols = "";
                    foreach (var pair in data)
                    {
                        string value = "'" + pair.Value[i] + "'";
                        values += value + ", ";

                        cols += pair.Key + ", ";
                    }

                    values = values.Substring(0, values.Length - 2);
                    cols = cols.Substring(0, cols.Length - 2);

                    string sql = $"INSERT INTO [{sheetName}$] ({cols}) VALUES({values});";
                    OleDbCommand command = new OleDbCommand(sql, connection);
                    command.ExecuteNonQuery();
                }
            }
        }

        public static void TransferItems(Dictionary<string, List<string>> data, Type dataType)
        {
            using (var factoryContext = new ComputersFactoryContext())
            {
                var itemSet = factoryContext.Set(dataType);
                int count = data[data.Keys.FirstOrDefault()].Count;
                for (int i = 0; i < count; i++)
                {
                    var item = Activator.CreateInstance(dataType);
                    foreach (var key in data.Keys)
                    {
                        var itemProperty = item.GetType().GetProperty(key);
                        Type propertyType = itemProperty.PropertyType;
                        var value = Convert.ChangeType(data[key][i], propertyType);
                        itemProperty.SetValue(item, value);
                    }

                    itemSet.Add(item);
                }

                factoryContext.SaveChanges();
            }
        }

        public static void TransferAllData()
        {
            var defaultSheet = "Sheet1";
            var excelPath = @"..\..\..\..\Excel\";
            var excelFileExt = ".xlsx";
            var namespaceName = "ComputersFactory.Models.";

            //Keep this order
            var orderedItemTypes = new string[]
            {
                "Manufacturers",
                "ComputerTypes",
                "Processors",
                "Memorycards",
                "Videocards",
                "Computers"
            };

            foreach (var itemType in orderedItemTypes)
            {
                var typeName = namespaceName + itemType;
                Assembly assem = typeof(ComputersFactoryContext).Assembly;
                Type type = assem.GetType(typeName);
                
                var excelFileName = excelPath + itemType + excelFileExt;
                TransferItems(ReadFromFile(excelFileName, defaultSheet), type);
            }
        }
    }
}

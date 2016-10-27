using System.Collections.Generic;
using System.Data.OleDb;

namespace ComputersFactory
{
    public static class ExcelHandler
    {

        /// <summary>
        /// Reads an excel file
        /// </summary>
        /// <param name="filePath">The excel file path and name</param>
        /// <param name="sheetName">The sheet name to be read</param>
        /// <returns>Returns a dictionary where the key is the column name and the value is a list of the columns values</returns>
        public static Dictionary<string, List<string>> ReadFile(string filePath, string sheetName)
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
    }
}

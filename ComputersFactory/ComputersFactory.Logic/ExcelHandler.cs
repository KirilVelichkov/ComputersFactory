using ComputersFactory.Models;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;

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

		public static void TransferToExcel(Dictionary<string, List<string>> data, string filePath, string sheetName)
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

		public static void TransferComputers(Dictionary<string, List<string>> data)
        {
            var factoryContext = new ComputersFactoryContext();

            var computerTypeIds = new List<int>();
            var processorIds = new List<int>();
            var memorycardIds = new List<int>();
            var videocardIds = new List<int>();
            var prices = new List<decimal>();

            var computers = new List<Computer>();

            var first = data.Keys.First();
            var count = data[first].Count;

            foreach (var dataItem in data)
            {
                foreach (var item in dataItem.Value)
                {
                    if (dataItem.Key == "ComputerTypeId")
                    {
                        computerTypeIds.Add(int.Parse(item));
                    }
                    if (dataItem.Key == "ProcessorId")
                    {
                        processorIds.Add(int.Parse(item));
                    }
                    if (dataItem.Key == "MemorycardId")
                    {
                        memorycardIds.Add(int.Parse(item));
                    }
                    if (dataItem.Key == "VideocardId")
                    {
                        videocardIds.Add(int.Parse(item));
                    }
                    if (dataItem.Key == "Price")
                    {
                        prices.Add(decimal.Parse(item));
                    }
                }
            }

            for (int i = 0; i < count; i++)
            {
                computers.Add(new Computer()
                {
                    ComputerTypeId = computerTypeIds[i],
                    ProcessorId = processorIds[i],
                    MemorycardId = memorycardIds[i],
                    VideocardId = videocardIds[i],
                    Price = prices[i]
                });
            }

            using (factoryContext)
            {
                foreach (var computer in computers)
                {
                    factoryContext.Computers.Add(computer);
                }
                factoryContext.SaveChanges();
            }
        }

        public static void TransferManufacturers(Dictionary<string, List<string>> data)
        {
            var factoryContext = new ComputersFactoryContext();

            var manufacturers = new List<Manufacturer>();

            foreach (var dataItem in data)
            {
                foreach (var item in dataItem.Value)
                {
                    if (dataItem.Key == "Name")
                    {
                        manufacturers.Add(new Manufacturer()
                        {
                            Name = item
                        });
                    }
                }
            }

            using (factoryContext)
            {
                foreach (var manufacturer in manufacturers)
                {
                    factoryContext.Manufacturers.Add(manufacturer);
                }
                factoryContext.SaveChanges();
            }
        }

        public static void TransferProcessors(Dictionary<string, List<string>> data)
        {
            var factoryContext = new ComputersFactoryContext();

            var manufacturerIds = new List<int>();
            var models = new List<string>();
            var mhzs = new List<double>();
            var prices = new List<decimal>();

            var processors = new List<Processor>();

            var first = data.Keys.First();
            var count = data[first].Count;

            foreach (var dataItem in data)
            {
                foreach (var item in dataItem.Value)
                {
                    if (dataItem.Key == "ManufacturerId")
                    {
                        manufacturerIds.Add(int.Parse(item));
                    }
                    if (dataItem.Key == "Model")
                    {
                        models.Add(item);
                    }
                    if (dataItem.Key == "MhZ")
                    {
                        mhzs.Add(double.Parse(item));
                    }
                    if (dataItem.Key == "Price")
                    {
                        prices.Add(decimal.Parse(item));
                    }
                }
            }

            for (int i = 0; i < count; i++)
            {
                processors.Add(new Processor()
                {
                    ManufacturerId = manufacturerIds[i],
                    MhZ = mhzs[i],
                    Model = models[i],
                    Price = prices[i]
                });
            }

            using (factoryContext)
            {
                foreach (var processor in processors)
                {
                    factoryContext.Processors.Add(processor);
                }
                factoryContext.SaveChanges();
            }
        }

        public static void TransferMemorycards(Dictionary<string, List<string>> data)
        {
            var factoryContext = new ComputersFactoryContext();

            var manufacturerIds = new List<int>();
            var capacity = new List<string>();
            var mhzs = new List<double>();
            var prices = new List<decimal>();

            var memorycards = new List<Memorycard>();

            var first = data.Keys.First();
            var count = data[first].Count;

            foreach (var dataItem in data)
            {
                foreach (var item in dataItem.Value)
                {
                    if (dataItem.Key == "ManufacturerId")
                    {
                        manufacturerIds.Add(int.Parse(item));
                    }
                    if (dataItem.Key == "Capacity")
                    {
                        capacity.Add(item);
                    }
                    if (dataItem.Key == "MhZ")
                    {
                        mhzs.Add(double.Parse(item));
                    }
                    if (dataItem.Key == "Price")
                    {
                        prices.Add(decimal.Parse(item));
                    }
                }
            }

            for (int i = 0; i < count; i++)
            {
                memorycards.Add(new Memorycard()
                {
                    ManufacturerId = manufacturerIds[i],
                    MhZ = mhzs[i],
                    Capacity = capacity[i],
                    Price = prices[i]
                });
            }

            using (factoryContext)
            {
                foreach (var memorycard in memorycards)
                {
                    factoryContext.Memorycards.Add(memorycard);
                }
                factoryContext.SaveChanges();
            }
        }

        public static void TransferVideocards(Dictionary<string, List<string>> data)
        {
            var factoryContext = new ComputersFactoryContext();

            var manufacturerIds = new List<int>();
            var models = new List<string>();
            var memory = new List<string>();
            var prices = new List<decimal>();

            var videocards = new List<Videocard>();

            var first = data.Keys.First();
            var count = data[first].Count;

            foreach (var dataItem in data)
            {
                foreach (var item in dataItem.Value)
                {
                    if (dataItem.Key == "ManufacturerId")
                    {
                        manufacturerIds.Add(int.Parse(item));
                    }
                    if (dataItem.Key == "Model")
                    {
                        models.Add(item);
                    }
                    if (dataItem.Key == "Memory")
                    {
                        memory.Add(item);
                    }
                    if (dataItem.Key == "Price")
                    {
                        prices.Add(decimal.Parse(item));
                    }
                }
            }

            for (int i = 0; i < count; i++)
            {
                videocards.Add(new Videocard()
                {
                    ManufacturerId = manufacturerIds[i],
                    Model = models[i],
                    Memory = memory[i],
                    Price = prices[i]
                });
            }

            using (factoryContext)
            {
                foreach (var videocard in videocards)
                {
                    factoryContext.Videocards.Add(videocard);
                }
                factoryContext.SaveChanges();
            }
        }

        public static void TransferComputerTypes(Dictionary<string, List<string>> data)
        {
            var factoryContext = new ComputersFactoryContext();

            var computertypes = new List<ComputerType>();

            foreach (var dataItem in data)
            {
                foreach (var item in dataItem.Value)
                {
                    if (dataItem.Key == "Type")
                    {
                        computertypes.Add(new ComputerType()
                        {
                            Type = item
                        });
                    }
                }
            }

            using (factoryContext)
            {
                foreach (var type in computertypes)
                {
                    factoryContext.ComputerTypes.Add(type);
                }
                factoryContext.SaveChanges();
            }
        }

        public static void TransfertAllData()
        {
            //Keep this order
            TransferManufacturers(ReadFromFile(@"..\..\..\..\Excel\Manufacturers.xlsx", "Sheet1"));
            TransferComputerTypes(ReadFromFile(@"..\..\..\..\Excel\Computertypes.xlsx", "Sheet1"));
            TransferProcessors(ReadFromFile(@"..\..\..\..\Excel\Processors.xlsx", "Sheet1"));
            TransferMemorycards(ReadFromFile(@"..\..\..\..\Excel\Memories.xlsx", "Sheet1"));
            TransferVideocards(ReadFromFile(@"..\..\..\..\Excel\Videocards.xlsx", "Sheet1"));
            TransferComputers(ReadFromFile(@"..\..\..\..\Excel\Computers.xlsx", "Sheet1"));
        }
    }
}

using MySql.Data.MySqlClient;
using ComputersFactory.Logic.Reports;
using ComputersFactory.Models;
using System.Collections.Generic;

namespace ComputersFactory.Logic
{
    public class MySQLHandler
    {
        private const string MySQLConnectionString = "server=127.0.0.1;uid=root;pwd=123456;";
        private const string CreateDatabaseCommand = "DROP DATABASE IF EXISTS ComputersFactory; CREATE DATABASE ComputersFactory";
        private const string CreateReportsTable =
            @"USE ComputersFactory;
            CREATE TABLE Reports(
                ID int PRIMARY KEY AUTO_INCREMENT,
                ManufacturerName varchar(150) CHARSET utf8,
                MemoryManufacturer varchar(150) CHARSET utf8,
                MemoryCapacity varchar(50) CHARSET utf8,
                ProcessorModel varchar(150) CHARSET utf8,
                ProcessorMhz double,
	            VideoCardModel varchar(150) CHARSET utf8,
                VideoCardMemory varchar(150) CHARSET utf8
            )";
        private ComputersFactoryContext dbContext;

        public MySQLHandler(ComputersFactoryContext context)
        {
            this.dbContext = context;
        }

        public void LoadReportsInMySql(DataReportGenerator dataGenerator)
        {
            var context = new MySqlConnection(MySQLConnectionString);
            context.Open();

            this.CreateDatabase(context);
            this.CreateTable(context);
            
            var reports = dataGenerator.FillWithData(this.dbContext);

            foreach (var report in reports)
            {
                string dbInsert =
                    @"USE ComputersFactory;
                    INSERT INTO reports 
                    (ManufacturerName, MemoryManufacturer, MemoryCapacity, ProcessorModel, ProcessorMhz, VideoCardModel, VideoCardMemory)
                    VALUES ("+ "'" + report.ManufacturerName + "'" + "," +
                        "'" + report.MemoryManufacturer + "'" + "," +
                        "'" + report.MemoryCapacity + "'" + "," +
                        "'" + report.ProcessorModel + "'" + "," + 
                        report.ProcessorMhz + "," +
                        "'" + report.VideoCardModel + "'" + "," +
                        "'" + report.VideoCardMemory + "'" + ")";

                this.InsertData(dbInsert, context);
            }

            context.Close();
        }

        private void CreateDatabase(MySqlConnection connection)
        {
            var dbCommand = new MySqlCommand(CreateDatabaseCommand, connection);
            dbCommand.ExecuteNonQuery();
        }

        private void CreateTable(MySqlConnection connection)
        {
            var tableCommand = new MySqlCommand(CreateReportsTable, connection);
            tableCommand.ExecuteNonQuery();
        }

        private void InsertData(string command, MySqlConnection connection)
        {
            var insertCommand = new MySqlCommand(command, connection);
            insertCommand.ExecuteNonQuery();
        }

		public Dictionary<string, List<string>> ReadTable(string table)
		{
			MySqlConnection connection = new MySqlConnection(MySQLConnectionString);
			var result = new Dictionary<string, List<string>>();

			connection.Open();
			connection.ChangeDatabase("computersfactory");

			using (connection)
			{
				MySqlCommand command = new MySqlCommand($"SELECT * FROM {table}", connection);

				using (MySqlDataReader dataReader = command.ExecuteReader())
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

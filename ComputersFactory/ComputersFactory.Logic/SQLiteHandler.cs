using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace ComputersFactory.Logic
{
	public static class SQLiteHandler
	{
		private const string ConnectionString = "Data Source={0}; Version=3;";

		public static Dictionary<string, List<string>> ReadTable(string filePath, string table)
		{
			var result = new Dictionary<string, List<string>>();
			string connString = string.Format(ConnectionString, filePath);

			using (SQLiteConnection connection = new SQLiteConnection(connString))
			{
				connection.Open();

				string query = $"SELECT * FROM {table}";
				SQLiteCommand command = new SQLiteCommand(query, connection);
				using (SQLiteDataReader dataReader = command.ExecuteReader())
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

		public static void TransferSQLiteData()
		{
			string path = @"..\..\..\Excel-Reports\{0}.xlsx";
			string dbPath = @"..\..\..\..\SQLiteDB\ComputersFactory.db";
			ExcelHandler.TransferToExcel(ReadTable(dbPath, "Computers"), string.Format(path, "Computers"), "Sheet1");
			ExcelHandler.TransferToExcel(ReadTable(dbPath, "Manufacturers"), string.Format(path, "Manufacturers"), "Sheet1");
			ExcelHandler.TransferToExcel(ReadTable(dbPath, "ComputerTypes"), string.Format(path, "ComputerTypes"), "Sheet1");
			ExcelHandler.TransferToExcel(ReadTable(dbPath, "Memories"), string.Format(path, "Memories"), "Sheet1");
			ExcelHandler.TransferToExcel(ReadTable(dbPath, "Processors"), string.Format(path, "Processors"), "Sheet1");
			ExcelHandler.TransferToExcel(ReadTable(dbPath, "VideoCards"), string.Format(path, "VideoCards"), "Sheet1");
		}
	}
}

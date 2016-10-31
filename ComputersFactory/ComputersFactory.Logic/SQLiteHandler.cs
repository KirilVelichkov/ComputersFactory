using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

		public static void TransferAllData()
		{
			string path = @"..\..\..\Excel-Reports\{0}.xlsx";
			string dbPath = @"..\..\..\..\SQLiteDB\ComputersFactory.db";
			TransferToExcel(ReadTable(dbPath, "Computers"), string.Format(path, "Computers"), "Sheet1");
			TransferToExcel(ReadTable(dbPath, "Manufacturers"), string.Format(path, "Manufacturers"), "Sheet1");
			TransferToExcel(ReadTable(dbPath, "ComputerTypes"), string.Format(path, "ComputerTypes"), "Sheet1");
			TransferToExcel(ReadTable(dbPath, "Memories"), string.Format(path, "Memories"), "Sheet1");
			TransferToExcel(ReadTable(dbPath, "Processors"), string.Format(path, "Processors"), "Sheet1");
			TransferToExcel(ReadTable(dbPath, "VideoCards"), string.Format(path, "VideoCards"), "Sheet1");
		}
	}
}

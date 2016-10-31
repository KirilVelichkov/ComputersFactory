using ComputersFactory.Models.Migrations;
using ComputersFactory.Logic;
using MongoDB.Bson;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.OleDb;
using System.Linq;
using ComputersFactory.Models;
using ComputersFactory.Logic.Reports;

namespace ComputersFactory.ConsoleClient
{
	public class StartUp
	{
		public static void Main()
		{
			//Database.SetInitializer(new MigrateDatabaseToLatestVersion<ComputersFactoryContext, Configuration>());

			//var context = new ComputersFactoryContext();
			//context.Database.CreateIfNotExists();


			//ExcelHandler.TransfertAllData();
			//Task1();

			//Task2();

			//Task3();

			//Task4();

			Task5();

			//Task6();

			//var mongo = new MongoDBHanlder("ScrewdriverDB");
			//mongo.TransferToMSSQL().Wait();

			// Creates xml report
			//var xmlReporter = new XmlExporter();
			//xmlReporter.CreateXmlReport(context);


			//Generate sample json reports
			//var exporter = new JsonExporter(context);
			//exporter.CreateJsonReports("../../../Json-Reports");
		}


		public static void Task1()
		{
			//Button - Create Database
			Database.SetInitializer(new MigrateDatabaseToLatestVersion<ComputersFactoryContext, Configuration>());

			var context = new ComputersFactoryContext();
			context.Database.CreateIfNotExists();

			//Button - Extract from Zip
			ZipHanlder.ExtractExcelFiles(@"..\..\..\..\Excel\Excel.zip");

			//Button - Load Data from MongoDB to MSSQL
			var mongo = new MongoDBHanlder("ScrewdriverDB");
			mongo.TransferToMSSQL().Wait();

			//Button - Load data from Excel to MSSQL
			ExcelHandler.TransfertAllData();

		}

		public static void Task2()
		{
			//Button - Create PDF Rerpot
			var context = new ComputersFactoryContext();

			var pdf = new PdfExporter(context);
			pdf.CreatePdf("../../../Pdf-Reports");
		}

		public static void Task3()
		{
			//Button - Create XML Report
			var context = new ComputersFactoryContext();
			var path = @"..\..\..\";
			var xmlReporter = new XmlExporter();
			xmlReporter.CreateXmlReport(context, path);
		}

		public static void Task4()
		{
			//Button - Create JSON Report and Load data to MySQL
			var context = new ComputersFactoryContext();
			var mySql = new MySQLHandler(context);

			mySql.LoadReportsInMySql();

			var exporter = new JsonExporter(context);
			exporter.CreateJsonReports("../../../Json-Reports");
		}

		public static void Task5()
		{
			XMLHandler.TransferXMLToSQLServer("../../../../XMLFile/Manufacturers.xml");
		}

		public static void Task6()
		{
			SQLiteHandler.TransferSQLiteData();
			MySQLHandler.TransferMySQLData();
		}

	}
}

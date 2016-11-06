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

            //var mongo = new MongoDBHanlder("ScrewdriverDB");
            //mongo.TransferToMSSQL().Wait();

            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<ComputersFactoryContext, Configuration>());

            //var context = new ComputersFactoryContext();
            //context.Database.CreateIfNotExists();


            ExcelHandler.TransferAllData();
            //Task1();

            //Task2();

            //Task3();

            //Task4();

            //Task5();

            //Task6();
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
			ExcelHandler.TransferAllData();

		}

		public static void Task2()
		{
			//Button - Create PDF Rerpot
			var context = new ComputersFactoryContext();

			var pdf = new PdfExporter(context);
			pdf.GenerateReport("../../../Pdf-Reports");
		}

		public static void Task3()
		{
			//Button - Create XML Report
			var context = new ComputersFactoryContext();
			var path = @"..\..\..\";
			var xmlReporter = new XmlExporter(context);
			xmlReporter.GenerateReport(path);
		}

		public static void Task4()
		{
			//Button - Create JSON Report and Load data to MySQL
			var context = new ComputersFactoryContext();
			var mySql = new MySQLHandler(context);
            var dataGenerator = new DataReportGenerator();
			mySql.LoadReportsInMySql(dataGenerator);

			var exporter = new JsonExporter(context);
			exporter.GenerateReport("../../../Json-Reports");
		}

		public static void Task5()
		{
			ComputersFactoryContext context = new ComputersFactoryContext();
			var rootNode = XMLHandler.ReadXMLFile("../../../../XMLFile/Manufacturers.xml");

			MongoDBHanlder mongoDbHandler = new MongoDBHanlder("ScrewdriverDB");

			XMLHandler.TransferXMLToSQLServer(context, rootNode);
			XMLHandler.TransferXMLToMongoDB(mongoDbHandler, rootNode);
		}

		public static void Task6()
		{
			SQLiteHandler.TransferSQLiteData();
            var excelHandler = new ExcelHandler();
            var context = new ComputersFactoryContext();
            var mySqlHandler = new MySQLHandler(context);
            var excelExporter = new ExcelExporter(excelHandler, mySqlHandler);
            excelExporter.GenerateReport("../../../Excel-Reports/Reports.xlsx");
        }
	}
}

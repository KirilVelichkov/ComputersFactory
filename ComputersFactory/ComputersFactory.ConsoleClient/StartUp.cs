using ComputersFactory.Models.Migrations;
using ComputersFactory.Logic.Reports;
using MongoDB.Bson;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.OleDb;
using System.Linq;
using ComputersFactory.Models;

namespace ComputersFactory.ConsoleClient
{
    public class StartUp
    {
        public static void Main()
        {

            //Dolnite 3 reda suzdavat bazata. Ne gi trii za sega
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ComputersFactoryContext, Configuration>());

            var context = new ComputersFactoryContext();
            //context.Database.CreateIfNotExists();



            //var mongo = new MongoDB("ScrewdriverDB");

            //Kogato viknesh red kod se 4upi na predposledniqt loop NQMAM IDEQ ZASHTO
            //Ako komentirash Computers Loop-a i runnesh metoda i posle razkomentirash loopa i pak go runnesh shte raboti....
            //mongo.TransferToMSSQL().Wait();

            // Creates xml report
            //var xmlReporter = new XmlExporter();
            //xmlReporter.CreateXmlReport(context);

            //Generate sample table in pdf file
            //var pdf = new PdfGenerator();
            //pdf.CreatePdf($"C:\\Users\\{Environment.UserName}\\Desktop\\ComputersFactory\\Processors.pdf", context);

            //Generate sample json reports
            //var exporter = new JsonExporter(context);
            //exporter.CreateJsonReport($"C:\\Users\\{Environment.UserName}\\Desktop\\ComputersFactory\\reports.json");
        }
    }
}

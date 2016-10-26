using CodeFirst;
using CodeFirst.Migrations;
using CodeFirst.Models;
using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputersFactory
{
    class StartUp
    {
        static void Main()
        {
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<ComputersFactoryContext, Configuration>());

            //var context = new ComputersFactoryContext();
            //context.Database.CreateIfNotExists();



            //-- READ FROM EXCEL
            //string con = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\Kiril\Desktop\ScrewdriverDB Team Project\Excel\Computers.xlsx;" +
            //             @"Extended Properties='Excel 8.0;HDR=Yes;'";

            //using (OleDbConnection connection = new OleDbConnection(con))
            //{
            //    connection.Open();
            //    OleDbCommand command = new OleDbCommand("SELECT * FROM [Sheet1$]", connection);
            //    using (OleDbDataReader dataReader = command.ExecuteReader())
            //    {
            //        var columnNames = dataReader.GetSchemaTable();
            //        Console.WriteLine(" {0}", new string('-', 25));
            //        Console.WriteLine("| {0} | {1} |", columnNames.Rows[0]["ColumnName"].ToString().PadRight(15), columnNames.Rows[1]["ColumnName"]);
            //        Console.WriteLine(" {0}", new string('-', 25));

            //        while (dataReader.Read())
            //        {
            //            var name = dataReader[0].ToString();
            //            var score = dataReader[1].ToString();
            //            Console.WriteLine("| {0} | {1} |", name.PadRight(15), score.PadRight(5));
            //        }
            //        Console.WriteLine(" {0}", new string('-', 25));
            //    }
            //}

            //--- EXTRACT ECXEL FROM  ZIP
            //var filePath = @"C:\Users\Kiril\Desktop\ScrewdriverDB Team Project\Excel\Excel.zip";
            //var fileName = new string[] { "Manufacturers.xlsx", "Processors.xlsx", "VideoCards.xlsx", "Memories.xlsx", "ComputerTypes.xlsx" };

            //using (ZipFile zip = Ionic.Zip.ZipFile.Read(filePath))
            //{
            //    foreach (var item in fileName)
            //    {


            //        ZipEntry e = zip[item];

            //        using (FileStream outputStream = new FileStream(@"C:\Users\Kiril\Desktop\ScrewdriverDB Team Project\Excel\" + item, FileMode.OpenOrCreate))
            //        {
            //            e.Extract(outputStream);
            //        }
            //    }
            //}









        }
    }
}

using CodeFirst;
using CodeFirst.Migrations;
using CodeFirst.Models;
using MongoDB.Bson;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputersFactory
{
    class StartUp
    {
        static void Main()
        {

            //Dolnite 3 reda suzdavat bazata. Ne gi trii za sega
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<ComputersFactoryContext, Configuration>());

            //var context = new ComputersFactoryContext();
            //context.Database.CreateIfNotExists();



            var mongo = new MongoDB("ScrewdriverDB");

            //Kogato viknesh red kod se 4upi na predposledniqt loop NQMAM IDEQ ZASHTO
            //Ako komentirash Computers Loop-a i runnesh metoda i posle razkomentirash loopa i pak go runnesh shte raboti....
            //mongo.TransferToMSSQL().Wait();
        }

    }
}

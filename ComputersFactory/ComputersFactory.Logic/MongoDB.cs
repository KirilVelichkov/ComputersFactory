using ComputersFactory.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ComputersFactory.Logic
{
    public class MongoDB
    {
        private const string conString = "mongodb://localhost:27017";
        private readonly MongoClient client;
        private readonly IMongoDatabase dataBase;

        public MongoDB(string dataBaseName)
        {
            this.client = new MongoClient(conString);
            this.dataBase = this.client.GetDatabase(dataBaseName);
        }

        private async Task<IList<Computer>> GetComputersData()
        {
            var collection = this.dataBase.GetCollection<BsonDocument>("Computers");

            var result = (await collection.Find(new BsonDocument()).ToListAsync())
                .Select(bs => BsonSerializer.Deserialize<Computer>(bs)).ToList();

            return result;
        }

        private async Task<IList<Processor>> GetProcessorsData()
        {
            var collection = this.dataBase.GetCollection<BsonDocument>("Processors");

            var result = (await collection.Find(new BsonDocument()).ToListAsync())
                .Select(bs => BsonSerializer.Deserialize<Processor>(bs)).ToList();

            return result;
        }

        public async Task<IList<Manufacturer>> GetManufacturersData()
        {
            var collection = this.dataBase.GetCollection<BsonDocument>("Manufacturers");

            var result = (await collection.Find(new BsonDocument()).ToListAsync())
                .Select(bs => BsonSerializer.Deserialize<Manufacturer>(bs)).ToList();

            return result;
        }

        private async Task<IList<ComputerType>> GetComputerTypesData()
        {
            var collection = this.dataBase.GetCollection<BsonDocument>("ComputerTypes");

            var result = (await collection.Find(new BsonDocument()).ToListAsync())
                .Select(bs => BsonSerializer.Deserialize<ComputerType>(bs)).ToList();

            return result;
        }

        private async Task<IList<Memorycard>> GetMemorycardsData()
        {
            var collection = this.dataBase.GetCollection<BsonDocument>("Memorycards");

            var result = (await collection.Find(new BsonDocument()).ToListAsync())
                .Select(bs => BsonSerializer.Deserialize<Memorycard>(bs)).ToList();

            return result;
        }

        private async Task<IList<Videocard>> GetVideocardsData()
        {
            var collection = this.dataBase.GetCollection<BsonDocument>("Videocards");

            var result = (await collection.Find(new BsonDocument()).ToListAsync())
                .Select(bs => BsonSerializer.Deserialize<Videocard>(bs)).ToList();

            return result;
        }

        public async Task TransferToMSSQL()
        {
            var factoryContext = new ComputersFactoryContext();

            var manufacturers = (await this.GetManufacturersData()).ToList();
            var processors = (await this.GetProcessorsData()).ToList();
            var memorycards = (await this.GetMemorycardsData()).ToList();
            var videocards = (await this.GetVideocardsData()).ToList();
            var computerTypes = (await this.GetComputerTypesData()).ToList();
            var computers = (await this.GetComputersData()).ToList();


            using (factoryContext)
            {
             
                foreach (var manufacturer in manufacturers)
                {
                    factoryContext.Manufacturers.Add(manufacturer);

                }

                foreach (var processor in processors)
                {
                    factoryContext.Processors.Add(processor);
                }

                foreach (var memorycard in memorycards)
                {
                    factoryContext.Memorycards.Add(memorycard);
                }

                foreach (var videocard in videocards)
                {
                    factoryContext.Videocards.Add(videocard);
                }

                foreach (var type in computerTypes)
                {
                    factoryContext.ComputerTypes.Add(type);

                }

                foreach (var computer in computers)
                {
                    factoryContext.Computers.Add(computer);
                }

                factoryContext.SaveChanges();
            }
        }
    }
}
using ComputersFactory.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ComputersFactory.Logic
{
	public class MongoDBHanlder
	{
		private const string conString = "mongodb://localhost:27017";
		private readonly MongoClient client;
		private readonly IMongoDatabase dataBase;

		public MongoDBHanlder(string dataBaseName)
		{
			this.client = new MongoClient(conString);
			this.dataBase = this.client.GetDatabase(dataBaseName);
		}


		private async Task<IList<T>> GetData<T>(string collectionName) where T : class
		{

			var collection = this.dataBase.GetCollection<BsonDocument>(collectionName);

			var result = (await collection.Find(new BsonDocument()).ToListAsync())
				.Select(bs => BsonSerializer.Deserialize<T>(bs)).ToList();

			return result;
		}

		public async Task TransferToMSSQL()
		{
			var factoryContext = new ComputersFactoryContext();

			var manufacturers = (await this.GetData<Manufacturer>("Manufacturers")).ToList();
			var processors = (await this.GetData<Processor>("Processors")).ToList();
			var memorycards = (await this.GetData<Memorycard>("Memorycards")).ToList();
			var videocards = (await this.GetData<Videocard>("Videocards")).ToList();
			var computerTypes = (await this.GetData<ComputerType>("ComputerTypes")).ToList();
			var computers = (await this.GetData<Computer>("Computers")).ToList();

			try
			{
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


					factoryContext.SaveChanges();

					foreach (var computer in computers)
					{
						factoryContext.Computers.Add(computer);
					}

					factoryContext.SaveChanges();
				}
			}
			catch (DbEntityValidationException e)
			{
				foreach (var eve in e.EntityValidationErrors)
				{
					Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
						eve.Entry.Entity.GetType().Name, eve.Entry.State);
					foreach (var ve in eve.ValidationErrors)
					{
						Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
							ve.PropertyName, ve.ErrorMessage);
					}
				}
				throw;
			}
		}

		public void InsertData(XmlNode xmlData)
		{
			foreach (XmlNode child in xmlData.ChildNodes)
			{
				var manufacturer = new BsonDocument()
				{
					{ "Name", child["name"].InnerText }
				};
				var collection = this.dataBase.GetCollection<BsonDocument>("Manufacturers");
				collection.InsertOne(manufacturer);
			}

		}
	}
}

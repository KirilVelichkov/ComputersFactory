using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using ComputersFactory.Logic;
using ComputersFactory.Models;

namespace ComputersFactory.Logic
{
	public class XMLHandler
	{
		public static XmlNode ReadXMLFile(string filePath)
		{
			XmlDocument document = new XmlDocument();
			document.Load(filePath);
			XmlNode rootNode = document.DocumentElement;

			return rootNode;
		}

		public static void TransferXMLToSQLServer(ComputersFactoryContext context, XmlNode rootNode)
		{
			foreach (XmlNode node in rootNode.ChildNodes)
			{
				context.Manufacturers.Add(new Manufacturer()
				{
					Name = node["name"].InnerText
				});
			}

			context.SaveChanges();
		}

		public static void TransferXMLToMongoDB(MongoDBHanlder mongoHandler, XmlNode rootNode)
		{
			mongoHandler.InsertData(rootNode);
		}
	}
}

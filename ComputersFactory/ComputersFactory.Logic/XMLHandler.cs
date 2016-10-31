using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using ComputersFactory.Models;

namespace ComputersFactory.Logic
{
	public class XMLHandler
	{
		public static void TransferXMLToSQLServer(string filePath)
		{
			XmlDocument document = new XmlDocument();
			document.Load(filePath);
			XmlNode rootNode = document.DocumentElement;

			ComputersFactoryContext context = new ComputersFactoryContext();
			foreach (XmlNode node in rootNode.ChildNodes)
			{
				context.Manufacturers.Add(new Manufacturer()
				{
					Name = node["name"].InnerText
				});
			}

			context.SaveChanges();
		}
	}
}

using System;
using System.IO;
using System.Xml.Serialization;
using CodeFirst;

namespace ComputersFactory.Reports
{
    public class XmlExporter
    {
        public void CreateXmlReport(ComputersFactoryContext context)
        {
            var generatedXml = new XmlReportGenerator();
            var reports = generatedXml.FillXmlWithData(context);
            var computerFactoryReports = 
                new ComputerFactoryReports
                {
                    ComputerReports = reports
                };

            using (var stream = File.Create($"C:\\Users\\{Environment.UserName}\\Desktop\\ComputersFactory\\reports.xml"))
            {
                var xmlSerializer = new XmlSerializer(typeof(ComputerFactoryReports));
                xmlSerializer.Serialize(stream, computerFactoryReports);
            }
        }
    }
}
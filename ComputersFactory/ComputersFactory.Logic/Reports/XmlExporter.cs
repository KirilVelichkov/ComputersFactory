using System;
using System.IO;
using System.Xml.Serialization;
using ComputersFactory.Models;

namespace ComputersFactory.Logic.Reports
{
    public class XmlExporter
    {
        public void CreateXmlReport(ComputersFactoryContext context)
        {
            var dataGenerator = new DataReportGenerator();
            var reports = dataGenerator.FillWithData(context);
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
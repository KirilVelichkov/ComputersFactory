using System;
using System.IO;
using System.Xml.Serialization;
using ComputersFactory.Models;

namespace ComputersFactory.Logic.Reports
{
    public class XmlExporter : IExporter
    {
        private ComputersFactoryContext dbContext;

        public XmlExporter(ComputersFactoryContext context)
        {
            this.dbContext = context;
        }

        public void GenerateReport(string path)
        {
            var dataGenerator = new DataReportGenerator();
            var reports = dataGenerator.FillWithData(this.dbContext);
            var computerFactoryReports = 
                new ComputerFactoryReports
                {
                    ComputerReports = reports
                };

            Directory.CreateDirectory(path);
            using (var stream = File.Create($"{path}/reports.xml"))
            {
                var xmlSerializer = new XmlSerializer(typeof(ComputerFactoryReports));
                xmlSerializer.Serialize(stream, computerFactoryReports);
            }
        }
    }
}
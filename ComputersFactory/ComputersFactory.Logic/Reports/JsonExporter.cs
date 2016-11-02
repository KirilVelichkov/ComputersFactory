using System.IO;
using Newtonsoft.Json;
using ComputersFactory.Models;
using System;

namespace ComputersFactory.Logic.Reports
{
    public class JsonExporter : IExporter
    {
        private ComputersFactoryContext context;

        public JsonExporter(ComputersFactoryContext context)
        {
            this.context = context;
        }
        
        public void GenerateReport(string path)
        {
            var dataGenerator = new DataReportGenerator();
            var reports = dataGenerator.FillWithData(context);

            Directory.CreateDirectory(path);

            foreach (var report in reports)
            {
                var filePath = path + "/" + report.ID + ".json";
                File.Create(filePath).Close();
                using (var stream = new StreamWriter(filePath))
                {
                    var json = JsonConvert.SerializeObject(report, Formatting.Indented);
                    stream.Write(json);
                }
            }
        }
    }
}

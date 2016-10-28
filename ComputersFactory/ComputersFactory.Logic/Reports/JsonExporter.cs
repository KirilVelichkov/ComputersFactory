using System.IO;
using Newtonsoft.Json;
using ComputersFactory.Models;

namespace ComputersFactory.Logic.Reports
{
    public class JsonExporter
    {
        private ComputersFactoryContext context;

        public JsonExporter(ComputersFactoryContext context)
        {
            this.context = context;
        }

        public void CreateJsonReport(string path)
        {
            var dataGenerator = new DataReportGenerator();
            var data = dataGenerator.FillWithData(context);
            var json = JsonConvert.SerializeObject(data, Formatting.Indented);

            File.Create(path).Close();
            using (var stream = new StreamWriter(path))
            {
                stream.Write(json);
            }
        }
    }
}

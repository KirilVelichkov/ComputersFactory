using System.Collections.Generic;
using System.Linq;
using CodeFirst;

namespace ComputersFactory.Reports
{
    public class XmlReportGenerator
    {
        private int reportId = 0;

        public int Id { get { return this.reportId += 1; } }

        public List<Report> FillXmlWithData(ComputersFactoryContext context)
        {
            var reports = new List<Report>();

            foreach (var manufacturer in context.Manufacturers)
            {
                var report = new Report
                {
                    ID = this.Id,
                    ManufacturerName = manufacturer.Name,
                    MemoryManufacturer = manufacturer.Memorycards
                        .Select(m => m.Manufacturer.Name)
                        .FirstOrDefault(),
                    MemoryCapacity = manufacturer.Memorycards
                        .Select(m => m.Capacity)
                        .FirstOrDefault(),
                    ProcessorModel = manufacturer.Processors
                        .Select(p => p.Model)
                        .FirstOrDefault(),
                    ProcessorMhz = manufacturer.Processors
                        .Select(p => p.MhZ)
                        .FirstOrDefault(),
                    VideoCardModel = manufacturer.Videocards
                        .Select(v => v.Model)
                        .FirstOrDefault(),
                    VideoCardMemory = manufacturer.Videocards
                        .Select(v => v.Memory)
                        .FirstOrDefault()
                };

                reports.Add(report);
            }
            
            return reports;
        }
    }
}

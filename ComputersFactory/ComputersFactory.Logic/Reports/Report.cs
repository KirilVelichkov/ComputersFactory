using System;
using System.Xml.Serialization;

namespace ComputersFactory.Logic.Reports
{
    [Serializable]
    public class Report
    {
        [XmlElement("report-id")]
        public int ID { get; set; }
        
        [XmlAttribute("manufacturer-name")]
        public string ManufacturerName { get; set; }

        [XmlElement("memory-manufacturer")]
        public string MemoryManufacturer { get; set; }

        [XmlElement("memory-capacity")]
        public string MemoryCapacity { get; set; }

        [XmlElement("processor-model")]
        public string ProcessorModel { get; set; }

        [XmlElement("processor-mhz")]
        public double ProcessorMhz { get; set; }

        [XmlElement("videocard-model")]
        public string VideoCardModel { get; set; }

        [XmlElement("videocard-memory")]
        public string VideoCardMemory { get; set; }
    }
}

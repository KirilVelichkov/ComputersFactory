using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace ComputersFactory.Logic.Reports
{
    [Serializable]
    [XmlRoot("computer-factory-reports")]
    public class ComputerFactoryReports
    {
        [XmlElement("report")]
        public List<Report> ComputerReports { get; set; }
    }
}

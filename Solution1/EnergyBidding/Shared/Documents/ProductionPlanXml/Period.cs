using System.Collections.Generic;
using System.Xml.Serialization;

namespace EnergyBidding.Shared.Documents.ProductionPlanXml
{
    [XmlRoot(ElementName = "Period", Namespace = "http://www.energinet.dk/schemas/BalRespXML/OperationalScheduleDocument/v13")]
    public class Period
    {
        [XmlElement(ElementName = "TimeInterval", Namespace = "http://www.energinet.dk/schemas/BalRespXML/OperationalScheduleDocument/v13")]
        public TimeInterval TimeInterval { get; set; }
        [XmlElement(ElementName = "Resolution", Namespace = "http://www.energinet.dk/schemas/BalRespXML/OperationalScheduleDocument/v13")]
        public Resolution Resolution { get; set; }
        [XmlElement(ElementName = "Interval", Namespace = "http://www.energinet.dk/schemas/BalRespXML/OperationalScheduleDocument/v13")]
        public List<Interval> Interval { get; set; }
    }
}
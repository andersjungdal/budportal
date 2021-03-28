using System.Xml.Serialization;

namespace EnergyBidding.Shared.Documents.ProductionPlanXml
{
    [XmlRoot(ElementName = "TimeInterval", Namespace = "http://www.energinet.dk/schemas/BalRespXML/OperationalScheduleDocument/v13")]
    public class TimeInterval
    {
        [XmlAttribute(AttributeName = "v")]
        public string V { get; set; }
    }
}
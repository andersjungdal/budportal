using System.Xml.Serialization;

namespace EnergyBidding.Shared.Documents.ProductionPlanXml
{
    [XmlRoot(ElementName = "ScheduleTimeInterval", Namespace = "http://www.energinet.dk/schemas/BalRespXML/MessageHeader/v13")]
    public class ScheduleTimeInterval
    {
        [XmlAttribute(AttributeName = "v")]
        public string V { get; set; }
    }
}
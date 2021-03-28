using System.Xml.Serialization;

namespace EnergyBidding.Shared.Documents.ProductionPlanXml
{
    [XmlRoot(ElementName = "SenderRole", Namespace = "http://www.energinet.dk/schemas/BalRespXML/MessageHeader/v13")]
    public class SenderRole
    {
        [XmlAttribute(AttributeName = "v")]
        public string V { get; set; }
    }
}
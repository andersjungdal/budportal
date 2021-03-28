using System.Xml.Serialization;

namespace EnergyBidding.Shared.Documents.ProductionPlanXml
{
    [XmlRoot(ElementName = "ReceiverRole", Namespace = "http://www.energinet.dk/schemas/BalRespXML/MessageHeader/v13")]
    public class ReceiverRole
    {
        [XmlAttribute(AttributeName = "v")]
        public string V { get; set; }
    }
}
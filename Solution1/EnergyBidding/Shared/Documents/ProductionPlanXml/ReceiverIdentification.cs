using System.Xml.Serialization;

namespace EnergyBidding.Shared.Documents.ProductionPlanXml
{
    [XmlRoot(ElementName = "ReceiverIdentification", Namespace = "http://www.energinet.dk/schemas/BalRespXML/MessageHeader/v13")]
    public class ReceiverIdentification
    {
        [XmlAttribute(AttributeName = "codingScheme")]
        public string CodingScheme { get; set; }
        [XmlAttribute(AttributeName = "v")]
        public string V { get; set; }
    }
}
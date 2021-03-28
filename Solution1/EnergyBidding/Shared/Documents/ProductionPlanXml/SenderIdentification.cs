using System.Xml.Serialization;

namespace EnergyBidding.Shared.Documents.ProductionPlanXml
{
    [XmlRoot(ElementName = "SenderIdentification", Namespace = "http://www.energinet.dk/schemas/BalRespXML/MessageHeader/v13")]
    public class SenderIdentification
    {
        [XmlAttribute(AttributeName = "codingScheme")]
        public string CodingScheme { get; set; }
        [XmlAttribute(AttributeName = "v")]
        public long V { get; set; }
    }
}
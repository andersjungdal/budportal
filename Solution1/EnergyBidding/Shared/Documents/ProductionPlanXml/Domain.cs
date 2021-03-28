using System.Xml.Serialization;

namespace EnergyBidding.Shared.Documents.ProductionPlanXml
{
    [XmlRoot(ElementName = "Domain", Namespace = "http://www.energinet.dk/schemas/BalRespXML/MessageHeader/v13")]
    public class Domain
    {
        [XmlAttribute(AttributeName = "codingScheme")]
        public string CodingScheme { get; set; }
        [XmlAttribute(AttributeName = "v")]
        public string V { get; set; }
    }
}
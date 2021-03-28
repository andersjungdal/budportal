using System.Xml.Serialization;

namespace EnergyBidding.Shared.Documents.ProductionPlanXml
{
    [XmlRoot(ElementName = "DocumentType", Namespace = "http://www.energinet.dk/schemas/BalRespXML/MessageHeader/v13")]
    public class DocumentType
    {
        [XmlAttribute(AttributeName = "v")]
        public string V { get; set; }
    }
}
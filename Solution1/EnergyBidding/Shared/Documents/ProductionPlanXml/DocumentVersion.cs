using System.Xml.Serialization;

namespace EnergyBidding.Shared.Documents.ProductionPlanXml
{
    [XmlRoot(ElementName = "DocumentVersion", Namespace = "http://www.energinet.dk/schemas/BalRespXML/MessageHeader/v13")]
    public class DocumentVersion
    {
        [XmlAttribute(AttributeName = "v")]
        public string V { get; set; }
    }
}
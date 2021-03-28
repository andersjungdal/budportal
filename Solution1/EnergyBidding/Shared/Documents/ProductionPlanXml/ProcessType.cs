using System.Xml.Serialization;

namespace EnergyBidding.Shared.Documents.ProductionPlanXml
{
    [XmlRoot(ElementName = "ProcessType", Namespace = "http://www.energinet.dk/schemas/BalRespXML/MessageHeader/v13")]
    public class ProcessType
    {
        [XmlAttribute(AttributeName = "v")]
        public string V { get; set; }
    }
}
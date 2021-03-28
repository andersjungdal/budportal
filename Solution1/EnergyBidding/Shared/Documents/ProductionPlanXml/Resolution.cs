using System.Xml.Serialization;

namespace EnergyBidding.Shared.Documents.ProductionPlanXml
{
    [XmlRoot(ElementName = "Resolution", Namespace = "http://www.energinet.dk/schemas/BalRespXML/OperationalScheduleDocument/v13")]
    public class Resolution
    {
        [XmlAttribute(AttributeName = "v")]
        public string V { get; set; }
    }
}
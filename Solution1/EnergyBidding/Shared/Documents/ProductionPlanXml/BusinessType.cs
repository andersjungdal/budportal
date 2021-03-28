using System.Xml.Serialization;

namespace EnergyBidding.Shared.Documents.ProductionPlanXml
{
    [XmlRoot(ElementName = "BusinessType", Namespace = "http://www.energinet.dk/schemas/BalRespXML/OperationalScheduleDocument/v13")]
    public class BusinessType
    {
        [XmlAttribute(AttributeName = "v")]
        public string V { get; set; }
    }
}
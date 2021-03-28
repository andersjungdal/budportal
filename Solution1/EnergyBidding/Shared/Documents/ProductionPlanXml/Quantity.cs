using System.Xml.Serialization;

namespace EnergyBidding.Shared.Documents.ProductionPlanXml
{
    [XmlRoot(ElementName = "Quantity", Namespace = "http://www.energinet.dk/schemas/BalRespXML/OperationalScheduleDocument/v13")]
    public class Quantity
    {
        [XmlAttribute(AttributeName = "v")]
        public double V { get; set; }
    }
}
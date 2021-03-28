using System.Xml.Serialization;

namespace EnergyBidding.Shared.Documents.ProductionPlanXml
{
    [XmlRoot(ElementName = "UnitIdentification", Namespace = "http://www.energinet.dk/schemas/BalRespXML/OperationalScheduleDocument/v13")]
    public class UnitIdentification
    {
        [XmlAttribute(AttributeName = "v")]
        public string V { get; set; }
    }
}
using System.Xml.Serialization;

namespace EnergyBidding.Shared.Documents.ProductionPlanXml
{
    [XmlRoot(ElementName = "MeasurementUnit", Namespace = "http://www.energinet.dk/schemas/BalRespXML/OperationalScheduleDocument/v13")]
    public class MeasurementUnit
    {
        [XmlAttribute(AttributeName = "v")]
        public string V { get; set; }
    }
}
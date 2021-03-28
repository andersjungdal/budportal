using System.Xml.Serialization;

namespace EnergyBidding.Shared.Documents.ProductionPlanXml
{
    [XmlRoot(ElementName = "TimeSeriesIdentification", Namespace = "http://www.energinet.dk/schemas/BalRespXML/OperationalScheduleDocument/v13")]
    public class TimeSeriesIdentification
    {
        [XmlAttribute(AttributeName = "v")]
        public string V { get; set; }
    }
}
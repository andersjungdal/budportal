using System.Xml.Serialization;

namespace EnergyBidding.Shared.Documents.ProductionPlanXml
{
    [XmlRoot(ElementName = "TimeSeriesVersion", Namespace = "http://www.energinet.dk/schemas/BalRespXML/OperationalScheduleDocument/v13")]
    public class TimeSeriesVersion
    {
        [XmlAttribute(AttributeName = "v")]
        public string V { get; set; }
    }
}
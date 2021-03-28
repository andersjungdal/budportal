using System.Xml.Serialization;

namespace EnergyBidding.Shared.Documents.ProductionPlanXml
{
    [XmlRoot(ElementName = "OperationalScheduleTimeSeries", Namespace = "http://www.energinet.dk/schemas/BalRespXML/OperationalScheduleDocument/v13")]
    public class OperationalScheduleTimeSeries
    {
        [XmlElement(ElementName = "TimeSeriesIdentification", Namespace = "http://www.energinet.dk/schemas/BalRespXML/OperationalScheduleDocument/v13")]
        public TimeSeriesIdentification TimeSeriesIdentification { get; set; }
        [XmlElement(ElementName = "TimeSeriesVersion", Namespace = "http://www.energinet.dk/schemas/BalRespXML/OperationalScheduleDocument/v13")]
        public TimeSeriesVersion TimeSeriesVersion { get; set; }
        [XmlElement(ElementName = "BusinessType", Namespace = "http://www.energinet.dk/schemas/BalRespXML/OperationalScheduleDocument/v13")]
        public BusinessType BusinessType { get; set; }
        [XmlElement(ElementName = "Product", Namespace = "http://www.energinet.dk/schemas/BalRespXML/OperationalScheduleDocument/v13")]
        public Product Product { get; set; }
        [XmlElement(ElementName = "MeasurementUnit", Namespace = "http://www.energinet.dk/schemas/BalRespXML/OperationalScheduleDocument/v13")]
        public MeasurementUnit MeasurementUnit { get; set; }
        [XmlElement(ElementName = "UnitTypeIdentification", Namespace = "http://www.energinet.dk/schemas/BalRespXML/OperationalScheduleDocument/v13")]
        public UnitTypeIdentification UnitTypeIdentification { get; set; }
        [XmlElement(ElementName = "Period", Namespace = "http://www.energinet.dk/schemas/BalRespXML/OperationalScheduleDocument/v13")]
        public Period Period { get; set; }
        [XmlElement(ElementName = "UnitIdentification", Namespace = "http://www.energinet.dk/schemas/BalRespXML/OperationalScheduleDocument/v13")]
        public UnitIdentification UnitIdentification { get; set; }
    }
}
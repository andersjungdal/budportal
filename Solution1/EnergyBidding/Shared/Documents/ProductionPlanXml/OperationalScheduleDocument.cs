using System.Collections.Generic;
using System.Xml.Serialization;

namespace EnergyBidding.Shared.Documents.ProductionPlanXml
{
    [XmlRoot(ElementName = "OperationalScheduleDocument", Namespace = "http://www.energinet.dk/schemas/BalRespXML/OperationalScheduleDocument/v13")]
    public class OperationalScheduleDocument
    {
        public OperationalScheduleDocument(){}
        [XmlElement(ElementName = "MessageHeader", Namespace = "http://www.energinet.dk/schemas/BalRespXML/MessageHeader/v13")]
        public Heatters.MessageHeader MessageHeader { get; set; }
        [XmlElement(ElementName = "OperationalScheduleTimeSeries", Namespace = "http://www.energinet.dk/schemas/BalRespXML/OperationalScheduleDocument/v13")]
        public List<OperationalScheduleTimeSeries> OperationalScheduleTimeSeries { get; set; }
        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }
        [XmlAttribute(AttributeName = "h", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string H { get; set; }
    }
}
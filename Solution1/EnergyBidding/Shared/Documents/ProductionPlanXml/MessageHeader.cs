using System.Xml.Serialization;

namespace EnergyBidding.Shared.Documents.ProductionPlanXml
{
    [XmlRoot(ElementName = "MessageHeader", Namespace = "http://www.energinet.dk/schemas/BalRespXML/MessageHeader/v13")]
    public class MessageHeader
    {
        [XmlElement(ElementName = "DocumentIdentification", Namespace = "http://www.energinet.dk/schemas/BalRespXML/MessageHeader/v13")]
        public DocumentIdentification DocumentIdentification { get; set; }
        [XmlElement(ElementName = "DocumentVersion", Namespace = "http://www.energinet.dk/schemas/BalRespXML/MessageHeader/v13")]
        public DocumentVersion DocumentVersion { get; set; }
        [XmlElement(ElementName = "DocumentType", Namespace = "http://www.energinet.dk/schemas/BalRespXML/MessageHeader/v13")]
        public DocumentType DocumentType { get; set; }
        [XmlElement(ElementName = "ProcessType", Namespace = "http://www.energinet.dk/schemas/BalRespXML/MessageHeader/v13")]
        public ProcessType ProcessType { get; set; }
        [XmlElement(ElementName = "SenderIdentification", Namespace = "http://www.energinet.dk/schemas/BalRespXML/MessageHeader/v13")]
        public SenderIdentification SenderIdentification { get; set; }
        [XmlElement(ElementName = "SenderRole", Namespace = "http://www.energinet.dk/schemas/BalRespXML/MessageHeader/v13")]
        public SenderRole SenderRole { get; set; }
        [XmlElement(ElementName = "ReceiverIdentification", Namespace = "http://www.energinet.dk/schemas/BalRespXML/MessageHeader/v13")]
        public ReceiverIdentification ReceiverIdentification { get; set; }
        [XmlElement(ElementName = "ReceiverRole", Namespace = "http://www.energinet.dk/schemas/BalRespXML/MessageHeader/v13")]
        public ReceiverRole ReceiverRole { get; set; }
        [XmlElement(ElementName = "DocumentDateTime", Namespace = "http://www.energinet.dk/schemas/BalRespXML/MessageHeader/v13")]
        public DocumentDateTime DocumentDateTime { get; set; }
        [XmlElement(ElementName = "ScheduleTimeInterval", Namespace = "http://www.energinet.dk/schemas/BalRespXML/MessageHeader/v13")]
        public ScheduleTimeInterval ScheduleTimeInterval { get; set; }
        [XmlElement(ElementName = "Domain", Namespace = "http://www.energinet.dk/schemas/BalRespXML/MessageHeader/v13")]
        public Domain Domain { get; set; }
    }
}
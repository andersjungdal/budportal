namespace EnergyBidding.Shared.Documents.Heatters
{
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true,
        Namespace = "http://www.energinet.dk/schemas/BalRespXML/MessageHeader/v13")]
    [System.Xml.Serialization.XmlRootAttribute(
        Namespace = "http://www.energinet.dk/schemas/BalRespXML/MessageHeader/v13", IsNullable = false)]
    public partial class MessageHeader
    {
        
        public MessageHeaderDocumentIdentification DocumentIdentification { get; set; }
        
        public MessageHeaderDocumentVersion DocumentVersion { get; set; }
        
        public MessageHeaderDocumentType DocumentType { get; set; }
        
        public MessageHeaderProcessType ProcessType { get; set; }
        
        public MessageHeaderSenderIdentification SenderIdentification { get; set; }
        
        public MessageHeaderSenderRole SenderRole { get; set; }
        
        public MessageHeaderReceiverIdentification ReceiverIdentification { get; set; }
         
        public MessageHeaderReceiverRole ReceiverRole { get; set; }
        
        public MessageHeaderDocumentDateTime DocumentDateTime { get; set; }
        
        public MessageHeaderScheduleTimeInterval ScheduleTimeInterval { get; set; }
        
        public MessageHeaderDomain Domain { get; set; }
    }
}
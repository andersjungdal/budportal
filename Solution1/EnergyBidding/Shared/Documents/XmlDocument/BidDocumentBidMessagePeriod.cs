namespace EnergyBidding.Shared.Documents.XmlDocument
{
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true,
        Namespace = "http://www.energinet.dk/schemas/BalRespXML/BidDocument/v13")]
    public partial class BidDocumentBidMessagePeriod
    {
        
        public BidDocumentBidMessagePeriodBidInterval BidInterval { get; set; }
        
        public BidDocumentBidMessagePeriodResolution Resolution { get; set; }
        
        [System.Xml.Serialization.XmlElementAttribute("Interval")]
        public BidDocumentBidMessagePeriodInterval[] Interval { get; set; }
    }
}
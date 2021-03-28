namespace EnergyBidding.Shared.Documents.XmlDocument
{
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true,
        Namespace = "http://www.energinet.dk/schemas/BalRespXML/BidDocument/v13")]
    public partial class BidDocumentBidMessagePeriodInterval
    {
        
        public BidDocumentBidMessagePeriodIntervalPosition Position { get; set; }
        
        public BidDocumentBidMessagePeriodIntervalPrice Price { get; set; }
        
        public BidDocumentBidMessagePeriodIntervalQuantity Quantity { get; set; }
    }
}
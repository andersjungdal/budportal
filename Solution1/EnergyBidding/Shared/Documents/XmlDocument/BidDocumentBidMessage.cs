namespace EnergyBidding.Shared.Documents.XmlDocument
{
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true,
        Namespace = "http://www.energinet.dk/schemas/BalRespXML/BidDocument/v13")]
    public partial class BidDocumentBidMessage
    {
        
        public BidDocumentBidIdentification BidIdentification { get; set; }
        
        public BidDocumentContractIdentification ContractIdentification { get; set; }
        
        public BidDocumentBidMessageBusinessType BusinessType { get; set; }
        
        public BidDocumentBidMessageMeasurementUnitQuantity MeasurementUnitQuantity { get; set; }
        
        public BidDocumentBidMessageMeasurementUnitPrice MeasurementUnitPrice { get; set; }
        
        public BidDocumentBidMessageCurrency Currency { get; set; }
        
        public BidDocumentBidMessageStartGradient StartGradient { get; set; }
        
        public BidDocumentBidMessageStopGradient StopGradient { get; set; }
        
        public BidDocumentBidMessageDeadTime DeadTime { get; set; }
        
        public BidDocumentBidMessagePeriod Period { get; set; }
    }
}
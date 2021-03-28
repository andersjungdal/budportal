namespace EnergyBidding.Shared.Documents.XmlDocument
{
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true,
        Namespace = "http://www.energinet.dk/schemas/BalRespXML/BidDocument/v13")]
    public partial class BidDocumentContractIdentification
    {
        
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string v { get; set; }
    }
}
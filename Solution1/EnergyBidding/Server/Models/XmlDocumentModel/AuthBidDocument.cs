namespace EnergyBidding.Server.Models.XmlDocumentModel
{
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true,
        Namespace = "http://www.energinet.dk/schemas/BalRespXML/BidDocument/v13")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.energinet.dk/schemas/BalRespXML/BidDocument/v13",
        IsNullable = false)]

    public class AuthBidDocument
    {
        [System.Xml.Serialization.XmlElementAttribute(Namespace =
            "http://www.energinet.dk/schemas/BalRespXML/MessageHeader/v13", IsNullable = false)]
        public AuthMessageHeader MessageHeader { get; set; }

    }
}
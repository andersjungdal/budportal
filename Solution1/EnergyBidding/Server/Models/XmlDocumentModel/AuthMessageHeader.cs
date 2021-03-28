using EnergyBidding.Shared.Documents.Heatters;

namespace EnergyBidding.Server.Models.XmlDocumentModel
{
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true,
        Namespace = "http://www.energinet.dk/schemas/BalRespXML/BidDocument/v13")]
    public class AuthMessageHeader
    {
        public MessageHeaderSenderIdentification SenderIdentification { get; set; }

    }
}
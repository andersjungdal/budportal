using System.Collections.Generic;
using EnergyBidding.Shared.Documents.Heatters;

namespace EnergyBidding.Shared.Documents.XmlDocument
{
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true,
        Namespace = "http://www.energinet.dk/schemas/BalRespXML/BidDocument/v13")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.energinet.dk/schemas/BalRespXML/BidDocument/v13",
        IsNullable = false)]
    public partial class BidDocument
    {
        [System.Xml.Serialization.XmlElementAttribute(Namespace =
            "http://www.energinet.dk/schemas/BalRespXML/MessageHeader/v13", IsNullable = false)]
        public MessageHeader MessageHeader { get; set; }
        
        [System.Xml.Serialization.XmlElementAttribute("BidMessage")]
        public List<BidDocumentBidMessage> BidMessage { get; set; }
    }
}
namespace EnergyBidding.Shared.Documents.Heatters
{
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true,
        Namespace = "http://www.energinet.dk/schemas/BalRespXML/MessageHeader/v13")]
    public partial class MessageHeaderScheduleTimeInterval
    {
        
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string v { get; set; }
    }
}
using System;
using System.Xml.Serialization;

namespace EnergyBidding.Shared.Documents.ProductionPlanXml
{
    [XmlRoot(ElementName = "DocumentDateTime", Namespace = "http://www.energinet.dk/schemas/BalRespXML/MessageHeader/v13")]
    public class DocumentDateTime
    {
        [XmlAttribute(AttributeName = "v")]
        public DateTime V { get; set; }
    }
}
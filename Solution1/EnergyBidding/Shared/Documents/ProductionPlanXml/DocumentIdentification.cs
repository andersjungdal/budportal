using System.Xml.Serialization;

namespace EnergyBidding.Shared.Documents.ProductionPlanXml
{
	[XmlRoot(ElementName = "DocumentIdentification", Namespace = "http://www.energinet.dk/schemas/BalRespXML/MessageHeader/v13")]
	public class DocumentIdentification
	{
		[XmlAttribute(AttributeName = "v")]
		public string V { get; set; }
	}
}

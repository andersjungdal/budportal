using EnergyBidding.Shared.Documents.XmlDocument;

namespace BlazorBusinessLogic.Models.UI
{
    public class BidElement
    {
        public bool Changes { get; set; }
        public BidDocumentBidMessagePeriodInterval Data { get; set; }
    }
}
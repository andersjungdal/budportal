using EnergyBidding.Shared.Documents.XmlDocument;

namespace BlazorBuisnessLogic.Net5.Models.UI
{
    public class BidElement
    {
        public bool Changes { get; set; }
        public BidDocumentBidMessagePeriodInterval Data { get; set; }
    }
}
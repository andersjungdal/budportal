using ModelsInterfaces.LockData.RawBidLogged;

namespace BlazorBuisnessLogic.Net5.Models.General.LockData
{
    public class RawBidCell : IRawBidCell
    {
        public double Quantity { get; set; }
        public double Prize { get; set; }
        public int Index { get; set; }
    }
}
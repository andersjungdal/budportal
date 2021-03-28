using System;
using ModelsInterfaces;

namespace BlazorBuisnessLogic.Net5.Models.General
{
    public class BidDateAndVersion : IRawBidDateAndVersion<Area>
    {
        public DateTime Date { get; set; }
        public Area Area { get; set; }
        public int Version { get; set; }
    }
}
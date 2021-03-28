using System;
using ModelsInterfaces;

namespace BlazorBusinessLogic.Models.General
{
    public class BidDateAndVersion : IRawBidDateAndVersion<Area>
    {
        public DateTime Date { get; set; }
        public Area Area { get; set; }
        public int Version { get; set; }
    }
}
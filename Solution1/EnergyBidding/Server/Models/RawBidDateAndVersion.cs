using System;
using ModelsInterfaces;

namespace EnergyBidding.Server.Models
{
    public class RawBidDateAndVersion : IRawBidDateAndVersion<Area>
    {
        public DateTime Date { get; set; }
        public Area Area { get; set; }
        public int Version { get; set; }
    }
}
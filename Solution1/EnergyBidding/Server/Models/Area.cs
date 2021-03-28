using System;
using ModelsInterfaces;

namespace EnergyBidding.Server.Models
{
    public class Area : IArea
    {
        public Guid PublicIdentifier { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
    }
}
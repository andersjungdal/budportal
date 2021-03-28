using System;
using ModelsInterfaces;

namespace BlazorBuisnessLogic.Net5.Models.General
{
    public class Area : IArea
    {
        public Guid PublicIdentifier { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
    }
}
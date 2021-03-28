using ModelsInterfaces;
using System;

namespace RawDatabaseModelling.Models
{
    public class Aktion : IAktion
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
    }
}
using System;
using System.Diagnostics.CodeAnalysis;
using ModelsInterfaces;

namespace DatabaseModelling.DbModels
{
    public class Area : IArea
    {
        public int Id { get; set; }
        [NotNull]
        public Guid PublicIdentifier { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
    }
}
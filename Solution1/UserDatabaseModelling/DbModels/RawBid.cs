using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using ModelsInterfaces;

namespace DatabaseModelling.DbModels
{
    public class RawBid : IRawBid<Area, User, Company, XmlTemplate>
    {
        public int Id { get; set; }
        public Guid PublicIdentifier { get; set; }
        [NotNull]
        public Company Company { get; set; }
        [NotNull]
        public User User { get; set; }
        [NotNull]
        public Area Area { get; set; }
        [Column(TypeName = "xml"), NotNull]
        public string XmlString { get; set; }

        public DateTime Date { get; set; }
        public int Version { get; set; }
    }
}
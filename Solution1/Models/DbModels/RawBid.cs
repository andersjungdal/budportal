using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using ModelsInterfaces;

namespace Models.DbModels
{
    public class RawBid : IRawBid<Aktion, User, Company>
    {
        public int Id { get; set; }
        public Guid PublicIdentifier { get; set; }
        [NotNull]
        public Company Company { get; set; }
        public User User { get; set; }
        [NotNull]
        public Aktion Aktion { get; set; }
        [Column(TypeName = "xml"), NotNull]
        public string XmlString { get; set; }
    }
}
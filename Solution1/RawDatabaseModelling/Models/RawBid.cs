using ModelsInterfaces;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Xml;
using System.Xml.Linq;
using UserDatabaseModelling.Models;

namespace RawDatabaseModelling.Models
{
    public class RawBid : IRawBid<Aktion, User, Company>
    {
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }
        public Guid UserId { get; set; }
        [Column(TypeName = "xml"), NotNull]
        public string XmlString { get; set; }
        [NotMapped]
        public XmlDocument Dokument { get; set; }
        public Guid PublicIdentifier { get; set; }
        public Company Company { get; set; }
        public User User { get; set; }
        public Aktion Aktion { get; set; }
    }
}
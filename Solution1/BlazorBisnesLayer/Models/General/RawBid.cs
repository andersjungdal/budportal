using System;
using ModelsInterfaces;

namespace BlazorBusinessLogic.Models.General
{
    public class RawBid : IRawBid<Area, User, Company, XmlTemplate>
    {
        public Guid PublicIdentifier { get; set; }
        public Company Company { get; set; }
        public User User { get; set; }
        public Area Area { get; set; }
        public string XmlString { get; set; }
        public DateTime Date { get; set; }
        public int Version { get; set; }
    }
}

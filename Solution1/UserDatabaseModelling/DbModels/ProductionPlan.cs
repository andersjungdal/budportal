using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using ModelsInterfaces;

namespace DatabaseModelling.DbModels
{
    public class ProductionPlan : IProductionPlan<Area, User, Company, XmlTemplate>
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        [NotNull]
        public Area Area { get; set; }
        public int Version { get; set; }
        public Guid PublicIdentifier { get; set; }
        [NotNull]
        public Company Company { get; set; }
        [NotNull]
        public User User { get; set; }
        [Column(TypeName = "xml"), NotNull]
        public string XmlString { get; set; }
    }
}

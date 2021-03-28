using System;
using ModelsInterfaces;

namespace BlazorBusinessLogic.Models.General
{
    public class ProductionPlan : IProductionPlan<Area, User, Company, XmlTemplate>
    {
        public DateTime Date { get; set; }
        public Area Area { get; set; }
        public int Version { get; set; }
        public Guid PublicIdentifier { get; set; }
        public Company Company { get; set; }
        public User User { get; set; }
        public string XmlString { get; set; }
    }
}
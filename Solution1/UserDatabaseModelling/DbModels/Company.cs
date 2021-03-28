using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using ModelsInterfaces;

namespace DatabaseModelling.DbModels
{
    public class Company : ICompany<XmlTemplate>
    {
        public int Id { get; set; }
        public Guid PublicIdentifier { get; set; }
        public string Name { get; set; }
        public Zone Zone { get; set; } = new Zone();
        [NotMapped]
        public string City 
        {
            get
            {
                return Zone.City;
            }
            set
            {
                Zone.City = value;
            }
        }
        [NotMapped]
        public int ZipCode 
        {
            get
            {
                return Zone.ZipCode;
            }
            set
            {
                Zone.ZipCode = value;
            }
        }

        public Roaden Roaden { get; set; } = new Roaden();
        [NotMapped]
        public string Road {
            get
            {
                return Roaden.Road;
            }
            set
            {
                Roaden.Road = value;
            }
        }
        public string StreetNumber { get; set; }
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public long XmlIdentifier { get; set; }
        public XmlTemplate RawBidTemplate { get; set; }
        public XmlTemplate ProductionPlanTemplate { get; set; }
    }
}
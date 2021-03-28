using System;
using ModelsInterfaces;

namespace BlazorBuisnessLogic.Net5.Models.General
{
    public class Company : ICompany<XmlTemplate>
    {
        public Guid PublicIdentifier { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public int ZipCode { get; set; }
        public string Road { get; set; }
        public string StreetNumber { get; set; }
        public long XmlIdentifier { get; set; }
        public XmlTemplate RawBidTemplate { get; set; }
        public XmlTemplate ProductionPlanTemplate { get; set; }
    }
}
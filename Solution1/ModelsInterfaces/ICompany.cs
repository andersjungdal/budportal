using System;

namespace ModelsInterfaces
{
    public interface ICompany<TXmlTemplate> where TXmlTemplate : IXmlTemplate
    {
        Guid PublicIdentifier { get; set; }
        string Name { get; set; }
        string City { get; set; }
        int ZipCode { get; set; }
        string Road { get; set; }
        string StreetNumber { get; set; }
        long XmlIdentifier { get; set; }
        TXmlTemplate RawBidTemplate { get; set; }
        TXmlTemplate ProductionPlanTemplate { get; set; }
    }
}
using System;
using System.Xml;

namespace ModelsInterfaces
{
    public interface IRawBid<TArea, TUser, TCompany, TXmlTemplate> : IRawBidDateAndVersion<TArea> where TArea : IArea where TUser : IUser<TCompany, TXmlTemplate> where TCompany : ICompany<TXmlTemplate> where TXmlTemplate : IXmlTemplate
    {
        Guid PublicIdentifier { get; set; }
        TCompany Company { get; set; }
        TUser User { get; set; }
        string XmlString { get; set; }
    }
}
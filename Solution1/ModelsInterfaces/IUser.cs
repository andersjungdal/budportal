using System;
using ModelsInterfaces.Enums;

namespace ModelsInterfaces
{
    public interface IUser<TCompany, TXmlTemplate> where TCompany : ICompany<TXmlTemplate> where TXmlTemplate:IXmlTemplate

    {
    Guid Id { get; set; }
    string UserName { get; set; }
    TCompany Company { get; set; }
    Role Role { get; set; }
    }
}
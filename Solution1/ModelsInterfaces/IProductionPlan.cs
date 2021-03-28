using System;
using System.Collections.Generic;
using System.Text;

namespace ModelsInterfaces
{
    public interface IProductionPlan<TArea, TUser, TCompany, TXmlTemplate> : IRawBid<TArea, TUser, TCompany, TXmlTemplate> where TArea : IArea where TUser : IUser<TCompany, TXmlTemplate> where TCompany : ICompany<TXmlTemplate> where TXmlTemplate : IXmlTemplate
    {

    }
}

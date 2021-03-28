using ModelsInterfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ApiGateway.BusinessLogic
{
    public static class Translasion
    {
        public static TArea AreaToArea<TArea>(this IArea FromArea) where TArea : IArea, new()
        {
            if (FromArea == null)
            {
                return new TArea();
            }

            return new TArea()
            {
                PublicIdentifier = FromArea?.PublicIdentifier ?? Guid.Empty, Description = FromArea?.Description,
                Type = FromArea?.Type
            };
        }

        public static TRawBid RawBidToRawBid<TRawBid, TArea, TUser, TCompany, TXmlTemplate, TFromArea, TFromUser,
            TFromCompany, TFromXmlTemplate>(
            this IRawBid<TFromArea, TFromUser, TFromCompany, TFromXmlTemplate> FromRawBid)
            where TRawBid : IRawBid<TArea, TUser, TCompany, TXmlTemplate>, new()
            where TArea : IArea, new()
            where TCompany : ICompany<TXmlTemplate>, new()
            where TXmlTemplate : IXmlTemplate, new()
            where TUser : IUser<TCompany, TXmlTemplate>, new()
            where TFromArea : IArea, new()
            where TFromCompany : ICompany<TFromXmlTemplate>, new()
            where TFromXmlTemplate : IXmlTemplate, new()
            where TFromUser : IUser<TFromCompany, TFromXmlTemplate>, new()
        {
            return new TRawBid()
            {
                PublicIdentifier = FromRawBid.PublicIdentifier,
                XmlString = FromRawBid.XmlString,
                Area = FromRawBid.Area.AreaToArea<TArea>(),
                Company = FromRawBid.Company.CompanyToCompany<TCompany, TXmlTemplate, TFromXmlTemplate>(),
                User = FromRawBid.User.UserToUser<TUser, TCompany, TXmlTemplate, TFromCompany, TFromXmlTemplate>(),
                Date = FromRawBid.Date
            };
        }

        public static TCompany CompanyToCompany<TCompany, TXmlTemplate, TFromXmlTemplate>(
            this ICompany<TFromXmlTemplate> FromCompany)
            where TCompany : ICompany<TXmlTemplate>, new()
            where TXmlTemplate : IXmlTemplate, new()
            where TFromXmlTemplate : IXmlTemplate, new()
        {
            if (FromCompany == null)
            {
                return new TCompany();
            }

            return new TCompany()
            {
                PublicIdentifier = FromCompany.PublicIdentifier,
                Name = FromCompany.Name,
                City = FromCompany.City,
                ZipCode = FromCompany.ZipCode,
                Road = FromCompany.Road,
                XmlIdentifier = FromCompany.XmlIdentifier,
                RawBidTemplate = FromCompany.RawBidTemplate.XmlTemplateToXmlTemplate<TXmlTemplate>(),
                StreetNumber = FromCompany.StreetNumber
            };
        }

        public static TUser UserToUser<TUser, TCompany, TXmlTemplate, TFromCompany, TFromXmlTemplate>(
            this IUser<TFromCompany, TFromXmlTemplate> FromUser)
            where TUser : IUser<TCompany, TXmlTemplate>, new()
            where TCompany : ICompany<TXmlTemplate>, new()
            where TXmlTemplate : IXmlTemplate, new()
            where TFromCompany : ICompany<TFromXmlTemplate>, new()
            where TFromXmlTemplate : IXmlTemplate, new()
        {
            if (FromUser == null)
            {
                return new TUser();
            }

            return new TUser()
            {
                Id = FromUser.Id, UserName = FromUser.UserName, Role = FromUser.Role,
                Company = FromUser.Company.CompanyToCompany<TCompany, TXmlTemplate, TFromXmlTemplate>()
            };
        }

        public static TXmlTemplate XmlTemplateToXmlTemplate<TXmlTemplate>(this IXmlTemplate FromXmlTenplate)
            where TXmlTemplate : IXmlTemplate, new()
        {
            if (FromXmlTenplate == null)
            {
                return new TXmlTemplate();
            }

            return new TXmlTemplate()
            {
                PublicIdentifire = FromXmlTenplate.PublicIdentifire,
                XMLTemplate = FromXmlTenplate.XMLTemplate
            };
        }
    }
}

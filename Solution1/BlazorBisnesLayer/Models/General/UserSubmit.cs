using System;
using ModelsInterfaces;
using ModelsInterfaces.Enums;

namespace BlazorBusinessLogic.Models.General
{
    public class UserSubmit<TCompany, TXmlTemplate> : IUserSubmit<TCompany, TXmlTemplate> where TCompany : ICompany<TXmlTemplate> where TXmlTemplate:IXmlTemplate
    {
        public UserSubmit()
        {

        }

        public UserSubmit(IUser<TCompany, TXmlTemplate> user)
        {
            Id = user.Id;
            UserName = user.UserName;
            Company = user.Company;
            Role = user.Role;
        }
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public TCompany Company { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
    }
}
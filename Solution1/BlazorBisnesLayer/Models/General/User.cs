using System;
using ModelsInterfaces;
using ModelsInterfaces.Enums;

namespace BlazorBusinessLogic.Models.General
{
    public class User : IUser<Company, XmlTemplate>
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public Company Company { get; set; }
        public Role Role { get; set; }
    }
}
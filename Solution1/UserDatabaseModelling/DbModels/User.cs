using System;
using Microsoft.AspNetCore.Identity;
using ModelsInterfaces;
using ModelsInterfaces.Enums;

namespace DatabaseModelling.DbModels
{
    public class User : IdentityUser<Guid>, IUser<Company, XmlTemplate>
    {
        public Company Company { get; set; }
        public Role Role { get; set; }

    }
}

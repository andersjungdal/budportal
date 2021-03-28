using System;
using Microsoft.AspNetCore.Identity;
using ModelsInterfaces;
using ModelsInterfaces.Enums;

namespace Models.DbModels
{
    public class User : IdentityUser<Guid>, IUser<Company>
    {
        public Role? Roles { get ; set ; }
        public Company Company { get; set; }
    }
}

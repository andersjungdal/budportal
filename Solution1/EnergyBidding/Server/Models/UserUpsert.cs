using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ModelsInterfaces;
using ModelsInterfaces.Enums;

namespace EnergyBidding.Server.Models
{
    public class UserUpsert : IUserSubmit<Company, XmlTemplate>
    {

        public string Password { get; set; }
        public string PasswordHash
        {
            get { return Password; }
        }
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public Company Company { get; set; }
        public Role Role { get; set; }
    }
}

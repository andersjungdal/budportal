using System;
using ModelsInterfaces.Enums;

namespace EnergyBidding.Server.Authorization.AuthAttributes
{
    public class RoleAuthorizationNiveau : Attribute
    {
        public Role Niveau { get; }

        public RoleAuthorizationNiveau(Role niveau)
        {
            Niveau = niveau;
        }
    }
}

using System;
using Microsoft.AspNetCore.Http;

namespace EnergyBidding.Server.Authorization.AuthAttributes
{
    public class OnlyOwnerRequired : Attribute, IRequestFinder
    {
        public Guid Finder(HttpContext context, IServiceProvider serviceProvider)
        {
            return Guid.Parse("B88C198F-432A-4B0C-A5A9-F3E903692A5C");
        }
    }
}
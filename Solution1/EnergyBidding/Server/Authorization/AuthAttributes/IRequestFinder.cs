using System;
using Microsoft.AspNetCore.Http;

namespace EnergyBidding.Server.Authorization.AuthAttributes
{
    public interface IRequestFinder
    {
        public Guid Finder(HttpContext context, IServiceProvider serviceProvider);
    }
}
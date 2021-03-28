using System;
using Microsoft.AspNetCore.Http;

namespace EnergyBidding.Server.Authorization.AuthAttributes
{
    public class FromQueryRequired : Attribute, IRequestFinder
    {
        public string QueryName { get; set; }

        public FromQueryRequired(string queryName)
        {
            QueryName = queryName;
        }
        public Guid Finder(HttpContext context, IServiceProvider serviceProvider)
        {
            if (Guid.TryParse(context.Request.Query[QueryName].ToString(), out Guid identifier))
            {
                return identifier;
            }
            return Guid.Empty;
        }
    }
}
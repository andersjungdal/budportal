using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using EnergyBidding.Server.Authorization.AuthAttributes;
using EnergyBidding.Server.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace EnergyBidding.Server.Authorization.AuthMiddleware
{
    public class MockCompanyMiddelware
    {
        private readonly RequestDelegate _next;
        private readonly IServiceProvider _serviceProvider;

        public MockCompanyMiddelware(RequestDelegate next, IServiceProvider serviceProvider)
        {
            _next = next;
            _serviceProvider = serviceProvider;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            var endpoint = httpContext.GetEndpoint();

            if (endpoint != null)
            {
                foreach (var attribute in endpoint.Metadata)
                {
                    if (typeof(IRequestFinder).IsAssignableFrom(attribute.GetType()))
                    {
                        IRequestFinder requirement = attribute as IRequestFinder;
                        if (!httpContext.Request.Headers.TryGetValue("User", out StringValues Users))
                        {
                            httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                            return;
                        }
                        User user = JsonSerializer.Deserialize<User>(Users[0]);

                        if (!(user.Company.PublicIdentifier.Equals(requirement.Finder(httpContext, _serviceProvider)) ||
                              user.Company.PublicIdentifier.ToString().Equals("B88C198F-432A-4B0C-A5A9-F3E903692A5C",StringComparison.CurrentCultureIgnoreCase)))
                        {
                            httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                            return;
                        }
                    }
                }
            }
            await _next(httpContext);
        }
    }
}
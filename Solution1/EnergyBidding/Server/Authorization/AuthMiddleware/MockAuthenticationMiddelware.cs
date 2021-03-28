using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using EnergyBidding.Server.Authorization.AuthAttributes;
using EnergyBidding.Server.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using ModelsInterfaces.Enums;

namespace EnergyBidding.Server.Authorization.AuthMiddleware
{
    public class MockAuthenticationMiddelware
    {

        private readonly RequestDelegate _next;
        public MockAuthenticationMiddelware(RequestDelegate next)
        {
            _next = next;
        }
        // IMyScopedService is injected into Invoke
        public async Task Invoke(HttpContext httpContext)
        {
            var endpoint = httpContext.GetEndpoint();
            //Is there a user

            
            Role niveau = Role.Anonymous;

            if (endpoint != null)
            {
                foreach (var attribute in endpoint.Metadata)
                {
                    if (attribute.GetType().Equals(typeof(RoleAuthorizationNiveau)))
                    {
                        RoleAuthorizationNiveau authNiveau = attribute as RoleAuthorizationNiveau;
                        if (authNiveau.Niveau == Role.NonAuthorized)
                        {
                            return;
                        }
                        if (niveau > authNiveau.Niveau)
                        {
                            niveau = authNiveau.Niveau;
                        }
                    }
                }
            }

            if (niveau != Role.Anonymous)
            {
                if (!httpContext.Request.Headers.TryGetValue("User", out StringValues Users))
                {
                    httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    return;
                }
                User user = JsonSerializer.Deserialize<User>(Users[0]);
                if (user == null)
                {
                    httpContext.Response.StatusCode = (int) HttpStatusCode.Unauthorized;
                    return;
                }

                if (user.Role > niveau)
                {
                    httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    return;
                }
            }
            await _next(httpContext);
        }
    }
}

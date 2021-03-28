using System;
using System.IO;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Http;

namespace EnergyBidding.Server.Authorization.AuthAttributes
{
    public class FromBodyCompanyRequired : Attribute, IRequestFinder
    {
        public Guid Finder(HttpContext context, IServiceProvider serviceProvider)
        {
            try
            {
                context.Request.EnableBuffering();
                using (StreamReader stream = new StreamReader(context.Request.Body, Encoding.UTF8, true, 1024, true))
                {
                    Models.Company company = JsonSerializer.Deserialize<Models.Company>(stream.ReadToEndAsync().Result);
                    stream.BaseStream.Seek(0, SeekOrigin.Begin);
                    return company.PublicIdentifier;
                }
            }
            catch (Exception e)
            {
                return Guid.Empty;
            }
        }
    }
}
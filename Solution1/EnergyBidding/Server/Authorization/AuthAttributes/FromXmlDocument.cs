using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Xml.Serialization;
using EnergyBidding.Server.Models;
using EnergyBidding.Server.Models.XmlDocumentModel;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace EnergyBidding.Server.Authorization.AuthAttributes
{
    public class FromXmlDocument : Attribute, IRequestFinder
    {
        public Guid Finder(HttpContext context, IServiceProvider serviceProvider)
        {
            //TODO: Be able to read from Jason object as well.
            AuthBidDocument xmlDocument;
            HttpClient httpClient = serviceProvider.CreateScope().ServiceProvider.GetService<HttpClient>();
            context.Request.EnableBuffering();
            XmlSerializer serializer = new XmlSerializer(typeof(AuthBidDocument));
            //TODO Uptimere!!!
            using (StreamReader stream = new StreamReader(context.Request.Body, Encoding.UTF8, leaveOpen: true))
            {
                string s = stream.ReadToEndAsync().Result;
                using (TextReader reader = new StringReader(s))
                {
                    xmlDocument = (AuthBidDocument)serializer.Deserialize(reader);
                }
                context.Request.Body.Seek(0, SeekOrigin.Begin);
            }

            return httpClient.GetFromJsonAsync<Company>(
                $"http://localhost:7071/api/GetCompanyByXmlIdentifier?XmlIdentifier={xmlDocument.MessageHeader.SenderIdentification.v}").Result.PublicIdentifier;
        }
    }
}
using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Xml.Serialization;
using EnergyBidding.Server.Models.XmlDocumentModel;
using Microsoft.AspNetCore.Http;

namespace EnergyBidding.Server.Authorization.AuthAttributes
{
    public class FromBodyRawBidRequired : Attribute, IRequestFinder
    {
        public Guid Finder(HttpContext context, IServiceProvider serviceProvider)
        {
            try
            {
                context.Request.EnableBuffering();
                Models.RawBid rawBid;
                AuthBidDocument xmlDocument;
                using (StreamReader stream = new StreamReader(context.Request.Body, Encoding.UTF8, true,-1, true))
                {
                    rawBid = JsonSerializer.Deserialize<Models.RawBid>(stream.ReadToEndAsync().Result);
                    stream.BaseStream.Seek(0, SeekOrigin.Begin);
                }
                //TODO find out way it creashes
                //XmlSerializer serializer = new XmlSerializer(typeof(AuthBidDocument));
                //using (TextReader reader = new StringReader(rawBid.XmlString))
                //{
                //    xmlDocument = (AuthBidDocument)serializer.Deserialize(reader);
                //}
                //return xmlDocument.MessageHeader.SenderIdentification.v.Equals(rawBid.Company.XmlIdentifier)?rawBid.Company.PublicIdentifier:Guid.Empty;

                return rawBid.Company.PublicIdentifier;
            }
            catch (Exception e)
            {
                return Guid.Empty;
            }
        }
    }
}
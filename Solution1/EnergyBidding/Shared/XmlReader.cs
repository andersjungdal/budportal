using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using EnergyBidding.Shared.Documents.XmlDocument;

namespace EnergyBidding.Shared
{
    public static class XmlReader
    {
        public static async Task<T> ReadRawBidXml<T>(string Document) where T : class
        {
            T Return = null;
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                using (TextReader reader = new StringReader(Document))
                {
                    Return = (T) serializer.Deserialize(reader);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Faild To red document:\n"+ Document + "\nMessage:\n" + e.Message);
            }

            return Return;
        }
    }
}

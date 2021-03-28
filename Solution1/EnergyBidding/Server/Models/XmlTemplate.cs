using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ModelsInterfaces;

namespace EnergyBidding.Server.Models
{
    public class XmlTemplate : IXmlTemplate
    {
        public int Id { get; set; }
        public Guid PublicIdentifire { get; set; }
        public string XMLTemplate { get; set; }
    }
}

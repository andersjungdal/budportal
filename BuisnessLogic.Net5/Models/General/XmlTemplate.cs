using System;
using ModelsInterfaces;

namespace BlazorBuisnessLogic.Net5.Models.General
{
    public class XmlTemplate : IXmlTemplate
    {
        public Guid PublicIdentifire { get; set; }
        public string XMLTemplate { get; set; }
    }
}
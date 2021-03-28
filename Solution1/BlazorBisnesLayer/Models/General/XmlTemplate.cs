using System;
using ModelsInterfaces;

namespace BlazorBusinessLogic.Models.General
{
    public class XmlTemplate : IXmlTemplate
    {
        public Guid PublicIdentifire { get; set; }
        public string XMLTemplate { get; set; }
    }
}
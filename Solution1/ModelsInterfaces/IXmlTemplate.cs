using System;

namespace ModelsInterfaces
{
    public interface IXmlTemplate
    {
        Guid PublicIdentifire { get; set; }
        String XMLTemplate { get; set; }
    }
}
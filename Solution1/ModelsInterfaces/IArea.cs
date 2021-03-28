using System;

namespace ModelsInterfaces
{
    public interface IArea
    {
        Guid PublicIdentifier { get; set; }
        string Description { get; set; }
        string Type { get; set; }
    }
}
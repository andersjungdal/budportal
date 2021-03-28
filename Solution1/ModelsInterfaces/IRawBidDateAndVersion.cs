using System;
using System.Collections.Generic;
using System.Text;

namespace ModelsInterfaces
{
    public interface IRawBidDateAndVersion<TArea> where TArea : IArea
    {
        DateTime Date { get; set; }
        TArea Area { get; set; }
        int Version { get; set; }
    }
}

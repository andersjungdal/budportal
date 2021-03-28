using System.Collections.Generic;

namespace ModelsInterfaces.LockData.RawBidLogged
{
    public interface IRawBidColumn<TRawBidCell> where TRawBidCell:IRawBidCell
    {
        List<TRawBidCell> Rows { get; set; }
        string CollumName { get; set; }


    }
}
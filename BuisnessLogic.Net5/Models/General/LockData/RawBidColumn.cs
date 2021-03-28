using System.Collections.Generic;
using ModelsInterfaces.LockData.RawBidLogged;

namespace BlazorBuisnessLogic.Net5.Models.General.LockData
{
    public class RawBidColumn : IRawBidColumn<RawBidCell>
    {
        public List<RawBidCell> Rows { get; set; }
        public string CollumName { get; set; }
    }
}
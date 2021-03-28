using System;
using System.Collections.Generic;
using ModelsInterfaces.LockData.RawBidLogged;

namespace DatabaseModelling.DbModels.LockData.RawBidLock
{
    public class RawBidColumn : IRawBidColumn<RawBidCell>
    {
        public int Id { get; set; }
        public string CollumName { get; set; }
        public Guid RawBidPublicIdentifier { get; set; }
        public List<RawBidCell> Rows { get; set; }
    }
}
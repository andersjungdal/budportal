using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ModelsInterfaces.LockData.RawBidLogged;

namespace DatabaseModelling.DbModels.LockData.RawBidLock
{
    public class RawBidCell : IRawBidCell
    {
        [Key]
        [Column(Order = 1)]
        public int RawBidColumnId { get; set; }
        [Key]
        [Column(Order = 2)]
        public int Index { get; set; }
        public double Quantity { get; set; }
        public double Prize { get; set; }

        public RawBidColumn rawBidColumn { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ModelsInterfaces.LockData.ProductionPlanLogged;

namespace DatabaseModelling.DbModels.LockData.ProductionPlanLock
{
    public class ProductionPlanCell : IProductionPlanCell
    {
        [Key]
        [Column(Order = 1)]
        [ForeignKey("ProductionPlanColumn")]
        public int ColumnId { get; set; }
        [Key]
        [Column(Order = 2)] 
        public int Index { get; set; }
        public double Quantity { get; set; }
        public ProductionPlanColumn ProductionPlanColumn { get; set; }
    }
}
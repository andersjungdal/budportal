using ModelsInterfaces.LockData.ProductionPlanLogged;

namespace BlazorBuisnessLogic.Net5.Models.General.LockData
{
    public class ProductionPlanCell : IProductionPlanCell

    {
        public double Quantity { get; set; }
        public int Index { get; set; }
    }
}
using System.Collections.Generic;

namespace ModelsInterfaces.LockData.ProductionPlanLogged
{
    public interface IProductionPlanColumn<TProductionPlanCell> where TProductionPlanCell : IProductionPlanCell
    {
        List<TProductionPlanCell> Rows { get; set; }
        string CollumName { get; set; }
    }
}
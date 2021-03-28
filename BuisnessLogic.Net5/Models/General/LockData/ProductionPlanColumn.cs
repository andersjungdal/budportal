using ModelsInterfaces.LockData.ProductionPlanLogged;
using System.Collections.Generic;

namespace BlazorBuisnessLogic.Net5.Models.General.LockData
{
    public class ProductionPlanColumn : IProductionPlanColumn<ProductionPlanCell>
    {
        public List<ProductionPlanCell> Rows { get; set; }
        public string CollumName { get; set; } 

    }
}
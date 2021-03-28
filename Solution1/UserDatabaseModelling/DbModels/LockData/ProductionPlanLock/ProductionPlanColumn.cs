using System;
using System.Collections.Generic;
using ModelsInterfaces.LockData.ProductionPlanLogged;

namespace DatabaseModelling.DbModels.LockData.ProductionPlanLock
{
    public class ProductionPlanColumn : IProductionPlanColumn<ProductionPlanCell>
    {
        public int Id { get; set; }
        public Guid ProductionPlanPublicIdentifier{ get; set; }
        public List<ProductionPlanCell> Rows { get; set; }
        public string CollumName { get; set; }

    }
}
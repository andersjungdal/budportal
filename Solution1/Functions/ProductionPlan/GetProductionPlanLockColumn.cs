using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using DatabaseModelling.DbModels.LockData.ProductionPlanLock;
using DatabaseModelling.DbModels.LockData.RawBidLock;
using Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Functions.ProductionPlan
{
    public class GetProductionPlanLockColumn
    {
        public IDataBase<ProductionPlanColumn, Guid> ProductionPlanLockCollumn { get; set; }

        public GetProductionPlanLockColumn(IDataBase<ProductionPlanColumn, Guid> productionPlanLockCollumn)
        {
            ProductionPlanLockCollumn = productionPlanLockCollumn;
        }
        [FunctionName("ProductionPlanLockGet")]
        public async Task<IActionResult> GetLockColumn(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)]
            HttpRequest req,
            ILogger log)
        {

            if (!Guid.TryParse(req.Query["ProductionPlanPublicIdentifier"], out Guid id))
            {
                return new BadRequestResult();
            }

            List<ProductionPlanColumn> columns = await ProductionPlanLockCollumn.ReadAsync(x => x.ProductionPlanPublicIdentifier == id);
            foreach (var Colums in columns)
            {
                foreach (var cells in Colums.Rows)
                {
                    cells.ProductionPlanColumn = null;
                }
            }
            return new OkObjectResult(columns);
        }
    }
}

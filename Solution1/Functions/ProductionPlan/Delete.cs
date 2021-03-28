using System;
using System.IO;
using System.Threading.Tasks;
using Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Functions.ProductionPlan
{
    public class Delete
    {
        public IDataBase<DatabaseModelling.DbModels.ProductionPlan, Guid> Database;

        public Delete(IDataBase<DatabaseModelling.DbModels.ProductionPlan, Guid> database)
        {
            Database = database;
        }

        [FunctionName("ProductionPlanDelete")]
        public async Task<IActionResult> DeleteProductionPlan(
            [HttpTrigger(AuthorizationLevel.Function, "delete", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");


            if (!Guid.TryParse(req.Query["PublicIdentifier"], out Guid id))
            {
                return new BadRequestResult();
            }

            await Database.Delete(id);

            return new OkResult();
        }
    }
}

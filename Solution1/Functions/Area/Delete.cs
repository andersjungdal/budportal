using System;
using System.Threading.Tasks;
using Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace Functions.Area
{
    public class Delete
    {
        public IDataBase<DatabaseModelling.DbModels.Area, Guid> Database;

        public Delete(IDataBase<DatabaseModelling.DbModels.Area, Guid> database)
        {
            Database = database;
        }
        [FunctionName("AreaDelete")]
        public async Task<IActionResult> Run(
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

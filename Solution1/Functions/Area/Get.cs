using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace Functions.Area
{
    public class Get
    {
        public IDataBase<DatabaseModelling.DbModels.Area, Guid> Database;

        public Get(IDataBase<DatabaseModelling.DbModels.Area, Guid> database)
        {
            Database = database;
        }
        [FunctionName("AreaGet")]
        public async Task<IActionResult> GetArea(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            log.LogInformation("Database conection " + Database);

            List<DatabaseModelling.DbModels.Area> areas = await Database.ReadAllAsync();
            if (areas == null || areas.Count <= 0)
            {
                return new StatusCodeResult((int)HttpStatusCode.NotFound);
            }
            return new OkObjectResult(areas);
        }
        [FunctionName("AreaGetByArea")]
        public async Task<IActionResult> GetAreaWithArea(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            
            if (!Guid.TryParse(req.Query["PublicIdentifier"], out Guid PublicIdentifier))
            {
                return new BadRequestResult();
            }
            List<DatabaseModelling.DbModels.Area> areas = await Database.ReadAsync(x => x.PublicIdentifier.Equals(PublicIdentifier));
            
            if (areas == null || areas.Count <= 0)
            {
                return new StatusCodeResult((int)HttpStatusCode.NotFound);
            }
            return new OkObjectResult(areas[0]);
        }
    }
}

using System;
using System.Threading.Tasks;
using ApiGateway.BusinessLogic;
using ApiGateway.BusinessLogic.Extensions;
using Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace Functions.Area
{
    public class Create
    {
        public IDataBase<DatabaseModelling.DbModels.Area, Guid> Database;

        public Create(IDataBase<DatabaseModelling.DbModels.Area, Guid> database)
        {
            Database = database;
        }
        [FunctionName("AreaCreate")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            DatabaseModelling.DbModels.Area data = await req.JasonDeserialize<DatabaseModelling.DbModels.Area>();
            if (data == null)
            {
                return new BadRequestResult();
            }
            data.PublicIdentifier = Guid.NewGuid();
            await Database.CreateAsync(data);
            return new OkObjectResult(data);
        }
    }
}

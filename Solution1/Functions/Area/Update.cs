using System;
using System.IO;
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
    public class Update
    {
        public IDataBase<DatabaseModelling.DbModels.Area, Guid> Database;

        public Update(IDataBase<DatabaseModelling.DbModels.Area, Guid> database)
        {
            Database = database;
        }
        [FunctionName("AreaUpdate")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "put", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            DatabaseModelling.DbModels.Area data = await req.JasonDeserialize<DatabaseModelling.DbModels.Area>();

            if (data == null)
            {
                return new BadRequestResult();
            }

            await Database.UpdateAsync(data);

            return new OkObjectResult(data);
        }
    }
}

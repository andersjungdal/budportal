using System;
using System.IO;
using System.Threading.Tasks;
using ApiGateway.BusinessLogic;
using ApiGateway.BusinessLogic.Extensions;
using Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Functions.User
{
    public class Update
    {
        public IDataBase<DatabaseModelling.DbModels.User, Guid> Database;

        public Update(IDataBase<DatabaseModelling.DbModels.User, Guid> database)
        {
            Database = database;
        }
        [FunctionName("UserUpdate")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "put", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            DatabaseModelling.DbModels.User data = await req.JasonDeserialize<DatabaseModelling.DbModels.User>();

            if (data == null)
            {
                return new BadRequestResult();
            }

            await Database.UpdateAsync(data);

            return new OkObjectResult(data);
        }
    }
}

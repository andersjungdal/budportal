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
    public class Create
    {
        public IDataBase<DatabaseModelling.DbModels.User, Guid> Database;

        public Create(IDataBase<DatabaseModelling.DbModels.User, Guid> database)
        {
            Database = database;
        }
        [FunctionName("UserCreate")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            DatabaseModelling.DbModels.User data = await req.JasonDeserialize<DatabaseModelling.DbModels.User>();
            if (data == null && data.Company == null && data.Company?.PublicIdentifier != Guid.Empty)
            {
                return new BadRequestResult();
            }
            data.Id = Guid.NewGuid();
            await Database.CreateAsync(data);
            return new OkObjectResult(data);
        }
    }
}

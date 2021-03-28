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

namespace Functions.Company
{
    public class Create
    {
        public IDataBase<DatabaseModelling.DbModels.Company, Guid> Database;

        public Create(IDataBase<DatabaseModelling.DbModels.Company, Guid> database)
        {
            Database = database;
        }
        [FunctionName("CompanyCreate")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            DatabaseModelling.DbModels.Company data = await req.JasonDeserialize<DatabaseModelling.DbModels.Company>();

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

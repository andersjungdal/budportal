using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using DatabaseModelling.DbModels;
using Interfaces;
using ModelsInterfaces;

namespace Functions.Company
{
    public  class Get
    {
        public IDataBase<DatabaseModelling.DbModels.Company, Guid> Database;

        public Get(IDataBase<DatabaseModelling.DbModels.Company, Guid> database)
        {
            Database = database;
        }

        [FunctionName("GetCompany")]
        public async Task<ActionResult<ICompany<DatabaseModelling.DbModels.XmlTemplate>>> GetCompany(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)]
            HttpRequest req,
            ILogger log)
        {
            List<DatabaseModelling.DbModels.Company> companies = await Database.ReadAllAsync();
            return new OkObjectResult(companies);
        }

        [FunctionName("GetCompanyByCompany")]
        public  async Task<IActionResult> GetCompanyByCompany(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            List<DatabaseModelling.DbModels.Company> companies;
            if (!Guid.TryParse(req.Query["PublicIdentifier"], out Guid PublicIdentifier))
            {
                return new BadRequestResult();
            }
            companies = await Database.ReadAsync(x => x.PublicIdentifier.Equals(PublicIdentifier));

            if (companies == null || companies.Count <= 0)
            {
                return new StatusCodeResult((int)HttpStatusCode.NotFound);
            }

            return new OkObjectResult(companies[0]);
        }
        [FunctionName("GetCompanyByXmlIdentifier")]
        public  async Task<IActionResult> GetCompanyByXmlIdentifier(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            List<DatabaseModelling.DbModels.Company> companies;
            if (!long.TryParse(req.Query["XmlIdentifier"], out long XmlIdentifier))
            {
                return new BadRequestResult();
            }
            companies = await Database.ReadAsync(x => x.XmlIdentifier.Equals(XmlIdentifier));

            if (companies == null || companies.Count <= 0)
            {
                return new StatusCodeResult((int)HttpStatusCode.NotFound);
            }

            return new OkObjectResult(companies[0]);
        }
    }
}

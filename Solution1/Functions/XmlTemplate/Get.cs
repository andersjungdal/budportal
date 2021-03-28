using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Functions.XmlTemplate
{
    public class Get
    {
        public IDataBase<DatabaseModelling.DbModels.Company, Guid> CompanydataBase { get; set; }
        public IDataBase<DatabaseModelling.DbModels.XmlTemplate, Guid> DataBase { get; set; }

        public Get(IDataBase<DatabaseModelling.DbModels.Company, Guid> CompanydataBase, IDataBase<DatabaseModelling.DbModels.XmlTemplate, Guid> DataBase)
        {
            this.CompanydataBase = CompanydataBase;
            this.DataBase = DataBase;
        }
        [FunctionName("GetXmlTemplate")]
        public async Task<IActionResult> GetXmlTemplateById(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            if (!Guid.TryParse(req.Query["PublicIdentifier"], out Guid id))
            {
                return new BadRequestResult();
            }


            List<DatabaseModelling.DbModels.XmlTemplate> xmlTemplates = await DataBase.ReadAsync(x => x.PublicIdentifire.Equals(id));
            if (xmlTemplates == null || xmlTemplates.Count <= 0)
            {
                return new BadRequestResult();
            }
            return new OkObjectResult(xmlTemplates[0]);
        }
        [FunctionName("GetRawBidTemplateByCompany")]
        public async Task<IActionResult> GetRawBidTemplate(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            if (!Guid.TryParse(req.Query["PublicIdentifier"], out Guid id))
            {
                return new BadRequestResult();
            }


            List<DatabaseModelling.DbModels.Company> companies = (await CompanydataBase.ReadAsync(x => x.PublicIdentifier.Equals(id)));
            if (companies == null || companies.Count <= 0)
            {
                return new BadRequestResult();
            }
            return new OkObjectResult(companies[0].RawBidTemplate);
        }
        [FunctionName("GetProductionPlanTemplateByCompany")]
        public async Task<IActionResult> GetProductionPlanTemplateByCompany(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            if (!Guid.TryParse(req.Query["PublicIdentifier"], out Guid id))
            {
                return new BadRequestResult();
            }

            List<DatabaseModelling.DbModels.Company> companies = (await CompanydataBase.ReadAsync(x => x.PublicIdentifier.Equals(id)));
            if (companies == null || companies.Count <= 0)
            {
                return new BadRequestResult();
            }
            return new OkObjectResult(companies[0].ProductionPlanTemplate);
        }
    }
}

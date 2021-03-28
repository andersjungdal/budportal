using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
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
    public class Get
    {
        private readonly IDataBase<DatabaseModelling.DbModels.ProductionPlan, Guid> _dataBase;

        public Get(IDataBase<DatabaseModelling.DbModels.ProductionPlan, Guid> dataBase)
        {
            _dataBase = dataBase;
        }
        [FunctionName("ProductionPlanGet")]
        public async Task<IActionResult> GetProductionPlan(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            if (!Guid.TryParse(req.Query["ProductionPlanPublicIdentifier"], out Guid id))
            {
                return new BadRequestResult();
            }

            DatabaseModelling.DbModels.ProductionPlan plans = (await _dataBase.ReadAsync(x => x.PublicIdentifier.Equals(id)))
                .OrderByDescending(x => x.Version).FirstOrDefault();
            if (plans == null)
            {
                return new BadRequestResult();
            }

            return new OkObjectResult(plans);
        }
        [FunctionName("ProductionPlanDatesByCompany")]
        public async Task<IActionResult> GetProductionPlanDatesByCompany(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            if (!Guid.TryParse(req.Query["CompanyPublicIdentifier"], out Guid companyid))
            {
                return new BadRequestResult();
            }

            List<DatabaseModelling.DbModels.ProductionPlan> ProductionPlan = await _dataBase.ReadAsync(x => x.Company.PublicIdentifier.Equals(companyid));

            if (ProductionPlan == null || ProductionPlan.Count <= 0)
            {
                return new NoContentResult();
            }

            List<DatabaseModelling.DbModels.ProductionPlan> rightVersionProductionPlan = new List<DatabaseModelling.DbModels.ProductionPlan>();

            foreach (DatabaseModelling.DbModels.ProductionPlan productionPlan in rightVersionProductionPlan)
            {
                DatabaseModelling.DbModels.ProductionPlan old = rightVersionProductionPlan.FirstOrDefault(x => x.PublicIdentifier == productionPlan.PublicIdentifier);
                if (old == null)
                {
                    rightVersionProductionPlan.Add(productionPlan);
                }
                else
                {
                    if (old.Version < productionPlan.Version)
                    {
                        rightVersionProductionPlan.Remove(old);
                        rightVersionProductionPlan.Add(productionPlan);
                    }
                }
            }

            return new OkObjectResult(rightVersionProductionPlan);
        }
        [FunctionName("ProductionPlanByCompanyAreaAndDate")]
        public async Task<IActionResult> GetProductionByCompanyAreaAndDate(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            if (!Guid.TryParse(req.Query["CompanyPublicIdentifier"], out Guid companyid))
            {
                return new BadRequestResult();
            }
            if (!Guid.TryParse(req.Query["AreaPublicIdentifier"], out Guid areaid))
            {
                return new BadRequestResult();
            }
            log.LogInformation("Request Date: " + req.Query["Date"]);
            if (!DateTime.TryParse(req.Query["Date"], out DateTime date))
            {
                return new BadRequestResult();
            }

            List<DatabaseModelling.DbModels.ProductionPlan> ProductionPlan = await _dataBase.ReadAsync(x =>
            
                 x.Company.PublicIdentifier.Equals(companyid) &&
                       x.Area.PublicIdentifier.Equals(areaid)&&
                x.Date.Year == date.Year && x.Date.Month == date.Month && x.Date.Day == date.Day);

            if (ProductionPlan == null || ProductionPlan.Count <= 0)
            {
                return new NoContentResult();
            }

            DatabaseModelling.DbModels.ProductionPlan oldProductionPlan = new DatabaseModelling.DbModels.ProductionPlan();
            foreach (DatabaseModelling.DbModels.ProductionPlan productionPlan in ProductionPlan)
            {
                if (oldProductionPlan == null)
                {
                    oldProductionPlan = productionPlan;
                }
                else
                {
                    if (oldProductionPlan.Version < productionPlan.Version)
                    {
                        oldProductionPlan = productionPlan;
                    }
                }
            }

            return new OkObjectResult(oldProductionPlan);
        }
        [FunctionName("ProductionPlanByCompanyVersionAreaAndDate")]
        public async Task<IActionResult> GetProductionByCompanyVersionAreaAndDate(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            if (!Guid.TryParse(req.Query["CompanyPublicIdentifier"], out Guid companyid))
            {
                return new BadRequestResult();
            }
            if (!Guid.TryParse(req.Query["AreaPublicIdentifier"], out Guid areaid))
            {
                return new BadRequestResult();
            }
            log.LogInformation("Request Date: " + req.Query["Date"]);
            if (!DateTime.TryParse(req.Query["Date"], out DateTime date))
            {
                return new BadRequestResult();
            }
            if (!int.TryParse(req.Query["Version"], out int version))
            {
                return new BadRequestResult();
            }

            List<DatabaseModelling.DbModels.ProductionPlan> ProductionPlan = await _dataBase.ReadAsync(x =>
            
                 x.Company.PublicIdentifier.Equals(companyid) &&
                       x.Area.PublicIdentifier.Equals(areaid)&&
                x.Date.Year == date.Year && x.Date.Month == date.Month && 
                 x.Date.Day == date.Day && x.Version == version);

            if (ProductionPlan == null || ProductionPlan.Count <= 0)
            {
                return new NoContentResult();
            }

            DatabaseModelling.DbModels.ProductionPlan oldProductionPlan = null;
            foreach (DatabaseModelling.DbModels.ProductionPlan productionPlan in ProductionPlan)
            {
                if (oldProductionPlan == null)
                {
                    oldProductionPlan = productionPlan;
                }
                else
                {
                    if (oldProductionPlan.Version < productionPlan.Version)
                    {
                        oldProductionPlan = productionPlan;
                    }
                }
            }

            return new OkObjectResult(oldProductionPlan);
        }

        [FunctionName("ProductionPlanGetDatesByCompany")]
        public async Task<IActionResult> GetRawBidDatesByCompany(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            if (!Guid.TryParse(req.Query["CompanyPublicIdentifier"], out Guid companyid))
            {
                return new BadRequestResult();
            }

            List<DatabaseModelling.DbModels.ProductionPlan> productionPlans = await _dataBase.ReadAsync(x => x.Company.PublicIdentifier.Equals(companyid));

            if (productionPlans == null || productionPlans.Count <= 0)
            {
                return new NoContentResult();
            }

            List<DatabaseModelling.DbModels.ProductionPlan> rightVersionProductionPlan = new List<DatabaseModelling.DbModels.ProductionPlan>();

            foreach (DatabaseModelling.DbModels.ProductionPlan productionPlan in productionPlans)
            {
                DatabaseModelling.DbModels.ProductionPlan old = rightVersionProductionPlan.FirstOrDefault(x => x.PublicIdentifier == productionPlan.PublicIdentifier);
                if (old == null)
                {
                    rightVersionProductionPlan.Add(productionPlan);
                }
                else
                {
                    if (old.Version < productionPlan.Version)
                    {
                        rightVersionProductionPlan.Remove(old);
                        rightVersionProductionPlan.Add(productionPlan);
                    }
                }
            }

            return new OkObjectResult(rightVersionProductionPlan);
        }

    }
}

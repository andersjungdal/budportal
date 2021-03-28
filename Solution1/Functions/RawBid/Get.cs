using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ApiGateway.BusinessLogic;
using DatabaseModelling.DbModels;
using Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Logging;
using ModelsInterfaces;
using Newtonsoft.Json;

namespace Functions.RawBid
{
    public class Get
    {
        private readonly IDataBase<DatabaseModelling.DbModels.RawBid, Guid> _dataBase;
        public HttpClient Client { get; set; }

        public Get(HttpClient client, IDataBase<DatabaseModelling.DbModels.RawBid, Guid> dataBase)
        {
            _dataBase = dataBase;
            Client = client;
        }
        [FunctionName("RawBidGet")]
        public async Task<IActionResult> GetRawBid(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            if (!Guid.TryParse(req.Query["RawBidPublicIdentifier"], out Guid id))
            {
                return new BadRequestResult();
            }

            DatabaseModelling.DbModels.RawBid rawBids = (await _dataBase.ReadAsync(x => x.PublicIdentifier.Equals(id)))
                .OrderByDescending(x=>x.Version).FirstOrDefault();
            if (rawBids == null)
            {
                return new BadRequestResult();
            }

            return new OkObjectResult(rawBids);
        }
        [FunctionName("RawBidByAreaAndCompany")]
        public async Task<IActionResult> GetRawBidByAreaAndCompany(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            if (!Guid.TryParse(req.Query["AreaPublicIdentifier"], out Guid areaid)||!Guid.TryParse(req.Query["CompanyPublicIdentifier"], out Guid companyid))
            {
                return new BadRequestResult();
            }

            List<DatabaseModelling.DbModels.RawBid> rawBids = await _dataBase.ReadAsync(x => (x.Area?.PublicIdentifier.Equals(areaid) ?? false) && x.Company.PublicIdentifier.Equals(companyid));
            if (rawBids == null || rawBids.Count <= 0)
            {
                return new BadRequestResult();
            }

            List<IRawBid<DatabaseModelling.DbModels.Area, DatabaseModelling.DbModels.User, DatabaseModelling.DbModels.Company, DatabaseModelling.DbModels.XmlTemplate>> IRawBid = new List<IRawBid<DatabaseModelling.DbModels.Area, DatabaseModelling.DbModels.User, DatabaseModelling.DbModels.Company, DatabaseModelling.DbModels.XmlTemplate>>();
            foreach (DatabaseModelling.DbModels.RawBid result in rawBids)
            {
                IRawBid.Add(result);
            }

            return new OkObjectResult(IRawBid);
        }
        [FunctionName("RawBidByAreaDateAndCompany")]
        public async Task<IActionResult> GetRawBidByAreaDateAndCompany(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            if (!Guid.TryParse(req.Query["AreaPublicIdentifier"], out Guid areaid)||!Guid.TryParse(req.Query["CompanyPublicIdentifier"], out Guid companyid) ||!DateTime.TryParse(req.Query["Date"], out DateTime date))
            {
                return new BadRequestResult();
            }

            List<DatabaseModelling.DbModels.RawBid> rawBids = await _dataBase.ReadAsync(x =>
            {
                return (x.Area?.PublicIdentifier.Equals(areaid) ?? false) &&
                       x.Date.Year == date.Year && x.Date.Month == date.Month && x.Date.Day == date.Day &&
                       x.Company.PublicIdentifier.Equals(companyid);
            });

            if (rawBids == null || rawBids.Count <= 0)
            {
                return new NoContentResult();
            }

            DatabaseModelling.DbModels.RawBid oldProductionPlan = null;
            foreach (DatabaseModelling.DbModels.RawBid productionPlan in rawBids)
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
        [FunctionName("RawBidByAreaDateVersionAndCompany")]
        public async Task<IActionResult> GetRawBidByVersionAreaDateAndCompany(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            if (!Guid.TryParse(req.Query["AreaPublicIdentifier"],
                out Guid areaid)||!Guid.TryParse(req.Query["CompanyPublicIdentifier"],
                out Guid companyid) ||!DateTime.TryParse(req.Query["Date"],
                out DateTime date) || !int.TryParse(req.Query["Version"],
                out int version))
            {
                return new BadRequestResult();
            }

            List<DatabaseModelling.DbModels.RawBid> rawBids = await _dataBase.ReadAsync(x =>
            {
                return (x.Area?.PublicIdentifier.Equals(areaid) ?? false) &&
                       x.Date.Year == date.Year && x.Date.Month == date.Month && x.Date.Day == date.Day && x.Version == version &&
                       x.Company.PublicIdentifier.Equals(companyid);
            });

            if (rawBids == null || rawBids.Count <= 0)
            {
                return new NoContentResult();
            }

            return new OkObjectResult(rawBids[0]);
        }
        [FunctionName("RawBidByCompany")]
        public async Task<IActionResult> GetRawBidByCompany(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            if (!Guid.TryParse(req.Query["CompanyPublicIdentifier"], out Guid companyid))
            {
                return new BadRequestResult();
            }

            List<DatabaseModelling.DbModels.RawBid> rawBids = await _dataBase.ReadAsync(x => x.Company.PublicIdentifier.Equals(companyid));


            if (rawBids == null || rawBids.Count <= 0)
            {
                return new NoContentResult();
            }
            //TODO chech if you need IRawBid
            List<IRawBid<DatabaseModelling.DbModels.Area, DatabaseModelling.DbModels.User, DatabaseModelling.DbModels.Company, DatabaseModelling.DbModels.XmlTemplate>> IRawBid = new List<IRawBid<DatabaseModelling.DbModels.Area, DatabaseModelling.DbModels.User, DatabaseModelling.DbModels.Company, DatabaseModelling.DbModels.XmlTemplate>>();
            foreach (DatabaseModelling.DbModels.RawBid result in rawBids)
            {
                IRawBid.Add(result);
            }

            return new OkObjectResult(IRawBid);
        }
        [FunctionName("RawBidGetAll")]
        public async Task<IActionResult> GetAllRawBid(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");


            List<DatabaseModelling.DbModels.RawBid> rawBids = await _dataBase.ReadAllAsync();


            if (rawBids == null || rawBids.Count <= 0)
            {
                return new BadRequestResult();
            }

            List<IRawBid<DatabaseModelling.DbModels.Area, DatabaseModelling.DbModels.User, DatabaseModelling.DbModels.Company, DatabaseModelling.DbModels.XmlTemplate>> IRawBid = new List<IRawBid<DatabaseModelling.DbModels.Area, DatabaseModelling.DbModels.User, DatabaseModelling.DbModels.Company, DatabaseModelling.DbModels.XmlTemplate>>();
            foreach (DatabaseModelling.DbModels.RawBid result in rawBids)
            {
                IRawBid.Add(result);
            }

            return new OkObjectResult(IRawBid);
        }
        /// ////////////////////////////////////
        [FunctionName("RawBidGetDatesByCompany")]
        public async Task<IActionResult> GetRawBidDatesByCompany(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            if (!Guid.TryParse(req.Query["CompanyPublicIdentifier"], out Guid companyid))
            {
                return new BadRequestResult();
            }

            List<DatabaseModelling.DbModels.RawBid> rawBids = await _dataBase.ReadAsync(x => x.Company.PublicIdentifier.Equals(companyid));

            if (rawBids == null || rawBids.Count <= 0)
            {
                return new NoContentResult();
            }

            List<DatabaseModelling.DbModels.RawBid> rightVersionRawBids = new List<DatabaseModelling.DbModels.RawBid>();

            foreach (DatabaseModelling.DbModels.RawBid rawBid in rawBids)
            {
                DatabaseModelling.DbModels.RawBid old = rightVersionRawBids.FirstOrDefault(x => x.PublicIdentifier == rawBid.PublicIdentifier);
                if (old == null)
                {
                    rightVersionRawBids.Add(rawBid);
                }
                else
                {
                    if (old.Version < rawBid.Version)
                    {
                        rightVersionRawBids.Remove(old);
                        rightVersionRawBids.Add(rawBid);
                    }
                }
            }

            return new OkObjectResult(rightVersionRawBids);
        }
        ///////////////////////////////////////////////
    }
}

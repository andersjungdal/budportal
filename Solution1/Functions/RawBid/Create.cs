using System;
using System.Configuration;
using System.IO;
using System.Threading.Tasks;
using System.Xml;
using ApiGateway.BusinessLogic;
using ApiGateway.BusinessLogic.Interfaces;
using Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace Functions.RawBid
{
    public class Create
    {
        public IXmlReader XmlReader { get; set; }
        public ILockRows LockRows { get; set; }

        public IDataBase<DatabaseModelling.DbModels.RawBid, Guid> Database { get; set; }
        public IDataBase<DatabaseModelling.DbModels.User, Guid> User { get; set; }
        public Create(IDataBase<DatabaseModelling.DbModels.RawBid, Guid> database, IDataBase<DatabaseModelling.DbModels.User, Guid> user , IXmlReader xmlReader, ILockRows lockRows)
        {
            XmlReader = xmlReader;
            LockRows = lockRows;
            Database = database;
            User = user;
        }

        [FunctionName("RawBidCreate")]
        public async Task<IActionResult> RunXml(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            string body;
            using (StreamReader reader = new StreamReader(req.Body))
            {
                body = reader.ReadToEnd();
            }

            if (string.IsNullOrEmpty(body))
            {
                return new BadRequestResult();
            }
            DatabaseModelling.DbModels.RawBid bid = new DatabaseModelling.DbModels.RawBid(){XmlString = body};
            //TODO: GET user from header
            ///MOQ!!!
            bid.User = (await User.ReadAllAsync())[0];

            if (!await XmlReader.PopulateRawBid(bid))
            {
                return new BadRequestResult();
            }
            if (DateTime.UtcNow.AddHours(2).Year > bid.Date.Year)
            {
                return new BadRequestResult();
            }
            if (DateTime.UtcNow.AddHours(2).Year == bid.Date.Year && DateTime.UtcNow.AddHours(2).DayOfYear > bid.Date.DayOfYear)
            {
                return new BadRequestResult();
            }

            if (bid.Date.Year == DateTime.UtcNow.Year && bid.Date.DayOfYear == DateTime.UtcNow.AddHours(2).DayOfYear)
            {
                if (!await LockRows.SaveLockRawBid(bid.Company, bid.Area, bid.Date))
                {
                    return new BadRequestResult();
                }
            }

            //TODO Take the tjek for otheres bid out form Crate
            await Database.CreateAsync(bid);

            return new OkObjectResult(bid);
        }
    }
}
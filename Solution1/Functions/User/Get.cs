using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using ApiGateway.BusinessLogic;
using DatabaseModelling.DbModels;
using ModelsInterfaces;

namespace Functions.User
{
    public class Get
    {
        public IDataBase<DatabaseModelling.DbModels.User, Guid> DataBase { get; set; }
        public HttpClient Client { get; set; }

        public Get(HttpClient client, IDataBase<DatabaseModelling.DbModels.User, Guid> dataBase)
        {
            DataBase = dataBase;
            Client = client;
        }

        [FunctionName("UserById")]
        public async Task<IActionResult> GetUserById(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            if (!Guid.TryParse(req.Query["Id"], out Guid id))
            {
                return new BadRequestResult();
            }

            List<DatabaseModelling.DbModels.User> user = await DataBase.ReadAsync(x => x.Id.Equals(id));
            if (user == null || user.Count <= 0)
            {
                return new BadRequestResult();
            }
            return new OkObjectResult(user[0]);
        }
        [FunctionName("UserByCompany")]
        public async Task<IActionResult> GetUserByCompany(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            if (!Guid.TryParse(req.Query["PublicIdentifier"], out Guid companyid))
            {
                return new BadRequestResult();
            }
            List<DatabaseModelling.DbModels.User> users = await DataBase.ReadAsync(x => x.Company.PublicIdentifier.Equals(companyid));

            if (users == null || users.Count <= 0)
            {
                return new BadRequestResult();
            }

            List<IUser<DatabaseModelling.DbModels.Company, DatabaseModelling.DbModels.XmlTemplate>> IUser = new List<IUser<DatabaseModelling.DbModels.Company, DatabaseModelling.DbModels.XmlTemplate>>();

            foreach (DatabaseModelling.DbModels.User result in users)
            {
                IUser.Add(result);
            }
            return new OkObjectResult(IUser);
        }
        [FunctionName("UserGet")]
        public async Task<IActionResult> UserGet(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            List<DatabaseModelling.DbModels.User> users = await DataBase.ReadAllAsync();

            if (users == null || users.Count <= 0)
            {
                return new BadRequestResult();
            }

            List<IUser<DatabaseModelling.DbModels.Company, DatabaseModelling.DbModels.XmlTemplate>> IUser = new List<IUser<DatabaseModelling.DbModels.Company, DatabaseModelling.DbModels.XmlTemplate>>();

            foreach (DatabaseModelling.DbModels.User result in users)
            {
                IUser.Add(result);
            }
            return new OkObjectResult(IUser);
        }
    }
}

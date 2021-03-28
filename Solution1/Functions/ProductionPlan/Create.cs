using System;
using System.IO;
using System.Threading.Tasks;
using ApiGateway.BusinessLogic.Interfaces;
using Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Functions.ProductionPlan
{
    public class Create
    {
        public IXmlReader XmlReader { get; set; }
        public ILockProductionPlanRows LockProductionPlanRows { get; set; }

        public IDataBase<DatabaseModelling.DbModels.ProductionPlan, Guid> Database { get; set; }
        public IDataBase<DatabaseModelling.DbModels.User, Guid> User { get; set; }
        public Create(IDataBase<DatabaseModelling.DbModels.ProductionPlan, Guid> database, IDataBase<DatabaseModelling.DbModels.User, Guid> user, IXmlReader xmlReader, ILockProductionPlanRows lockProductionPlanRows)
        {
            XmlReader = xmlReader;
            LockProductionPlanRows = lockProductionPlanRows;
            Database = database;
            User = user;
        }

        [FunctionName("ProductionPlanCreate")]
        public async Task<IActionResult> CreateProductionPlan(
            [HttpTrigger(AuthorizationLevel.Function,"post", Route = null)] HttpRequest req,
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
            DatabaseModelling.DbModels.ProductionPlan productionPlan = new DatabaseModelling.DbModels.ProductionPlan() { XmlString = body };

            productionPlan.User = (await User.ReadAllAsync())[0];


            if (!await XmlReader.PopulateProductionPlan(productionPlan))
            {
                return new BadRequestResult();
            }

            if (DateTime.UtcNow.AddHours(2).Year > productionPlan.Date.Year)
            {
                return new BadRequestResult();
            }
            if (DateTime.UtcNow.AddHours(2).Year == productionPlan.Date.Year && DateTime.UtcNow.AddHours(2).DayOfYear > productionPlan.Date.DayOfYear)
            {
                return new BadRequestResult();
            }

            if (productionPlan.Date.Year == DateTime.UtcNow.Year && productionPlan.Date.DayOfYear == DateTime.UtcNow.AddHours(2).DayOfYear)
            {
                if (!await LockProductionPlanRows.SaveLockProductionPlan(productionPlan.Company, productionPlan.Area, productionPlan.Date))
                {
                    return new BadRequestResult();
                }
            }

            await Database.CreateAsync(productionPlan);

            return new OkObjectResult(productionPlan);
        }
    }
}

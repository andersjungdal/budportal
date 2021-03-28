using System;
using System.IO;
using System.Threading.Tasks;
using ApiGateway.BusinessLogic;
using ApiGateway.BusinessLogic.Extensions;
using ApiGateway.BusinessLogic.Interfaces;
using Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Functions.ProductionPlan
{
    public class Update
    {
        public IDataBase<DatabaseModelling.DbModels.User, Guid> UserDataBase { get; set; }
        public ILockProductionPlanRows LockProductionPlanRows { get; set; }
        public IDataBase<DatabaseModelling.DbModels.ProductionPlan, Guid> Database;

        public Update(IDataBase<DatabaseModelling.DbModels.ProductionPlan, Guid> database, IDataBase<DatabaseModelling.DbModels.User, Guid> userDataBase, ILockProductionPlanRows lockProductionPlanRows)
        {
            UserDataBase = userDataBase;
            LockProductionPlanRows = lockProductionPlanRows;
            Database = database;
        }

        [FunctionName("ProductionPlanUpdate")]
        public async Task<IActionResult> UpdateProductinPlan(
            [HttpTrigger(AuthorizationLevel.Function, "put", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            DatabaseModelling.DbModels.ProductionPlan data = await req.JasonDeserialize<DatabaseModelling.DbModels.ProductionPlan>();
            data.User = (await UserDataBase.ReadAllAsync())[0];
            if (data == null)
            {
                return new BadRequestResult();
            }
            if (DateTime.UtcNow.AddHours(2).Year > data.Date.Year)
            {
                return new BadRequestResult();
            }
            if (DateTime.UtcNow.AddHours(2).Year == data.Date.Year && DateTime.UtcNow.AddHours(2).DayOfYear > data.Date.DayOfYear)
            {
                return new BadRequestResult();
            }

            if (data.Date.Year == DateTime.UtcNow.Year && data.Date.DayOfYear == DateTime.UtcNow.AddHours(2).DayOfYear)
            {
                if (!await LockProductionPlanRows.SaveLockProductionPlan(data.Company, data.Area, data.Date))
                {
                    return new BadRequestResult();
                }
            }

            await Database.UpdateAsync(data);

            return new OkObjectResult(data);
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace FakeEnerginetAPI
{
    public static class Function1
    {
        [FunctionName("GetProductionPlanColumnIdByCompanyAndArea")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            
            if (!Guid.TryParse(req.Query["CompanyPublicIdentifier"], out Guid companyid) || !Guid.TryParse(req.Query["AreaPublicIdentifier"], out Guid areaid))
            {
                return new BadRequestResult();
            }

            Random random = new Random( companyid.GetHashCode()-areaid.GetHashCode()); 
            string[] instances = new string[random.Next(30)];
            for (int i = 0; i < instances.Length; i++)
            {
                int value = 0;
                do
                {
                    value = random.Next(100, 999);
                } 
                while (instances.FirstOrDefault(x =>
                {
                    if (x==null)
                    {
                        return false;
                    }
                    return x.Contains(value.ToString());
                }) != null);

                instances[i] = "unitCode" + value;
            }
            return new OkObjectResult(instances);
        }
    }
}

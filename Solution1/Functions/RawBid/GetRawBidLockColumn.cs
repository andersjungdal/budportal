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
using DatabaseModelling.DbModels.LockData.RawBidLock;
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
    public class GetRawBidLockColumn
    {
        public IDataBase<RawBidColumn, int> RawBidLockCollumn { get; set; }

        public GetRawBidLockColumn(IDataBase<RawBidColumn, int> rawBidLockCollumn)
        {
            RawBidLockCollumn = rawBidLockCollumn;
        }
        [FunctionName("RawBidLockGet")]
        public async Task<IActionResult> GetLockColumn(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)]
            HttpRequest req,
            ILogger log)
        {

            if (!Guid.TryParse(req.Query["RawBidPublicIdentifier"], out Guid id))
            {
                return new BadRequestResult();
            }

            List<RawBidColumn> columns = await RawBidLockCollumn.ReadAsync(x => x.RawBidPublicIdentifier == id);
            foreach (var Colums in columns)
            {
                foreach (var cells in Colums.Rows)
                {
                    cells.rawBidColumn = null;
                }
            }
            return new OkObjectResult(columns);
        }
    }
}
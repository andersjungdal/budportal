using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using EnergyBidding.Server.Authorization.AuthAttributes;
using EnergyBidding.Server.Externeal.Conections;
using EnergyBidding.Server.Models;
using Microsoft.AspNetCore.Mvc;
using ModelsInterfaces;
using ModelsInterfaces.Enums;
using RawBid = DatabaseModelling.DbModels.RawBid;

namespace EnergyBidding.Server.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class RawBidController : ControllerBase
    {
        public FunctionsProxy Proxy { get; set; }
        public RawBidController(FunctionsProxy proxy)
        {
            Proxy = proxy;
        }
        [RoleAuthorizationNiveau(Role.Bid)]
        [HttpGet("ByRawBid")]
        public async Task<ActionResult<Models.RawBid>> Get([FromQuery] Guid RawBidPublicIdentifier)
        {
            HttpResponseMessage response = await Proxy.CallFunction($"RawBidGet?RawBidPublicIdentifier={RawBidPublicIdentifier}");
            return StatusCode((int)response.StatusCode, await response.Content.ReadFromJsonAsync<RawBid>());
        }
        [RoleAuthorizationNiveau(Role.Bid)]
        [FromQueryRequired("CompanyPublicIdentifier")]
        [HttpGet("ByAreaAndCompany")]
        public async Task<ActionResult<IRawBid<Area, User, Company, XmlTemplate>[]>> GetArea([FromQuery] Guid AreasPublicIdentifier, [FromQuery] Guid CompanyPublicIdentifier)
        {
            HttpResponseMessage response = await Proxy.CallFunction($"RawBidByAreaAndCompany?AreasPublicIdentifier={AreasPublicIdentifier}&CompanyPublicIdentifier={CompanyPublicIdentifier}");
            return StatusCode((int)response.StatusCode, await response.Content.ReadFromJsonAsync<RawBid>());
        }
        [RoleAuthorizationNiveau(Role.Bid)]
        [FromQueryRequired("CompanyPublicIdentifier")]
        [HttpGet("ByAreaDateAndCompany")]
        public async Task<ActionResult<IRawBid<Area, User, Company, XmlTemplate>[]>> GetAreaAndDate([FromQuery] Guid AreaPublicIdentifier, DateTime date, [FromQuery] Guid CompanyPublicIdentifier)
        {
            HttpResponseMessage response = await Proxy.CallFunction($"RawBidByAreaDateAndCompany?AreasPublicIdentifier={AreaPublicIdentifier}&Date={date}&CompanyPublicIdentifier={CompanyPublicIdentifier}");
            if (response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, await response.Content.ReadFromJsonAsync<RawBid[]>());
            }

            return StatusCode((int) response.StatusCode);
        }
        [RoleAuthorizationNiveau(Role.Bid)]
        [FromQueryRequired("CompanyPublicIdentifier")]
        [HttpGet("ByCompany")]
        public async Task<ActionResult<IRawBid<Area, User, Company, XmlTemplate>[]>> GetCompany([FromQuery] Guid CompanyPublicIdentifier)
        {
            HttpResponseMessage response = await Proxy.CallFunction($"RawBidByCompany?CompanyPublicIdentifier={CompanyPublicIdentifier}");
            if (response.IsSuccessStatusCode)
            {
                return StatusCode((int) response.StatusCode, await response.Content.ReadFromJsonAsync<RawBid[]>());
            }

            return StatusCode((int) response.StatusCode);

        }

        [RoleAuthorizationNiveau(Role.Bid)]
        [OnlyOwnerRequired]
        [HttpGet]
        public async Task<ActionResult<Models.RawBid[]>> GetAll()
        {
            HttpResponseMessage response = await Proxy.CallFunction("RawBidGetAll");
            return StatusCode((int)response.StatusCode, await response.Content.ReadFromJsonAsync<RawBid>());
        }
        /// ///////////////////////////////
        //[RoleAuthorizationNiveau(Role.Bid)]
        //[OnlyOwnerRequired]
        [HttpGet("DatesByCompany")]
        public async Task<ActionResult<RawBidDateAndVersion[]>> GetBidDates([FromQuery] Guid CompanyPublicIdentifier)
        {
            HttpResponseMessage response = await Proxy.CallFunction($"RawBidGetDatesByCompany?CompanyPublicIdentifier={CompanyPublicIdentifier}");
            if (response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, await response.Content.ReadFromJsonAsync<RawBidDateAndVersion[]>());
            }

            return StatusCode((int)response.StatusCode);
        }
        /// ///////////////////////////////
        [RoleAuthorizationNiveau(Role.Bid)]
        //[FromXmlDocument]
        [HttpPost]
        public async Task<ActionResult<Models.RawBid>> Post()
        {
            string body = "";
            using (StreamReader stream = new StreamReader(Request.BodyReader.AsStream()))
            {
                body = stream.ReadToEnd();
            }
            HttpResponseMessage response = await Proxy.CallFunctionXml("RawBidCreate", body, HttpMethod.Post);
            return StatusCode((int)response.StatusCode, await response.Content.ReadFromJsonAsync<Models.RawBid>());
        }

        [RoleAuthorizationNiveau(Role.Bid)]
        [FromBodyRawBidRequired]
        [HttpPut]
        public async Task<ActionResult<Models.RawBid>> Put([FromBody] Models.RawBid bid)
        {
            HttpResponseMessage response = await Proxy.CallFunction("RawBidUpdate", bid, HttpMethod.Put);
            return StatusCode((int)response.StatusCode, await response.Content.ReadFromJsonAsync<RawBid>());
        }

        [RoleAuthorizationNiveau(Role.NonAuthorized)]
        [OnlyOwnerRequired]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] Guid PublicIdentifier)
        {
            HttpResponseMessage response = await Proxy.CallFunction("RawBidDelete?RawBidPublicIdentifier={RawBidPublicIdentifier}", HttpMethod.Delete);
            return StatusCode((int)response.StatusCode);
        }
    }
}

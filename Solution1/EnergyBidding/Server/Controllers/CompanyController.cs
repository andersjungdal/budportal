using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Reflection;
using System.Threading.Tasks;
using ApiGateway.BusinessLogic;
using DatabaseModelling.DbModels;
using EnergyBidding.Server.Authorization;
using EnergyBidding.Server.Authorization.AuthAttributes;
using EnergyBidding.Server.Externeal.Conections;
using Microsoft.AspNetCore.Mvc;
using ModelsInterfaces;
using ModelsInterfaces.Enums;

namespace EnergyBidding.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompanyController : ControllerBase
    {
        public FunctionsProxy Proxy { get; set; }
        public CompanyController(FunctionsProxy proxy)
        {
            Proxy = proxy;
        }
        [HttpGet]
        public async Task<ActionResult<ICompany<XmlTemplate>[]>> Get()
        {
            HttpResponseMessage response = await Proxy.CallFunction($"GetCompany");
            return StatusCode((int)response.StatusCode, await response.Content.ReadFromJsonAsync<Models.Company[]>());
        }
        [RoleAuthorizationNiveau(Role.Bid)]
        //[FromQueryRequired("PublicIdentifier")]
        [HttpGet("ByCompany")]
        public async Task<ActionResult<ICompany<XmlTemplate>>> Get([FromQuery] Guid PublicIdentifier)
        {
            HttpResponseMessage response = await Proxy.CallFunction($"GetCompanyByCompany?PublicIdentifier={PublicIdentifier}");
            return StatusCode((int)response.StatusCode, await response.Content.ReadFromJsonAsync<Models.Company>());
        }
        [RoleAuthorizationNiveau(Role.Admin)]
        [OnlyOwnerRequired]
        [HttpPost]
        public async Task<ActionResult<ICompany<XmlTemplate>>> Post([FromBody] Models.Company bid)
        {
            HttpResponseMessage response = await Proxy.CallFunction("CompanyCreate", bid, HttpMethod.Post);
            return StatusCode((int)response.StatusCode, await response.Content.ReadFromJsonAsync<Models.Company>());
        }
        [RoleAuthorizationNiveau(Role.Admin)]
        [FromBodyCompanyRequired]
        [HttpPut]
        public async Task<ActionResult<ICompany<XmlTemplate>>> Put([FromBody] Models.Company bid)
        {
            HttpResponseMessage response = await Proxy.CallFunction("CompanyUpdate", bid, HttpMethod.Put);
            return StatusCode((int)response.StatusCode, await response.Content.ReadFromJsonAsync<Models.Company>());
        }

        [RoleAuthorizationNiveau(Role.Admin)]
        [OnlyOwnerRequired]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] Guid PublicIdentifier)
        {
            HttpResponseMessage response = await Proxy.CallFunction($"CompanyDelete?PublicIdentifier={PublicIdentifier}", HttpMethod.Delete);
            return StatusCode((int)response.StatusCode);
        }
    }
}

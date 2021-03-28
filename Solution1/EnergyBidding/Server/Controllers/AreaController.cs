using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ApiGateway.BusinessLogic;
using DatabaseModelling.DbModels;
using EnergyBidding.Server.Authorization;
using EnergyBidding.Server.Authorization.AuthAttributes;
using EnergyBidding.Server.Externeal.Conections;
using Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModelsInterfaces;
using ModelsInterfaces.Enums;

namespace EnergyBidding.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AreaController : ControllerBase
    {
        public FunctionsProxy Proxy { get; set; }
        public AreaController(FunctionsProxy proxy)
        {
            Proxy = proxy;
        }
        [RoleAuthorizationNiveau(Role.Bid)]
        [HttpGet]
        public async Task<ActionResult<IArea[]>> Get()
        {
            HttpResponseMessage response = await Proxy.CallFunction("AreaGet");
            if (response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, await response.Content.ReadFromJsonAsync<Models.Area[]>());
            }

            return StatusCode((int) response.StatusCode);
        }
        [RoleAuthorizationNiveau(Role.Bid)]
        [HttpGet("ByArea")]
        public async Task<ActionResult<IArea>> Get([FromQuery] Guid PublicIdentifier)
        {
            HttpResponseMessage response = await Proxy.CallFunction($"AreaGetByArea?PublicIdentifier={PublicIdentifier}");
            return StatusCode((int)response.StatusCode, await response.Content.ReadFromJsonAsync<Models.Area>());
        }
        [RoleAuthorizationNiveau(Role.NonAuthorized)]
        [HttpPost]
        public async Task<ActionResult<IArea>> Post([FromBody] Models.Area bid)
        {
            HttpResponseMessage response = await Proxy.CallFunction("AreaCreate", bid, HttpMethod.Post);
            return StatusCode((int)response.StatusCode, await response.Content.ReadFromJsonAsync<Models.Area>());
        }

        [RoleAuthorizationNiveau(Role.NonAuthorized)]
        [HttpPut]
        public async Task<ActionResult<IArea>> Put([FromBody] Models.Area bid)
        {
            HttpResponseMessage response = await Proxy.CallFunction("AreaUpdate", bid, HttpMethod.Put);
            return StatusCode((int)response.StatusCode, await response.Content.ReadFromJsonAsync<Models.Area>());
        }

        [RoleAuthorizationNiveau(Role.NonAuthorized)]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] Guid PublicIdentifier)
        {
            HttpResponseMessage response = await Proxy.CallFunction("AreaDelete?PublicIdentifier={PublicIdentifier}", HttpMethod.Delete);
            return StatusCode((int)response.StatusCode);
        }
    }
}

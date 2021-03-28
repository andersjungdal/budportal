using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using EnergyBidding.Server.Authorization.AuthAttributes;
using EnergyBidding.Server.Externeal.Conections;
using EnergyBidding.Server.Models;
using Microsoft.AspNetCore.Mvc;
using ModelsInterfaces;
using ModelsInterfaces.Enums;

namespace EnergyBidding.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        public FunctionsProxy Proxy { get; set; }
        public UserController(FunctionsProxy proxy)
        {
            Proxy = proxy;
        }
        [RoleAuthorizationNiveau(Role.Bid)]
        [HttpGet("ByUser")]
        public async Task<ActionResult<IUser<Company, XmlTemplate>>> Get([FromQuery] Guid Id)
        {
            HttpResponseMessage response = await Proxy.CallFunction($"UserById?Id={Id}");
            return StatusCode((int)response.StatusCode, await response.Content.ReadFromJsonAsync<Models.User>());
        }
        [RoleAuthorizationNiveau(Role.Bid)]
        [FromQueryRequired("PublicIdentifier")]
        [HttpGet("ByCompany")]
        public async Task<ActionResult<IUser<Company, XmlTemplate>[]>> GetByCompany([FromQuery] Guid PublicIdentifier)
        {
            HttpResponseMessage response = await Proxy.CallFunction($"UserByCompany?PublicIdentifier={PublicIdentifier}");
            return StatusCode((int)response.StatusCode, await response.Content.ReadFromJsonAsync<Models.User[]>());

        }
        [RoleAuthorizationNiveau(Role.Bid)]
        [OnlyOwnerRequired]
        [HttpGet]
        public async Task<ActionResult<IUser<Company, XmlTemplate>[]>> Get()
        {
            HttpResponseMessage response = await Proxy.CallFunction("UserGet");
            return StatusCode((int)response.StatusCode, await response.Content.ReadFromJsonAsync<Models.User>());
        }
        [RoleAuthorizationNiveau(Role.Admin)]
        [FromBodyUserRequired]
        [HttpPost]
        public async Task<ActionResult<IUser<Company, XmlTemplate>>> Post([FromBody] UserUpsert bid)
        {
            HttpResponseMessage response = await Proxy.CallFunction("UserCreate", bid,HttpMethod.Post);
            return StatusCode((int)response.StatusCode, await response.Content.ReadFromJsonAsync<Models.User>());
        }

        [RoleAuthorizationNiveau(Role.Admin)]
        [FromBodyUserRequired]
        [HttpPut]
        public async Task<ActionResult<IUser<Company, XmlTemplate>>> Put([FromBody] UserUpsert bid)
        {
            HttpResponseMessage response = await Proxy.CallFunction("UserUpdate", bid,HttpMethod.Put);
            return StatusCode((int)response.StatusCode, await response.Content.ReadFromJsonAsync<Models.User>());
        }
        [RoleAuthorizationNiveau(Role.Admin)]
        [FromBodyUserRequired]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] Guid PublicIdentifier)
        {
            HttpResponseMessage response = await Proxy.CallFunction($"UserDelete?PublicIdentifier={PublicIdentifier}", HttpMethod.Delete);
            return StatusCode((int)response.StatusCode);
        }
    }
}

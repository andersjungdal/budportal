using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BlazorBuisnessLogic.Net5.Extensions;
using BlazorBuisnessLogic.Net5.Interfaces;
using BlazorBuisnessLogic.Net5.Models.General;
using Newtonsoft.Json;

namespace BlazorBuisnessLogic.Net5.ApiConnections
{
    public class AreasApiConnections
    {
        public StateHolder Holder { get; set; }
        public IApiErrorMessage ErrorMessage { get; set; }
        public HttpClient HttpClient { get; set; }
        //Locks
        private static readonly object CashGennerastionLock = new object();
        public AreasApiConnections(HttpClient httpClient, StateHolder holder, IApiErrorMessage errorMessage)
        {
            HttpClient = httpClient;
            Holder = holder;
            ErrorMessage = errorMessage;
        }
        public async Task<List<Area>> GetWithCache()
        {
            return await Get();
        }
        private async Task<List<Area>> Get()
        {
            HttpResponseMessage response = await HttpClient.GetAsync(UriGenneratore.GennreadURL($"/api/AreaGet"));
            if (await ErrorMessage.FaildToConnect(response))
            {
                List<Area> newArea = await response.ReadFromJasonAsync<List<Area>>();
                return newArea;
            }

            return null;
        }
        public async Task<Area> Get(Guid AreaPublicIdentifier)
        {
            HttpResponseMessage response = await HttpClient.GetAsync(
                UriGenneratore.GennreadURL($"/api/AreaGetByArea?PublicIdentifier={AreaPublicIdentifier}"));
            if (await ErrorMessage.FaildToConnect(response))
            {
                Area newArea = await response.ReadFromJasonAsync<Area>();
                return newArea;
            }
            return null;
        }
        public async Task<Area> Create(Area area)
        {
            HttpResponseMessage response = await HttpClient.PostAsync(UriGenneratore.GennreadURL($"/api/AreaCreate"), new StringContent(JsonConvert.SerializeObject(area), Encoding.UTF8, "application/json"));
            if (await ErrorMessage.FaildToConnect(response))
            {
                Area newArea = await response.ReadFromJasonAsync<Area>();
                return newArea;
            }
            return null;
        }
        public async Task<Area> Update(Area area)
        {
            HttpResponseMessage response = await HttpClient.PutAsync(UriGenneratore.GennreadURL($"/api/AreaUpdate"), new StringContent(JsonConvert.SerializeObject(area), Encoding.UTF8, "application/json"));
            if (await ErrorMessage.FaildToConnect(response))
            {
                Area newArea = await response.ReadFromJasonAsync<Area>();
                return newArea;
            }
            return null;
        }
        public async Task Deleate(Guid areaPublicIdentifier)
        {
            HttpResponseMessage response = await HttpClient.DeleteAsync(UriGenneratore.GennreadURL($"/api/AreaDelete?PublicIdentifier={areaPublicIdentifier}"));
            await ErrorMessage.FaildToConnect(response);
        }
    }
}
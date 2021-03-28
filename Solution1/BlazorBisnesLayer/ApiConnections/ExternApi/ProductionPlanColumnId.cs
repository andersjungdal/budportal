using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using BlazorBusinessLogic.Extensions;
using BlazorBusinessLogic.Interfaces;
using BlazorBusinessLogic.Models.General;

namespace BlazorBusinessLogic.ApiConnections.ExternApi
{
    public class ProductionPlanColumnId
    {
        public StateHolder Holder { get; set; }
        public IApiErrorMessage ErrorMessage { get; set; }
        public HttpClient HttpClient { get; set; }

        public ProductionPlanColumnId(HttpClient httpClient, StateHolder holder, IApiErrorMessage errorMessage)
        {
            HttpClient = httpClient;
            Holder = holder;
            ErrorMessage = errorMessage;
        }

        public async Task<List<string>> GetByCompanyPublicIdentifier(Guid companyPublicIdentifier, Guid areaPublicIdentifier)
        {
            HttpResponseMessage response = await HttpClient.GetAsync($"http://localhost:7072/api/GetProductionPlanColumnIdByCompanyAndArea?CompanyPublicIdentifier={companyPublicIdentifier}" +
                                                                                                $"&AreaPublicIdentifier={areaPublicIdentifier}");
            if (await ErrorMessage.FaildToConnect(response))
            {
                List<string> newPlans = await response.ReadFromJasonAsync<List<string>>();
                return newPlans;
            }

            return null;
        }

    }
}
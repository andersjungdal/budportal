using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BlazorBusinessLogic.Extensions;
using BlazorBusinessLogic.Interfaces;
using BlazorBusinessLogic.Models.General;
using Newtonsoft.Json;

namespace BlazorBusinessLogic.ApiConnections
{
    public class ProductionPlanApiConnection : IDocumentListAPI
    {
        public StateHolder Holder { get; set; }
        public IApiErrorMessage ErrorMessage { get; set; }
        public HttpClient HttpClient { get; set; }

        public ProductionPlanApiConnection(HttpClient httpClient, StateHolder holder, IApiErrorMessage errorMessage)
        {
            HttpClient = httpClient;
            Holder = holder;
            ErrorMessage = errorMessage;
        }

        public async Task<List<ProductionPlan>> GetByCompanyPublicIdentifier(Guid companyPublicIdentifier)
        {
            HttpResponseMessage response = await HttpClient.GetAsync(UriGenneratore.GennreadURL($"/api/ProductionPlanDatesByCompany?CompanyPublicIdentifier={companyPublicIdentifier}"));
            if (await ErrorMessage.FaildToConnect(response))
            {
                List<ProductionPlan> newBids = await response.ReadFromJasonAsync<List<ProductionPlan>>();
                return newBids;
            }

            return null;
        }
        public async Task<ProductionPlan> GetByCompanyAreaAndDate(Guid companyPublicIdentifier, Guid areaPublicIdentifier, DateTime date, HttpStatusCode[] statusCodes = null)
        {
            HttpResponseMessage response = await HttpClient.GetAsync(UriGenneratore.GennreadURL($"/api/ProductionPlanByCompanyAreaAndDate?CompanyPublicIdentifier={companyPublicIdentifier}" +
                                                                                                $"&AreaPublicIdentifier={areaPublicIdentifier}" +
                                                                                                $"&Date={date.ToString("yyyy-MM-dd")}"));
            if (await ErrorMessage.FaildToConnect(response, statusCodes))
            {
                ProductionPlan newBids = await response.ReadFromJasonAsync<ProductionPlan>();
                return newBids;
            }

            return null;
        }
        public async Task<ProductionPlan> GetByCompanyVersionAreaAndDate(Guid companyPublicIdentifier, Guid areaPublicIdentifier, DateTime date, int version, HttpStatusCode[] statusCodes = null)
        {
            HttpResponseMessage response = await HttpClient.GetAsync(UriGenneratore.GennreadURL($"/api/ProductionPlanByCompanyVersionAreaAndDate?CompanyPublicIdentifier={companyPublicIdentifier}" +
                                                                                                $"&AreaPublicIdentifier={areaPublicIdentifier}" +
                                                                                                $"&Date={date.ToString("yyyy-MM-dd")}" +
                                                                                                $"&Version={version}"));
            if (await ErrorMessage.FaildToConnect(response, statusCodes))
            {
                ProductionPlan newBids = await response.ReadFromJasonAsync<ProductionPlan>();
                return newBids;
            }

            return null;
        }

        public async Task<ProductionPlan> Get(Guid PublicIdentifier)
        {
            HttpResponseMessage response = await HttpClient.GetAsync(UriGenneratore.GennreadURL($"/api/ProductionPlanGet?ProductionPlanPublicIdentifier={PublicIdentifier}"));
            if (await ErrorMessage.FaildToConnect(response))
            {
                return await response.ReadFromJasonAsync<ProductionPlan>();
            }

            return null;

        }
        public async Task<List<BidDateAndVersion>> GetRawBidDatesByTheCompany(Guid companyPublicIdentifier)
        {
            HttpResponseMessage response = await HttpClient.GetAsync(UriGenneratore.GennreadURL($"/api/ProductionPlanGetDatesByCompany?CompanyPublicIdentifier={companyPublicIdentifier}"));
            if (await ErrorMessage.FaildToConnect(response))
            {
                List<BidDateAndVersion> returner = await response.ReadFromJasonAsync<List<BidDateAndVersion>>();
                Console.WriteLine(returner.Count);
                return returner;
            }

            return null;
        }

        public async Task<ProductionPlan> Create(string XmlProductionPlan)
        {
            HttpResponseMessage response =
                await HttpClient.PostAsync(UriGenneratore.GennreadURL($"/api/ProductionPlanCreate"),
                    new StringContent(XmlProductionPlan, Encoding.UTF8, "application/xml"));
            if (await ErrorMessage.FaildToConnect(response))
            {
                return await response.ReadFromJasonAsync<ProductionPlan>();
            }
            return null;

        }

        public async Task<ProductionPlan> Update(ProductionPlan productionPlan)
        {
            HttpResponseMessage response =
                await HttpClient.PutAsync(UriGenneratore.GennreadURL($"/api/ProductionPlanUpdate"), 
                    new StringContent(JsonConvert.SerializeObject(productionPlan), Encoding.UTF8, "application/json"));
            if (await ErrorMessage.FaildToConnect(response))
            {
                return await response.ReadFromJasonAsync<ProductionPlan>();
            }
            return null;
        }
    }
}
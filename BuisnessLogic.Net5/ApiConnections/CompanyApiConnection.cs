using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BlazorBuisnessLogic.Net5.Extensions;
using BlazorBuisnessLogic.Net5.Interfaces;
using BlazorBuisnessLogic.Net5.Models.General;
using ModelsInterfaces;
using Newtonsoft.Json;

namespace BlazorBuisnessLogic.Net5.ApiConnections
{
    public class CompanyApiConnection
    {
        private Company owner { get; set; }
        public StateHolder Holder { get; set; }
        public IApiErrorMessage ErrorMessage { get; set; }
        public HttpClient HttpClient { get; set; }
        public CompanyApiConnection(HttpClient httpClient, StateHolder holder, IApiErrorMessage errorMessage)
        {
            HttpClient = httpClient;
            Holder = holder;
            ErrorMessage = errorMessage;
        }

        public async Task<List<Company>> GetCompanyWithCache()
        {
            return await Get();
        }
        public async Task<List<Company>> Get()
        {
            HttpResponseMessage response = await HttpClient.GetAsync(UriGenneratore.GennreadURL("/api/GetCompany"));
            if (await ErrorMessage.FaildToConnect(response))
            {
                return await response.ReadFromJasonAsync<List<Company>>();
            }

            return null;
        }

        public async Task<Company> Get(Guid PublicIdentifier)
        {
            HttpResponseMessage response = await HttpClient.GetAsync(UriGenneratore.GennreadURL($"/api/GetCompanyByCompany?PublicIdentifier={PublicIdentifier}"));
            if (await ErrorMessage.FaildToConnect(response))
            {
                return await response.ReadFromJasonAsync<Company>();
            }

            return null;
        }
        public async Task<Company> GetOwner()
        {
            while (owner==null)
            {
                //TODO: get another GET request for owner
                HttpResponseMessage response = await HttpClient.GetAsync(UriGenneratore.GennreadURL($"/api/GetCompanyByCompany?PublicIdentifier=B88C198F-432A-4B0C-A5A9-F3E903692A5C"));
                if (response.IsSuccessStatusCode)
                {
                    owner = await response.ReadFromJasonAsync<Company>();
                }
            }
            return owner;
        }

        public async Task<Company> Create(ICompany<XmlTemplate> company)
        {
            HttpResponseMessage response = await HttpClient.PostAsync(UriGenneratore.GennreadURL($"/api/CompanyCreate"), new StringContent(JsonConvert.SerializeObject(company), Encoding.UTF8, "application/json"));
            if (await ErrorMessage.FaildToConnect(response))
            {
                return await response.ReadFromJasonAsync<Company>();
            }
            return null;
        }
        public async Task<Company> Update(Company company)
        {
            HttpResponseMessage response = await HttpClient.PutAsync(UriGenneratore.GennreadURL($"/api/CompanyUpdate"), new StringContent(JsonConvert.SerializeObject(company), Encoding.UTF8, "application/json"));
            if (await ErrorMessage.FaildToConnect(response))
            {
                return await response.ReadFromJasonAsync<Company>();
            }
            return null;
        }
        public async Task<bool> Delete(Guid companyPublicIdentifier)
        {
            HttpResponseMessage response = await HttpClient.DeleteAsync(UriGenneratore.GennreadURL($"/api/CompanyDelete?PublicIdentifier={companyPublicIdentifier}")); 
            return await ErrorMessage.FaildToConnect(response);
        }
    }
}
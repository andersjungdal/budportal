using System;
using System.Net.Http;
using System.Threading.Tasks;
using BlazorBuisnessLogic.Net5.Extensions;
using BlazorBuisnessLogic.Net5.Interfaces;
using BlazorBuisnessLogic.Net5.Models.General;

namespace BlazorBuisnessLogic.Net5.ApiConnections
{
    public class XmlTemplateApiConnection
    {
        public HttpClient HttpClient { get; set; }
        public StateHolder Holder { get; set; }
        public IApiErrorMessage ErrorMessage { get; set; }

        public XmlTemplateApiConnection(HttpClient httpClient, StateHolder holder, IApiErrorMessage errorMessage)
        {
            HttpClient = httpClient;
            Holder = holder;
            ErrorMessage = errorMessage;
        }

        public XmlTemplate Get(Guid XmlTemplatePublicIdentifier)
        {
            return null;
        }

        public async Task<XmlTemplate> GetRawBidByCompany(Guid CompanyPublicIdentifier)
        {
            HttpResponseMessage response = await HttpClient.GetAsync(UriGenneratore.GennreadURL($"/api/GetRawBidTemplateByCompany?PublicIdentifier={CompanyPublicIdentifier}"));

            if (await ErrorMessage.FaildToConnect(response))
            {
                XmlTemplate newArea = await response.ReadFromJasonAsync<XmlTemplate>();
                return newArea;
            }
            return null;
        }
        public async Task<XmlTemplate> GetProductionPlanTempByCompany(Guid CompanyPublicIdentifier)
        {
            HttpResponseMessage response = await HttpClient.GetAsync(UriGenneratore.GennreadURL($"/api/GetProductionPlanTemplateByCompany?PublicIdentifier={CompanyPublicIdentifier}"));

            if (await ErrorMessage.FaildToConnect(response))
            {
                XmlTemplate newArea = await response.ReadFromJasonAsync<XmlTemplate>();
                return newArea;
            }
            return null;
        }
        
    }
}
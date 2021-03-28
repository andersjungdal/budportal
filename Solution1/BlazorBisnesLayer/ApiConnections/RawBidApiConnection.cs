using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BlazorBusinessLogic.Extensions;
using BlazorBusinessLogic.Interfaces;
using BlazorBusinessLogic.Models.General;
using ModelsInterfaces;
using Newtonsoft.Json;

namespace BlazorBusinessLogic.ApiConnections
{
    public class RawBidApiConnection: IDocumentListAPI
    {
        public StateHolder Holder { get; set; }
        public IApiErrorMessage ErrorMessage { get; set; }
        public HttpClient HttpClient { get; set; }
        public RawBidApiConnection(HttpClient httpClient, StateHolder holder, IApiErrorMessage errorMessage)
        {
            HttpClient = httpClient;
            Holder = holder;
            ErrorMessage = errorMessage;
        }

        public async Task<List<RawBid>> Get()
        {
            HttpResponseMessage response = await HttpClient.GetAsync(UriGenneratore.GennreadURL("/api/RawBidGetAll"));
            if (await ErrorMessage.FaildToConnect(response))
            {
                return await response.ReadFromJasonAsync<List<RawBid>>();
            }

            return null;
        }

        public async Task<RawBid> Get(Guid PublicIdentifier)
        {
            HttpResponseMessage response = await HttpClient.GetAsync(UriGenneratore.GennreadURL($"/api/RawBidGet?RawBidPublicIdentifier={PublicIdentifier}"));
            if (await ErrorMessage.FaildToConnect(response))
            {
                return await response.ReadFromJasonAsync<RawBid>();
            }

            return null;
        }

        public async Task<List<RawBid>> GetByArea(Guid areaPublicIdentifier, Guid companyPublicIdentifier = new Guid())
        {
            companyPublicIdentifier = companyPublicIdentifier.Equals(Guid.Empty) ? Holder.User.Company.PublicIdentifier : companyPublicIdentifier;
            HttpResponseMessage response = await HttpClient.GetAsync(UriGenneratore.GennreadURL($"/api/RawBidByAreaAndCompany?AreaPublicIdentifier={areaPublicIdentifier}&CompanyPublicIdentifier={companyPublicIdentifier}"));
            if (await ErrorMessage.FaildToConnect(response))
            {
                List<RawBid> newBids = await response.ReadFromJasonAsync<List<RawBid>>();
                return newBids;
            }

            return null;
        }
        public async Task<RawBid> GetByAreaAndDate(Guid areaPublicIdentifier, DateTime date, Guid companyPublicIdentifier, HttpStatusCode[] IgnoreCode = null)
        {
            HttpResponseMessage response = await HttpClient.GetAsync(UriGenneratore.GennreadURL($"/api/RawBidByAreaDateAndCompany?AreaPublicIdentifier={areaPublicIdentifier}&Date={date.ToString("yyyy-MM-dd")}&CompanyPublicIdentifier={companyPublicIdentifier}"));
            if (await ErrorMessage.FaildToConnect(response, IgnoreCode))
            {
                return await response.ReadFromJasonAsync<RawBid>();
            }
            return null;
        }
        public async Task<RawBid> GetByVersionAreaCompanyAndDate(Guid areaPublicIdentifier, DateTime date, Guid companyPublicIdentifier, int version, HttpStatusCode[] IgnoreCode = null)
        {
            HttpResponseMessage response = await HttpClient.GetAsync(UriGenneratore.GennreadURL($"/api/RawBidByAreaDateVersionAndCompany?AreaPublicIdentifier={areaPublicIdentifier}&Date={date.ToString("yyyy-MM-dd")}&CompanyPublicIdentifier={companyPublicIdentifier}&Version={version}"));
            if (await ErrorMessage.FaildToConnect(response, IgnoreCode))
            {
                return await response.ReadFromJasonAsync<RawBid>();
            }
            return null;
        }
        public async Task<List<RawBid>> GetByCompany(Guid companyPublicIdentifier)
        {
            HttpResponseMessage response = await HttpClient.GetAsync(UriGenneratore.GennreadURL($"/api/RawBidByCompany?CompanyPublicIdentifier={companyPublicIdentifier}"));
            if (await ErrorMessage.FaildToConnect(response))
            {
                List<RawBid> newBids = await response.ReadFromJasonAsync<List<RawBid>>();
                return newBids;
            }

            return null;
        }

        /// ////////////////////////////////
        public async Task<List<BidDateAndVersion>> GetRawBidDatesByTheCompany(Guid companyPublicIdentifier)
        {
            HttpResponseMessage response = await HttpClient.GetAsync(UriGenneratore.GennreadURL($"/api/RawBidGetDatesByCompany?CompanyPublicIdentifier={companyPublicIdentifier}"));
            if (await ErrorMessage.FaildToConnect(response))
            {
                List<BidDateAndVersion> returner = await response.ReadFromJasonAsync<List<BidDateAndVersion>>();
                Console.WriteLine(returner.Count);
                return returner;
            }

            return null;
        }
        /// ///////////////////////////////
        public async Task<RawBid> Create(string xmlBid)
        {
            HttpResponseMessage response = await HttpClient.PostAsync(UriGenneratore.GennreadURL("/api/RawBidCreate"), new StringContent(xmlBid, Encoding.UTF8, "application/xml"));
            if (await ErrorMessage.FaildToConnect(response))
            {
                RawBid rawBid = await response.ReadFromJasonAsync<RawBid>();
                return rawBid;
            }
            return null;
        }
        public async Task<RawBid> Update(RawBid rawBid)
        {
            HttpResponseMessage response = await HttpClient.PutAsync(UriGenneratore.GennreadURL("/api/RawBidUpdate"), new StringContent(JsonConvert.SerializeObject(rawBid), Encoding.UTF8, "application/json"));
            if (await ErrorMessage.FaildToConnect(response))
            {
                return await response.ReadFromJasonAsync<RawBid>();
            }
            return null;
        }
        public async Task Delete(Guid xmlBidPublicIdentifier)
        {
            HttpResponseMessage response = await HttpClient.DeleteAsync(UriGenneratore.GennreadURL($"/api/RawBidDelete?PublicIdentifier={xmlBidPublicIdentifier}"));
            await ErrorMessage.FaildToConnect(response);
        }
    }
}
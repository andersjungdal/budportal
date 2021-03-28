using System;
using System.Collections.Generic;
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
    public class UserApiConnection
    {
        public StateHolder Holder { get; set; }
        public IApiErrorMessage ErrorMessage { get; set; }
        public HttpClient HttpClient { get; set; }
        public UserApiConnection(HttpClient httpClient, StateHolder holder, IApiErrorMessage errorMessage)
        {
            HttpClient = httpClient;
            Holder = holder;
            ErrorMessage = errorMessage;
        }
        public async Task<List<User>> Get()
        {
            HttpResponseMessage response = await HttpClient.GetAsync(UriGenneratore.GennreadURL($"/api/UserGet"));
            if (await ErrorMessage.FaildToConnect(response))
            {
                List<User> newUser = await response.ReadFromJasonAsync<List<User>>();
                return newUser;
            }
            return null;
        }
        public async Task<User> Get(Guid userPublicIdentifier)
        {
            HttpResponseMessage response = await HttpClient.GetAsync(UriGenneratore.GennreadURL($"/api/UserById?PublicIdentifier={userPublicIdentifier}"));
            if (await ErrorMessage.FaildToConnect(response))
            {
                User newUser = await response.ReadFromJasonAsync<User>();
                return newUser;
            }
            return null;
        }
        public async Task<List<User>> GetByCompany(Guid companyPublicIdentifier)
        {
            HttpResponseMessage response = await HttpClient.GetAsync(UriGenneratore.GennreadURL($"/api/UserByCompany?PublicIdentifier={companyPublicIdentifier}"));
            if (await ErrorMessage.FaildToConnect(response))
            {
                List<User> newUser = await response.ReadFromJasonAsync<List<User>>();
                return newUser;
            }
            return null;
        }
        public async Task<User> Create<T>(T user) where T: IUserSubmit<Company, XmlTemplate>
        {
            HttpResponseMessage response = await HttpClient.PostAsync(UriGenneratore.GennreadURL($"/api/UserCreate"), new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json"));
            if (await ErrorMessage.FaildToConnect(response))
            {
                User newUser = await response.ReadFromJasonAsync<User>();
                return newUser;
            }
            return null;
        }
        public async Task<User> Update(User user)
        {
            HttpResponseMessage response = await HttpClient.PutAsync(UriGenneratore.GennreadURL($"/api/UserUpdate"), new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json"));
            if (await ErrorMessage.FaildToConnect(response))
            {
                User newUser = await response.ReadFromJasonAsync<User>();
                return newUser;
            }
            return null;
        }
        public async Task<bool> Delete(Guid userPublicIdentifier)
        {
            HttpResponseMessage response = await HttpClient.DeleteAsync(UriGenneratore.GennreadURL($"/api/UserDelete?PublicIdentifier={userPublicIdentifier}"));
            return await ErrorMessage.FaildToConnect(response);
        }
    }
}
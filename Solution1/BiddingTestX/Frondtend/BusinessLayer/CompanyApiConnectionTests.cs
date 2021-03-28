using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using BlazorBusinessLogic;
using BlazorBusinessLogic.ApiConnections;
using BlazorBusinessLogic.Interfaces;
using BlazorBusinessLogic.Models.General;
using Microsoft.AspNetCore.Components;
using Moq;
using Moq.Protected;
using Xunit;

namespace BiddingTestX.Frondtend.BusinessLayer
{
    public class CompanyApiConnectionTests
    {

        [Fact]
        public async void Get()
        {
            //Arrange
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "api/Company");
            Mock<HttpMessageHandler> moqHttpMessageHandler = new Mock<HttpMessageHandler>();
            moqHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(Resoureces.Companys)
                });
            HttpClient client = new HttpClient(moqHttpMessageHandler.Object);
            client.BaseAddress = new Uri("https://localhost:44373/");
            StateHolder state = new StateHolder();
            Mock<NavigationManager> nav = new Mock<NavigationManager>();
            IApiErrorMessage errorMessage = new ApiErrorMessage(state, nav.Object);

            CompanyApiConnection companyApi = new CompanyApiConnection(client, state, errorMessage);
            //Act
            var companies = await companyApi.GetCompanyWithCache();
            //Assert
            Assert.Equal(JsonSerializer.Serialize(companies), JsonSerializer.Serialize(JsonSerializer.Deserialize<List<Company>>(Resoureces.Companys)));
        }
        [Fact]
        public async void GetNull()
        {
            //Arrange
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "api/Company");
            Mock<HttpMessageHandler> moqHttpMessageHandler = new Mock<HttpMessageHandler>();
            moqHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Content = new StringContent(Resoureces.Companys)
                });
            HttpClient client = new HttpClient(moqHttpMessageHandler.Object);
            client.BaseAddress = new Uri("https://localhost:44373/");
            StateHolder state = new StateHolder();
            Mock<NavigationManager> nav = new Mock<NavigationManager>();
            IApiErrorMessage errorMessage = new ApiErrorMessage(state, nav.Object);
            CompanyApiConnection companyApi = new CompanyApiConnection(client, state,errorMessage);
            //Act
            var companies = await companyApi.GetCompanyWithCache();
            //Assert
            Assert.Null(companies);
        }

        [Fact]
        public async void GetCompanyPublicIdentifier()
        {
            //Arrange
            var des = JsonSerializer.Deserialize<Company>(Resoureces.Company);
            Guid publicIdentifier = des.PublicIdentifier;
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"api/Company/ByCompany?PublicIdentifier={publicIdentifier}");
            Mock<HttpMessageHandler> moqHttpMessageHandler = new Mock<HttpMessageHandler>();
            moqHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(Resoureces.Company)
                });
            HttpClient client = new HttpClient(moqHttpMessageHandler.Object);
            client.BaseAddress = new Uri("https://localhost:44373/");
            StateHolder state = new StateHolder();
            Mock<NavigationManager> nav = new Mock<NavigationManager>();
            IApiErrorMessage errorMessage = new ApiErrorMessage(state, nav.Object);
            CompanyApiConnection companyApi = new CompanyApiConnection(client, state,errorMessage);
            //Act
            var company = await companyApi.Get(publicIdentifier);
            //Assert
            Assert.Equal(JsonSerializer.Serialize(company), JsonSerializer.Serialize(JsonSerializer.Deserialize<Company>(Resoureces.Company)));
        }
        [Fact]
        public async void GetCompanyPublicIdentifierNull()
        {
            //Arrange
            var des = JsonSerializer.Deserialize<Company>(Resoureces.Company);
            Guid publicIdentifier = des.PublicIdentifier;
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"api/Company/ByCompany?PublicIdentifier={publicIdentifier}");
            Mock<HttpMessageHandler> moqHttpMessageHandler = new Mock<HttpMessageHandler>();
            moqHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Content = new StringContent(Resoureces.Company)
                });
            HttpClient client = new HttpClient(moqHttpMessageHandler.Object);
            client.BaseAddress = new Uri("https://localhost:44373/");
            StateHolder state = new StateHolder();
            Mock<NavigationManager> nav = new Mock<NavigationManager>();
            IApiErrorMessage errorMessage = new ApiErrorMessage(state, nav.Object);
            CompanyApiConnection companyApi = new CompanyApiConnection(client, state,errorMessage);
            //Act
            var company = await companyApi.Get(publicIdentifier);
            //Assert
            Assert.Null(company);
        }

        [Fact]
        public async void Create()
        {
            //Arrange
            Area newaktion = new Area()
            { Description = "newdescription", PublicIdentifier = new Guid(), Type = "newtype" };

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, $"/api/Action");
            Mock<HttpMessageHandler> moqHttpMessageHandler = new Mock<HttpMessageHandler>();
            moqHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(JsonSerializer.Serialize(newaktion))
                });
            HttpClient client = new HttpClient(moqHttpMessageHandler.Object);
            client.BaseAddress = new Uri("https://localhost:44373/");
            StateHolder state = new StateHolder();
            Mock<NavigationManager> nav = new Mock<NavigationManager>();
            IApiErrorMessage errorMessage = new ApiErrorMessage(state, nav.Object);
            AreasApiConnections areasApi = new AreasApiConnections(client, state, errorMessage);
            //Act

            var aktion = await areasApi.Create(newaktion);
            //Assert
            Assert.Equal(JsonSerializer.Serialize(aktion), JsonSerializer.Serialize(newaktion));
        }

        [Fact]
        public async void Update()
        {
            //Arrange
            Area newaktion = new Area()
            { Description = "newdescription", PublicIdentifier = new Guid(), Type = "newtype" };

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, $"/api/Action");
            Mock<HttpMessageHandler> moqHttpMessageHandler = new Mock<HttpMessageHandler>();
            moqHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(JsonSerializer.Serialize(newaktion))
                });
            HttpClient client = new HttpClient(moqHttpMessageHandler.Object);
            client.BaseAddress = new Uri("https://localhost:44373/");
            StateHolder state = new StateHolder();
            Mock<NavigationManager> nav = new Mock<NavigationManager>();
            IApiErrorMessage errorMessage = new ApiErrorMessage(state, nav.Object);
            AreasApiConnections areasApi = new AreasApiConnections(client, state,errorMessage);
            //Act

            var aktion = await areasApi.Update(newaktion);
            //Assert
            Assert.Equal(JsonSerializer.Serialize(aktion), JsonSerializer.Serialize(newaktion));
        }
        [Fact]
        public async void Delete()
        {
            //Arrange
            var des = JsonSerializer.Deserialize<Company>(Resoureces.Company);
            Guid publicIdentifier = des.PublicIdentifier;
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, $"api/Company/ByCompany?PublicIdentifier={publicIdentifier}");
            Mock<HttpMessageHandler> moqHttpMessageHandler = new Mock<HttpMessageHandler>();
            moqHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                });
            HttpClient client = new HttpClient(moqHttpMessageHandler.Object);
            client.BaseAddress = new Uri("https://localhost:44373/");
            StateHolder state = new StateHolder();
            Mock<NavigationManager> nav = new Mock<NavigationManager>();
            IApiErrorMessage errorMessage = new ApiErrorMessage(state, nav.Object);
            CompanyApiConnection companyApi = new CompanyApiConnection(client, state, errorMessage);
            //Act
            await companyApi.Delete(publicIdentifier);
            //Assert
            Assert.Null(state.PubUp);
        }
        [Fact]
        public async void DeleteFail()
        {
            //Arrange
            var des = JsonSerializer.Deserialize<Company>(Resoureces.Company);
            Guid publicIdentifier = des.PublicIdentifier;
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, $"api/Company/ByCompany?PublicIdentifier={publicIdentifier}");
            Mock<HttpMessageHandler> moqHttpMessageHandler = new Mock<HttpMessageHandler>();
            moqHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.BadRequest,
                });
            HttpClient client = new HttpClient(moqHttpMessageHandler.Object);
            client.BaseAddress = new Uri("https://localhost:44373/");
            StateHolder state = new StateHolder();
            Mock<NavigationManager> nav = new Mock<NavigationManager>();
            IApiErrorMessage errorMessage = new ApiErrorMessage(state, nav.Object);
            CompanyApiConnection companyApi = new CompanyApiConnection(client, state, errorMessage);
            //Act
            await companyApi.Delete(publicIdentifier);
            //Assert
            Assert.NotNull(state.PubUp);
        }

    }
}
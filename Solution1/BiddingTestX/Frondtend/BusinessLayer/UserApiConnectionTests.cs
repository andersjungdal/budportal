using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using BlazorBusinessLogic;
using BlazorBusinessLogic.ApiConnections;
using BlazorBusinessLogic.Interfaces;
using BlazorBusinessLogic.Models.General;
using Microsoft.AspNetCore.Components;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using Xunit;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace BiddingTestX.Frondtend.BusinessLayer
{
    public class UserApiConnectionTests
    {

        [Fact]
        public async void Get()
        {
            //Arrange
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "api/User");
            Mock<HttpMessageHandler> moqHttpMessageHandler = new Mock<HttpMessageHandler>();
            moqHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(Resoureces.Users)
                });
            HttpClient client = new HttpClient(moqHttpMessageHandler.Object);
            client.BaseAddress = new Uri("https://localhost:44373/");
            StateHolder state = new StateHolder();
            Mock<NavigationManager> nav = new Mock<NavigationManager>();
            IApiErrorMessage errorMessage = new ApiErrorMessage(state, nav.Object);
            UserApiConnection userApi = new UserApiConnection(client, state, errorMessage);
            //Act
            var users = await userApi.Get();
            //Assert
            Assert.Equal(JsonSerializer.Serialize(users), JsonSerializer.Serialize(JsonConvert.DeserializeObject<List<User>>(Resoureces.Users)));
        }
        [Fact]
        public async void GetNull()
        {
            //Arrange
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "api/User");
            Mock<HttpMessageHandler> moqHttpMessageHandler = new Mock<HttpMessageHandler>();
            moqHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Content = new StringContent(Resoureces.Users)
                });
            HttpClient client = new HttpClient(moqHttpMessageHandler.Object);
            client.BaseAddress = new Uri("https://localhost:44373/");
            StateHolder state = new StateHolder();
            Mock<NavigationManager> nav = new Mock<NavigationManager>();
            IApiErrorMessage errorMessage = new ApiErrorMessage(state, nav.Object);
            UserApiConnection userApi = new UserApiConnection(client, state, errorMessage);
            //Act
            var users = await userApi.Get();
            //Assert
            Assert.Null(users);
        }
        [Fact]
        public async void GetUserPublicIdentifier()
        {
            //Arrange
            Guid publicIdentifier = Guid.Parse("95cc3957-b472-4cb6-b998-6db6ae4fcb91");
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"api/User/ByUser?PublicIdentifier={publicIdentifier}");
            Mock<HttpMessageHandler> moqHttpMessageHandler = new Mock<HttpMessageHandler>();
            moqHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(Resoureces.User)
                });
            HttpClient client = new HttpClient(moqHttpMessageHandler.Object);
            client.BaseAddress = new Uri("https://localhost:44373/");
            StateHolder state = new StateHolder();
            Mock<NavigationManager> nav = new Mock<NavigationManager>();
            IApiErrorMessage errorMessage = new ApiErrorMessage(state, nav.Object);
            UserApiConnection userApi = new UserApiConnection(client, state,errorMessage);
            //Act
            var user = await userApi.Get(publicIdentifier);
            //Assert
            Assert.Equal(JsonSerializer.Serialize(user), JsonSerializer.Serialize(JsonConvert.DeserializeObject<User>(Resoureces.User)));
        }
        [Fact]
        public async void GetUserPublicIdentifierNull()
        {
            //Arrange
            Guid publicIdentifier = Guid.Parse("95cc3957-b472-4cb6-b998-6db6ae4fcb91");
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"api/User/ByUser?PublicIdentifier={publicIdentifier}");
            Mock<HttpMessageHandler> moqHttpMessageHandler = new Mock<HttpMessageHandler>();
            moqHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Content = new StringContent(Resoureces.User)
                });
            HttpClient client = new HttpClient(moqHttpMessageHandler.Object);
            client.BaseAddress = new Uri("https://localhost:44373/");
            StateHolder state = new StateHolder();
            Mock<NavigationManager> nav = new Mock<NavigationManager>();
            IApiErrorMessage errorMessage = new ApiErrorMessage(state, nav.Object);
            UserApiConnection userApi = new UserApiConnection(client, state,errorMessage);
            //Act
            var user = await userApi.Get(publicIdentifier);
            //Assert
            Assert.Null(user);
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
            AreasApiConnections areasApi = new AreasApiConnections(client, state,errorMessage);
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
            Guid publicIdentifier = Guid.Parse("95cc3957-b472-4cb6-b998-6db6ae4fcb91");
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, $"api/User/ByUser?PublicIdentifier={publicIdentifier}");
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
            UserApiConnection userApi = new UserApiConnection(client, state,errorMessage);
            //Act
            await userApi.Delete(publicIdentifier);
            //Assert
            Assert.Null(state.PubUp);
        }
        [Fact]
        public async void DeleteFail()
        {
            //Arrange
            Guid publicIdentifier = Guid.Parse("95cc3957-b472-4cb6-b998-6db6ae4fcb91");
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, $"api/User/ByUser?PublicIdentifier={publicIdentifier}");
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
            UserApiConnection userApi = new UserApiConnection(client, state,errorMessage);
            //Act
            await userApi.Delete(publicIdentifier);
            //Assert
            Assert.NotNull(state.PubUp);
        }

    }
}
using System;
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
    public class RawBidApiConnectionTests
    {
        //TODO FixTest
        [Fact]
        public async void Get()
        {
            //Arrange
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "api/RawBid");
            Mock<HttpMessageHandler> moqHttpMessageHandler = new Mock<HttpMessageHandler>();
            moqHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(Resoureces.RawBids)
                });
            HttpClient client = new HttpClient(moqHttpMessageHandler.Object);
            client.BaseAddress = new Uri("https://localhost:44373/");
            StateHolder state = new StateHolder();
            Mock<NavigationManager> nav = new Mock<NavigationManager>();
            IApiErrorMessage errorMessage = new ApiErrorMessage(state, nav.Object);
            RawBidApiConnection rawbidApi = new RawBidApiConnection(client, state,errorMessage);
            //Act
            var rawbids = await rawbidApi.Get();
            //Assert

            var test1 = Newtonsoft.Json.JsonConvert.DeserializeObject<RawBid[]>(Resoureces.RawBids);
            string Two = JsonSerializer.Serialize(test1);
            Console.WriteLine(Two);
            Assert.Equal(JsonSerializer.Serialize(rawbids), Two);
        }
        [Fact]
        public async void GetNull()
        {
            //Arrange
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "api/RawBid");
            Mock<HttpMessageHandler> moqHttpMessageHandler = new Mock<HttpMessageHandler>();
            moqHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Content = new StringContent(Resoureces.RawBids)
                });
            HttpClient client = new HttpClient(moqHttpMessageHandler.Object);
            client.BaseAddress = new Uri("https://localhost:44373/");
            StateHolder state = new StateHolder();
            Mock<NavigationManager> nav = new Mock<NavigationManager>();
            IApiErrorMessage errorMessage = new ApiErrorMessage(state, nav.Object);
            RawBidApiConnection rawbidApi = new RawBidApiConnection(client, state,errorMessage);
            //Act
            var rawbids = await rawbidApi.Get();
            //Assert
            Assert.Null(rawbids);
        }
        [Fact]
        public async void GetRawBidPublicIdentifier()
        {
            //Arrange
            var des = JsonSerializer.Deserialize<RawBid>(Resoureces.RawBid);
            Guid publicIdentifier = des.PublicIdentifier;
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"api/RawBid/ByRawBid?PublicIdentifier={publicIdentifier}");
            Mock<HttpMessageHandler> moqHttpMessageHandler = new Mock<HttpMessageHandler>();
            moqHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(Resoureces.RawBid)
                });
            HttpClient client = new HttpClient(moqHttpMessageHandler.Object);
            client.BaseAddress = new Uri("https://localhost:44373/");
            StateHolder state = new StateHolder();
            Mock<NavigationManager> nav = new Mock<NavigationManager>();
            IApiErrorMessage errorMessage = new ApiErrorMessage(state, nav.Object);
            RawBidApiConnection rawbidApi = new RawBidApiConnection(client, state,errorMessage);
            //Act
            var rawbid = await rawbidApi.Get(publicIdentifier);
            //Assert
            var test1 = Newtonsoft.Json.JsonConvert.DeserializeObject<RawBid>(Resoureces.RawBid);
            string Two = JsonSerializer.Serialize(test1);
            Console.WriteLine(Two);
            Assert.Equal(JsonSerializer.Serialize(rawbid), Two);
        }
        [Fact]
        public async void GetRawBidByAktionPublicIdentifier()
        {
            //Arrange
            var des = JsonSerializer.Deserialize<RawBid>(Resoureces.Aktion);
            Guid akpublicIdentifier = des.PublicIdentifier;
            var comdes = JsonSerializer.Deserialize<RawBid>(Resoureces.Company);
            Guid compublicIdentifier = comdes.PublicIdentifier;
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"api/RawBid/ByRawBid?akPublicIdentifier={akpublicIdentifier}?comPublicIdentifier={compublicIdentifier}");
            Mock<HttpMessageHandler> moqHttpMessageHandler = new Mock<HttpMessageHandler>();
            moqHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(Resoureces.RawBids)
                });
            HttpClient client = new HttpClient(moqHttpMessageHandler.Object);
            client.BaseAddress = new Uri("https://localhost:44373/");
            StateHolder state = new StateHolder();
            Mock<NavigationManager> nav = new Mock<NavigationManager>();
            IApiErrorMessage errorMessage = new ApiErrorMessage(state, nav.Object);
            RawBidApiConnection rawbidApi = new RawBidApiConnection(client, state, errorMessage);
            //Act
            var rawbid = await rawbidApi.GetByArea(akpublicIdentifier, compublicIdentifier);
            //Assert

            var test1 = Newtonsoft.Json.JsonConvert.DeserializeObject<RawBid[]>(Resoureces.RawBids);
            string Two = JsonSerializer.Serialize(test1);
            Assert.Equal(JsonSerializer.Serialize(rawbid), Two);
        }
        [Fact]
        public async void GetRawBidByCompanyPublicIdentifier()
        {
            //Arrange
            var des = JsonSerializer.Deserialize<RawBid>(Resoureces.Company);
            Guid publicIdentifier = des.PublicIdentifier;
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"api/RawBid/ByRawBid?PublicIdentifier={publicIdentifier}");
            Mock<HttpMessageHandler> moqHttpMessageHandler = new Mock<HttpMessageHandler>();
            moqHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(Resoureces.RawBids)
                });
            HttpClient client = new HttpClient(moqHttpMessageHandler.Object);
            client.BaseAddress = new Uri("https://localhost:44373/");
            StateHolder state = new StateHolder();
            Mock<NavigationManager> nav = new Mock<NavigationManager>();
            IApiErrorMessage errorMessage = new ApiErrorMessage(state, nav.Object);
            RawBidApiConnection rawbidApi = new RawBidApiConnection(client, state,errorMessage);
            //Act
            var rawbid = await rawbidApi.GetByCompany(publicIdentifier);
            //Assert

            var test1 = Newtonsoft.Json.JsonConvert.DeserializeObject<RawBid[]>(Resoureces.RawBids);
            string Two = JsonSerializer.Serialize(test1);
            Assert.Equal(JsonSerializer.Serialize(rawbid), Two);
        }
        [Fact]
        public async void GetRawBidByAktionPublicIdentifierAndDate()
        {
            //Arrange
            var des = JsonSerializer.Deserialize<RawBid>(Resoureces.Aktion);
            Guid akpublicIdentifier = des.PublicIdentifier;
            var company = JsonSerializer.Deserialize<RawBid>(Resoureces.Company);
            Guid compublicIdentifier = company.PublicIdentifier;
            DateTime date = des.Date;
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"api/RawBid/ByAktionDateAndCompany?AktionPublicIdentifier={akpublicIdentifier}&Date={date}&CompanyPublicIdentifier={compublicIdentifier}");
            Mock<HttpMessageHandler> moqHttpMessageHandler = new Mock<HttpMessageHandler>();
            moqHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(Resoureces.RawBids)
                });
            HttpClient client = new HttpClient(moqHttpMessageHandler.Object);
            client.BaseAddress = new Uri("https://localhost:44373/");
            StateHolder state = new StateHolder();
            Mock<NavigationManager> nav = new Mock<NavigationManager>();
            IApiErrorMessage errorMessage = new ApiErrorMessage(state, nav.Object);
            RawBidApiConnection rawbidApi = new RawBidApiConnection(client, state,errorMessage);
            //Act
            var rawbid = await rawbidApi.GetByAreaAndDate(akpublicIdentifier, date, compublicIdentifier);
            //Assert

            var test1 = Newtonsoft.Json.JsonConvert.DeserializeObject<RawBid[]>(Resoureces.RawBids);
            string Two = JsonSerializer.Serialize(test1);
            Assert.Equal(JsonSerializer.Serialize(rawbid), Two);
        }
        [Fact]
        public async void GetRawBidPublicIdentifierNull()
        {
            //Arrange
            var des = JsonSerializer.Deserialize<RawBid>(Resoureces.RawBid);
            Guid publicIdentifier = des.PublicIdentifier;
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"api/RawBid/ByRawBid?PublicIdentifier={publicIdentifier}");
            Mock<HttpMessageHandler> moqHttpMessageHandler = new Mock<HttpMessageHandler>();
            moqHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Content = new StringContent(Resoureces.RawBid)
                });
            HttpClient client = new HttpClient(moqHttpMessageHandler.Object);
            client.BaseAddress = new Uri("https://localhost:44373/");
            StateHolder state = new StateHolder();
            Mock<NavigationManager> nav = new Mock<NavigationManager>();
            IApiErrorMessage errorMessage = new ApiErrorMessage(state, nav.Object);
            RawBidApiConnection rawbidApi = new RawBidApiConnection(client, state,errorMessage);
            //Act
            var rawbid = await rawbidApi.Get(publicIdentifier);
            //Assert
            Assert.Null(rawbid);
        }
        [Fact]
        public async void GetRawBidByAktionPublicIdentifierNull()
        {
            //Arrange
            var des = JsonSerializer.Deserialize<RawBid>(Resoureces.Aktion);
            Guid akpublicIdentifier = des.PublicIdentifier;
            var comdes = JsonSerializer.Deserialize<RawBid>(Resoureces.Company);
            Guid compublicIdentifier = comdes.PublicIdentifier;
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"api/RawBid/ByRawBid?akPublicIdentifier={akpublicIdentifier}?comPublicIdentifier={compublicIdentifier}");
            Mock<HttpMessageHandler> moqHttpMessageHandler = new Mock<HttpMessageHandler>();
            moqHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Content = new StringContent(Resoureces.RawBids)
                });
            HttpClient client = new HttpClient(moqHttpMessageHandler.Object);
            client.BaseAddress = new Uri("https://localhost:44373/");
            StateHolder state = new StateHolder();
            Mock<NavigationManager> nav = new Mock<NavigationManager>();
            IApiErrorMessage errorMessage = new ApiErrorMessage(state, nav.Object);
            RawBidApiConnection rawbidApi = new RawBidApiConnection(client, state,errorMessage);
            //Act
            var rawbid = await rawbidApi.GetByArea(akpublicIdentifier, compublicIdentifier);
            //Assert
            Assert.Null(rawbid);
        }
        [Fact]
        public async void GetRawBidByCompanyPublicIdentifierNull()
        {
            //Arrange
            var des = JsonSerializer.Deserialize<RawBid>(Resoureces.Company);
            Guid publicIdentifier = des.PublicIdentifier;
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"api/RawBid/ByRawBid?PublicIdentifier={publicIdentifier}");
            Mock<HttpMessageHandler> moqHttpMessageHandler = new Mock<HttpMessageHandler>();
            moqHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Content = new StringContent(Resoureces.RawBid)
                });
            HttpClient client = new HttpClient(moqHttpMessageHandler.Object);
            client.BaseAddress = new Uri("https://localhost:44373/");
            StateHolder state = new StateHolder();
            Mock<NavigationManager> nav = new Mock<NavigationManager>();
            IApiErrorMessage errorMessage = new ApiErrorMessage(state, nav.Object);
            RawBidApiConnection rawbidApi = new RawBidApiConnection(client, state,errorMessage);
            //Act
            var rawbid = await rawbidApi.GetByCompany(publicIdentifier);
            //Assert
            Assert.Null(rawbid);
        }
        [Fact]
        public async void GetRawBidByAktionPublicIdentifierAndDateNull()
        {
            //Arrange
            var des = JsonSerializer.Deserialize<RawBid>(Resoureces.Aktion);
            Guid akpublicIdentifier = des.PublicIdentifier;
            var comdes = JsonSerializer.Deserialize<RawBid>(Resoureces.Company);
            Guid compublicIdentifier = comdes.PublicIdentifier;
            DateTime date = des.Date;
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"api/RawBid/ByRawBid?akPublicIdentifier={akpublicIdentifier}&Date={date}?comPublicIdentifier={compublicIdentifier}");
            Mock<HttpMessageHandler> moqHttpMessageHandler = new Mock<HttpMessageHandler>();
            moqHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Content = new StringContent(Resoureces.RawBids)
                });
            HttpClient client = new HttpClient(moqHttpMessageHandler.Object);
            client.BaseAddress = new Uri("https://localhost:44373/");
            StateHolder state = new StateHolder();
            Mock<NavigationManager> nav = new Mock<NavigationManager>();
            IApiErrorMessage errorMessage = new ApiErrorMessage(state, nav.Object);
            RawBidApiConnection rawbidApi = new RawBidApiConnection(client, state,errorMessage);
            //Act
            var rawbid = await rawbidApi.GetByAreaAndDate(akpublicIdentifier, date, compublicIdentifier);
            //Assert
            Assert.Null(rawbid);
        }
        [Fact]
        public async void Create()
        {
            //Arrange
            RawBid newrawBid = new RawBid()
            { Area = new Area(), PublicIdentifier = new Guid(), Company = new Company(), Date = new DateTime(), User = new User(), Version = 1, XmlString = Resoureces.mFRR_regulerkraftbud };

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, $"/api/RawBid");
            Mock<HttpMessageHandler> moqHttpMessageHandler = new Mock<HttpMessageHandler>();
            moqHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(JsonSerializer.Serialize(newrawBid))
                });
            HttpClient client = new HttpClient(moqHttpMessageHandler.Object);
            client.BaseAddress = new Uri("https://localhost:44373/");
            StateHolder state = new StateHolder();
            Mock<NavigationManager> nav = new Mock<NavigationManager>();
            IApiErrorMessage errorMessage = new ApiErrorMessage(state, nav.Object);
            RawBidApiConnection rawBidApiConnection = new RawBidApiConnection(client, state,errorMessage);
            //Act

            var rawbid = await rawBidApiConnection.Create(newrawBid.XmlString);
            //Assert
            Assert.Equal(JsonSerializer.Serialize(newrawBid), JsonSerializer.Serialize(rawbid));
        }
        [Fact]
        public async void Update()
        {
            //Arrange
            RawBid newrawbid = new RawBid()
            { Area = new Area(), PublicIdentifier = new Guid(), Company = new Company(), Date = new DateTime(), User = new User(), Version = 1 };

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, $"/api/RawBid");
            Mock<HttpMessageHandler> moqHttpMessageHandler = new Mock<HttpMessageHandler>();
            moqHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(JsonSerializer.Serialize(newrawbid))
                });
            HttpClient client = new HttpClient(moqHttpMessageHandler.Object);
            client.BaseAddress = new Uri("https://localhost:44373/");
            StateHolder state = new StateHolder();
            Mock<NavigationManager> nav = new Mock<NavigationManager>();
            IApiErrorMessage errorMessage = new ApiErrorMessage(state, nav.Object);
            RawBidApiConnection rawBidApiConnection = new RawBidApiConnection(client, state,errorMessage);
            //Act

            var rawBid = await rawBidApiConnection.Update(newrawbid);
            //Assert
            Assert.Equal(JsonSerializer.Serialize(newrawbid), JsonSerializer.Serialize(rawBid));
        }
        [Fact]
        public async void Delete()
        {
            //Arrange
            var des = JsonSerializer.Deserialize<RawBid>(Resoureces.RawBid);
            Guid publicIdentifier = des.PublicIdentifier;
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, $"api/RawBid/ByRawBid?PublicIdentifier={publicIdentifier}");
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
            RawBidApiConnection rawbidApi = new RawBidApiConnection(client, state,errorMessage);
            //Act
            await rawbidApi.Delete(publicIdentifier);
            //Assert
            Assert.Null(state.PubUp);
        }
        [Fact]
        public async void DeleteFail()
        {
            //Arrange
            var des = JsonSerializer.Deserialize<RawBid>(Resoureces.RawBid);
            Guid publicIdentifier = des.PublicIdentifier;
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, $"api/RawBid/ByRawBid?PublicIdentifier={publicIdentifier}");
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
            RawBidApiConnection rawbidApi = new RawBidApiConnection(client, state,errorMessage);
            //Act
            await rawbidApi.Delete(publicIdentifier);
            //Assert
            Assert.NotNull(state.PubUp);
        }

    }
}
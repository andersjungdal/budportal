using System.Net;
using System.Net.Http;
using BlazorBusinessLogic;
using BlazorBusinessLogic.ApiConnections;
using BlazorBusinessLogic.Interfaces;
using Microsoft.AspNetCore.Components;
using Moq;
using Xunit;

namespace BiddingTestX.Frondtend.BusinessLayer
{
    public class ApiErrorMessageTest
    {
        [Fact]
        public async void FaildToConnectSuccesStatusCode()
        {
            //Arrange
            Mock<HttpResponseMessage> httpResponseMessage = new Mock<HttpResponseMessage>();
            StateHolder stateHolder = new StateHolder();
            Mock<NavigationManager> nav = new Mock<NavigationManager>();
            IApiErrorMessage apiErrorMessage = new ApiErrorMessage(stateHolder,nav.Object);
            var isConnect = await apiErrorMessage.FaildToConnect(httpResponseMessage.Object.EnsureSuccessStatusCode());
            //Act//Assert
            Assert.True(isConnect);
            Assert.Null(stateHolder.PubUp);
        }
        [Fact]
        public async void FaildToConnectInternalServerError()
        {
            //Arrange
            Mock<HttpResponseMessage> httpResponseMessage = new Mock<HttpResponseMessage>();
            httpResponseMessage.Object.StatusCode = HttpStatusCode.InternalServerError;
            StateHolder stateHolder = new StateHolder();
            //Act
            Mock<NavigationManager> nav = new Mock<NavigationManager>();
            IApiErrorMessage apiErrorMessage = new ApiErrorMessage(stateHolder, nav.Object);
            var isConnect = await apiErrorMessage.FaildToConnect(httpResponseMessage.Object);
            //Assert
            Assert.False(isConnect);
            Assert.NotNull(stateHolder.PubUp);
            Assert.Equal($"Dos not have any connection\nPlease try again\nError:{HttpStatusCode.InternalServerError}", stateHolder.PubUp.Message);
        }
        [Fact]
        public async void FaildToConnectBadRequest()
        {
            //Arrange
            Mock<HttpResponseMessage> httpResponseMessage = new Mock<HttpResponseMessage>();
            httpResponseMessage.Object.StatusCode = HttpStatusCode.BadRequest;
            StateHolder stateHolder = new StateHolder();
            //Act
            Mock<NavigationManager> nav = new Mock<NavigationManager>();
            IApiErrorMessage apiErrorMessage = new ApiErrorMessage(stateHolder, nav.Object);
            var isConnect = await apiErrorMessage.FaildToConnect(httpResponseMessage.Object);
            //Assert
            Assert.False(isConnect);
            Assert.NotNull(stateHolder.PubUp);
            Assert.Equal($"You do not have all the necessary Data\nPlease check the Data and try again\nError:{HttpStatusCode.BadRequest}",
                stateHolder.PubUp.Message);

        }
        [Fact]
        public async void FaildToConnectServiceUnavailable()
        {
            //Arrange
            Mock<HttpResponseMessage> httpResponseMessage = new Mock<HttpResponseMessage>();
            httpResponseMessage.Object.StatusCode = HttpStatusCode.ServiceUnavailable;
            StateHolder stateHolder = new StateHolder();
            //Act
            Mock<NavigationManager> nav = new Mock<NavigationManager>();
            IApiErrorMessage apiErrorMessage = new ApiErrorMessage(stateHolder, nav.Object);
            var isConnect = await apiErrorMessage.FaildToConnect(httpResponseMessage.Object);
            //Assert
            Assert.False(isConnect);
            Assert.NotNull(stateHolder.PubUp);
            Assert.Equal($"The server is currently down if you get this message again\nPlease contact Energinet\nError:{HttpStatusCode.ServiceUnavailable}",
                stateHolder.PubUp.Message);
        }
    }
}
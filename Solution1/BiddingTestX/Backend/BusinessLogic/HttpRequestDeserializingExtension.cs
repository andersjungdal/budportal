using System.IO;
using System.Text;
using System.Threading.Tasks;
using ApiGateway.BusinessLogic;
using ApiGateway.BusinessLogic.Extensions;
using DatabaseModelling.DbModels;
using Microsoft.AspNetCore.Http;
using Moq;
using Newtonsoft.Json;
using Xunit;

namespace BiddingTestX.Backend.BusinessLogic
{
    public class HttpRequestDeserializingExtension
    {
        [Fact]
        public async Task JsonDeserialize_DeserializeAktion()
        {
            //Arrange
            byte[] byteArray = Encoding.ASCII.GetBytes(Resoureces.Aktion);
            Mock<HttpRequest> MockHttpRequest = new Mock<HttpRequest>();
            MockHttpRequest.Setup(x => x.Body).Returns(new MemoryStream(byteArray));
            //Act
            Area resulr = await MockHttpRequest.Object.JasonDeserialize<Area>();
            //Assert
            Area Tjækker = JsonConvert.DeserializeObject<Area>(Resoureces.Aktion);
            Assert.Equal(resulr.Description, Tjækker.Description);
            Assert.Equal(resulr.Type, Tjækker.Type);
            Assert.Equal(resulr.Description, Tjækker.Description);
        }
    }
}

using System;
using System.Collections.Generic;
using ApiGateway.BusinessLogic;
using DatabaseModelling.DbModels;
using Interfaces;
using ModelsInterfaces;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using Xunit;

namespace BiddingTestX.Backend.BusinessLogic
{
    public class XmlReaderTest
    {
        //[Fact]
        //public async void ActionToAction_ConvertDBAktionToApiAktion()
        //{
        //    //Arrange
        //    Mock<IDataBase<Area, Guid>> AktionDbMoq = new Mock<IDataBase<Area,Guid>>();
        //    Mock<IDataBase<Company, Guid>> CompanyDbMoq = new Mock<IDataBase<Company, Guid>>();

        //    CompanyDbMoq.Setup(x => x.ReadAsync(It.IsAny<Func<Company, bool>>())).ReturnsAsync(new List<Company>{JsonConvert.DeserializeObject<Company>(Resoureces.Company)});
        //    AktionDbMoq.Setup(x => x.ReadAsync(It.IsAny<Func<Area, bool>>())).ReturnsAsync(new List<Area>{JsonConvert.DeserializeObject<Area>(Resoureces.Aktion)});

        //    XmlReader testObj = new XmlReader(AktionDbMoq.Object, CompanyDbMoq.Object);
        //    RawBid bid = new RawBid();
        //    bid.XmlString = Resoureces.mFRR_regulerkraftbud;
        //    //Act
        //    await testObj.PopulateRawBid(bid);
        //    //Assert
        //    Assert.NotNull(bid.Company);
        //    Assert.NotNull(bid.Area);
        //    Assert.Equal(bid.Date.Ticks, 637365024000000000);
        //}
    }
}

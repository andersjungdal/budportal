//using ApiGateway.BusinessLogic;
//using EnergyBidding.Server.Models;
//using Newtonsoft.Json;
//using Xunit;

//namespace BiddingTestX.BusinessLogic
//{
//    public class Translasion
//    {

//        [Fact]
//        public void ActionToAction_ConvertDBAktionToApiAktion()
//        {
//            //Arrange

//            DatabaseModelling.DbModels.Areas obj = JsonConvert.DeserializeObject<DatabaseModelling.DbModels.Areas>(Resoureces.Areas);
//            //Act
//            Areas result = obj.AreaToArea<Areas>();
//            //Assert
//            Areas Tjækker = JsonConvert.DeserializeObject<Areas>(Resoureces.Areas);
//            Assert.NotEqual(result.GetType(), obj.GetType());
//            Assert.Equal(result.Description, Tjækker.Description);
//            Assert.Equal(result.Type, Tjækker.Type);
//            Assert.Equal(result.PublicIdentifier, Tjækker.PublicIdentifier);
//        }
//        [Fact]
//        public void ActionToAction_ConvertDBCompanyToApiCompany()
//        {
//            //Arrange

//            DatabaseModelling.DbModels.Company obj = JsonConvert.DeserializeObject<DatabaseModelling.DbModels.Company>(Resoureces.Company);
//            //Act
//            Company result = obj.CompanyToCompany<Company>();
//            //Assert
//            Company Tjækker = JsonConvert.DeserializeObject<Company>(Resoureces.Company);
//            Assert.NotEqual(result.GetType(), obj.GetType());
//            Assert.Equal(result.Name, Tjækker.Name);
//            Assert.Equal(result.City, Tjækker.City);
//            Assert.Equal(result.ZipCode, Tjækker.ZipCode);
//            Assert.Equal(result.StreetNumber, Tjækker.StreetNumber);
//            Assert.Equal(result.Road, Tjækker.Road);
//            Assert.Equal(result.PublicIdentifier, Tjækker.PublicIdentifier);
//        }
//        [Fact]
//        public void UserToUser_ConvertDBUserToUser()
//        {
//            //Arrange

//            DatabaseModelling.DbModels.User obj = JsonConvert.DeserializeObject<DatabaseModelling.DbModels.User>(Resoureces.User);
//            //Act
//            User result = obj.UserToUser<User, Company, DatabaseModelling.DbModels.Company> ();
//            //Assert
//            User Tjækker = JsonConvert.DeserializeObject<User>(Resoureces.User);
//            Assert.NotEqual(result.GetType(), obj.GetType());
//            Assert.Equal(result.Id, Tjækker.Id);
//            Assert.Equal(result.UserName, Tjækker.UserName);
//            Assert.Equal(result.Roles, Tjækker.Roles);
//        }
//        [Fact]
//        public void RawBidToRawBid_ConvertDBRawBidToApiRawBid()
//        {
//            //Arrange

//            DatabaseModelling.DbModels.RawBid obj = JsonConvert.DeserializeObject<DatabaseModelling.DbModels.RawBid>(Resoureces.RawBid);
//            //Act
//            RawBid result = obj.RawBidToRawBid<RawBid,Areas, User, Company, DatabaseModelling.DbModels.Areas, DatabaseModelling.DbModels.User, DatabaseModelling.DbModels.Company> ();
//            //Assert
//            RawBid Tjækker = JsonConvert.DeserializeObject<RawBid>(Resoureces.RawBid);
//            Assert.NotEqual(result.GetType(), obj.GetType());
//            Assert.Equal(result.XmlString, Tjækker.XmlString);
//            Assert.Equal(result.PublicIdentifier, Tjækker.PublicIdentifier);
//        }
//    }
//}
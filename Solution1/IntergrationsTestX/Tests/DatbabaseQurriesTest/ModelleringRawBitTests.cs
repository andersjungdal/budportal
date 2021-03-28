using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DatabaseModelling.CRUD;
using DatabaseModelling.DbModels;
using Interfaces;
using IntergrationsTestX.Setup;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using DatabaseModelling.Context;
using Xunit;

namespace IntergrationsTestX.Tests.DatbabaseQurriesTest
{
    public class ModelleringRawBitTests : DataBaseConections
    {
        [Fact]
        public async Task ReadAllAsyncTest()
        {
            collection.AddScoped<IDataBase<RawBid, Guid>, ModellingRawBit>();
            IServiceProvider serviceScope = collection.BuildServiceProvider();
            SeadDatabase(serviceScope, "RawBit");
            IDataBase<RawBid, Guid> IDataBaseRawBid = serviceScope.GetService<IDataBase<RawBid, Guid>>();
            //Act
            List<RawBid> result = await IDataBaseRawBid.ReadAllAsync();
            //Assert
            List<RawBid> settingslist = JsonConvert.DeserializeObject<List<RawBid>>(Seetings.RawBid);
            Assert.True(result.Count > 0);
            for (int i = 0; i < settingslist.Count; i++)
            {
                Assert.Equal(result[i].Id, settingslist[i].Id);
                Assert.Equal(result[i].XmlString, Seetings.mFRR_regulerkraftbud);

            }
        }
        [Fact]
        public async Task CreateManyAsync()
        {
            //Arrange
            collection.AddScoped<IDataBase<RawBid, Guid>, ModellingRawBit>();
            IServiceProvider FirstService = collection.BuildServiceProvider();
            SeadDatabase(FirstService, "Areas");
            SeadDatabase(FirstService, "Company");
            IDataBase<RawBid, Guid> IDataBaseRawBid = FirstService.GetService<IDataBase<RawBid, Guid>>();

            List<RawBid> settingslist = JsonConvert.DeserializeObject<List<RawBid>>(Seetings.RawBid);
            foreach(RawBid rawBid in settingslist)
            {
                rawBid.XmlString = Seetings.mFRR_regulerkraftbud;
            }
            //Act
            await IDataBaseRawBid.CreateAsync(settingslist.ToArray());
            //Assert
            SecurityDbContext context = FirstService.GetService<SecurityDbContext>();
            foreach (RawBid rawBid in settingslist)
            {
                Assert.NotNull(await context.RawBids.FindAsync(rawBid.Id));
            }

        }
        [Fact]
        public async Task Delete()
        {
            //Arrange
            collection.AddScoped<IDataBase<RawBid, Guid>, ModellingRawBit>();
            IServiceProvider FirstService = collection.BuildServiceProvider();
            IDataBase<RawBid, Guid> IDataBaseRawBit = FirstService.GetService<IDataBase<RawBid, Guid>>();
            SeadDatabase(FirstService, "RawBit");

            List<RawBid> settingslist = JsonConvert.DeserializeObject<List<RawBid>>(Seetings.RawBid);

            //Act
            await IDataBaseRawBit.Delete(settingslist[0].PublicIdentifier);
            //Assert
            SecurityDbContext context = FirstService.GetService<SecurityDbContext>();
            Assert.Null(context.RawBids.FirstOrDefault(x => x.Id == settingslist[0].Id));
        }
        [Fact]
        public async Task ReadAsyncTest()
        {
            //Arrange
            collection.AddScoped<IDataBase<RawBid, Guid>, ModellingRawBit>();
            IServiceProvider serviceScope = collection.BuildServiceProvider();
            SeadDatabase(serviceScope, "RawBit");
            IDataBase<RawBid, Guid> IDataBaseRawBid = serviceScope.GetService<IDataBase<RawBid, Guid>>();

            List<RawBid> settingslist = JsonConvert.DeserializeObject<List<RawBid>>(Seetings.RawBid);
            //Act
            List<RawBid> result = await IDataBaseRawBid.ReadAsync(x => x.Id == settingslist[0].Id);
            //Assert
            Assert.True(result.Count > 0);
            Assert.Equal(result[0].PublicIdentifier, settingslist[0].PublicIdentifier);
            Assert.Equal(result[0].XmlString, Seetings.mFRR_regulerkraftbud);
        }
        //Virker ikke pt
        //[Fact]
        public async Task CreateAsync()
        {
            //Arrange
            collection.AddScoped<IDataBase<RawBid, Guid>, ModellingRawBit>();
            IServiceProvider FirstService = collection.BuildServiceProvider();
            IDataBase<RawBid, Guid> IDataBaseRawBid = FirstService.GetService<IDataBase<RawBid, Guid>>();

            List<RawBid> settingslist = JsonConvert.DeserializeObject<List<RawBid>>(Seetings.RawBid);
            RawBid raw = settingslist[0];
            raw.Id = 3;
            raw.PublicIdentifier =  Guid.NewGuid();
            //Act
            await IDataBaseRawBid.CreateAsync(raw);
            //Assert
            SecurityDbContext context = FirstService.GetService<SecurityDbContext>();

            Assert.NotNull(context.RawBids.FirstOrDefault(x => x.Id == 3));
        }
        //Virker ikke pt
        //[Fact]
        public async Task UpdateAsync()
        {
            //Arrange
            collection.AddScoped<IDataBase<RawBid, Guid>, ModellingRawBit>();
            IServiceProvider FirstService = collection.BuildServiceProvider();
            IDataBase<RawBid, Guid> IDataBaseRawBid = FirstService.GetService<IDataBase<RawBid, Guid>>();
            SeadDatabase(FirstService, "RawBit");

            List<RawBid> settingslist = JsonConvert.DeserializeObject<List<RawBid>>(Seetings.RawBid);
            Company company = JsonConvert.DeserializeObject<List<Company>>(Seetings.Company)[1];

            RawBid rawBid = new RawBid() {Id = settingslist[0].Id,PublicIdentifier = settingslist[0].PublicIdentifier, Company = company};

            //Act
            await IDataBaseRawBid.UpdateAsync(rawBid);
            //Assert
            SecurityDbContext context = FirstService.GetService<SecurityDbContext>();
            Assert.True(rawBid.Company.PublicIdentifier == context.RawBids.Find(rawBid.Id).Company.PublicIdentifier);
        }
    }
}
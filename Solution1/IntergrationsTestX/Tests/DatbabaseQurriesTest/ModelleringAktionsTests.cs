using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatabaseModelling.CRUD;
using DatabaseModelling.DbModels;
using Interfaces;
using IntergrationsTestX.Setup;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using DatabaseModelling.Context;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace IntergrationsTestX.Tests.DatbabaseQurriesTest
{
    public class ModelleringAktionsTests : DataBaseConections
    {
            [Fact]
            public async Task CreateManyAsync()
            {
                //Arrange
                collection.AddScoped<IDataBase<Area, Guid>, ModellingArea>();
                IServiceProvider FirstService = collection.BuildServiceProvider();
                IDataBase<Area, Guid> IDataBaseAktion = FirstService.GetService<IDataBase<Area, Guid>>();

                List<Area> settingslist = JsonConvert.DeserializeObject<List<Area>>(Seetings.Aktions);

                //Act
                await IDataBaseAktion.CreateAsync(settingslist.ToArray());
                //Assert
                SecurityDbContext context = FirstService.GetService<SecurityDbContext>();
                foreach (Area Aktion in settingslist)
                {
                    Assert.NotNull(await context.Areas.FirstOrDefaultAsync(x => x.PublicIdentifier == Aktion.PublicIdentifier));
                }

            }
            [Fact]
            public async Task Delete()
            {
                //Arrange
                collection.AddScoped<IDataBase<Area, Guid>, ModellingArea>();
                IServiceProvider FirstService = collection.BuildServiceProvider();
                IDataBase<Area, Guid> IDataBaseAktion = FirstService.GetService<IDataBase<Area, Guid>>();
                SeadDatabase(FirstService, "Areas");

                List<Area> settingslist = JsonConvert.DeserializeObject<List<Area>>(Seetings.Aktions);

                //Act
                await IDataBaseAktion.Delete(settingslist[0].PublicIdentifier);
                //Assert
                SecurityDbContext context = FirstService.GetService<SecurityDbContext>();
                Assert.Null(context.Areas.FirstOrDefault(x => x.PublicIdentifier == settingslist[0].PublicIdentifier));
            }
            [Fact]
            public async Task ReadAllAsyncTest()
            {
                //Arrange

                collection.AddScoped<IDataBase<Area, Guid>, ModellingArea>();
                IServiceProvider serviceScope = collection.BuildServiceProvider();
                SeadDatabase(serviceScope, "Areas");
                IDataBase<Area, Guid> IDataBaseAktion = serviceScope.GetService<IDataBase<Area, Guid>>();
                //Act
                List<Area> result = await IDataBaseAktion.ReadAllAsync();

                //Assert
                List<Area> settingslist = JsonConvert.DeserializeObject<List<Area>>(Seetings.Aktions);
                Assert.True(result.Count > 0);
                for (int i = 0; i < settingslist.Count; i++)
                {
                    Assert.Equal(result[i].Description, settingslist[i].Description);
                    Assert.Equal(result[i].Type, settingslist[i].Type);

                }
            }

            [Fact]
            public async Task ReadAsyncTest()
            {
                //Arrange
                collection.AddScoped<IDataBase<Area, Guid>, ModellingArea>();
                IServiceProvider serviceScope = collection.BuildServiceProvider();
                SeadDatabase(serviceScope, "Areas");
                IDataBase<Area, Guid> IDataBaseAktion = serviceScope.GetService<IDataBase<Area, Guid>>();

                List<Area> settingslist = JsonConvert.DeserializeObject<List<Area>>(Seetings.Aktions);
                //Act
                List<Area> result = await IDataBaseAktion.ReadAsync(x => x.PublicIdentifier == settingslist[0].PublicIdentifier);
                //Assert
                Assert.True(result.Count > 0);
                Assert.Equal(result[0].Description, settingslist[0].Description);
                Assert.Equal(result[0].Type, settingslist[0].Type);
            }
            [Fact]
            public async Task CreateAsync()
            {
                //Arrange
                collection.AddScoped<IDataBase<Area, Guid>, ModellingArea>();
                IServiceProvider FirstService = collection.BuildServiceProvider();
                IDataBase<Area, Guid> IDataBaseAktion = FirstService.GetService<IDataBase<Area, Guid>>();

                List<Area> settingslist = JsonConvert.DeserializeObject<List<Area>>(Seetings.Aktions);
                Area area = new Area() {Description = "descriptiontest", Type = "typetest" };
                //Act
                await IDataBaseAktion.CreateAsync(area);
                //Assert
                SecurityDbContext context = FirstService.GetService<SecurityDbContext>();

                Assert.NotNull(context.Areas.FirstOrDefault(x => x.Type == "typetest"));
            }

        [Fact]
        public async Task UpdateAsync()
            {
                //Arrange
                collection.AddScoped<IDataBase<Area, Guid>, ModellingArea>();
                IServiceProvider FirstService = collection.BuildServiceProvider();
                IDataBase<Area, Guid> IDataBaseAktion = FirstService.GetService<IDataBase<Area, Guid>>();
                SeadDatabase(FirstService, "Areas");

                List<Area> settingslist = JsonConvert.DeserializeObject<List<Area>>(Seetings.Aktions);

                Area area = settingslist[0];
                area.Type = "Jondow";
                //Act
                await IDataBaseAktion.UpdateAsync(area);
                //Assert
                SecurityDbContext context = FirstService.GetService<SecurityDbContext>();
                Assert.True(area.Type == context.Areas.FirstOrDefault(x => x.PublicIdentifier == area.PublicIdentifier).Type);
            }
    }
}
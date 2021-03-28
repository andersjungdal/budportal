using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatabaseModelling.CRUD;
using DatabaseModelling.DbModels;
using Interfaces;
using IntergrationsTestX.Setup;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ModelsInterfaces.Enums;
using Newtonsoft.Json;
using DatabaseModelling.Context;
using Xunit;

namespace IntergrationsTestX.Tests.DatbabaseQurriesTest
{
    public class ModelleringUserTests : DataBaseConections
    {
        [Fact]
        public async Task CreateManyAsync()
        {
            //Arrange
            collection.AddScoped<IDataBase<User, Guid>, ModellingUser>();
            IServiceProvider FirstService = collection.BuildServiceProvider();
            IDataBase<User, Guid> IDataBaseUser = FirstService.GetService<IDataBase<User, Guid>>();

            List<User> settingslist = JsonConvert.DeserializeObject<List<User>>(Seetings.User);

            //Act
            await IDataBaseUser.CreateAsync(settingslist.ToArray());
            //Assert
            SecurityDbContext context = FirstService.GetService<SecurityDbContext>();
            foreach (User User in settingslist)
            {
                Assert.NotNull(await context.Users.FindAsync(User.Id));
            }
        }
        [Fact]
        public async Task Delete()
        {
            //Arrange
            collection.AddScoped<IDataBase<User, Guid>, ModellingUser>();
            IServiceProvider FirstService = collection.BuildServiceProvider();
            SeadDatabase(FirstService, "User");
            IDataBase<User, Guid> IDataBaseUser = FirstService.GetService<IDataBase<User, Guid>>();

            List<User> settingslist = JsonConvert.DeserializeObject<List<User>>(Seetings.User);

            //Act
            await IDataBaseUser.Delete(settingslist[0].Id);
            //Assert
            SecurityDbContext context = FirstService.GetService<SecurityDbContext>();
            Assert.Null(await context.Users.FirstOrDefaultAsync(x => x.Id == settingslist[0].Id));
        }
        [Fact]
        public async Task ReadAllAsyncTest()
        {
            //Arrange

            collection.AddScoped<IDataBase<User, Guid>, ModellingUser>();
            IServiceProvider serviceScope = collection.BuildServiceProvider();
            SeadDatabase(serviceScope, "User");
            IDataBase<User, Guid> IDataBaseUser = serviceScope.GetService<IDataBase<User, Guid>>();
            //Act
            List<User> result = await IDataBaseUser.ReadAllAsync();

            //Assert
            List<User> settingslist = JsonConvert.DeserializeObject<List<User>>(Seetings.User);
            Assert.True(result.Count > 0);
            for (int i = 0; i < settingslist.Count; i++)
            {
                Assert.Equal(result[i].UserName, settingslist[i].UserName);
                Assert.Equal(result[i].Role, settingslist[i].Role);

            }
        }

        [Fact]
        public async Task ReadAsyncTest()
        {
            //Arrange
            collection.AddScoped<IDataBase<User, Guid>, ModellingUser>();
            IServiceProvider serviceScope = collection.BuildServiceProvider();
            SeadDatabase(serviceScope, "User");
            IDataBase<User, Guid> IDataBaseUser = serviceScope.GetService<IDataBase<User, Guid>>();

            List<User> settingslist = JsonConvert.DeserializeObject<List<User>>(Seetings.User);
            //Act
            List<User> result = await IDataBaseUser.ReadAsync(x => x.Id == settingslist[0].Id);
            //Assert
            Assert.True(result.Count > 0);
            Assert.Equal(result[0].UserName, settingslist[0].UserName);
            Assert.Equal(result[0].Role, settingslist[0].Role);
        }
        [Fact]
        public async Task CreateAsync()
        {
            //Arrange
            collection.AddScoped<IDataBase<User, Guid>, ModellingUser>();
            IServiceProvider FirstService = collection.BuildServiceProvider();
            SeadDatabase(FirstService, "Company");
            IDataBase<User, Guid> IDataBaseUser = FirstService.GetService<IDataBase<User, Guid>>();

            User User = new User() { UserName = "TestStudent", Company = new Company{PublicIdentifier = Guid.Parse("14eb7383-7518-4ddb-a715-ea6ddd28213f")},Role = Role.Admin};
            //Act
            await IDataBaseUser.CreateAsync(User);
            //Assert
            SecurityDbContext context = FirstService.GetService<SecurityDbContext>();

            Assert.NotNull(context.Users.FirstOrDefault(x => x.UserName == "TestStudent"));
        }

        [Fact]
        public async Task UpdateAsync()
        {
            //Arrange
            collection.AddScoped<IDataBase<User, Guid>, ModellingUser>();
            IServiceProvider FirstService = collection.BuildServiceProvider();
            IDataBase<User, Guid> IDataBaseUser = FirstService.GetService<IDataBase<User, Guid>>();
            SeadDatabase(FirstService, "User");

            List<User> settingslist = JsonConvert.DeserializeObject<List<User>>(Seetings.User);

            User User = settingslist[0];
            User.Role = Role.Bid;
            //Act
            await IDataBaseUser.UpdateAsync(User);
            //Assert
            SecurityDbContext context = FirstService.GetService<SecurityDbContext>();
            Assert.True(User.Role == context.Users.FirstOrDefault(x => x.Id == User.Id).Role);
        }
    }
}
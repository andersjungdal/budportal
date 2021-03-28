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
using Xunit;

namespace IntergrationsTestX.Tests.DatbabaseQurriesTest
{
    public class ModelleringCompanyTest : DataBaseConections
    {
        [Fact]
        public async Task ReadAllAsyncTest()
        {
            //Arrange
            
            collection.AddScoped<IDataBase<Company, Guid>, ModellingCompany>();
            IServiceProvider serviceScope = collection.BuildServiceProvider();
            SeadDatabase(serviceScope, "Company");
            IDataBase<Company, Guid> IDataBaseCompany = serviceScope.GetService<IDataBase<Company, Guid>>();
            //Act
            List<Company> result = await IDataBaseCompany.ReadAllAsync();
            
            //Assert
            List<Company> settingslist = JsonConvert.DeserializeObject<List<Company>>(Seetings.Company);
            Assert.True(result.Count > 0);
            for (int i = 0; i < settingslist.Count; i++)
            {
                Assert.Equal(result[i].Name, settingslist[i].Name);
                Assert.Equal(result[i].Road, settingslist[i].Road);

            }
        }

        [Fact]
        public async Task ReadAsyncTest()
        {
            //Arrange
            collection.AddScoped<IDataBase<Company, Guid>, ModellingCompany>();
            IServiceProvider serviceScope = collection.BuildServiceProvider();
            SeadDatabase(serviceScope, "Company");
            IDataBase<Company, Guid> IDataBaseCompany = serviceScope.GetService<IDataBase<Company, Guid>>();

            List<Company> settingslist = JsonConvert.DeserializeObject<List<Company>>(Seetings.Company);
            //Act
            List<Company> result = await IDataBaseCompany.ReadAsync(x=>x.PublicIdentifier==settingslist[0].PublicIdentifier);
            //Assert
            Assert.True(result.Count > 0);
            Assert.Equal(result[0].Name,settingslist[0].Name);
            Assert.Equal(result[0].Road,settingslist[0].Road);
        }

        [Fact]
        public async Task CreateManyAsync()
        {
            //Arrange
            collection.AddScoped<IDataBase<Company, Guid>, ModellingCompany>();
            IServiceProvider FirstService = collection.BuildServiceProvider();
            IDataBase<Company, Guid> IDataBaseCompany = FirstService.GetService<IDataBase<Company, Guid>>();

            List<Company> settingslist = JsonConvert.DeserializeObject<List<Company>>(Seetings.Company);

            //Act
            await IDataBaseCompany.CreateAsync(settingslist.ToArray());
            //Assert
            SecurityDbContext context = FirstService.GetService<SecurityDbContext>();
            foreach (Company company in settingslist)
            {
                Assert.NotNull(await context.Companies.FindAsync(company.Id));
            }

        }
        [Fact]
        public async Task CreateAsync()
        {
            //Arrange
            collection.AddScoped<IDataBase<Company, Guid>, ModellingCompany>();
            IServiceProvider FirstService = collection.BuildServiceProvider();
            IDataBase<Company, Guid> IDataBaseCompany = FirstService.GetService<IDataBase<Company, Guid>>();

            List<Company> settingslist = JsonConvert.DeserializeObject<List<Company>>(Seetings.Company);
            Company company = new Company() {Name = "test",City = "vejle",StreetNumber = "a22",Road = "test",ZipCode = 7100};
            //Act
            await IDataBaseCompany.CreateAsync(company);
            //Assert
            SecurityDbContext context = FirstService.GetService<SecurityDbContext>();

            Assert.NotNull(context.Companies.FirstOrDefault(x=>x.Name=="test"));
        }
        [Fact]
        public async Task UpdateAsync()
        {
            //Arrange

            collection.AddScoped<IDataBase<Company, Guid>, ModellingCompany>();
            IServiceProvider FirstService = collection.BuildServiceProvider();
            IDataBase<Company, Guid> IDataBaseCompany = FirstService.GetService<IDataBase<Company, Guid>>();
            SeadDatabase(FirstService, "Company");

            List<Company> settingslist = JsonConvert.DeserializeObject<List<Company>>(Seetings.Company);

            Company company = new Company(){PublicIdentifier = settingslist[0].PublicIdentifier,Name = "Jondow" };
            //Act
            await IDataBaseCompany.UpdateAsync(company);
            //Assert
            SecurityDbContext context = FirstService.GetService<SecurityDbContext>();
            Assert.True(company.Name == context.Companies.FirstOrDefault(x=>x.PublicIdentifier==company.PublicIdentifier).Name);
        }
        [Fact]
        public async Task Delete()
        {
            //Arrange
            collection.AddScoped<IDataBase<Company, Guid>, ModellingCompany>();
            IServiceProvider FirstService = collection.BuildServiceProvider();
            IDataBase<Company, Guid> IDataBaseCompany = FirstService.GetService<IDataBase<Company, Guid>>();
            SeadDatabase(FirstService, "Company");

            List<Company> settingslist = JsonConvert.DeserializeObject<List<Company>>(Seetings.Company);

            //Act
            await IDataBaseCompany.Delete(settingslist[0].PublicIdentifier);
            //Assert
            SecurityDbContext context = FirstService.GetService<SecurityDbContext>();
            Assert.Null(context.Companies.FirstOrDefault(x => x.PublicIdentifier == settingslist[0].PublicIdentifier));
        }
    }
}
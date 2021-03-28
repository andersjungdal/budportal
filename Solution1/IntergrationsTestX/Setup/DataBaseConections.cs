using System;
using System.Collections.Generic;
using DatabaseModelling.DbModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using DatabaseModelling.Context;

namespace IntergrationsTestX.Setup
{
    public class DataBaseConections
    {
        public IServiceCollection collection { get; set; }

        public DataBaseConections()
        {
            collection = new ServiceCollection();
            collection.AddScoped<UserManager<User>>();
            collection.AddDbContext<SecurityDbContext>(options =>
                options.UseInMemoryDatabase("Identity"+Guid.NewGuid()));
            collection.AddIdentity<User, IdentityRole<Guid>>()
                .AddUserManager<UserManager<User>>()
                .AddEntityFrameworkStores<SecurityDbContext>();
        }

        public async void SeadDatabase(IServiceProvider serviceProvider, string SeeadTabel)
        {
            SecurityDbContext context = serviceProvider.GetService<SecurityDbContext>();
            switch (SeeadTabel)
            {
                case "User":
                    context.Users.AddRange(JsonConvert.DeserializeObject<List<User>>(Seetings.User));
                    break;
                case "RawBit":
                    SeadDatabase(serviceProvider, "Areas");
                    SeadDatabase(serviceProvider, "Company");
                    List<RawBid> rawBid = JsonConvert.DeserializeObject<List<RawBid>>(Seetings.RawBid);
                    foreach (var VARIABLE in rawBid)
                    {
                        VARIABLE.XmlString = Seetings.mFRR_regulerkraftbud;
                        VARIABLE.Area = await context.Areas.FirstOrDefaultAsync( x=> x.PublicIdentifier == VARIABLE.Area.PublicIdentifier);
                        VARIABLE.Company = await context.Companies.FirstOrDefaultAsync( x=> x.Id == VARIABLE.Company.Id);
                    }
                    await context.AddRangeAsync(rawBid);
                    break;
                case "Areas":
                    await context.Areas.AddRangeAsync(JsonConvert.DeserializeObject<List<Area>>(Seetings.Aktions));
                    break;
                case "Company":
                    await context.Companies.AddRangeAsync(JsonConvert.DeserializeObject<List<Company>>(Seetings.Company));
                    break;
            }
            await context.SaveChangesAsync();
        }
    }
}
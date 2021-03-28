using System;
using ApiGateway.BusinessLogic;
using ApiGateway.BusinessLogic.Interfaces;
using DatabaseModelling.CRUD;
using DatabaseModelling.CRUD.LockData;
using Functions;
using Interfaces;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using DatabaseModelling.Context;
using ApiGateway.BusinessLogic;

[assembly: FunctionsStartup(typeof(Startup))]

namespace Functions
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddDbContext<SecurityDbContext>(options =>
                options.UseSqlServer("Server=den1.mssql8.gear.host;Initial Catalog=bug;Persist Security Info=False;User ID=bug;Password=Xh12-?bo19Ma;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=60;"));
            builder.Services.AddScoped<IDataBase<DatabaseModelling.DbModels.RawBid, Guid>, ModellingRawBit>();
            builder.Services.AddScoped<ILockRows, LockRows>();
            builder.Services.AddScoped<ILockProductionPlanRows, LockProductionPlanRows>();
            builder.Services.AddScoped<IDataBase<DatabaseModelling.DbModels.LockData.RawBidLock.RawBidColumn, int>, ModellignRawBidColumn>();
            builder.Services.AddScoped<IDataBase<DatabaseModelling.DbModels.LockData.ProductionPlanLock.ProductionPlanColumn, int>, ModellignProductionPlanColumn>();
            builder.Services.AddScoped<IDataBase<DatabaseModelling.DbModels.Area, Guid>, ModellingArea>();
            builder.Services.AddScoped<IDataBase<DatabaseModelling.DbModels.Company, Guid>, ModellingCompany>();
            builder.Services.AddScoped<IDataBase<DatabaseModelling.DbModels.User, Guid>, ModellingUser>();
            builder.Services.AddScoped<IDataBase<DatabaseModelling.DbModels.ProductionPlan, Guid>, ModellingProductionPlan>();
            builder.Services.AddScoped<IDataBase<DatabaseModelling.DbModels.XmlTemplate, Guid>, ModellingXmlTemplate>();

            builder.Services.AddScoped<IXmlReader, XmlReader>();
            builder.Services.AddLogging();
        }
    }
}
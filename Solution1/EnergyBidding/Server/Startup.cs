using System;
using System.Net.Http;
using DatabaseModelling.Context;
using DatabaseModelling.CRUD;
using DatabaseModelling.DbModels;
using EnergyBidding.Server.Authorization;
using EnergyBidding.Server.Authorization.AuthMiddleware;
using EnergyBidding.Server.Externeal.Conections;
using Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing.Matching;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileSystemGlobbing.Internal.Patterns;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

namespace EnergyBidding.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            ////////////////////////////////////////// SecurityDb

            //services.AddDbContext<SecurityDbContext>(options =>
            //    options.UseSqlServer("Server=tcp:praktik-energinet.database.windows.net,1433;Initial Catalog=Bug;Persist Security Info=False;User ID=Bug;Password=Volvo2468;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=60;"));
            services.AddDbContext<SecurityDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("RawDatabase")));
            services.AddIdentity<User, IdentityRole<Guid>>()
                .AddEntityFrameworkStores<SecurityDbContext>();
            
            services.AddScoped<IDataBase<User, Guid>, ModellingUser>();
            services.AddScoped<IDataBase<Company, Guid>, ModellingCompany>();
            services.AddScoped<IDataBase<RawBid, Guid>, ModellingRawBit>();
            services.AddScoped<IDataBase<Area, Guid>, ModellingArea>();

            services.AddScoped<FunctionsProxy>();
            services.AddSingleton<HttpClient>(new HttpClient()
            {
                BaseAddress = new Uri(
    #if RELEASE
                    Configuration.GetValue<string>("FunctionURL")
    #else
                    "http://localhost:7071/api/"
    #endif
                )
            });

            //Authorization
            ////////////////////////////////////////

            services.AddControllersWithViews();
            services.AddRazorPages();
            
            ///////////////////////////////////////
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Application API", Version = "v1" });

                //c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                //{
                //    Description = @"JWT Authorization Header using the Bearer scheme.
                //    Enter 'Bearer' [space] {Token}.
                //    Example: 'Bearer 12345abcdef'",
                //    Name = "Authorization",
                //    In = ParameterLocation.Header,
                //    Type = SecuritySchemeType.ApiKey,
                //    Scheme = "Bearer"
                //});

                //c.AddSecurityRequirement(new OpenApiSecurityRequirement
                //{
                //    {
                //        new OpenApiSecurityScheme
                //        {
                //            Reference = new OpenApiReference
                //            {
                //                Type = ReferenceType.SecurityScheme,
                //                PublicIdentifier = "Bearer"
                //            },
                //            Scheme = "oauth2",
                //            Name = "Bearer",
                //            In = ParameterLocation.Header
                //        },
                //        new List<string>()
                //    }
                //});
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Resource API V1");
            });

            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            
            app.UseRouting();

            app.UseMiddleware<MockAuthenticationMiddelware>();
            app.UseMiddleware<MockCompanyMiddelware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
            });

        }
    }
}

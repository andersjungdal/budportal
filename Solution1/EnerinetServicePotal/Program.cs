using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BlazorBuisnessLogic.Net5;
using BlazorBuisnessLogic.Net5.ApiConnections;
using BlazorBuisnessLogic.Net5.ApiConnections.ExternApi;
using BlazorBuisnessLogic.Net5.Interfaces;
using Syncfusion.Blazor;

namespace EnerinetServicePotal
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MzY2ODI1QDMxMzgyZTMzMmUzMEdoKzFJWUZZaEJjaEJQZE5hYlpoZVZWZnc0QzBIckpmVVdYRlBzaklyMXM9;" +
                                                                           "MzY2ODI2QDMxMzgyZTMzMmUzMG9YLzNQYTdFdjZFY1BuM1Vtak51R1NDalRWZ0I3UlNGK25LUkNTUzU3SUE9");
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            HttpClient client = new HttpClient();
            client.BaseAddress =
#if DEBUG
                new Uri("http://localhost:7071/");
#else
            new Uri("https://hovedopgavebackend.azurewebsites.net");
#endif
            //builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddSingleton(client);

            builder.Services.AddScoped<AreasApiConnections>();
            builder.Services.AddScoped<CompanyApiConnection>();
            builder.Services.AddScoped<UserApiConnection>();
            builder.Services.AddScoped<RawBidApiConnection>();
            builder.Services.AddScoped<XmlTemplateApiConnection>();
            builder.Services.AddScoped<ProductionPlanApiConnection>();
            builder.Services.AddTransient<IApiErrorMessage, ApiErrorMessage>();
            builder.Services.AddSingleton<StateHolder>();
            builder.Services.AddSyncfusionBlazor();
            builder.Services.AddScoped<ProductionPlanColumnId>();

            await builder.Build().RunAsync();
        }
    }
}

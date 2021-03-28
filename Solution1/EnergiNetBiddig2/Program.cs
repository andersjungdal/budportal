using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using BlazorBusinessLogic;
using BlazorBusinessLogic.ApiConnections;
using BlazorBusinessLogic.ApiConnections.ExternApi;
using BlazorBusinessLogic.Interfaces;
using BlazorBusinessLogic.Models.General;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Syncfusion.Blazor;

namespace EnergiNetBiddig2
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");
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

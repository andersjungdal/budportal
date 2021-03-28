using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using BlazorBusinessLogic;
using BlazorBusinessLogic.ApiConnections;
using BlazorBusinessLogic.Interfaces;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Syncfusion.Blazor;

namespace EnergyBidding.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");


            builder.Services.AddSingleton(sp => new HttpClient { BaseAddress =
#if DEBUG
                new Uri(builder.HostEnvironment.BaseAddress)
                //new Uri("http://localhost:7071")
#else
                new Uri(builder.HostEnvironment.BaseAddress)
                //new Uri("https://energinetpraktikportal.azurewebsites.net/")
#endif
            });

            builder.Services.AddScoped<AreasApiConnections>();
            builder.Services.AddScoped<CompanyApiConnection>();
            builder.Services.AddScoped<UserApiConnection>();
            builder.Services.AddScoped<RawBidApiConnection>();
            builder.Services.AddTransient<IApiErrorMessage, ApiErrorMessage>();
            builder.Services.AddSingleton<StateHolder>();
            builder.Services.AddSyncfusionBlazor();
            await builder.Build().RunAsync();
        }
    }
}

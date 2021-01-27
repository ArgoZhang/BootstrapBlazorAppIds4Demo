using BootstrapBlazorAppDemo.Shared.Data;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace BootstrapBlazorAppDemo.WebAssembly {
    /// <summary>
    /// 
    /// </summary>
    public class Program {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static async Task Main(string[] args) {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            builder.RootComponents.Add<App>("app");

            builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            // 增加 BootstrapBlazor 组件
            builder.Services.AddBootstrapBlazor();

            builder.Services.AddSingleton<WeatherForecastService>();


            //builder.Services.AddApiAuthorization();
            builder.Services.AddOidcAuthentication(options =>
            {
                options.ProviderOptions.Authority = "https://localhost:5001";
                options.ProviderOptions.ClientId = "BlazorApp1.Client";
                options.ProviderOptions.DefaultScopes.Add("BlazorApp1.ServerAPI");
                options.ProviderOptions.RedirectUri = "https://localhost:44367/authentication/login-callback";
                options.ProviderOptions.PostLogoutRedirectUri = "https://localhost:44367/authentication/logout-callback";
                options.ProviderOptions.ResponseType = "code";
            });
       
            var host = builder.Build();
            await host.RunAsync();
        }
    }
}

using System;
using System.Net.Http;
using System.Threading.Tasks;
using BlazorPro.BlazorSize;
using BlazorStrap;
using BlazorStyled;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace blazor_tesourofieis {
    public class Program {
        public static async Task Main(string[] args) {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");
            builder.Services.AddScoped<State>();

            builder.Services.AddBlazorStyled();
            builder.Services.AddBootstrapCss();
            builder.Services.AddResizeListener(options => {
                options.ReportRate = 300;
                options.EnableLogging = true;
                options.SuppressInitEvent = false;
            });

            builder.Services.AddSingleton(new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            await builder.Build().RunAsync();
        }
    }
}
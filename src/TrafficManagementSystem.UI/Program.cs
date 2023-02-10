using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Toolbelt.Blazor.Extensions.DependencyInjection;
using TrafficManagementSystem.UI;
using TrafficManagementSystem.UI.Infrastructure;
using TrafficManagementSystem.UI.Infrastructure.Authentication;
using TrafficManagementSystem.UI.Infrastructure.Managers;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Logging.SetMinimumLevel(LogLevel.Warning);
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.AddClientServices();
builder.Services.AddTransient<AuthenticationHeaderHandler>()
                .AddScoped(sp => sp
                    .GetRequiredService<IHttpClientFactory>()
                    .CreateClient("TMS.API").EnableIntercept(sp))
                .AddHttpClient("TMS.API", client => client.BaseAddress = new Uri(builder.Configuration["ApiUrl"] + "/api/"))
                .AddHttpMessageHandler<AuthenticationHeaderHandler>();
builder.Services.AddHttpClientInterceptor();


await builder.Build().RunAsync();

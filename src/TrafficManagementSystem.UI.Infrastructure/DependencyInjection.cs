using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor.Services;
using TrafficManagementSystem.UI.Infrastructure.Authentication;
using TrafficManagementSystem.UI.Infrastructure.Extensions;
using TrafficManagementSystem.UI.Infrastructure.Managers;

namespace TrafficManagementSystem.UI.Infrastructure
{
    public static class DependencyInjection
    {
        public static WebAssemblyHostBuilder AddClientServices(this WebAssemblyHostBuilder builder)
        {
            builder.Services
                .AddOptions()
                .AddAuthorizationCore()
                .AddMudServices()
                .AddBlazoredLocalStorage()
                .AddScoped<AuthenticationStateProvider, AppAuthenticationStateProvider>()
                .AddTransient<AppAuthenticationStateProvider>()
                .AddSingleton<AppStateManager>()
                .AddTransient<ApiInterceptor>()
                .AddManagers();

            return builder;
        }

        public static IServiceCollection AddManagers(this IServiceCollection services)
        {
            services.AddTransient<IAuthenticationManager, AuthenticationManager>();
            services.AddTransient<IDriverManager, DriverManager>();
            services.AddTransient<IVehicleManager, VehicleManager>();
            services.AddTransient<IOffenceManager, OffenceManager>();
            services.AddTransient<IOffenceTypeManager, OffenceTypeManager>();
            //services.AddSingleton<ApplicationStateManager>();

            return services;
        }
    }

}

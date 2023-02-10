using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TrafficManagementSystem.Application.Interfaces.Services;
using TrafficManagementSystem.Application.Services;

namespace TrafficManagementSystem.Application
{
    public static class DependencyInjection
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<IDriverService, DriverService>();
            services.AddScoped<IVehicleService, VehicleService>();
            services.AddScoped<IOffenceService, OffenceService>();
            services.AddScoped<IOffenceTypeService, OffenceTypeService>();
        }
    }
}

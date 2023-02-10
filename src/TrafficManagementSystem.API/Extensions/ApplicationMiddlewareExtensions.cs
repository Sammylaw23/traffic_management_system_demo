using TrafficManagementSystem.API.Middlewares;

namespace TrafficManagementSystem.API.Extensions
{
    public static class ApplicationMiddlewareExtensions
    {
        public static void UseApiErrorHandler(this IApplicationBuilder app) =>
           app.UseMiddleware<ApiErrorHandler>();
    }
}

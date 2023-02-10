using System;

namespace TrafficManagementSystem.UI.Infrastructure.Extensions
{
    public static class ExceptionExtensions
    {
        public static string Format(this Exception ex) =>
            $"Error:ProcessError - Type: {ex.GetType()} Message: {ex.Message}";
    }
}

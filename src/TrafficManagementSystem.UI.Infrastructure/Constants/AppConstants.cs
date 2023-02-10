using MudBlazor;

namespace TrafficManagementSystem.UI.Infrastructure.Constants
{
    public static class AppConstants
    {
        public static class Designs
        {
            public const Variant InputTextVariant = Variant.Text;
            public const Variant ButtonVariant = Variant.Filled;
        }

        public static class SignalR
        {
            public const string HubUrl = "";
        }
        public static class Storage
        {
            public const string AuthToken = "itknxyz";
            public const string RefreshToken = "";
        }

        public static class ErrorMessages
        {
            public const string SessionTimeout = "Session timeout";
        }
    }

    public static class Endpoints
    {
        public static class Users
        {
            private const string _base = "users";
            public const string Login = _base + "/login";
        }

        public static class DriverEndpoints
        {
            private const string _base = "drivers";

            public const string GetDrivers = _base;

            public const string AddDriver = _base;

            public static string DeleteDriver(Guid id) =>
                $"{_base}/{id}";
            public static string GetDriver(Guid id) =>
                $"{_base}/{id}";
        }
        public static class VehicleEndpoints
        {
            private const string _base = "vehicles";

            public const string GetVehicles = _base;

            public const string AddVehicle = _base;

            public static string DeleteVehicle(Guid id) =>
                $"{_base}/{id}";
            public static string GetVehicle(Guid id) =>
                $"{_base}/{id}";
        }
        public static class OffenceEndpoints
        {
            private const string _base = "offences";

            public const string GetOffences = _base;

            public const string AddOffence = _base;

            public static string DeleteOffence(Guid id) =>
                $"{_base}/{id}";
            public static string GetOffence(Guid id) =>
                $"{_base}/{id}";
        }
        public static class OffenceTypeEndpoints
        {
            private const string _base = "offencetypes";

            public const string GetOffenceTypes = _base;

            public const string AddOffenceType = _base;

            public static string DeleteOffenceType(Guid id) =>
                $"{_base}/{id}";
            public static string GetOffenceType(Guid id) =>
                $"{_base}/{id}";
        }

    }
}

using System.Globalization;

namespace TrafficManagementSystem.Application.Exceptions
{
    public class ApiException : Exception
    {
        public ApiException() : base() { }

        public ApiException(string message) : base(message) { }

        public ApiException(string message, int statusCode) : base(message) => StatusCode = statusCode;

        public ApiException(string message, params object[] args)
            : base(string.Format(CultureInfo.CurrentCulture, message, args))
        {
        }

        public int? StatusCode { get; set; }
    }
}

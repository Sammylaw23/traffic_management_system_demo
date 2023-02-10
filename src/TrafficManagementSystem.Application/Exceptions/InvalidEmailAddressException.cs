namespace TrafficManagementSystem.Application.Exceptions
{
    public class InvalidEmailAddressException : Exception
    {
        public InvalidEmailAddressException(string email) : base($"Invalid email address '{email}'.") { }
    }
}

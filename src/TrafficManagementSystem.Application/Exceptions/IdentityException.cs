namespace TrafficManagementSystem.Application.Exceptions
{
    public class IdentityException : Exception
    {
        public IdentityException() : base() { }


        public IdentityException(IEnumerable<string> errors) : base(string.Join("|", errors))
        {
        }


    }
}

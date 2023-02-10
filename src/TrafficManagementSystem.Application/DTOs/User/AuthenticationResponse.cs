namespace TrafficManagementSystem.Application.DTOs.User
{
    public class AuthenticationResponse
    {
        //public string UserName { get; set; }
        public string Email { get; set; }
        //public bool IsVerified { get; set; }
        public string JWToken { get; set; }
        public string DisplayName { get; set; }

        //public string RefreshToken { get; set; }
        //public string Group { get; set; }
    }
}

namespace TrafficManagementSystem.Domain.Settings
{
    public class JWTSettings
    {
        public string SecretKey { get; set; }
        public int DurationInMinutes { get; set; }
    }
}

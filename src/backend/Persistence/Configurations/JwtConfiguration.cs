namespace Persistence.Configurations
{
    public class JwtConfiguration
    {
        public string? Audience { get; set; }
        public string? Authority { get; set; }
        public string? SecurityKey { get; set; }
        public int ExpriresHours { get; set; }
        public string? Issuer { get; set; }
    }
}


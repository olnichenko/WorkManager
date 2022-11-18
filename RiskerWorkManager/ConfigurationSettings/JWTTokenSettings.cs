namespace RiskerWorkManager.ConfigurationSettings
{
    public class JWTTokenSettings
    {
        public static string SectionName = "JWTTokenSettings";
        public string SecretKey { get; set; }
        public string Issuer { get; set; }
        public int ValidTokenHours { get; set; }
    }
}

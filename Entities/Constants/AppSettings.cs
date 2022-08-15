namespace Entities.Constants
{
    public class AppSettings
    {
        public string AppKey { get; set; }
        public int RefreshTokenTTL { get; set; }
        public int VerificationTTL { get; set; }
        public string Secret { get; set; }
        public string DominName { get; set; }
    }
}

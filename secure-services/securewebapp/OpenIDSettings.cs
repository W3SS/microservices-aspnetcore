namespace SecureWebApp
{
    // ReSharper disable once InconsistentNaming
    public class OpenIDSettings
    {
        public string Domain { get; set; }
        public string CallbackUrl { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }
}
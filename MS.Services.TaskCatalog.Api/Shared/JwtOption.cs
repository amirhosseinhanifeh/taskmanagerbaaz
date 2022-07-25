namespace MS.Services.TaskCatalog.Api.Shared
{
    public class JwtOption
    {
        public string Authority { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string ResponseType { get; set; }
        public string secretKey { get; set; }
        public string audience { get; set; }
        public string validIssuer { get; set; }
        public bool validateAudience { get; set; }
        public bool validateIssuer { get; set; }
        public bool validateLifetime { get; set; }
    }
}

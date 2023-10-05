namespace ShopSneaker.Identity.Infrastructure.Helper
{
    public class JwtTokenOption
    {
        public const string JwtTokenOptionImportConfig = "JwtTokenOption";
        public string SecurityKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}

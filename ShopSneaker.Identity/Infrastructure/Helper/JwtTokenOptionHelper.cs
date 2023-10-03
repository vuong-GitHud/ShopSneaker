namespace ShopSneaker.Identity.Infrastructure.Helper
{
    public class JwtTokenOptionHelper
    {
        public static JwtTokenOption AppSettingOption => GetAppSettingOption() != null ? GetAppSettingOption() : null;

        private static JwtTokenOption GetAppSettingOption()
        {
            return new JwtTokenOption
            {
                Audience = "IdentityServer",
                SecurityKey = "sd4OIfg+KJH9NZDy0t8W3TcNekrF+2d/1sFnWG4HnV8TZY30iTOdtVWJG8abWvB1GlOgJuQZdcF2Luqm/hccMw==",
                Issuer = "IdentityServer",
            };
        }
    }
}

namespace ShopSneaker.Identity.Infrastructure.Helper
{
    public class JwtTokenBuilderHelper
    {
        private static JwtTokenBuilder _jwtSecurityKeyBuilder;

        public static JwtTokenBuilder DefaultBuilder
        {
            get => _jwtSecurityKeyBuilder != null ? _jwtSecurityKeyBuilder : CreateDefaultBuilder();
            set => _jwtSecurityKeyBuilder = value;
        }

        private static JwtTokenBuilder CreateDefaultBuilder() => new JwtTokenBuilder(JwtTokenOptionHelper.AppSettingOption);
    }
}

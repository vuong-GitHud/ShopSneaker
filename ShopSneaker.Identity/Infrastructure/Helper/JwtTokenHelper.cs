﻿using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using ShopSneaker.Identity.Model;
using ShopSneaker.Identity.Infrastructure.Constant;

namespace ShopSneaker.Identity.Infrastructure.Helper
{
    public class JwtTokenHelper
    {
        private static JwtTokenBuilder _tokenBuilder;
        private static HttpContext Current => IdentityInjectionHelper.GetService<IHttpContextAccessor>().HttpContext;
        public static JwtTokenBuilder DefaultBuilder
        {
            get => _tokenBuilder != null ? _tokenBuilder : JwtTokenBuilderHelper.DefaultBuilder;
            set => _tokenBuilder = value;
        }
        #region method
        public static List<Claim> GetUserClaims(LoginVm authModel)
        {
            return new List<Claim>()
                {
                    new Claim(ClaimConstant.Name, authModel.Name ?? string.Empty),
                    new Claim(ClaimConstant.Email, authModel.Email ?? string.Empty),
                    new Claim(ClaimConstant.UserId, authModel.UserId ?? string.Empty),
                    new Claim(ClaimConstant.RoleId, authModel.RoleId ?? string.Empty),
                    new Claim(ClaimConstant.Role, authModel.UserRole ?? string.Empty),
                    new Claim(ClaimConstant.AccessToken, authModel.AccessToken ?? string.Empty),
                    new Claim(ClaimConstant.RefreshToken, authModel.RefreshToken ?? string.Empty),
                    new Claim(ClaimConstant.AccountType, authModel.AccountTypeId ?? string.Empty),
                    new Claim(ClaimConstant.TokenEffectiveDate, authModel.TokenEffectiveDate ?? string.Empty),
                    new Claim(ClaimConstant.TokenEffectiveTimeStick, authModel.TokenEffectiveTimeStick.ToString()),
                    new Claim(ClaimConstant.FirstName, authModel.FirstName ?? string.Empty),
                    new Claim(ClaimConstant.LastName, authModel.LastName ?? string.Empty),
                    new Claim(ClaimConstant.PhoneNumber, authModel.PhoneNumber ?? string.Empty),
                    new Claim(ClaimConstant.Country, authModel.Country ?? string.Empty),
                    new Claim(ClaimConstant.City, authModel.City ?? string.Empty),
                };
        }

        public static LoginVm GetSignInProfile(ClaimsPrincipal user)
        {
            return new LoginVm()
            {
                AccountTypeId = GetClaimValue(user, ClaimConstant.AccountType) ?? string.Empty,
                UserId = GetClaimValue(user, ClaimConstant.UserId) ?? string.Empty,
                RoleId = GetClaimValue(user, ClaimConstant.RoleId) ?? string.Empty,
                Name = GetClaimValue(user, ClaimConstant.Name) ?? string.Empty,
                Email = GetClaimValue(user, ClaimConstant.Email) ?? string.Empty,
                UserRole = GetClaimValue(user, ClaimConstant.Role) ?? string.Empty,
                AccessToken = GetClaimValue(user, ClaimConstant.AccessToken) ?? string.Empty,
                RefreshToken = GetClaimValue(user, ClaimConstant.RefreshToken) ?? string.Empty,
                TokenEffectiveDate = GetClaimValue(user, ClaimConstant.TokenEffectiveDate) ?? string.Empty,
                TokenEffectiveTimeStick = GetClaimValue(user, ClaimConstant.TokenEffectiveTimeStick) ?? string.Empty,
                Expired = GetClaimValue(user, ClaimConstant.TokenExpired) ?? string.Empty,
                FirstName = GetClaimValue(user, ClaimConstant.FirstName) ?? string.Empty,
                LastName = GetClaimValue(user, ClaimConstant.LastName) ?? string.Empty,
                PhoneNumber = GetClaimValue(user, ClaimConstant.PhoneNumber) ?? string.Empty,
                Country = GetClaimValue(user, ClaimConstant.Country) ?? string.Empty,
                City = GetClaimValue(user, ClaimConstant.City) ?? string.Empty,
            };
        }

        public static string GenerateJwtToken(LoginVm authModel, int expiredInMinute, JwtTokenBuilder tokenBuilder = null)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            tokenBuilder = tokenBuilder != null ? tokenBuilder : DefaultBuilder;
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(GetUserClaims(authModel)),
                Expires = DateTime.UtcNow.AddMinutes(expiredInMinute),
                SigningCredentials = DefaultBuilder.GetSigningCredentials(),
                Audience = DefaultBuilder.GetAudience(),
                Issuer = DefaultBuilder.GetIssuer(),
            };
            var strtoken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(strtoken);
            return token;
        }

        public static LoginVm GenerateJwtTokenModel(LoginVm authModel, int expiredInMinute, JwtTokenBuilder tokenBuilder = null)
        {
            var jwtToken = GenerateJwtToken(authModel, expiredInMinute, tokenBuilder);
            var result = DecodeJwtToken(jwtToken);
            result.AccessToken = jwtToken;
            return result;
        }

        public static LoginVm DecodeJwtToken(string token)
        {
            var principal = AuthenticateToken(token);
            if (principal == null)
                return null;
            return GetSignInProfile(principal);
        }

        public static ClaimsPrincipal AuthenticateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = DefaultBuilder.GetValidationParameters();

            SecurityToken validatedToken;
            var principal = tokenHandler.ValidateToken(token, validationParameters, out validatedToken);
            return principal;
        }

        public static string GetClaimValue(ClaimsPrincipal principal, string claimType)
        {
            ThrowIfPrincipalNull(principal);
            return principal.Identities.FirstOrDefault().FindFirst(claimType)?.Value;
        }

        private static void ThrowIfIdentityNull(ClaimsIdentity claimsIdentity)
        {
            if (claimsIdentity == null)
                throw new Exception("ClaimsIdentity is null.");
        }
        private static void ThrowIfPrincipalNull(ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new Exception("ClaimsPrincipal is null.");
            ThrowIfIdentityNull(principal.Identities.FirstOrDefault());
        }

        public static void RemoveClaimValue(List<string> lstkey = null)
        {
            var identity = Current?.User?.Identity as ClaimsIdentity;
            var claim = identity.Claims.Where(x => lstkey.Contains(x.Type)).ToList();
            foreach (var item in claim)
                identity.RemoveClaim(item);
            Current.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(Current.User.Identity));
        }
        #endregion method
    }
}

using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.V5.Pages.Account.Internal;
using ShopSneaker.Admin.Models;
using ShopSneaker.Areas.Identity.Data;
using ShopSneaker.Data.Entities;

namespace ShopSneaker.Admin.Infrastructure;

public class UserManagerService : IUserManagerService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public UserManagerService(ShopSneakerContext context, UserManager<AppUser> userManager, IHttpContextAccessor httpContextAccessor)
    {
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
    }
    
    public async Task<bool> LoginAsync(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if (user == null)
        {
            return false;
        }

        var checkPassword = await _userManager.CheckPasswordAsync(user, password);
        return checkPassword;
    }

    public async Task SignInAsync(AppUser user, bool remember = false)
    {
        if (user == null)
        {
            throw new AggregateException(nameof(user));
        }

        var claims = new List<Claim>
        {
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.Name, user.Email)
        };

        var userIdentity = new ClaimsIdentity(claims, "Cookie");
        var userPrincipal = new ClaimsPrincipal(userIdentity);

        var authenticationProperties = new AuthenticationProperties
        {
            IsPersistent = remember,
            IssuedUtc = DateTime.UtcNow,
            ExpiresUtc = DateTime.UtcNow.AddHours(60)
        };

        if (_httpContextAccessor.HttpContext != null)
        {
            await _httpContextAccessor.HttpContext.SignInAsync("Authentication", userPrincipal,
                authenticationProperties);
        }
    }
}
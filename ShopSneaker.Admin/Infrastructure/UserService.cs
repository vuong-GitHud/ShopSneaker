using Microsoft.AspNetCore.Identity;
using ShopSneaker.Data.Entities;

namespace ShopSneaker.Admin.Infrastructure;

public class UserService : IUserService
{
    private readonly UserManager<AppUser> _userManager;

    public UserService(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }


    public async Task<AppUser?> GetUserByEmail(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        return user;
    }
}
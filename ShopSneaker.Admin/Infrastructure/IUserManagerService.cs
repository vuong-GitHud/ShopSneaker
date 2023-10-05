using Microsoft.AspNetCore.Identity.UI.V5.Pages.Account.Internal;
using ShopSneaker.Admin.Models;
using ShopSneaker.Data.Entities;

namespace ShopSneaker.Admin.Infrastructure;

public interface IUserManagerService
{
    Task<bool> LoginAsync(string email, string password);
    
    Task SignInAsync(AppUser user, bool remember);

}
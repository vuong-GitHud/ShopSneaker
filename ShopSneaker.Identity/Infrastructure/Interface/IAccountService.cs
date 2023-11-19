using ShopSneaker.Identity.Model;

namespace ShopSneaker.Identity.Infrastructure.Interface;

public interface IAccountService
{
    Task<bool> CheckEmailExist(string email);
    
    Task<UserProfileViewModel> GetUserProfile(string userId);
}
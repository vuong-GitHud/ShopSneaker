using ShopSneaker.Models;

namespace ShopSneaker.Infacture.interfaces;

public interface IAuthenApi
{
    Task<AuthenViewModel> Authenticate(AuthenVm request);
    
    Task<bool> CheckEmailExist(string email);

    Task<RegisterViewModel> Register(RegisterModel model);

    Task<UserProfileViewModel> GetInfo(string userId);
}
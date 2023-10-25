using ShopSneaker.Models;

namespace ShopSneaker.Infacture.interfaces;

public interface IAuthenApi
{
    Task<AuthenViewModel> Authenticate(AuthenVm request);
}
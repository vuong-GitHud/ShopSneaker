using ShopSneaker.AdminMVC.Model;

namespace ShopSneaker.AdminMVC.Infrastructure
{
    public interface IAuthenAPI
    {
        Task<AuthenViewModel> Authenticate(AuthenModel request);

    }
}

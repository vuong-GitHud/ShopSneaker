using ShopSneaker.Identity.Model;

namespace ShopSneaker.Identity.Infrastructure.Interface
{
    public interface IUserService
    {
        Task<LoginVm> LoginByEmail(string email, string password);

        Task<RegisterViewModel> Register(RegisterRequest? model);
    }
}

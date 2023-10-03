using ShopSneaker.Data.Entities;

namespace ShopSneaker.Admin.Infrastructure;

public interface IUserService
{
    Task<AppUser?> GetUserByEmail(string email);

}
using ShopSneaker.Models;

namespace ShopSneaker.Infacture.Services;

public interface ICartService
{
    Task<List<CartViewModel>> GetCart(string userId);
    
}
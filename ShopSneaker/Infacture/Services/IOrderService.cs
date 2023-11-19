using ShopSneaker.Data.Entities;
using ShopSneaker.Models;

namespace ShopSneaker.Infacture.Services;

public interface IOrderService
{
    Task<List<Order>> GetListOrder(string userId);

    Task<OrderDetailViewModel> OrderDetail(int id);

    Task<bool> CancleOrder(int id);
}
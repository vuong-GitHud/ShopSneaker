using ShopSneaker.AdminMVC.Model.Dashboard;

namespace ShopSneaker.AdminMVC.Infrastructure.Services;

public interface IOrderService
{
    Task<OrderViewModel> GetOrder();

    Task<DashBoardViewModel> GetDashBoard();
}
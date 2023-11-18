using ShopSneaker.AdminMVC.Model.Dashboard;
using ShopSneaker.Areas.Identity.Data;

namespace ShopSneaker.AdminMVC.Infrastructure.Services;

public class OrderService : IOrderService
{
    private readonly ShopSneakerContext _context;

    public OrderService(ShopSneakerContext context)
    {
        _context = context;
    }

    public async Task<OrderViewModel> GetOrder()
    {
        var order = _context.Orders.OrderByDescending(o => o.OrderDate);
        if (order.Count() < 1)
        {
            return new OrderViewModel();
        }
        var monthly = DateTime.Now.AddDays(-30);
        var now = DateTime.Now;
        var currentYear = now.Year;
        var totalMonth = order.Where(o => o.OrderDate >= monthly && o.OrderDate <= now);
        var revenue = order.Where(o => o.isPayment && o.OrderDate >= monthly && o.OrderDate <= now);

        return new OrderViewModel()
        {
            TotalOrderMonth = totalMonth.Count(),
            TotalRevenue = revenue.Sum(r => r.Amount),
            Orders = order.Take(5).ToList(),
        };
    }
}
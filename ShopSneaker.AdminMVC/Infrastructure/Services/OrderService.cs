using Microsoft.EntityFrameworkCore;
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
    //Total Orders Monthly
    //Revenue Monthly

    public async Task<OrderViewModel> GetOrder()
    {
        var order = _context.Orders.OrderByDescending(o => o.OrderDate);
        if (order.Count() < 1)
        {
            return new OrderViewModel();
        }
        var monthly = DateTime.Now.AddDays(-30);
        var day = DateTime.Now.AddDays(-1);
        var now = DateTime.Now;
        var currentYear = now.Year;
        var totalMonth = order.Where(o => o.OrderDate >= monthly && o.OrderDate <= now);
        var totalDay = order.Where(o => o.OrderDate >= day && o.OrderDate <= now);
        var revenue = order.Where(o => o.isPayment && !o.isCancle && o.OrderDate >= monthly && o.OrderDate <= now);
        var revenueDay = order.Where(o => o.isPayment && !o.isCancle && o.OrderDate >= day && o.OrderDate <= now);

        return new OrderViewModel()
        {
            TotalOrderMonth = totalMonth.Count(),
            TotalRevenue = revenue.Sum(r => r.Amount),
            Orders = order.Take(5).ToList(),
        };
    }
    /// Sales Statistics admin

       public async Task<DashBoardViewModel> GetDashBoard()
    {
        var revenueMonthly = await _context.Orders.Where(o => o.OrderDate.Year == DateTime.Now.Year && o.isPayment)
            .GroupBy(o => o.OrderDate.Month).Select(g => new RevenueMonthly() //month/day/
            {
                X = g.Key,
                Y = g.Sum(o => o.Amount)
            }).ToListAsync();

        //Order Statistics
        var monthlyRevenue = DateTime.Now.AddDays(-30);
        var now = DateTime.Now;
        var query = from o in _context.Orders
            join od in _context.OrderDetails on o.Id equals od.OrderId into ood
            from od in ood.DefaultIfEmpty()
            join p in _context.Products on od.ProductId equals p.Id into odp
            from p in odp.DefaultIfEmpty()
            join c in _context.Categories on p.CategoryId equals c.Id into pc
            from c in pc.DefaultIfEmpty()
            where o.isPayment == true && o.OrderDate >= monthlyRevenue && o.OrderDate <= now
            select new { p.Id , c.Name, od.Quantity};
        var total = await query.ToListAsync();
        var quantity = total.Sum(x => x.Quantity);


        var products = await query.GroupBy(p => p.Name).Select(d => new PercentProduct()
        {
            Percent = (d.Sum(d => d.Quantity) * 100 / quantity),
            Category = d.Key,
        }).ToListAsync();
        
        return new DashBoardViewModel()
        {
            RevenueMonthlies = revenueMonthly,
            TotalProduct = quantity,
            PercentProducts = products
        };
    }
}
using ShopSneaker.Data.Entities;

namespace ShopSneaker.AdminMVC.Model.Dashboard;

public class OrderViewModel
{
    public int TotalOrderMonth { get; set; }

    public decimal TotalRevenue { get; set; }
    
    public List<Order> Orders { get; set; }
}
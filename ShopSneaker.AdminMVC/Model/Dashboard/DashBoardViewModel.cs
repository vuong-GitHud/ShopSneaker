using Microsoft.CodeAnalysis.Operations;

namespace ShopSneaker.AdminMVC.Model.Dashboard;

public class DashBoardViewModel
{
    public List<RevenueMonthly> RevenueMonthlies { get; set; } = new List<RevenueMonthly>();
    public int TotalProduct { get; set; }
    
    public List<PercentProduct> PercentProducts { get; set; }
}

public class RevenueMonthly
{
    public int X { get; set; }
    public decimal Y { get; set; }
}

public class PercentProduct
{
    public decimal Percent { get; set; }
    public string Category { get; set; }
}
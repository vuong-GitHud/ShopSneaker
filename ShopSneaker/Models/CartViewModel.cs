namespace ShopSneaker.Models;

public class CartViewModel
{
    public string ProductName { get; set; }

    public string ProductImg { get; set; }

    public decimal Price { get; set; }

    public int Quantity { get; set; }

    public decimal TotalPrice { get; set; }
}
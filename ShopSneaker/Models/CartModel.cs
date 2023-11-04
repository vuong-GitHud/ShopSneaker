namespace ShopSneaker.Models;

public class CartModel
{
    public int ProductId { get; set; }

    public string CustomerId { get; set; } = null!;
    
    public int Quantity { get; set; }
}
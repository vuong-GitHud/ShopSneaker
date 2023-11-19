namespace ShopSneaker.Models;

public class AddToCartResult
{
    public string? ErrorCode { get; set; }

    public string? ErrorMessage { get; set; }

    public bool Success { get; set; }

    public int TotalItems { get; set; }
}
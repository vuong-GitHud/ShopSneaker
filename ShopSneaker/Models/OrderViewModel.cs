namespace ShopSneaker.Models;

public class OrderViewModel
{
    public string? Email { get; set; }

    public string? UserId { get; set; }

    public string? Address { get; set; }

    public string? City { get; set; }

    public string? District { get; set; }

    public string? Ward { get; set; }

    public string? Fullname { get; set; }

    public int PhoneNumber { get; set; }

    public decimal Amount { get; set; }

    public List<CartViewModel> Carts { get; set; } = new List<CartViewModel>();
}
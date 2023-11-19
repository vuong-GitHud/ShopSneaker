namespace ShopSneaker.Models;

public class PaymentRequest
{
    public string TmnCode { get; set; }

    public decimal Amount { get; set; }

    public string OrderInfo { get; set; }

    public int OrderId { get; set; }

    public string ReturnUrl { get; set; }

    public string CancleUrl { get; set; }
}
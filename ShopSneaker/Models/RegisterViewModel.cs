namespace ShopSneaker.Models;

public class RegisterViewModel
{
    public bool IsSuccess { get; set; }
    
    public string ConfirmEmailToken { get; set; }
    
    public string UserId { get; set; }
}
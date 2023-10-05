namespace ShopSneaker.Admin.Models;

public class LoginVm
{
    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public bool RememberMe { get; set; } = false;
    
    public string? RequestPath { get; set; }
}
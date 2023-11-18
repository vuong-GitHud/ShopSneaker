using System.ComponentModel.DataAnnotations;

namespace ShopSneaker.Identity.Model;

public class RegisterRequest
{
    [Required]
    public string Email { get; set; }

    [Required]
    [MinLength(8, ErrorMessage = "Password length at least 8 characters")]
    public string Password { get; set; }

    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string PasswordConfirm { get; set; }

    public int RoleId { get; set; }
    
    public string? FullName { get; set; }
    
    public string? FirstName { get; set; }
    
    public string? LastName { get; set; }
}
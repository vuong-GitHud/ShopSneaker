namespace ShopSneaker.Models;

public class UserProfileViewModel
{
    public string UserId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? FullName { get; set; }

    public string? Email { get; set; }

    public DateTime? DOB { get; set; }

    public string? Street { get; set; }

    public string? City { get; set; }

    public string? State { get; set; }

    public string? Ward { get; set; }

    public string? Country { get; set; }

    public string? PhoneNumber { get; set; }

    public bool IsActivated { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? TokenEffectiveDate { get; set; }

    public long? TokenEffectiveTimeStick { get; set; }

    public string? RefreshToken { get; set; }

    public string RoleName { get; set; }

    public int RoleId { get; set; }


    //public List<UserRolesViewModel> UserRoles { get; set; }
}
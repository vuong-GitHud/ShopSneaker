namespace ShopSneaker.Identity.Model
{
    public class LoginVm
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string UserId { get; set; }
        public string RoleId { get; set; }
        public string Expired { get; set; }
        public string UserRole { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public string TokenEffectiveDate { get; set; }
        public string TokenEffectiveTimeStick { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string AccountTypeId { get; set; }
    }
}

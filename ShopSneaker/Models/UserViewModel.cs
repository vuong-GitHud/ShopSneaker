namespace ShopSneaker.Models
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public DateTime DOB { get; set; }

        public string Email { get; set; }

        public string UserName { get; set; }

        public int RoleId { get; set; }

        public string Role { get; set; }
    }
}

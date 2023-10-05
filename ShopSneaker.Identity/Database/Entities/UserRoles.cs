namespace ShopSneaker.Identity.Database.Entities
{
    public class UserRoles
    {
        public int RoleId { get; set; }

        public int UserId { get; set; }

        public virtual Users User { get; set; }

        public virtual Roles Role { get; set; }
    }
}

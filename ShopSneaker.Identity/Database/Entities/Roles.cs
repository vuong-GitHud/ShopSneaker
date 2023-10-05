using Microsoft.AspNetCore.Identity;

namespace ShopSneaker.Identity.Database.Entities
{
    public class Roles : IdentityRole<int>
    {
        public virtual List<UserRoles> UserRoles { get; set; }

        public virtual List<RoleControls> SubordinateRoles { get; set; }
    }
}

	using Microsoft.AspNetCore.Identity;

namespace ShopSneaker.Data.Entities
{
    public class AppRole : IdentityRole<Guid>
	{
		public string Description { get; set; }
	}
}

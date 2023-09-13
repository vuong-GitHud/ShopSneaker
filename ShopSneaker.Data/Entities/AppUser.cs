using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopSneaker.Data.Entities
{
	public class AppUser : IdentityUser<Guid>
	{
		public string FullName { get; set; }
		public DateTime DOB { get; set; }

		//public List<Cart> Carts { get; set; }
		public List<Order> Orders { get; set; }
	}
}

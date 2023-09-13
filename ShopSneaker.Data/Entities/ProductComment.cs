using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopSneaker.Data.Entities
{
	public class ProductComment
	{
		public int Id { get; set; }
		public int? ProductId { get; set; }
		public string TextComment { get; set; }
		public int Rating { get; set; }
		public Product Product { get; set; }
	}
}

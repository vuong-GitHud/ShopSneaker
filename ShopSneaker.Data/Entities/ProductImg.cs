using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopSneaker.Data.Entities
{
	public class ProductImg
	{
		public int Id { get; set; }
		public int ProductId { get; set; }
		public string ImgPath { get; set; }
		public long FileSize { get; set; }
		public bool IsDefault { get; set; }

		public Product Product { get; set; }
	}
}

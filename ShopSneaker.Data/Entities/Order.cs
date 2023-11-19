using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopSneaker.Data.Entities
{
	public class Order
	{
        public int Id { set; get; }
        public DateTime OrderDate { set; get; }
        public Guid UserId { set; get; }

		public decimal Amount { get; set; }

		public string CurrencyCode { get; set; }

		public string PaymentId { get; set; }

        public string? FullName { get; set; }

        public string? Address { get; set; }

        public string? City { get; set; }

        public string? District { get; set; }

        public string? Ward { get; set; }

        public int PhoneNumber { get; set; }

        public string? Email { get; set; }

        public string PostCode { get; set; }

        public bool isPayment { get; set; }
        
        public bool isCancle { get; set; }

        public List<OrderDetail> OrderDetails { get; set; }
	}
}

using System;
using System.Collections.Generic;

namespace eWellmanShoppingApp.Models.CodeFirst {
	public class Order {
		public int id { get; set; }
		public string addrSt { get; set; }
		public string addrCity { get; set; }
		public string addrState { get; set; }
		public string addrZip { get; set; }
		public string addrCountry { get; set; }
		public int phone { get; set; }
		public decimal orderTotal { get; set; }
		public DateTime orderDate { get; set; }
		public string orderDetails { get; set; }

		public virtual ApplicationUser customer { get; set; }
		public virtual ICollection<OrderItem> orderItems {get;set;}
	}
	public class OrderItem {
		public int id { get; set; }
		public int orderID { get; set; }
		public string itemID { get; set; }
		public int quantity { get; set; }
		public decimal itemPrice { get; set; }

		public virtual Item item { get; set; }
		public virtual Order order { get; set; }
	}
}
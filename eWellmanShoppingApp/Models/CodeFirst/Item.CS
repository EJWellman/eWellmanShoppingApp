using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eWellmanShoppingApp.Models.CodeFirst {
	public class Item {
		public int id { get; set; }
		public DateTime creationDate { get; set; }
		public DateTime? updatedDate { get; set; }
		public string name { get; set; }
		public decimal price { get; set; }
		public string desc { get; set; }
		public string mediaURL { get; set; }
	}
	public class CartItem {
		public string customerID { get; set; }
		public int id { get; set; }
		public string itemID { get; set; }
		public int count { get; set; }
		public DateTime creationDate { get; set; }

		public virtual Item item { get; set; }
		public virtual ApplicationUser customer { get; set;}

		public decimal unitTotal {
			get {
				return count * item.price;
			}
		}
	}
}
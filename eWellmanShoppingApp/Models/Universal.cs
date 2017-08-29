using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eWellmanShoppingApp.Models {
	public class Universal : Controller {
		public ApplicationDbContext db = new ApplicationDbContext();
		protected override void OnActionExecuting(ActionExecutingContext filterContext) {
			base.OnActionExecuting(filterContext);
			if (User.Identity.IsAuthenticated) {
				var currUser = db.Users.Find(User.Identity.GetUserId());
				ViewBag.firstName = currUser.firstName;
				ViewBag.lastName = currUser.lastName;
				ViewBag.fullName = currUser.fullName;
				ViewBag.itemsInCart = currUser.cartItems.ToList();
				ViewBag.pastOrders = currUser.orders.ToList();
				ViewBag.orderTotal = 0;
				foreach (var item in ViewBag.itemsInCart) {
					ViewBag.orderTotal += item.unitTotal;
				}
			}
		}


	}
}
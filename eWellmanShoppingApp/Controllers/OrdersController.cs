using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using eWellmanShoppingApp.Models;
using eWellmanShoppingApp.Models.CodeFirst;
using Microsoft.AspNet.Identity;

namespace eWellmanShoppingApp.Controllers
{
	public class OrdersController : Universal{

		// GET: Orders
		public ActionResult Index()
		{
			return View(db.orders.ToList());
		}

		// GET: Verify
		public ActionResult Verify(string streetIn, string cityIn, string stateIn, string zipIn, string countryIn, string phoneIn, string detailsIn) {
			var user = db.Users.Find(User.Identity.GetUserId());
			Order newOrder = new Order() {
				addrSt = streetIn,
				addrCity = cityIn,
				addrState = stateIn,
				addrZip = zipIn,
				addrCountry = countryIn,
				phone = phoneIn,
				orderDetails = detailsIn,
				orderDate = DateTime.Now,
				customerID = user.Id,
				orderTotal = ViewBag.orderTotal
			};
			return View(newOrder);
		}

		// GET: Orders/Details/5
		public ActionResult Details() {
			var currUser = db.Users.Find(User.Identity.GetUserId());
			List<Order> orders = currUser.orders.ToList();
			if (orders == null) {
				return HttpNotFound();
			}
			OrderItem orderItem = new OrderItem();
			
			return View();
		}

		//// GET: Orders/Create
		//public ActionResult Create() {
		//	return View();
		//}

		// POST: Orders/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Include = "addrSt,addrCity,addrState,addrZip,addrCountry,phone,orderDetails")]Order orderIn)
		{
			if (orderIn.addrSt == null || orderIn.addrCity == null || orderIn.addrState == null || orderIn.addrZip == null || orderIn.addrCountry == null || orderIn.phone == null) {
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			var user = db.Users.Find(User.Identity.GetUserId());
			if (user == null) {
				return Redirect(HttpContext.Request.UrlReferrer.AbsoluteUri);
			}
			Order newOrder = new Order() {
				addrSt = orderIn.addrSt,
				addrCity = orderIn.addrCity,
				addrState = orderIn.addrState,
				addrZip = orderIn.addrZip,
				addrCountry = orderIn.addrCountry,
				phone = orderIn.phone,
				orderDetails = orderIn.orderDetails,
				orderDate = DateTime.Now,
				customerID = user.Id,
				orderTotal = ViewBag.orderTotal
			};
			db.orders.Add(newOrder);
			foreach(var item in ViewBag.itemsInCart) {
				OrderItem newOrderItem = new OrderItem() {
					orderID = newOrder.id,
					itemID = item.item.id,
					quantity = item.count,
					itemPrice = item.item.price
				};
				db.orderItems.Add(newOrderItem);
				db.cartItems.Remove(item);
			}
			db.SaveChanges();

			return View("Completion");
		}

		//// GET: Orders/Edit/5
		//public ActionResult Edit(int? id)
		//{
		//    if (id == null)
		//    {
		//        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
		//    }
		//    Order order = db.orders.Find(id);
		//    if (order == null)
		//    {
		//        return HttpNotFound();
		//    }
		//    return View(order);
		//}

		//// POST: Orders/Edit/5
		//// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		//// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		//[HttpPost]
		//[ValidateAntiForgeryToken]
		//public ActionResult Edit([Bind(Include = "id,addrSt,addrCity,addrState,addrZip,addrCountry,phone,orderTotal,orderDate,orderDetails")] Order order)
		//{
		//    if (ModelState.IsValid)
		//    {
		//        db.Entry(order).State = EntityState.Modified;
		//        db.SaveChanges();
		//        return RedirectToAction("Index");
		//    }
		//    return View(order);
		//}

		//// GET: Orders/Delete/5
		//public ActionResult Delete(int? id)
		//{
		//    if (id == null)
		//    {
		//        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
		//    }
		//    Order order = db.orders.Find(id);
		//    if (order == null)
		//    {
		//        return HttpNotFound();
		//    }
		//    return View(order);
		//}

		//// POST: Orders/Delete/5
		//[HttpPost, ActionName("Delete")]
		//[ValidateAntiForgeryToken]
		//public ActionResult DeleteConfirmed(int id)
		//{
		//    Order order = db.orders.Find(id);
		//    db.orders.Remove(order);
		//    db.SaveChanges();
		//    return RedirectToAction("Index");
		//}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				db.Dispose();
			}
			base.Dispose(disposing);
		}
	}
}

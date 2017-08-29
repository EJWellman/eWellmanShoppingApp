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
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.Ajax.Utilities;

namespace eWellmanShoppingApp.Controllers
{
	public class CartItemsController : Universal{

		// GET: CartItems
		public ActionResult Index()
		{
			return View(db.cartItems.ToList());
		}

		

		// GET: CartItems/Details/5
		public ActionResult Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			CartItem cartItem = db.cartItems.Find(id);
			if (cartItem == null)
			{
				return HttpNotFound();
			}
			//foreach (var row in db.cartItems.SqlQuery("SELECT * FROM CartItems WHERE customerID=@0")) {

			//}

			return Redirect(HttpContext.Request.UrlReferrer.AbsoluteUri);
		}

		//// GET: CartItems/Create
		//public ActionResult Create()
		//{
		//    return View();
		//}

		// POST: CartItems/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(int? idIn) {
			if (idIn == null) {
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Item itemIn = db.items.Find(idIn);
			var user = db.Users.Find(User.Identity.GetUserId());
			if (itemIn == null) {
				return HttpNotFound();
			}
			if (user == null) {
				return Redirect(HttpContext.Request.UrlReferrer.AbsoluteUri);
			}
			CartItem cartItem = new CartItem() {
				count = 1,
				creationDate = DateTime.Now,
				customerID = user.Id,
				itemID = itemIn.id
			};
			CartItem itemToCheck = db.cartItems.SingleOrDefault(item => item.itemID == cartItem.itemID && item.customerID == cartItem.customerID);
			if (itemToCheck == null) {
				db.cartItems.Add(cartItem);
				db.SaveChanges();
			}
			else if (itemToCheck != null) {
				int index = db.cartItems.Find(itemToCheck.id).id;
				db.cartItems.Find(index).count++;
				db.SaveChanges();
			}

			return Redirect(HttpContext.Request.UrlReferrer.AbsoluteUri);
		}

		//// GET: CartItems/Edit/5
		//public ActionResult Edit(int? id)
  //      {
  //          if (id == null)
  //          {
  //              return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
  //          }
  //          CartItem cartItem = db.cartItems.Find(id);
  //          if (cartItem == null)
  //          {
  //              return HttpNotFound();
  //          }
  //          return View(cartItem);
  //      }

		// POST: CartItems/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(int? idIn, int countIn)
		{
			db.cartItems.Find(idIn).count = countIn;
			db.SaveChanges();
			//Original code
			//if (ModelState.IsValid) {
			//	db.Entry(cartItem).State = EntityState.Modified;
			//	db.SaveChanges();
			//	return RedirectToAction("Index");
			//}
			return Redirect(HttpContext.Request.UrlReferrer.AbsoluteUri);
		}

		// GET: CartItems/Delete/5
		public ActionResult Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			CartItem cartItem = db.cartItems.Find(id);
			if (cartItem == null)
			{
				return HttpNotFound();
			}
			return View(cartItem);
		}

		// POST: CartItems/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			CartItem cartItem = db.cartItems.Find(id);
			db.cartItems.Remove(cartItem);
			db.SaveChanges();
			return RedirectToAction("Index");
		}

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

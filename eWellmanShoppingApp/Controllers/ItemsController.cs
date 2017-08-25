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
using System.IO;

namespace eWellmanShoppingApp.Controllers
{
	public class ItemsController : Universal{

		// GET: Items
		public ActionResult Index(){
			return View(db.items.ToList());
		}
		// GET: Items
		public ActionResult ItemAdd() {
			return View(db.items.ToList());
		}

		public ActionResult State() {
				ViewBag.imageList = new SelectList(db.items, "id", "mediaURL");
			return View();

		}

		// GET: Items/Details/5
		public ActionResult Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Item item = db.items.Find(id);
			if (item == null)
			{
				return HttpNotFound();
			}
			return View(item);
		}
		// GET: Items/Details/Partial
		public ActionResult DetailsPartial(int? id) {
			if (id == null) {
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Item itemToPass = db.items.Find(id);
			return PartialView(itemToPass);
		}
		//public ActionResult DetailsPartial(int? id) {
		//	if (id == null) {
		//		return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
		//	}
		//	Item item = db.items.Find(id);
		//	if (item == null) {
		//		return HttpNotFound();
		//	}
		//	return View(item);
		//}

		// GET: Items/Create
		[Authorize(Roles = "Admin")]
		public ActionResult Create() {
			DirectoryInfo imgDirectory = new DirectoryInfo(HttpContext.Server.MapPath("~/Assets/images/"));
			ViewBag.imageList = new SelectList(imgDirectory.GetFiles().ToList());
			return View();
		}

		// POST: Items/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[Authorize(Roles = "Admin")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Include = "id,creationDate,updatedDate,name,price,desc")] Item item, HttpPostedFileBase image, string image2) {
			DirectoryInfo imgDirectory = new DirectoryInfo(HttpContext.Server.MapPath("~/Assets/images/"));
			ViewBag.imageList = new SelectList(imgDirectory.GetFiles().ToList());
			if (image != null && image.ContentLength > 0) {
				var ext = Path.GetExtension(image.FileName).ToLower();
				if (ext != ".png" && ext != ".jpg" && ext != ".jpeg" && ext != ".gif") {
					ModelState.AddModelError("image", "Invalid format");
				}
			}
			else if (image2 != null && image2.Length > 0) {
				var ext = Path.GetExtension(image2);
				if (ext != ".png" && ext != ".jpg" && ext != ".jpeg" && ext != ".gif") {
					ModelState.AddModelError("image", "Invalid format");
				}
			}
			if (image == null && image2 == "") {
				ModelState.AddModelError("image", "No file selected");
			}
			if (image == null && image2 != "") {
				var filePath = "/Assets/images/";
				var absPath = Server.MapPath("~" + filePath);
				item.mediaURL = filePath + image2;
				item.creationDate = DateTime.Now;
				item.updatedDate = DateTime.Now;
				db.items.Add(item);
				db.SaveChanges();
				return RedirectToAction("Index");
			}
			else if (image2 == "" && image != null) {
				var filePath = "/Assets/images/";
				var absPath = Server.MapPath("~" + filePath);
				item.mediaURL = filePath + image.FileName;
				image.SaveAs(Path.Combine(absPath, image.FileName));
				item.creationDate = DateTime.Now;
				item.updatedDate = DateTime.Now;
				db.items.Add(item);
				db.SaveChanges();
				return RedirectToAction("Index");
			}
			return View(item);
		}

		//[Authorize(Roles = "Admin")]
		//[HttpPost]
		//[ValidateAntiForgeryToken]
		//public ActionResult Create([Bind(Include = "id,creationDate,updatedDate,name,price,desc")] Item item, HttpPostedFileBase image)
		//{
		//	if (image != null && image.ContentLength > 0) {
		//		var ext = Path.GetExtension(image.FileName).ToLower();
		//		if (ext != ".png" && ext != ".jpg" && ext != ".jpeg" && ext != ".gif") {
		//			ModelState.AddModelError("image", "Invalid format");
		//		}
		//	}
		//	if (image == null) {
		//		ModelState.AddModelError("image", "No file selected");
		//	}
		//	if (ModelState.IsValid)
		//	{
		//		var filePath = "/Assets/images/";
		//		var absPath = Server.MapPath("~" + filePath);
		//		item.mediaURL = filePath + image.FileName;
		//		image.SaveAs(Path.Combine(absPath, image.FileName));
		//		item.creationDate = DateTime.Now;
		//		item.updatedDate = DateTime.Now;
		//		db.items.Add(item);
		//		db.SaveChanges();
		//		return RedirectToAction("Index");
		//	}

		//	return View(item);
		//}

		// GET: Items/Edit/5
		[Authorize(Roles = "Admin")]
		public ActionResult Edit(int? id) {
			DirectoryInfo imgDirectory = new DirectoryInfo(HttpContext.Server.MapPath("~/Assets/images/"));
			ViewBag.imageList = new SelectList(imgDirectory.GetFiles().ToList());
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Item item = db.items.Find(id);
			if (item == null)
			{
				return HttpNotFound();
			}
			return View(item);
		}

		// POST: Items/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[Authorize(Roles = "Admin")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "id,creationDate,updatedDate,name,price,desc")] Item item, HttpPostedFileBase image, string image2) {
			DirectoryInfo imgDirectory = new DirectoryInfo(HttpContext.Server.MapPath("~/Assets/images/"));
			ViewBag.imageList = new SelectList(imgDirectory.GetFiles().ToList());
			if (image != null && image.ContentLength > 0) {
				var ext = Path.GetExtension(image.FileName).ToLower();
				if (ext != ".png" && ext != ".jpg" && ext != ".jpeg" && ext != ".gif") {
					ModelState.AddModelError("image", "Invalid format");
				}
			}
			if (image2 != null && image2.Length > 0) {
				var ext = Path.GetExtension(image2);
				if (ext != ".png" && ext != ".jpg" && ext != ".jpeg" && ext != ".gif") {
					ModelState.AddModelError("image", "Invalid format");
				}
			}
			if (image == null && image2 == "") {
				ModelState.AddModelError("image", "No file selected");
			}
			db.items.Find(item.id).id = item.id;
			db.items.Find(item.id).creationDate = item.creationDate;
			db.items.Find(item.id).updatedDate = item.updatedDate;
			db.items.Find(item.id).name = item.name;
			db.items.Find(item.id).price = item.price;
			db.items.Find(item.id).desc = item.desc;
			if (image == null && image2 != "") {
				var filePath = "/Assets/images/";
				var absPath = Server.MapPath("~" + filePath);
				db.items.Find(item.id).mediaURL = filePath + image2;
				item.updatedDate = DateTime.Now;
				db.SaveChanges();
				return RedirectToAction("Index");
			}
			else if (image2 == "" && image != null) {
				var filePath = "/Assets/images/";
				var absPath = Server.MapPath("~" + filePath);
				db.items.Find(item.id).mediaURL = filePath + image.FileName;
				image.SaveAs(Path.Combine(absPath, image.FileName)); ;
				item.updatedDate = DateTime.Now;
				db.SaveChanges();
				return RedirectToAction("Index");
			}

			return View(item);
		}
			//if (ModelState.IsValid)
   //         {
			//	item.updatedDate = DateTime.Now;
   //             db.Entry(item).State = EntityState.Modified;
   //             db.SaveChanges();
   //             return RedirectToAction("Index");
   //         }
   //         return View(item);
   //     }

		// GET: Items/Delete/5
		[Authorize(Roles = "Admin")]
		public ActionResult Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Item item = db.items.Find(id);
			if (item == null)
			{
				return HttpNotFound();
			}
			return View(item);
		}

		// POST: Items/Delete/5
		[Authorize(Roles = "Admin")]
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			Item item = db.items.Find(id);
			var absPath = Server.MapPath("~" + item.mediaURL);
			System.IO.File.Delete(absPath);
			db.items.Remove(item);
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

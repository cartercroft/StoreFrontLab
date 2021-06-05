using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StoreFrontLab.DATA.EF;

namespace StoreFrontLab.UI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductMakesController : Controller
    {
        private StoreFrontEntities db = new StoreFrontEntities();

        // GET: ProductMakes
        public ActionResult Index()
        {
            return View(db.ProductMakes.ToList());
        }

        // GET: ProductMakes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductMake productMake = db.ProductMakes.Find(id);
            if (productMake == null)
            {
                return HttpNotFound();
            }
            return View(productMake);
        }

        // GET: ProductMakes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductMakes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MakeID,MakeName")] ProductMake productMake)
        {
            if (ModelState.IsValid)
            {
                db.ProductMakes.Add(productMake);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(productMake);
        }

        // GET: ProductMakes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductMake productMake = db.ProductMakes.Find(id);
            if (productMake == null)
            {
                return HttpNotFound();
            }
            return View(productMake);
        }

        // POST: ProductMakes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MakeID,MakeName")] ProductMake productMake)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productMake).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(productMake);
        }

        // GET: ProductMakes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductMake productMake = db.ProductMakes.Find(id);
            if (productMake == null)
            {
                return HttpNotFound();
            }
            return View(productMake);
        }

        // POST: ProductMakes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductMake productMake = db.ProductMakes.Find(id);
            db.ProductMakes.Remove(productMake);
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

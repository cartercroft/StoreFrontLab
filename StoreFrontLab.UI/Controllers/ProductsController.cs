﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StoreFrontLab.DATA.EF;
using System.Drawing;
using StoreFrontLab.UI.Utilities;

namespace StoreFrontLab.UI.Controllers
{
    public class ProductsController : Controller
    {
        private StoreFrontEntities db = new StoreFrontEntities();

        // GET: Products
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.ProductMake).Include(p => p.ProductStatus).Include(p => p.ProductType);
            return View(products.ToList());
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.MakeID = new SelectList(db.ProductMakes, "MakeID", "MakeName");
            ViewBag.ProductStatusID = new SelectList(db.ProductStatuses, "ProductStatusID", "ProductStatusName");
            ViewBag.TypeID = new SelectList(db.ProductTypes, "TypeID", "TypeName");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "ProductID,ProductName,Price,UnitsSold,TypeID,MakeID,Model,ProductStatusID,Description,ProductImage")] Product product, HttpPostedFileBase productImage)
        {
            if (ModelState.IsValid)
            {
                #region File Upload
                string file = "noImage.png";

                if (productImage != null)
                {
                    file = productImage.FileName;

                    string ext = file.Substring(file.LastIndexOf('.'));
                    string[] goodExts = { ".jpg", ".jpeg", ".png", ".gif" };

                    if (goodExts.Contains(ext.ToLower()) && productImage.ContentLength <= 5242880)
                    {
                        file = Guid.NewGuid() + ext;

                        #region Resize
                        string savePath = Server.MapPath("~/Content/images/");

                        Image resized = Image.FromStream(productImage.InputStream);

                        int maxImageSize = 400;
                        int maxThumbSize = 100;

                        ImageServices.ResizeImage(savePath, file, resized, maxImageSize, maxThumbSize);

                        #endregion
                    }
                }
                product.ProductImage = file;
                #endregion
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MakeID = new SelectList(db.ProductMakes, "MakeID", "MakeName", product.MakeID);
            ViewBag.ProductStatusID = new SelectList(db.ProductStatuses, "ProductStatusID", "ProductStatusName", product.ProductStatusID);
            ViewBag.TypeID = new SelectList(db.ProductTypes, "TypeID", "TypeName", product.TypeID);
            return View(product);
        }

        // GET: Products/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.MakeID = new SelectList(db.ProductMakes, "MakeID", "MakeName", product.MakeID);
            ViewBag.ProductStatusID = new SelectList(db.ProductStatuses, "ProductStatusID", "ProductStatusName", product.ProductStatusID);
            ViewBag.TypeID = new SelectList(db.ProductTypes, "TypeID", "TypeName", product.TypeID);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "ProductID,ProductName,Price,UnitsSold,TypeID,MakeID,Model,ProductStatusID,Description,ProductImage")] Product product, HttpPostedFileBase productImage)
        {
            if (ModelState.IsValid)
            {
                #region File Edit
                string file = "noImage.png";

                if (productImage != null)
                {
                    // The user has uploaded a file to include for that specific book.
                    file = productImage.FileName;
                    // Check that the uploaded file extension matches accepted extensions
                    string ext = file.Substring(file.LastIndexOf('.'));
                    string[] goodExts = { ".jpg", ".jpeg", ".png", ".gif" };

                    // Check that the file extension is in our list of acceptable extensions AND check that the file size is 4MB MAX
                    if (goodExts.Contains(ext.ToLower()) && productImage.ContentLength <= 5242880)

                        // Create a new file name (using a GUID)
                        file = Guid.NewGuid() + ext;

                        #region Resize Image
                        string savePath = Server.MapPath("~/Content/images/");
                        #endregion
                        // The code below will delete the old image from the file structure.
                        if (product.ProductImage != null && product.ProductImage != "noImage.png")
                        {
                            string path = Server.MapPath("~/Content/images/");
                            ImageServices.Delete(path, product.ProductImage);
                        }
                    }
                }

                // No matter what, update the photoUrl with the value of the file variable
                product.ProductImage = file;
                #endregion
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MakeID = new SelectList(db.ProductMakes, "MakeID", "MakeName", product.MakeID);
            ViewBag.ProductStatusID = new SelectList(db.ProductStatuses, "ProductStatusID", "ProductStatusName", product.ProductStatusID);
            ViewBag.TypeID = new SelectList(db.ProductTypes, "TypeID", "TypeName", product.TypeID);
            return View(product);
        }

        // GET: Products/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
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
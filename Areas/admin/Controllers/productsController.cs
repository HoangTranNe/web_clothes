﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using do_an_web.Models;

namespace do_an_web.Areas.admin.Controllers
{
    public class productsController : Controller
    {
        private webClothesEntities db = new webClothesEntities();

        // GET: admin/products
        public ActionResult Index()
        {
            var products = db.products.Include(p => p.brand).Include(p => p.category).Include(p => p.warehouse);
            return View(products.ToList());
        }

        // GET: admin/products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: admin/products/Create
        public ActionResult Create()
        {
            ViewBag.id_brand = new SelectList(db.brands, "id_brand", "name_brand");
            ViewBag.id_category = new SelectList(db.categories, "id_category", "name_category");
            ViewBag.id_warehouse = new SelectList(db.warehouses, "id_warehouse", "id_warehouse");
            return View();
        }

        // POST: admin/products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_products,id_warehouse,id_category,id_brand,name,price,discount,images")] product product)
        {
            if (ModelState.IsValid)
            {
                db.products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_brand = new SelectList(db.brands, "id_brand", "name_brand", product.id_brand);
            ViewBag.id_category = new SelectList(db.categories, "id_category", "name_category", product.id_category);
            ViewBag.id_warehouse = new SelectList(db.warehouses, "id_warehouse", "id_warehouse", product.id_warehouse);
            return View(product);
        }

        // GET: admin/products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_brand = new SelectList(db.brands, "id_brand", "name_brand", product.id_brand);
            ViewBag.id_category = new SelectList(db.categories, "id_category", "name_category", product.id_category);
            ViewBag.id_warehouse = new SelectList(db.warehouses, "id_warehouse", "id_warehouse", product.id_warehouse);
            return View(product);
        }

        // POST: admin/products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "id_products,id_warehouse,id_category,id_brand,name,price,discount,images")] product product, HttpPostedFileBase images, FormCollection form)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if(images!=null)
                    {
                        string _FileName = Path.GetFileName(images.FileName);
                        string _path = Path.Combine(Server.MapPath("~/bookimages"), _FileName);
                        images.SaveAs(_path);
                        product.images = _FileName;
                        _path = Path.Combine(Server.MapPath("~/bookimages"), form["oldimage"]);
                        if (System.IO.File.Exists(_path))
                            System.IO.File.Delete(_path);
                    }
                    else
                    {
                        product.images = form["oldimage"];
                        db.Entry(product).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
                catch
                {
                    ViewBag.Message = "Không Thành Công";
                }
                return RedirectToAction("Index");
            }
            ViewBag.id_brand = new SelectList(db.brands, "id_brand", "name_brand", product.id_brand);
            ViewBag.id_category = new SelectList(db.categories, "id_category", "name_category", product.id_category);
            ViewBag.id_warehouse = new SelectList(db.warehouses, "id_warehouse", "id_warehouse", product.id_warehouse);
            return View(product);
        }

        // GET: admin/products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: admin/products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            product product = db.products.Find(id);
            db.products.Remove(product);
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

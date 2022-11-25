using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
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
        public ActionResult Index(string searchString, int? page, int id_category = 0)
        {
            var items = db.products.ToList();
            /*if (page == null)
            {
                page = 1;
            }
            int pageSize = 3;
            int pageNumber = (page ?? 1);


            var products = db.products.Include(p => p.brand).Include(p => p.category).Include(p => p.warehouse);
            if (!String.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();
                products = products.Where(b => b.name.ToLower().Contains(searchString));
            }

            ViewBag.Keyword = searchString;
            if (!String.IsNullOrEmpty(searchString))
                products = products.Where(b => b.name.Contains(searchString));
            if (id_category != 0)
            {
                products = products.Where(c => c.id_category == id_category);
            }
            ViewBag.CategoryID = new SelectList(db.categories, "id_category", "name_category");*/

            /* return View(products.ToPagedList(pageNumber, pageSize));*/
            return View(items);
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
            ViewBag.id_warehouse = new SelectList(db.warehouses, "id_warehouse", "name_warehouse");
            return View();
        }

        // POST: admin/products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "id_products,id_warehouse,id_category,id_brand,name,price,discount,images")] product product, HttpPostedFileBase images)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (images.ContentLength > 0)
                    {
                        string _FileName = Path.GetFileName(images.FileName);
                        string _path = Path.Combine(Server.MapPath("~/Content/Images"), _FileName);
                        images.SaveAs(_path);
                        product.images = _FileName;
                    }
                    db.products.Add(product);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    ViewBag.Message = "Không Thành Công";
                }
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
            ViewBag.id_warehouse = new SelectList(db.warehouses, "id_warehouse", "name_warehouse", product.id_warehouse);
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
                    if (images != null)
                    {
                        string _FileName = Path.GetFileName(images.FileName);
                        string _path = Path.Combine(Server.MapPath("~/bookimages"), _FileName);
                        images.SaveAs(_path);
                        product.images = _FileName;
                        // get Path of old image for deleting it
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
                    ViewBag.Message = "Không Thành Công!!";
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

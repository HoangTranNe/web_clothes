using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using do_an_web.Models;
using PagedList;

namespace do_an_web.Areas.admin.Controllers
{
    public class productsController : Controller
    {
        private webClothesEntities db = new webClothesEntities();

        // GET: admin/products
        [HttpGet]
        public ActionResult Index(int? page, int? size)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "5", Value = "5" });
            items.Add(new SelectListItem { Text = "10", Value = "10" });
            items.Add(new SelectListItem { Text = "20", Value = "20" });
            items.Add(new SelectListItem { Text = "25", Value = "25" });
            items.Add(new SelectListItem { Text = "50", Value = "50" });
            items.Add(new SelectListItem { Text = "100", Value = "100" });
            items.Add(new SelectListItem { Text = "200", Value = "200" });

            foreach (var item in items)
            {
                if (item.Value == size.ToString()) item.Selected = true;
            }
            ViewBag.size = items;
            ViewBag.currentSize = size; 
            if (page == null) page = 1;
            var products = db.products.Include(p => p.brand).Include(p => p.category).OrderBy(b => b.id_products);
            int pageSize = (size ?? 5);            

            int pageNumber = (page ?? 1);

            return View(products.ToPagedList(pageNumber, pageSize));
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
            return View();
        }

        // POST: admin/products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "id_products,id_warehouse,id_category,id_brand,name_product,price,discount,descibe,images,images_size")] product product, HttpPostedFileBase images, HttpPostedFileBase images_size)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (images.ContentLength > 0)
                    {
                        string _FileName1 = Path.GetFileName(images.FileName);
                        string _path1 = Path.Combine(Server.MapPath("~/Content/Images"), _FileName1);
                        images.SaveAs(_path1);
                        product.images = _FileName1;
                    }
                    if (images_size.ContentLength > 0)
                    {
                        string _FileName2 = Path.GetFileName(images_size.FileName);
                        string _path2 = Path.Combine(Server.MapPath("~/Content/Images"), _FileName2);
                        images_size.SaveAs(_path2);
                        product.images_size = _FileName2;
                    }
                    db.products.Add(product);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    ViewBag.Message = "Không thành công";
                }
            }

            ViewBag.id_brand = new SelectList(db.brands, "id_brand", "name_brand", product.id_brand);
            ViewBag.id_category = new SelectList(db.categories, "id_category", "name_category", product.id_category);

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
            return View(product);
        }

        // POST: admin/products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "id_products,id_warehouse,id_category,id_brand,name_product,price,discount,descibe,images,images_size")] product product, HttpPostedFileBase images, HttpPostedFileBase images_size, FormCollection form)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (images.ContentLength > 0)
                    {
                        string _FileName1 = Path.GetFileName(images.FileName);
                        string _path1 = Path.Combine(Server.MapPath("~/Content/Images"), _FileName1);
                        images.SaveAs(_path1);
                        product.images = _FileName1;
                        // get Path of old image for deleting it
                        _path1 = Path.Combine(Server.MapPath("~/Content/Images"), form["oldimage"]);
                        if (System.IO.File.Exists(_path1))
                            System.IO.File.Delete(_path1);

                    }
                    if (images_size.ContentLength > 0)
                    {
                        string _FileName2 = Path.GetFileName(images_size.FileName);
                        string _path2 = Path.Combine(Server.MapPath("/Content/Images"), _FileName2);
                        images_size.SaveAs(_path2);
                        product.images_size = _FileName2;
                        // get Path of old image for deleting it
                        _path2 = Path.Combine(Server.MapPath("~/Content/Images"), form["oldimage"]);
                        if (System.IO.File.Exists(_path2))
                            System.IO.File.Delete(_path2);
                    }
                    else
                    {
                        product.images = product.images_size = form["oldimage"];
                        db.Entry(product).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
                catch
                {
                    ViewBag.Message = "không thành công!!";
                }
                return RedirectToAction("Index");
            }
            ViewBag.id_brand = new SelectList(db.brands, "id_brand", "name_brand", product.id_brand);
            ViewBag.id_category = new SelectList(db.categories, "id_category", "name_category", product.id_category);
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

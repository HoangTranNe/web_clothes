using do_an_web.Models;
using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace do_an_web.Areas.admin.Controllers
{
    public class productsController : Controller
    {
        webClothesEntities db = new webClothesEntities();

        // GET: admin/products
        public ActionResult Index()
        {
            var items = db.products.ToList();
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
        public ActionResult Create([Bind(Include = "id_products,id_warehouse,id_category,id_brand,name,price,discount,descibe,images,images_size")] product product, HttpPostedFileBase images)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    string fileName = Path.GetFileName(images.FileName);
                    string extension = Path.Combine(Server.MapPath("~/Content/Images/"), fileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    images.SaveAs(extension);
                    product.images = (fileName);
                }
                catch
                {
                    ViewBag.Message = "Không Thành Công";
                }
                db.products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_brand = new SelectList(db.brands, "id_brand", "name_brand", product.id_brand);
            ViewBag.id_category = new SelectList(db.categories, "id_category", "name_category", product.id_category);
            ViewBag.id_warehouse = new SelectList(db.warehouses, "id_warehouse", "name_warehouse", product.id_warehouse);
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
        // To protect from overposting   attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "id_products,id_warehouse,id_category,id_brand,name,price,discount,descibe,images,images_size")] product product, HttpPostedFileBase images, FormCollection form, HttpPostedFileBase images_size)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (images != null)
                    {
                        string _FileName1 = Path.GetFileName(images.FileName);

                        string _path1 = Path.Combine(Server.MapPath("~/Content/Images"), _FileName1);

                        images.SaveAs(_path1);
                        product.images = _FileName1;
                        // get Path of old image for deleting it
                        _path1 = Path.Combine(Server.MapPath("~/Content/Images"), form["Images"]);
                        if (System.IO.File.Exists(_path1))
                            System.IO.File.Delete(_path1);

                    }
                    if (images_size != null)
                    {
                        string _FileName2 = Path.GetFileName(images_size.FileName);

                        string _path2 = Path.Combine(Server.MapPath("~/Content/Images_Title"), _FileName2);

                        images.SaveAs(_path2);
                        product.images_size = _FileName2;
                        _path2 = Path.Combine(Server.MapPath("~/Content/Images_Title"), form["Images_Title"]);
                        if (System.IO.File.Exists(_path2))
                            System.IO.File.Delete(_path2);
                    }
                    else
                    {
                        product.images = form["Images"];
                        product.images_size = form["Images_Title"];
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
            ViewBag.id_warehouse = new SelectList(db.warehouses, "id_warehouse", "name_warehouse", product.id_warehouse);
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

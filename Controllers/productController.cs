using do_an_web.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace do_an_web.Controllers
{
    public class productController : Controller
    {
        private webClothesEntities db = new webClothesEntities();

        // GET: products
        public ActionResult Index()
        {
            var products = db.products.Include(p => p.brand).Include(p => p.category).Include(p => p.warehouse);
            return View(products.ToList());
        }

        // GET: products/Details/5
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

        // GET: products/Create
        public ActionResult Create()
        {
            ViewBag.id_brand = new SelectList(db.brands, "id_brand", "name_brand");
            ViewBag.id_category = new SelectList(db.categories, "id_category", "name_category");
            ViewBag.id_warehouse = new SelectList(db.warehouses, "id_warehouse", "name_warehouse");
            return View();
        }

        // POST: products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_products,id_warehouse,id_category,id_brand,name,price,discount,descibe,images,images_size")] product product, HttpPostedFileBase images, HttpPostedFileBase images_size)
        {
            if (ModelState.IsValid)
            {
                db.products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_brand = new SelectList(db.brands, "id_brand", "name_brand", product.id_brand);
            ViewBag.id_category = new SelectList(db.categories, "id_category", "name_category", product.id_category);
            ViewBag.id_warehouse = new SelectList(db.warehouses, "id_warehouse", "name_warehouse", product.id_warehouse);
            return View(product);
        }

        // GET: products/Edit/5
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

        // POST: products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_products,id_warehouse,id_category,id_brand,name,price,discount,descibe,images,images_size")] product product, HttpPostedFileBase images, FormCollection form, HttpPostedFileBase images_size)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_brand = new SelectList(db.brands, "id_brand", "name_brand", product.id_brand);
            ViewBag.id_category = new SelectList(db.categories, "id_category", "name_category", product.id_category);
            ViewBag.id_warehouse = new SelectList(db.warehouses, "id_warehouse", "name_warehouse", product.id_warehouse);
            return View(product);
        }

        // GET: products/Delete/5
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

        // POST: products/Delete/5
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

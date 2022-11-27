using do_an_web.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace do_an_web.Areas.admin.Controllers
{
    public class customer_orderController : Controller
    {
        private webClothesEntities db = new webClothesEntities();

        // GET: admin/customer_order
        public ActionResult Index()
        {
            var customer_order = db.customer_order.Include(c => c.customer).Include(c => c.product);
            return View(customer_order.ToList());
        }

        // GET: admin/customer_order/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            customer_order customer_order = db.customer_order.Find(id);
            if (customer_order == null)
            {
                return HttpNotFound();
            }
            return View(customer_order);
        }

        // GET: admin/customer_order/Create
        public ActionResult Create()
        {
            ViewBag.id_customer = new SelectList(db.customers, "id_customer", "name_customer");
            ViewBag.id_products = new SelectList(db.products, "id_products", "name");
            return View();
        }

        // POST: admin/customer_order/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_order,id_customer,id_products,id_cart,quantity_order,total")] customer_order customer_order)
        {
            if (ModelState.IsValid)
            {
                db.customer_order.Add(customer_order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_customer = new SelectList(db.customers, "id_customer", "name_customer", customer_order.id_customer);
            ViewBag.id_products = new SelectList(db.products, "id_products", "name", customer_order.id_products);
            return View(customer_order);
        }

        // GET: admin/customer_order/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            customer_order customer_order = db.customer_order.Find(id);
            if (customer_order == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_customer = new SelectList(db.customers, "id_customer", "name_customer", customer_order.id_customer);
            ViewBag.id_products = new SelectList(db.products, "id_products", "name", customer_order.id_products);
            return View(customer_order);
        }

        // POST: admin/customer_order/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_order,id_customer,id_products,id_cart,quantity_order,total")] customer_order customer_order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer_order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_customer = new SelectList(db.customers, "id_customer", "name_customer", customer_order.id_customer);
            ViewBag.id_products = new SelectList(db.products, "id_products", "name", customer_order.id_products);
            return View(customer_order);
        }

        // GET: admin/customer_order/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            customer_order customer_order = db.customer_order.Find(id);
            if (customer_order == null)
            {
                return HttpNotFound();
            }
            return View(customer_order);
        }

        // POST: admin/customer_order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            customer_order customer_order = db.customer_order.Find(id);
            db.customer_order.Remove(customer_order);
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

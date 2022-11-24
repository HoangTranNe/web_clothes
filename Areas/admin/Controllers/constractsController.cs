using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using do_an_web.Models;

namespace do_an_web.Areas.admin.Controllers
{
    public class constractsController : Controller
    {
        private webClothesEntities db = new webClothesEntities();

        // GET: admin/constracts
        public ActionResult Index()
        {
            var constracts = db.constracts.Include(c => c.partner).Include(c => c.product);
            return View(constracts.ToList());
        }

        // GET: admin/constracts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            constract constract = db.constracts.Find(id);
            if (constract == null)
            {
                return HttpNotFound();
            }
            return View(constract);
        }

        // GET: admin/constracts/Create
        public ActionResult Create()
        {
            ViewBag.id_partners = new SelectList(db.partners, "id_partners", "name_partners");
            ViewBag.id_products = new SelectList(db.products, "id_products", "name");
            return View();
        }

        // POST: admin/constracts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_constracts,id_partners,id_products,price_constracts,quantity")] constract constract)
        {
            if (ModelState.IsValid)
            {
                db.constracts.Add(constract);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_partners = new SelectList(db.partners, "id_partners", "name_partners", constract.id_partners);
            ViewBag.id_products = new SelectList(db.products, "id_products", "name", constract.id_products);
            return View(constract);
        }

        // GET: admin/constracts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            constract constract = db.constracts.Find(id);
            if (constract == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_partners = new SelectList(db.partners, "id_partners", "name_partners", constract.id_partners);
            ViewBag.id_products = new SelectList(db.products, "id_products", "name", constract.id_products);
            return View(constract);
        }

        // POST: admin/constracts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_constracts,id_partners,id_products,price_constracts,quantity")] constract constract)
        {
            if (ModelState.IsValid)
            {
                db.Entry(constract).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_partners = new SelectList(db.partners, "id_partners", "name_partners", constract.id_partners);
            ViewBag.id_products = new SelectList(db.products, "id_products", "name", constract.id_products);
            return View(constract);
        }

        // GET: admin/constracts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            constract constract = db.constracts.Find(id);
            if (constract == null)
            {
                return HttpNotFound();
            }
            return View(constract);
        }

        // POST: admin/constracts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            constract constract = db.constracts.Find(id);
            db.constracts.Remove(constract);
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

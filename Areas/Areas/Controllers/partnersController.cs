using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using do_an_web.Models;

namespace do_an_web.Controllers
{
    public class partnersController : Controller
    {
        private webClothesEntities db = new webClothesEntities();

        // GET: partners
        public ActionResult Index()
        {
            return View(db.partners.ToList());
        }

        // GET: partners/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            partner partner = db.partners.Find(id);
            if (partner == null)
            {
                return HttpNotFound();
            }
            return View(partner);
        }

        // GET: partners/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: partners/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_partners,name_partners,phone_partners,email_partners,require_partners")] partner partner)
        {
            if (ModelState.IsValid)
            {
                db.partners.Add(partner);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(partner);
        }

        // GET: partners/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            partner partner = db.partners.Find(id);
            if (partner == null)
            {
                return HttpNotFound();
            }
            return View(partner);
        }

        // POST: partners/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_partners,name_partners,phone_partners,email_partners,require_partners")] partner partner)
        {
            if (ModelState.IsValid)
            {
                db.Entry(partner).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(partner);
        }

        // GET: partners/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            partner partner = db.partners.Find(id);
            if (partner == null)
            {
                return HttpNotFound();
            }
            return View(partner);
        }

        // POST: partners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            partner partner = db.partners.Find(id);
            db.partners.Remove(partner);
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

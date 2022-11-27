﻿using do_an_web.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace do_an_web.Areas.admin.Controllers
{
    public class reportsController : Controller
    {
        private webClothesEntities db = new webClothesEntities();

        // GET: admin/reports
        public ActionResult Index()
        {
            var reports = db.reports.Include(r => r.customer);
            return View(reports.ToList());
        }

        // GET: admin/reports/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            report report = db.reports.Find(id);
            if (report == null)
            {
                return HttpNotFound();
            }
            return View(report);
        }

        // GET: admin/reports/Create
        public ActionResult Create()
        {
            ViewBag.id_customer = new SelectList(db.customers, "id_customer", "name_customer");
            return View();
        }

        // POST: admin/reports/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_reports,id_customer,subject_customer,contents_customer")] report report)
        {
            if (ModelState.IsValid)
            {
                db.reports.Add(report);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_customer = new SelectList(db.customers, "id_customer", "name_customer", report.id_customer);
            return View(report);
        }

        // GET: admin/reports/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            report report = db.reports.Find(id);
            if (report == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_customer = new SelectList(db.customers, "id_customer", "name_customer", report.id_customer);
            return View(report);
        }

        // POST: admin/reports/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_reports,id_customer,subject_customer,contents_customer")] report report)
        {
            if (ModelState.IsValid)
            {
                db.Entry(report).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_customer = new SelectList(db.customers, "id_customer", "name_customer", report.id_customer);
            return View(report);
        }

        // GET: admin/reports/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            report report = db.reports.Find(id);
            if (report == null)
            {
                return HttpNotFound();
            }
            return View(report);
        }

        // POST: admin/reports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            report report = db.reports.Find(id);
            db.reports.Remove(report);
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

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using do_an_web.Models;

namespace do_an_web.Controllers.Controller_Data
{
    public class warehousesController : Controller
    {
        private webClothesEntities db = new webClothesEntities();

        // GET: warehouses
        public ActionResult Index()
        {
            return View(db.warehouses.ToList());
        }

        // GET: warehouses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            warehouse warehouse = db.warehouses.Find(id);
            if (warehouse == null)
            {
                return HttpNotFound();
            }
            return View(warehouse);
        }

        // GET: warehouses/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: warehouses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_warehouse,id_products,quantity")] warehouse warehouse)
        {
            if (ModelState.IsValid)
            {
                db.warehouses.Add(warehouse);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(warehouse);
        }

        // GET: warehouses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            warehouse warehouse = db.warehouses.Find(id);
            if (warehouse == null)
            {
                return HttpNotFound();
            }
            return View(warehouse);
        }

        // POST: warehouses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_warehouse,id_products,quantity")] warehouse warehouse)
        {
            if (ModelState.IsValid)
            {
                db.Entry(warehouse).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(warehouse);
        }

        // GET: warehouses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            warehouse warehouse = db.warehouses.Find(id);
            if (warehouse == null)
            {
                return HttpNotFound();
            }
            return View(warehouse);
        }

        // POST: warehouses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            warehouse warehouse = db.warehouses.Find(id);
            db.warehouses.Remove(warehouse);
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

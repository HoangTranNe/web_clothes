using do_an_web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;

namespace do_an_web.Controllers
{
    public class searchController : Controller
    {
        // GET: search
        webClothesEntities db = new webClothesEntities();
        public ActionResult Search(string search)
        {
            var products = db.products.Include(p => p.brand).Include(p => p.category);
            if (!String.IsNullOrEmpty(search))
            {
                search = search.ToLower();
                products = products.Where(b => b.name_product.ToLower().Contains(search));
            }
            return View(products.ToList());
        }
    }
}
using do_an_web.Models;
using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Cryptography;
using System.ComponentModel.DataAnnotations;

namespace do_an_web.Controllers
{
    public class HomeController : Controller
    {
        private webClothesEntities db = new webClothesEntities();
        public ActionResult Index()
        {
            if (Session["id_customer"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult web_clothes_view()
        {
            return View(db.categories.ToList());
        }
        public ActionResult dieu_khoan_view()
        {
            return View();
        }
        public ActionResult huong_dan_mua_hang_view()
        {
            return View();
        }
        public ActionResult see_all_view()
        {
            return View();
        }
        public ActionResult chinh_sach_doi_tra_view()
        {
            return View();
        }
        public ActionResult login_view()
        {
            return View();
        }
        public ActionResult chinh_sach_bao_mat_thong_tin_view()
        {
            return View();
        }
        public ActionResult chinh_sach_thanh_toan_view()
        {
            return View();
        }
        public ActionResult product_details_view()
        {
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(customer _customer)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    var check = db.customers.FirstOrDefault(s => s.email_customer == _customer.email_customer);
                    if(check == null)
                    {
                        _customer.password_customer = GetMD5(_customer.password_customer);
                        db.Configuration.ValidateOnSaveEnabled = false;
                        db.customers.Add(_customer);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
                return View();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationsErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationsErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationsErrors.Entry.Entity.ToString(),
                            validationsErrors.ErrorMessage);
                        raise = new InvalidOperationException(message,raise);
                    }
                }
                throw raise;
            }
        }
    }
}
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

            if (ModelState.IsValid)
            {
                var check = db.customers.FirstOrDefault(s => s.email_customer == _customer.email_customer);
                if (check == null)
                {
                    _customer.password_customer = GetMD5(_customer.password_customer);
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.customers.Add(_customer);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.error = "Email already exists";
                    return View();
                }
            }
            return View();
        }
                       
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string email, string password)
        {
            if (ModelState.IsValid)
            {
                var f_password = GetMD5(password);
                var data = db.customer.Where(s => s.email_customer.Equals(email) && s.password_customer.Equals(f_password)).ToList();
                if(data.Count> 0)
                {
                    Session["Name"] = data.FirstOrDefault().name_customer;
                    Session["Email"] = data.FirstOrDefault().email_customer;
                    Session["Id_customer"] = data.FirstOrDefault().id_customer;
                }
                else
                {
                    ViewBag.error = "login failed";
                    return RedirectToAction("Login");
                }
            }
            return View();
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }

        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;
            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");
            }
            return byte2String;
        }
    }
}
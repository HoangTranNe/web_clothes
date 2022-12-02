using do_an_web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace do_an_web.Areas.admin.Controllers
{
    public class homeAdminController : Controller
    {
        webClothesEntities db = new webClothesEntities();
        // GET: admin/homeAdmin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Contact_View()
        {
            return View();
        }
        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(customer kh)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(kh.name_customer))
                    ModelState.AddModelError(string.Empty, "Tên đăng nhập không được để trống");
                if (string.IsNullOrEmpty(kh.password_customer))
                    ModelState.AddModelError(string.Empty, "Mật khẩu không được để trống");
                if (ModelState.IsValid)
                {
                    //Tìm khách hàng có tên đăng nhập và password hợp lệ trong CSDL
                    /*var check = db.customers.FirstOrDefault()*/
                    var khach = db.customers.FirstOrDefault(k => k.name_customer == kh.name_customer && k.password_customer == kh.password_customer);
                    var admin = db.customers.FirstOrDefault(k => k.name_customer == kh.name_customer && k.password_customer == kh.password_customer && k.role == 1);
                    if (admin != null)
                    {
                        return RedirectToAction("Index", "homeAdmin", new { area = "admin" });
                    }
                    else if (khach != null)
                    {

                        //Lưu vào session
                        Session["name_customer"] = kh.name_customer;
                        Session["user_id"] = kh.id_customer;
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ViewBag.ThongBao = "Tên đăng nhập hoặc mật khẩu không đúng";
                    }
                }
            }

            return View();       
        }
        public ActionResult Register()
        {
            return View();
        }
        public ActionResult LogOut()
        {
            Session.Clear();
            return Redirect("/");
        }
    }
        
}

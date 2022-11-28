using do_an_web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace do_an_web.Controllers
{
    public class UsersController : Controller
    {
        webClothesEntities db = new webClothesEntities();
        // GET: Users
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangKy(customer kh)
        {            
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(kh.name_customer))
                    ModelState.AddModelError(string.Empty, "Họ tên không được để trống");
                if (string.IsNullOrEmpty(kh.name_customer))
                    ModelState.AddModelError(string.Empty, "Tên đăng nhập không được để trống");
                if (string.IsNullOrEmpty(kh.password_customer))
                    ModelState.AddModelError(string.Empty, "Mật khẩu không được để trống");
                if (string.IsNullOrEmpty(kh.email_customer))
                    ModelState.AddModelError(string.Empty, "Email không được để trống");
                if (string.IsNullOrEmpty(kh.phone_customer.ToString()))
                    ModelState.AddModelError(string.Empty, "Điện thoại không được để trống");
                    //Kiểm tra xem có người nào đã đăng kí với tên đăng nhập này hay chưa
                    var khachhang = db.customers.FirstOrDefault(k => k.name_customer == kh.name_customer);
                if (khachhang != null)
                    ModelState.AddModelError(string.Empty, "Đã có người đăng kí tên này");
            if (ModelState.IsValid)
                {
                    db.customers.Add(kh);
                    db.SaveChanges();

                }
                else
                {
                    return View();
                }
            }
            return RedirectToAction("DangNhap");
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
                    var khach = db.customers.FirstOrDefault(k => k.name_customer == kh.name_customer && k.password_customer == kh.password_customer);
                    if (khach != null)
                    {
                        ViewBag.ThongBao = "Chúc mừng đăng nhập thành công";
                        //Lưu vào session

                        Session["TaiKhoan"] = khach;

                    }
                    else

                        ViewBag.ThongBao = "Tên đăng nhập hoặc mật khẩu không đúng";
                }
            }
            return View();
        }
    }
}

    
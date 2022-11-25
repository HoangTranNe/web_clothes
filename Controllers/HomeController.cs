using do_an_web.Models;
using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Cryptography;
using System.ComponentModel.DataAnnotations;
using PagedList;
using System.Web.WebPages.Html;
using System.Data.Entity;

namespace do_an_web.Controllers
{
    public class HomeController : Controller
    {
        private webClothesEntities db = new webClothesEntities();
        private List<product> Add_New_Product(int quantity)
        {
            return db.products.OrderByDescending(p => p.name).Take(quantity).ToList();
        }
        public ActionResult Index()
        {
            var new_pro = Add_New_Product(20);
            return View(new_pro);
        }

        public ActionResult dieu_khoan_view()
        {
            return View();
        }
        public ActionResult huong_dan_mua_hang_view()
        {
            return View();
        }
        public ActionResult chinh_sach_doi_tra_view()
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
        public ActionResult Contact_View()
        {
            return View();
        }
        public ActionResult takecategory()
        {
            var cate_list = db.categories.ToList();
            return PartialView(cate_list);
        }
        public ActionResult prowithcate(int id)
        {
            //Lấy các sách theo mã chủ đề được chọn
            var prowithcase_list = db.products.Where(p => p.id_category == id).ToList();
            //Trả về View để render các sách trên (tái sử dụng View Index ở trên, truyền vào danh sách)
            return View("Index", prowithcase_list);
        }
        public ActionResult Details(int id)
        {
            //Lấy sách có mã tương ứng
            var product = db.products.FirstOrDefault(p => p.id_products == id);
            return View(product);
        }
    }
}
using do_an_web.Models;
using PagedList;
using PagedList.Mvc;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace do_an_web.Controllers
{
    public class HomeController : Controller
    {
        webClothesEntities db = new webClothesEntities();
        public ActionResult Index(int? page, int? size)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "8", Value = "8" });
            items.Add(new SelectListItem { Text = "16", Value = "16" });
            items.Add(new SelectListItem { Text = "24", Value = "24" });

            foreach (var item in items)
            {
                if (item.Value == size.ToString()) item.Selected = true;
            }
            ViewBag.size = items;
            ViewBag.currentSize = size;
            if (page == null) page = 1;
            var products = db.products.Include(p => p.brand).Include(p => p.category).OrderBy(b => b.id_products);
            int pageSize = (size ?? 8);

            int pageNumber = (page ?? 1);

            return View(products.ToPagedList(pageNumber, pageSize));
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
        public ActionResult Details(int? id)
        {
            var product = db.products.FirstOrDefault(s => s.id_products == id);
            return View(product);
        }
    }
}
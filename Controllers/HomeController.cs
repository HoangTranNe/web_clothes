using do_an_web.Models;
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
using System.Web.UI.WebControls;

namespace do_an_web.Controllers
{
    public class HomeController : Controller
    {
        webClothesEntities db = new webClothesEntities();
        private List<product> Add_New_Product(int quantity)
        {
            return db.products.OrderByDescending(p => p.name_product).Take(quantity).ToList();
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
            return View("Index","Home", prowithcase_list);
        }
        public ActionResult Details(int? id)
        {
            var product = db.products.FirstOrDefault(s => s.id_products == id);
            return View(product);
        }
        /*public ActionResult fillcategory(int id)
        {
            #region
            fillcategory.Open();
            SqlCommand cmdp = con.CreateCommand();
            cmdp.CommandType = CommandType.Text;

            var categoryID = Request.QueryString["category"];
            int catId = string.IsNullOrEmpty(categoryID) ? 0 : int.Parse(categoryID);
            if (!string.IsNullOrEmpty(categoryID))
                cmdp.CommandText = " select * from products where [category_id] = " + catId;
            else
                cmdp.CommandText = "select * from products";
            cmdp.ExecuteNonQuery();
            DataTable dttp = new DataTable();
            SqlDataAdapter dap = new SqlDataAdapter(cmdp);
            dap.Fill(dttp);
            Datalist1.DataSource = dttp;
            Datalist1.DataBind();

            con.Close();
            #endregion
        }*/
    }
}
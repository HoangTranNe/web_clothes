using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace do_an_web.Models
{
    public class cartController : Controller
    {
        // GET: cart
        public ActionResult Index()
        {
            return View();
        }
        public List<needtobuy> makecart()
        {
            List<needtobuy> gioHang = Session["GioHang"] as List<needtobuy>;
            //Nếu giỏ hàng chưa tồn tại thì tạo mới và đưa vào Session
            if (gioHang == null)
            {
                gioHang = new List<needtobuy>();
                Session["GioHang"] = gioHang;
            }
            return gioHang;
        }
        public void addtocart(int id_products)
        {
            //Lấy giỏ hàng hiện tại
            List<needtobuy> gioHang = makecart();
            //Kiểm tra xem có tồn tại mặt hàng trong giỏ hay chưa
            //Nếu có thì tăng số lượng lên 1, ngược lại thêm vào giỏ
            needtobuy sanPham = gioHang.FirstOrDefault(s => s.id_product == id_products);
            if (sanPham == null) //Sản phẩm chưa có trong giỏ
            {
                sanPham = new needtobuy(id_products);
                gioHang.Add(sanPham);
            }
            else
            {
                sanPham.quantity++; //Sản phẩm đã có trong giỏ thì tăng số lượng lên 1
            }
            return RedirectToAction("Details", "products", new { id = id_products });
        }
    }
}
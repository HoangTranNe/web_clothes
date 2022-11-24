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
            List<needtobuy> cart = Session["cart"] as List<needtobuy>;
            //Nếu giỏ hàng chưa tồn tại thì tạo mới và đưa vào Session
            if (cart == null)
            {
                cart = new List<needtobuy>();
                Session["cart"] = cart;
            }
            return cart;
        }
        public RedirectToRouteResult addtocart(int id_products)
        {
            //Lấy giỏ hàng hiện tại
            List<needtobuy> cart = makecart();
            //Kiểm tra xem có tồn tại mặt hàng trong giỏ hay chưa
            //Nếu có thì tăng số lượng lên 1, ngược lại thêm vào giỏ
            needtobuy product = cart.FirstOrDefault(s => s.id_product == id_products);
            if (product == null) //Sản phẩm chưa có trong giỏ
            {
                product = new needtobuy(id_products);
                cart.Add(product);
            }
            else
            {
                product.quantity++; //Sản phẩm đã có trong giỏ thì tăng số lượng lên 1
            }
            return RedirectToAction("Details", "products", new { id = id_products });
        }
        private int caculate_total_quantity()
        {
            int quantity_total = 0;
            List<needtobuy> cart= makecart();
            if (cart != null)
            {
                quantity_total=cart.Sum(s => s.quantity);
            }
            return quantity_total;
        }
        private double caculate_total_price()
        {
            double total_price = 0;
            List<needtobuy> cart = makecart();
            if (cart != null)
                total_price = cart.Sum(s => s.price);
            return total_price;
        }
        public ActionResult showcart()
        {
            List<needtobuy> cart = makecart();
            //Nếu giỏ hàng trống thì trả về trang ban đầu
            if (cart == null || cart.Count == 0)
            {
                return RedirectToAction("Index", "products");
            }
            ViewBag.TongSL = caculate_total_quantity();
            ViewBag.TongTien = caculate_total_price();
            return View(cart); //Trả về View hiển thị thông tin giỏ hàng
        }
    }
}
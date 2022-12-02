﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace do_an_web.Models
{
    public class cartController : Controller
    {
        webClothesEntities database = new webClothesEntities();
        // GET: cart
        public ActionResult Index()
        {
            return View();
        }
        public List<needtobuy> makecart()
        {
            List<needtobuy> carts = Session["Cart"] as List<needtobuy>;
            //Nếu giỏ hàng chưa tồn tại thì tạo mới và đưa vào Session
            if (carts == null)
            {
                carts = new List<needtobuy>();
                Session["Cart"] = carts;
            }
            return carts;
        }
        public ActionResult addtocart(int id_product)
        {
            //Lấy giỏ hàng hiện tại
            List<needtobuy> carts = makecart();
            //Kiểm tra xem có tồn tại mặt hàng trong giỏ hay chưa
            //Nếu có thì tăng số lượng lên 1, ngược lại thêm vào giỏ
            needtobuy product = carts.FirstOrDefault(s => s.id_product == id_product);
            if (product == null) //Sản phẩm chưa có trong giỏ
            {
                product = new needtobuy(id_product);
                carts.Add(product);
            }
            else
            {
                product.quantity++; //Sản phẩm đã có trong giỏ thì tăng số lượng lên 1
            }
            return RedirectToAction("Details", "Home", new { id = id_product });
        }
        private int caculate_total_quantity()
        {
            int quantity_total = 0;
            List<needtobuy> carts = makecart();
            if (carts != null)
            {
                quantity_total = carts.Sum(s => s.quantity);
            }
            return quantity_total;
        }
        private double caculate_total_price()
        {
            double total_price = 0;
            List<needtobuy> carts = makecart();
            if (carts != null)
                total_price = carts.Sum(p=>p.total());
            return total_price;
        }
        public ActionResult showcart()
        {
            List<needtobuy> carts = makecart();
            //Nếu giỏ hàng trống thì trả về trang ban đầu
            if (carts == null || carts.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.TongSL = caculate_total_quantity();
            ViewBag.TongTien = caculate_total_price();
            return View(carts); //Trả về View hiển thị thông tin giỏ hàng
        }
        public ActionResult cartpartial()
        {
            ViewBag.TongSL = caculate_total_quantity();
            ViewBag.TongTien = caculate_total_price();
            return PartialView();
        }
        public ActionResult deleteproduct(int id_product)
        {
            List<needtobuy> gioHang = makecart();
            //Lấy sản phẩm trong giỏ hàng
            var sanpham = gioHang.FirstOrDefault(s => s.id_product == id_product);
            if (sanpham != null)
            {
                gioHang.RemoveAll(s => s.id_product == id_product);
                return RedirectToAction("showcart"); //Quay về trang giỏ hàng
            }
            if (gioHang.Count == 0) //Quay về trang chủ nếu giỏ hàng không có gì
                return RedirectToAction("Index", "Home");
            return RedirectToAction("showcart");
        }
        public ActionResult CapNhatMatHang(int id_product, int quantity)
        {
            List<needtobuy> gioHang = makecart();
            //Lấy sản phẩm trong giỏ hàng
            var sanpham = gioHang.FirstOrDefault(s => s.id_product == id_product);
            if (sanpham != null)
            {
                //Cập nhật lại số lượng tương ứng
                //Lưu ý số lượng phải lớn hơn hoặc bằng 1
                sanpham.quantity = quantity;
            }
            return RedirectToAction("showcart"); //Quay về trang giỏ hàng

        }
        public ActionResult DatHang()
        {           
            List<needtobuy> gioHang = makecart();
            if (gioHang == null || gioHang.Count == 0) //Chưa có giỏ hàng hoặc chưa có sp
                return RedirectToAction("Index", "Home");
            ViewBag.TongSL = caculate_total_quantity();
            ViewBag.TongTien = caculate_total_price();
            return RedirectToAction("HoanThanhDonHang"); //Trả về View hiển thị thông tin giỏ hàng
        }
        //Xác nhận đơn và lưu vào CSDL
        public ActionResult DongYDatHang()
        {
            customer khach = Session["TaiKhoan"] as customer; //Khách
            List<needtobuy> gioHang = makecart(); //Giỏ hàng
            customer_order DonHang = new customer_order(); //Tạo mới đơn đặt hàng
            DonHang.id_customer= khach.id_customer;
            DonHang.date_buy = DateTime.Now;
            DonHang.price = (float)caculate_total_price();
            DonHang.address_customer = khach.address_customer;
            DonHang.phone_customer = khach.phone_customer;  

            database.customer_order.Add(DonHang);
            database.SaveChanges();
            //Lần lượt thêm từng chi tiết cho đơn hàng trên
            foreach (var sanpham in gioHang)
            {
                details_order chitiet = new details_order();
                chitiet.id_order = DonHang.id_order;
                chitiet.id_products = sanpham.id_product;
                chitiet.quantity_order = sanpham.quantity;
                chitiet.unit_price = (float)sanpham.price;
                database.details_order.Add(chitiet);
            }
            database.SaveChanges();
            //Xóa giỏ hàng
            Session["Cart"] = null;
            return RedirectToAction("HoanThanhDonHang");
        }
        public ActionResult HoanThanhDonHang()
        {
            return View();
        }
    }
}
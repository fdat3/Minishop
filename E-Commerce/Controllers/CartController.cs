using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using E_Commerce.Models;
using Microsoft.Ajax.Utilities;

namespace E_Commerce.Controllers
{
    public class CartController : Controller
    {
        QuanLySanPhamEntities db = new QuanLySanPhamEntities();
        // GET: Cart
        public ActionResult CartView()
        {
            List<CartItem> lstCart = GetCart();
            return View(lstCart);
        }

        // Phuong thay lay gio hang
        public List<CartItem> GetCart()
        { 
            List<CartItem> lstCart = Session["CartItem"] as List<CartItem>;
            if(lstCart == null)
            {
                // Khoi tao gio hang neu gio hang chua ton tai
                lstCart = new List<CartItem>();
                Session["CartItem"] = lstCart;
            }
            return lstCart;
        }
        // Phuong thuc them gio hang
        public ActionResult AddCart(int MaSP)
        {
            //Check san pham da ton tai trong DB hay chua
            SanPham sp = db.SanPhams.SingleOrDefault(n => n.MaSP == MaSP);
            if (sp == null)
            {
                // Bao loi duong dan khong hop le
                Response.StatusCode = 404;
                return null;
            }
            // Lay gio hang
            List<CartItem> lstCart = GetCart();
            // Truong hop 1: San pham da co trong gio hang
            CartItem CheckItem = lstCart.SingleOrDefault(n => n.MaSP == MaSP);
            if(CheckItem != null)
            {
                if(sp.SoLuongTonKho < CheckItem.SoLuong) // Truong hop het han
                {
                    return null; // Update sau
                } 
                CheckItem.SoLuong++;
                CheckItem.TongTien = CheckItem.SoLuong * CheckItem.DonGia;
                ViewBag.TotalItem = TotalItem();
                return PartialView("CartPartial");
            }
            // Neu SP chua ton tai
            CartItem iCart = new CartItem(MaSP);
            // Them vao danh sach
            lstCart.Add(iCart);
            ViewBag.TotalItem = TotalItem();
            return PartialView("CartPartial");
        }

        public ActionResult RemoveCart(int MaSP)
        {
            if (Session["CartItem"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            SanPham sp = db.SanPhams.SingleOrDefault(n => n.MaSP == MaSP);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<CartItem> lstCart = GetCart();
            CartItem CheckItem = lstCart.SingleOrDefault(n => n.MaSP == MaSP);
            if (CheckItem == null)
            {
                return RedirectToAction("Index", "Home");
            }
            lstCart.Remove(CheckItem);
            return PartialView("CartPartial");
        }

        // Phuong thuc tong so tien phai thanh toan
        public decimal TotalBill()
        {
            List<CartItem> lstItem = Session["CartItem"] as List<CartItem>;
            if(lstItem == null)
            {
                return 0;
            }
            return lstItem.Sum(n => n.TongTien);
        }


        // Phuong thuc tinh tong so luong hang
        public double TotalItem()
        {
            // Lay gio hang 
            List<CartItem> lstItem = Session["CartItem"] as List<CartItem>;
            if(lstItem == null)
            {
                return 0;
            }
            return lstItem.Sum(n => n.SoLuong);

        }
        public ActionResult CartPartial()
        {
            if(TotalItem() == 0)
            {
                ViewBag.TotalItem = 0;
                ViewBag.TotalBill = 0;
                return PartialView();
            }
            ViewBag.TotalItem = TotalItem();
            ViewBag.TotalBill = TotalBill();
            return PartialView();
        }
        public ActionResult EditCart(int MaSP)
        {
            if (Session["CartItem"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            SanPham sp = db.SanPhams.SingleOrDefault(n => n.MaSP == MaSP);
            if(sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<CartItem> lstCart = GetCart();
            CartItem CheckItem = lstCart.SingleOrDefault(n => n.MaSP == MaSP);
            if(CheckItem == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(CheckItem);
        }

        public ActionResult Order()
        {
            // Check cart
            if (Session["CartItem"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            if (Session["TaiKhoan"] == null)
            {
                return RedirectToAction("DangNhap", "Home");
            } else
            {
            return RedirectToAction("CartView");

            }
            // Call Database
            DonDatHang ddh = new DonDatHang();
            ddh.NgayDatHang = DateTime.Now;
            ddh.TinhTrangDH = false;
            ddh.DaThanhToan = false;
            ddh.UuDai = 0;
            
            // Add to Database
            db.DonDatHangs.Add(ddh);
            //Sync
            db.SaveChanges();

            //Call GetCart Method
            List<CartItem> lstCart = GetCart();
            foreach(var item in lstCart)
            {
                ChiTietDonDatHang ctd = new ChiTietDonDatHang();
                ctd.MaDDH = ddh.MaDDH;
                ctd.MaSP = item.MaSP;
                ctd.TenSP = item.TenSP;
                ctd.SoLuong = item.SoLuong;
                ctd.DonGia = item.DonGia;
                //Add to Database
                db.ChiTietDonDatHangs.Add(ctd);
            }
            db.SaveChanges();
            Session["CartITem"] = null;
            return RedirectToAction("CartView");
        }
    }
}
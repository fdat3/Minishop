using E_Commerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Commerce.Areas.AdminPage.Controllers
{
    public class StatsController : Controller
    {
        QuanLySanPhamEntities db = new QuanLySanPhamEntities();
        // GET: AdminPage/Stats
        public ActionResult Index()
        {
            ViewBag.UserView = HttpContext.Application["PageView"].ToString(); //User View
            ViewBag.UserOnline = HttpContext.Application["UserOnline"].ToString(); //User Online
            ViewBag.TongDDH = ThongKeDonHang();
            ViewBag.SLTV = ThongKeThanhVien();
            ViewBag.Total = ThongKeTongDoanhThu();
            return View();
        }

        public double ThongKeDonHang()
        {
            double ddh = db.DonDatHangs.Count();
            return ddh;
        }

        public double ThongKeThanhVien()
        {
            double tv = db.ThanhViens.Count();
            return tv;
        }


        public decimal ThongKeTongDoanhThu()
        {
            //Tat ca so tien tu khi thanh lap
            decimal totalMoney = db.ChiTietDonDatHangs.Sum(n => n.SoLuong * n.DonGia).Value;
            return totalMoney;
        }

        public decimal DoanhThuTheoThangNam(int Thang, int Nam)
        {
            //Danh sach cac don dat hang trong thang/nam tuong ung
            var lsDH = db.DonDatHangs.Where(n => n.NgayDatHang.Value.Month == Thang && n.NgayDatHang.Value.Year == Nam);
            decimal TotalMoneyMY = 0;

            foreach(var item in lsDH)
            {
                TotalMoneyMY += decimal.Parse(item.ChiTietDonDatHangs.Sum(n => n.SoLuong * n.DonGia).Value.ToString());
            }
            return TotalMoneyMY;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using E_Commerce.Models;

namespace E_Commerce.Controllers
{
    public class KhachHangController : Controller
    {   
        // Khai báo mảng
        QuanLySanPhamEntities db = new QuanLySanPhamEntities();
        // GET: KhachHang

        public ActionResult OrtherWay()
        {
            //Truy vấn dữ liệu thông qua câu lệnh
            // Đối tượng lsKH lấy dữ liệu từ bảng KH
            var lsKH = db.KhachHangs;
            return View(lsKH);
        }

        public ActionResult GroupDuLieu()
        {
            //Truy vấn dữ liệu thông qua câu lệnh
            // Đối tượng lsKH lấy dữ liệu từ bảng KH
            List<ThanhVien> lsKH = db.ThanhViens.OrderBy(n => n.TaiKhoan).ToList();
            return View(lsKH);
        }
    }
}
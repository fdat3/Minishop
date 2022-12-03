using E_Commerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.Mvc;
using CaptchaMvc.HtmlHelpers;
using CaptchaMvc;

namespace E_Commerce.Controllers
{
    public class HomeController : Controller
    {
        QuanLySanPhamEntities db = new QuanLySanPhamEntities();
        // GET: Home
        public ActionResult Index()
        {
            var lsSPM = db.SanPhams.Where(n => n.HangMoi == 1);
            ViewBag.lsSPM = lsSPM;
            return View(lsSPM);
        }



        [HttpGet]
        public ActionResult DangKy()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DangKy(ThanhVien tv)
        {
            //Nếu khách hàng điền đầy đủ thông tin
            if (ModelState.IsValid)
            {
                //Tiến hành mã hóa
                tv.MatKhau = Hash.hashPassword(tv.MatKhau);
                //Thêm tài khoản vào DB ThanhVien
                db.ThanhViens.Add(tv);
                // Lưu các thay đổi
                db.SaveChanges();
                //Thông báo và đưa người dùng về trang chủ
                ViewBag.ThongBao = "Đăng ký thành công !";
            }
            return View();
        }

        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DangNhap(ThanhVien tv)
        {
            // Khởi tạo và kiểm tra thông tin tài khoản mật khẩu
            var currentAccount = db.ThanhViens.SingleOrDefault(n => n.TaiKhoan.Equals(tv.TaiKhoan));

            if (currentAccount != null)
            {
                if (Hash.validatePassword(tv.MatKhau, currentAccount.MatKhau))
                {
                    Session["TaiKhoan"] = tv;
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("DangNhap");
        }

        public ActionResult DangXuat()
        {

            Session["TaiKhoan"] = null;
            return RedirectToAction("Index");
        }
    }
}
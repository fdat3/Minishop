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
        public ActionResult DangKy(ThanhVien tv, FormCollection f)
        {
                if(ModelState.IsValid)
            {
                ViewBag.ThongBao = "Đăng ký thành công !";
                db.ThanhViens.Add(tv);
                db.SaveChanges();
            }
                return View();
        }

        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DangNhap(FormCollection f)
        {
            string sTaiKhoan = f["TaiKhoan"].ToString();
            string sMatKhau = f["MatKhau"].ToString();

            ThanhVien tv = db.ThanhViens.SingleOrDefault(n => n.TaiKhoan == sTaiKhoan && n.MatKhau == sMatKhau);
            if (tv == null)
            {
                ViewBag.ErrorMessage = "Sai ten dang nhap hoac mat khau !";
                return RedirectToAction("DangNhap");
            }
            Session["TaiKhoan"] = tv;
            return RedirectToAction("Index");
        }

        public ActionResult DangXuat()
        {
            Session["TaiKhoan"] = null;
            return RedirectToAction("Index");
        }
    }
}
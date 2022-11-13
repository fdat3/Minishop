using E_Commerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Commerce.Areas.Admin.Controllers
{
    public class IndexController : Controller
    {

        QuanLySanPhamEntities db = new QuanLySanPhamEntities();
        // GET: Admin/Index
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult DangKy()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DangKy(ThanhVien tv, FormCollection f)
        {
            if (ModelState.IsValid)
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
    }
}
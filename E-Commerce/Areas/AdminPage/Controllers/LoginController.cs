using E_Commerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Commerce.Areas.AdminPage.Controllers
{
    public class LoginController : Controller
    {
        QuanLySanPhamEntities db = new QuanLySanPhamEntities();
        // GET: AdminPage/Login
        public ActionResult Index()
        {
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
                    return RedirectToAction("Index", "Stats", new { Area = "AdminPage" });
                } else
                {
                    return RedirectToAction("Index", "Login", new { Area = "AdminPage" });
                }
            }
            return RedirectToAction("Index", "Stats", new { Area = "AdminPage"});
        }

        public ActionResult DangXuat()
        {

            Session["TaiKhoan"] = null;
            return RedirectToAction("Index");
        }
    }
}
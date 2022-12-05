using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using E_Commerce.Models;
using Microsoft.Ajax.Utilities;

namespace E_Commerce.Areas.AdminPage.Controllers
{
    public class EditorController : Controller
    {
        QuanLySanPhamEntities db = new QuanLySanPhamEntities();
        // GET: AdminPage/Editor
        [HttpGet]
        public ActionResult Index()
        {
            BaiViet post = new BaiViet();
            post.NgayDang = DateTime.Now;
            ThanhVien user = Session["TaiKhoan"] as ThanhVien;
            post.TenTG = user.TaiKhoan;
            return View(post);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Index(BaiViet post)
        {
            post.daDuyet = false;
            post.NgayDang = DateTime.Now;
            ThanhVien user = Session["TaiKhoan"] as ThanhVien;
            post.TenTG = user.TaiKhoan;
            db.BaiViets.Add(post);
            db.SaveChanges();
            return View(post);
        }
    }
}
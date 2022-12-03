using E_Commerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace E_Commerce.Controllers
{
    public class CommentController : Controller
    {
        QuanLySanPhamEntities db = new QuanLySanPhamEntities();
        // GET: Comment
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PostComment(BinhLuan cmt)
        {
            ThanhVien tv = Session["TaiKhoan"] as ThanhVien;
            cmt.MaTV = tv.MaTV;
            cmt.ThoiGian = DateTime.Now;
            db.BinhLuans.Add(cmt);
            db.SaveChanges();
            return RedirectToAction("XemChiTiet", "SanPham");
        }
    }
}


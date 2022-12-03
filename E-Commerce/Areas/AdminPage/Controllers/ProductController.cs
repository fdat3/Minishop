using E_Commerce.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Commerce.Areas.AdminPage.Controllers
{
    public class ProductController : Controller
    {
        // GET: AdminPage/Product
        QuanLySanPhamEntities db = new QuanLySanPhamEntities();
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]

        public ActionResult NewProduct()
        {
            ViewBag.MaNCC = new SelectList(db.NhaCungCaps.OrderBy(n => n.TenNCC), "MaNCC", "TenNCC");
            ViewBag.MaLoaiSP = new SelectList(db.LoaiSanPhams.OrderBy(n => n.MaLoaiSP), "MaLoaiSP", "TenLoaiSP");
            ViewBag.MaNSX = new SelectList(db.NhaSanXuats.OrderBy(n => n.MaNSX), "MaNSX", "TenNSX");


            return View();
        }

        [HttpPost]
        public ActionResult NewProduct(HttpPostedFileBase HinhAnh, SanPham sp)
        {
            //Check img already
            if (HinhAnh.ContentLength > 0)
            {
                //Take img name
                var fileName = Path.GetFileName(HinhAnh.FileName);
                //Take img and move to img's folder
                var path = Path.Combine(Server.MapPath("~/Content/images/SanPham"), fileName);
                //If img already, noti to user
                if (System.IO.File.Exists(path))
                {
                    ViewBag.upload = "Image already !";
                }
                else
                {
                    //Them anh vao thu muc
                    HinhAnh.SaveAs(path);
                    sp.HinhAnh = fileName;
                    return View();
                }
            }
            db.SanPhams.Add(sp);
            db.SaveChanges();
            return RedirectToAction("Index", "Dashboard");
        }
    }
}
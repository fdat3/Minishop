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
        public ActionResult NewProduct(HttpPostedFileBase[] HinhAnh, SanPham sp)
        {
            int err = 0;
            for(int i = 0; i < HinhAnh.Count(); i++)
            {
                if (HinhAnh[i] != null)
                {
                    if (HinhAnh[i].ContentLength > 0)
                    {
                        if (HinhAnh[i].ContentType != "image/jpeg" && HinhAnh[i].ContentType != "image/png")
                        {
                            ViewBag.upload = "Error";
                            err++;
                        }
                        else
                        {
                            var fileName = Path.GetFileName(HinhAnh[0].FileName);
                            //Take img and move to img's folder
                            var path = Path.Combine(Server.MapPath("~/Content/images/SanPham"), fileName);
                            sp.HinhAnh = fileName;
                            //Check img already
                            if (System.IO.File.Exists(path))
                            {
                                ViewBag.already = "Image Exist";
                                err++;
                            }
                        }
                    }
                }
            }
           
            db.SanPhams.Add(sp);
            db.SaveChanges();
            return RedirectToAction("Index", "Dashboard");
        }
    }
}
using E_Commerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.Net;

namespace E_Commerce.Controllers
{
    public class SanPhamController : Controller
    {
        QuanLySanPhamEntities db = new QuanLySanPhamEntities();

        // Tao Partial View San Pham
        // GET: SanPham
        public ActionResult SanPhamParticial()
        {
            return PartialView();
        }
        // GET: SanPham
        public ActionResult SanPhamParticialDeal()
        {
            return PartialView();
        }
        //Trang xem san pham chi tiet
        [HttpGet]
        public ActionResult XemChiTiet(int? id, BinhLuan review)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var product = db.SanPhams.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            //Add comment
            review.MaSP = product.MaSP;
            ViewData["product"] = product;
            return View("XemChiTiet", review);
        }
        [HttpPost]
        public ActionResult postComment(BinhLuan review, double? rating)
        {
            ThanhVien tv = Session["TaiKhoan"] as ThanhVien;
            review.ThoiGian = DateTime.Now;
            review.MaTV = tv.MaTV;
            review.Rating = rating;
            db.BinhLuans.Add(review);
            db.SaveChanges();
            return RedirectToAction("XemChiTiet", "SanPham", new { id = review.MaSP });
        }

        public ActionResult Shop(int? page)
        {
            var lsSP = db.SanPhams;
            //ViewBag.lsSP = lsSP;
            //List<SanPham> lsp = Product.getProduct();
            //Number of item in 1 page
            int PageSize = 9;
            int PageNumber = (page ?? 1);

            return View(lsSP.OrderBy(n => n.MaSP).ToPagedList(PageNumber, PageSize));
        }
    }
}
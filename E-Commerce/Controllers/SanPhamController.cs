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
        [ChildActionOnly]
        // GET: SanPham
        public ActionResult SanPhamParticial ()
        {
            return PartialView();
        }

        [ChildActionOnly]
        // GET: SanPham
        public ActionResult SanPhamParticialDeal()
        {
            return PartialView();
        }
        //Trang xem san pham chi tiet
        public ActionResult XemChiTiet(int? id, string tensp)
        {
            if(id == null) {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            SanPham sp = db.SanPhams.SingleOrDefault(n => n.MaSP == id);
            if(sp == null)
            {
                return HttpNotFound();
            }

            return View(sp);
        }
        
        public JsonResult ListName(string value)
        {
            var data = new SanPham().ListName(value);
            return Json(new
            {
                data = data,
                status = true
            },JsonRequestBehavior.AllowGet);
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
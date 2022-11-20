using E_Commerce.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Commerce.Controllers
{
    public class SearchController : Controller
    {
        QuanLySanPhamEntities db = new QuanLySanPhamEntities();
        // GET: Search
        public ActionResult SearchResult(string keyword)
        {
            var lsSP = db.SanPhams.Where(n => n.TenSP.Contains(keyword));
            ViewBag.keyword = keyword;
            return View(lsSP);
        }

        public ActionResult MiniSearch(int? MaLoaiSP)
        {
            List<SanPham> lstSP = Product.getProductByType(MaLoaiSP);
            return View(lstSP);
        }

        [HttpPost]
        public ActionResult TakeKeyword(string keyword)
        {
            ViewBag.keyword = keyword;
            return RedirectToAction("SearchResult", new { @keyword = keyword });
        }
    }
}
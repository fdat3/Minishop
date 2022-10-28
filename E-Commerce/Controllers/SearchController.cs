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
        public ActionResult SearchResult(string keyword, int? page)
        {
            if(Request.HttpMethod != "GET")
            {
                page = 1;
            }

            int pageSize = 10;
            int pageNumber = page ?? 1;
            var lsSP = db.SanPhams.Where(n => n.TenSP.Contains(keyword));
            ViewBag.keyword = keyword;
            return PartialView(lsSP.OrderBy(n => n.TenSP).ToPagedList(pageNumber,pageSize));
        }

        public ActionResult MiniSearch(int? MaLoaiSP, int? MaNSX)
        {
            var lstSP = db.SanPhams.Where(n => n.MaLoaiSP == MaLoaiSP && n.MaNSX == MaNSX);
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
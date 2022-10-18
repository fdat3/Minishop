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
            return View(lsSP.OrderBy(n => n.TenSP).ToPagedList(pageNumber,pageSize));
        }
        [HttpPost]
        public ActionResult TakeKeyword(string keyword)
        {
            return RedirectToAction("SearchResult", new { @keyword = keyword });
        }
    }
}
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
        public ActionResult SearchResult(string search)
        {
            var lsSP = db.SanPhams.Where(n => n.TenSP.Contains(search));
            ViewBag.keyword = search;
            return View(lsSP);
        }

        public ActionResult MiniSearch(int? MaLoaiSP)
        {
            List<SanPham> lstSP = Product.getProductByType(MaLoaiSP);
            return View(lstSP);
        }

        //AutoComplete

        public JsonResult GetSearchValue(string search)
        {
            QuanLySanPhamEntities db = new QuanLySanPhamEntities();
            List<Product> allsearch = db.SanPhams.Where(n => n.TenSP.Contains(search)).Select(n => new Product
            {
                MaSP = n.MaSP,
                TenSP = n.TenSP
            }).ToList();
            return new JsonResult { Data = allsearch, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }


        /*public JsonResult GetValueSearch(string search)
        {
            QuanLySanPhamEntities db = new QuanLySanPhamEntities();
            List<Product> allItem = db.SanPhams.Where(n => n.TenSP.Contains(search))
            .Select(n => new Product
            {
                MaSP = n.MaSP,
                TenSP = n.TenSP
            }).ToList();
            return new JsonResult { Data = allItem, JsonRequestBehavior = JsonRequestBehavior.AllowGet }; 
        }*/

        [HttpPost]
        public ActionResult TakeKeyword(string search)
        {
            ViewBag.keyword = search;
            return RedirectToAction("SearchResult", new { @keyword = search });
        }
    }
}
using E_Commerce.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace E_Commerce.Controllers
{
    public class BlogController : Controller
    {
        QuanLySanPhamEntities db = new QuanLySanPhamEntities();
        // GET: Blog
        public ActionResult Index()
        {
            List<BaiViet> lPost = db.BaiViets.ToList();
            //ViewBag.lsSP = lsSP;
            //List<SanPham> lsp = Product.getProduct();
            //Number of item in 1 page

            return View("Index", lPost);
        }

        public ActionResult blogDetail(int maBV)
        {
            if (maBV != null)
            {
                BaiViet bv = db.BaiViets.Where(n => n.MaBV.Equals(maBV)).First<BaiViet>();
                ViewData["post"] = bv;
            }
            return View("blogDetail");
        }
    }
}
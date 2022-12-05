using E_Commerce.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Commerce.Controllers
{
    public class BlogController : Controller
    {
        QuanLySanPhamEntities db = new QuanLySanPhamEntities();
        // GET: Blog
        public ActionResult Index(int? page)
        {
            var lPost = db.BaiViets;
            //ViewBag.lsSP = lsSP;
            //List<SanPham> lsp = Product.getProduct();
            //Number of item in 1 page
            int PageSize = 9;
            int PageNumber = (page ?? 1);

            return View(lPost.OrderBy(n => n.MaBV).ToPagedList(PageNumber, PageSize));
        }

        public ActionResult blogDetail(int? maBV)
        {
            if(maBV == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            BaiViet post = db.BaiViets.SingleOrDefault(n => n.MaBV == maBV);
            if(post == null)
            {
                return HttpNotFound();

            }
            return View(post);
        }
    }
}
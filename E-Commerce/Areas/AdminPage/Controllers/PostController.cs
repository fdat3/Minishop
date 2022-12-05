using E_Commerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Commerce.Areas.AdminPage.Controllers
{
    public class PostController : Controller
    {
        // GET: AdminPage/Post
        private static QuanLySanPhamEntities db = new QuanLySanPhamEntities();
        private static bool daDuyet;
        [HttpGet]
        public ActionResult Index(string isActive)
        {
            daDuyet = isActive != null &&  isActive.Equals("1");
            updatePost();
            return View();
        }
        [HttpPost]

        public ActionResult deletePost(int maBV)
        {
            BaiViet bv = db.BaiViets.Find(maBV);
            db.BaiViets.Remove(bv);
            db.SaveChanges();
            updatePost();
            return View("Index");
        }
        [HttpPost]

        public ActionResult activePost(int maBV)
        {
            BaiViet bv = db.BaiViets.Find(maBV);
            bv.daDuyet = !daDuyet;
            db.SaveChanges();
            updatePost();
            return View("Index");
        }
        private void updatePost()
        {
            List<BaiViet> post = db.BaiViets.Where(n => n.daDuyet == daDuyet).ToList<BaiViet>();
            ViewData["ListPost"] = post;
        }
    }
}
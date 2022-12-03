using E_Commerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Commerce.Areas.AdminPage.Controllers
{
    public class ListProductController : Controller
    {
        QuanLySanPhamEntities db = new QuanLySanPhamEntities();
        // GET: AdminPage/ListProduct
        public ActionResult Index()
        {
            return View();
        }


    }
}
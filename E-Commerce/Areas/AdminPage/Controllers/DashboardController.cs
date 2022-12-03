using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Commerce.Areas.AdminPage.Controllers
{
    public class DashboardController : Controller
    {
        // GET: AdminPage/Dashboard
        public ActionResult Index()
        {
            return View();
        }
    }
}
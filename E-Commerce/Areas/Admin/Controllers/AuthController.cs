using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Commerce.Areas.Admin.Controllers
{
    public class AuthController : Controller
    {
        // GET: Admin/Auth
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {

            return View();
        }

        public ActionResult Register()
        {
            
            return View();
        }

        public ActionResult ForgetPW()
        {
            return View();
        }
    }
}
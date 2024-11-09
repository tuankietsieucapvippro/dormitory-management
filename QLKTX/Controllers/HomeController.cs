using QLKTX.Common;
using QLKTX.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLKTX.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Session["Username"] != null) // Sử dụng Session
                                             // Hoặc có thể sử dụng Cookie
                                             // if (Request.Cookies["Username"] != null)
            {
                ViewBag.UserName = Session["Username"]; // Sử dụng Session
                                                        // Hoặc có thể sử dụng Cookie
                                                        // ViewBag.UserName = Request.Cookies["Username"].Value;
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

    }
}
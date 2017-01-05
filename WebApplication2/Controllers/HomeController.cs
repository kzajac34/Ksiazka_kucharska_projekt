using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Informacje o stronie";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Informacje kontaktowe";

            return View();
        }
    }
}
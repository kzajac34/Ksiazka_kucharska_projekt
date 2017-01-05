using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication2.Controllers
{
    [Authorize]
    public class MemberController : Controller
    {
        //główna po zalogowaniu
        public ActionResult MemberView()
        {
            return View();
        }
        //dodawanie przepisu
        public ActionResult AddRecView()
        {
            return View();
        }
        //przepisy urzytkownika
        public ActionResult YourRecView()
        {
            return View();
        }
        //komentarze użytkownika
        public ActionResult YourCommView()
        {
            return View();
        }
    }
}
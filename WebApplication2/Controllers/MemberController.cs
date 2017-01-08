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
        Models.Entities db = new Models.Entities();
        //główna po zalogowaniu
        public ActionResult MemberView()
        {
            return View(db.Recipes.ToList());
        }
        //dodawanie przepisu
        [HttpGet]
        public ActionResult AddRecView()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddRecView(Models.Recipes recipe)
        {
            if (ModelState.IsValid)
            {
                db.Recipes.Add(recipe);
                db.SaveChanges();
                return RedirectToAction("MemberView");
            }

            return View(recipe);
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
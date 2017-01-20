using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Net;

namespace WebApplication2.Controllers
{
    [Authorize]
    public class MemberController : Controller
    {
        Models.Entities db = new Models.Entities();
        //główna po zalogowaniu
        public ActionResult MemberView()
        {
            ViewBag.Category = db.Category.ToList();
            ViewBag.Users = db.AspNetUsers.ToList();
            return View(db.Recipes.ToList());
        }
        //dodawanie przepisu
        [HttpGet]
        public ActionResult AddRecView()
        {
            ViewBag.Category = db.Category.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult AddRecView(Models.Recipes recipe, HttpPostedFileBase image1)
        {

            if (ModelState.IsValid && image1!= null)
            {
                recipe.user_ID = User.Identity.GetUserId();
                recipe.recipe_date = DateTime.Now;
                recipe.picture = new byte[image1.ContentLength];
                image1.InputStream.Read(recipe.picture, 0, image1.ContentLength);
                db.Recipes.Add(recipe);
                db.SaveChanges();
                return RedirectToAction("MemberView");
            }

            return View(recipe);
        }
        //przepisy urzytkownika
        public ActionResult YourRecView()
        {
            var currentUserId = User.Identity.GetUserId();
            var recUserDb = db.Recipes.Where(c => c.user_ID == currentUserId);
            return View(recUserDb);
        }

        [HttpGet]
        public ActionResult Delete(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var recipe = db.Recipes.Find(Id);
            if (recipe == null)
            {
                return HttpNotFound();
            }
            return View(recipe);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int Id)
        {
            var recipe = db.Recipes.Find(Id);
            db.Recipes.Remove(recipe);
            db.SaveChanges();
            return RedirectToAction("YourRecView");
        }


        //komentarze użytkownika
        public ActionResult YourCommView()
        {
            return View();
        }
    }
}
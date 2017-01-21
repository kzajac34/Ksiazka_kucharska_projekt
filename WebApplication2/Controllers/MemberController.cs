using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Net;
using System.Data.Entity;
using System.Text;

namespace WebApplication2.Controllers
{
    [Authorize]
    public class MemberController : Controller
    {
        Models.Entities db = new Models.Entities();
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

            if (ModelState.IsValid)
            {
                
                recipe.user_ID = User.Identity.GetUserId();
                recipe.recipe_date = DateTime.Now;
                if (image1 != null)
                {
                    recipe.picture = new byte[image1.ContentLength];
                    image1.InputStream.Read(recipe.picture, 0, image1.ContentLength);
                }
                else
                recipe.picture = Encoding.ASCII.GetBytes("0000");
                db.Recipes.Add(recipe);
                db.SaveChanges();
                
            }

            return RedirectToAction("RecipesView", "Home");
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
            ViewBag.Users = db.AspNetUsers;
            ViewBag.Category = db.Category;
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


        //edycja
        public ActionResult Edit(int id =0)
        {
            var recipe = db.Recipes.Find(id);
            ViewBag.Category = db.Category.ToList();
            if (recipe == null)
            {
                return HttpNotFound();
            }
            return View(recipe);
        }
        [HttpPost]
        public ActionResult Edit(Models.Recipes recipe, HttpPostedFileBase image1)
        {
            if(ModelState.IsValid )
            {
                recipe.recipe_date = DateTime.Now;
                if (image1 != null)
                {
                    recipe.picture = new byte[image1.ContentLength];
                    image1.InputStream.Read(recipe.picture, 0, image1.ContentLength);
                    
                }
                db.Entry(recipe).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("YourRecView");
            }
            return View("YourRecView");

        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;


namespace WebApplication2.Controllers
{
    public class modelClass
    {
        [Key]
        public string selectedRecipe { get; set; }
        public Models.Entities db { get; set; }
    }

    public class HomeController : Controller
    {
        Models.Entities db = new Models.Entities();

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

        public ActionResult RecipesView()
        {
            ViewBag.Category = db.Category.ToList();
            ViewBag.Users = db.AspNetUsers.ToList();

            var modelDB = new modelClass();
            
            modelDB.db = db;
            return View(modelDB);
        }
        public ActionResult _partial(int id)
        {
            var modelDB = new modelClass();
            modelDB.selectedRecipe = id.ToString();
            modelDB.db = db;
            return PartialView(modelDB);
        }
    }
}
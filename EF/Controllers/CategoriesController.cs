using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EF.Models;

namespace EF.Controllers
{
    public class CategoriesController : Controller
    {
        // GET: Categories
        public ActionResult Index()
        {
            EFStoreEntities db = new EFStoreEntities();
            List<Category> categories = db.Categories.ToList();

            return View(categories);
        }
    }
}
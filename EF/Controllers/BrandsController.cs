using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EF.Models;


namespace EF.Controllers
{
    public class BrandsController : Controller
    {
        // GET: Brand
        public ActionResult Index()
        {
            EFStoreEntities db = new EFStoreEntities();
            List<Brand> brands = db.Brands.ToList();

            return View(brands);
        }
    }
}
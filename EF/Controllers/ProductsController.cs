using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EF.Models;

namespace EF.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Products
        public ActionResult Index(string search = "", string sortColumn = "ProductID", string sortBy = "asc", int page = 1)
        {
            EFStoreEntities db = new EFStoreEntities();

            // Search 
            List<Product> products = db.Products.Where(product => product.ProductName.Contains(search)).ToList();
            ViewBag.search = search;

            // Sort
            switch(sortColumn)
            {
                case "ProductID":
                    if (sortBy == "asc")
                    {
                        products = products.OrderBy(prod => prod.ProductID).ToList();
                    } else
                    {
                        products = products.OrderByDescending(prod => prod.ProductID).ToList();
                    }
                    break;
                default:
                    break;
            }

            ViewBag.sortBy = sortBy;
            ViewBag.sortColumn = sortColumn;

            // Pagination 
            int numRecordPerPage = 5;
            int recordSize = products.Count;
            int pageSize = Convert.ToInt32(Math.Ceiling((decimal)recordSize / numRecordPerPage));
            // page 1: get 5 record -> page 2: skip 5 -> page 2: skip 10
            int numRecordToSkip = (page - 1) * numRecordPerPage;

            ViewBag.currentPage = page;    
            ViewBag.pageSize = pageSize;

            products = products.Skip(numRecordToSkip).Take(numRecordPerPage).ToList();

            return View(products);
        }

        public ActionResult Details (int id)
        {
            EFStoreEntities db = new EFStoreEntities();
            Product product = db.Products.Where(prod => prod.ProductID == id).FirstOrDefault();
            return View(product);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create (Product product)
        {
            EFStoreEntities db = new EFStoreEntities();
            db.Products.Add(product);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            EFStoreEntities db = new EFStoreEntities();
            Product product = db.Products.Where(prod => prod.ProductID == id).FirstOrDefault();
            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(int id, Product pro)
        {
            EFStoreEntities db = new EFStoreEntities();
            Product product = db.Products.Where(prod => prod.ProductID == id).FirstOrDefault();

            // Update
            product.ProductName = pro.ProductName;
            product.ProductID = pro.ProductID;
            product.Price = pro.Price;
            product.DateOfPurchase = pro.DateOfPurchase;
            product.CategoryID = pro.CategoryID;
            product.BrandId = pro.BrandId;
            product.Active = pro.Active;

            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            EFStoreEntities db = new EFStoreEntities();
            Product product = db.Products.Where(prod => prod.ProductID == id).FirstOrDefault();


            return View(product);
        }

        [HttpPost]
        public ActionResult Delete(int id, Product pro)
        {
            EFStoreEntities db = new EFStoreEntities();
            Product product = db.Products.Where(prod => prod.ProductID == id).FirstOrDefault();

            // Remove
            db.Products.Remove(product);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
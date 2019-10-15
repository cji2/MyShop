using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Contracts;
using MyShop.Core.Models;

namespace MyShop.WebUI.Controllers
{
    public class HomeController : Controller
    {

        // the following has the property, List<Product> products.
        IRepository<Product> context;
        //InMemoryRepository<Product> context;
        //ProductRepository context;

        // newly added following has the property, List<ProductCategory> productCategories
        IRepository<ProductCategory> productCategories;
        //ProductCategoryRepository productCategories;

        // initialize the repository, which will call the constructor, ProductRepository()
        public HomeController(IRepository<Product> productContext, IRepository<ProductCategory> productCategoryContext)
        {
            /* the class, ProductRepository has the property, List<Product> products. */
            context = productContext;
            //context = new InMemoryRepository<Product>();

            // newly added following has the property, List<ProductCategory> productCategories
            productCategories = productCategoryContext;
            //productCategories = new InMemoryRepository<ProductCategory>();
        }

        public ActionResult Index()
        {
            /* method Collection() returns IQueryable<Product>. */
            List<Product> products = context.Collection().ToList();
            return View(products);
        }

        public ActionResult Details(string Id)
        {
            Product product = context.Find(Id);
            if (product == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(product);
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
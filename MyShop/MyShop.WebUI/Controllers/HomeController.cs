using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.Core.ViewModels;

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

        /* the followiing parameter allows user to choose a category.
         And null value of Category allows for category not to have any values. */
        public ActionResult Index(string Category=null)
        {
            /* create empty list of products */
            List<Product> products;
            /* method Collection() returns IQueryable<Product>. */
            //List<Product> products = context.Collection().ToList();

            List<ProductCategory> categories = productCategories.Collection().ToList();

            if (Category == null)
            {   /* we display all products */
                products = context.Collection().ToList();
            }
            else
            {
                /* we display the products of category that user chosen. */
                products = context.Collection().Where(p => p.Category == Category).ToList();
            }

            /* using the following class, ProductListViewModel allows us to combine products with category,
             since it has both Products and ProductCategories properties. */
            ProductListViewModel model = new ProductListViewModel();
            model.Products = products;
            model.ProductCategories = categories;

            return View(model);
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
using MyShop.Core.Models;
using MyShop.Core.Contracts;
using MyShop.Core.ViewModels;
using MyShop.DataAccess.InMemory;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
        // the following has the property, List<Product> products.
        IRepository<Product> context;
        //InMemoryRepository<Product> context;
        //ProductRepository context;

        // newly added following has the property, List<ProductCategory> productCategories
        IRepository<ProductCategory> productCategories;
        //ProductCategoryRepository productCategories;

        // initialize the repository, which will call the constructor, ProductRepository()
        public ProductManagerController(IRepository<Product> productContext, IRepository<ProductCategory> productCategoryContext)
        {
            /* the class, ProductRepository has the property, List<Product> products. */
            context = productContext;
            //context = new InMemoryRepository<Product>();

            // newly added following has the property, List<ProductCategory> productCategories
            productCategories = productCategoryContext;
            //productCategories = new InMemoryRepository<ProductCategory>();
        }

        // GET: ProductManager
        public ActionResult Index()
        {
            /* method Collection() returns IQueryable<Product>. */
            List<Product> products = context.Collection().ToList();
            return View(products);
        }

        public ActionResult Create()
        {
            /* newly added class, ProductManagerViewModel has the property
                    1) Product Product  2) IEnumerable<ProductCategory> ProductCategories */
            ProductManagerViewModel viewModel = new ProductManagerViewModel();

            // the following is refactored.
            viewModel.Product = new Product();

            /* method Collection() returns IQueryable<ProductCategory>. */
            viewModel.ProductCategories = productCategories.Collection();

            return View(viewModel);
            //return View(product);

        }
        /* this is overwriting method with the above, Create() 
        we use [HttpPost] when we post & submit a data. */
        [HttpPost]
        public ActionResult Create(Product product, HttpPostedFileBase file)
        {

            if (!ModelState.IsValid)
            {
                return View(product);
            }
            else
            {

                if (file != null)
                {
                    product.Image = product.Id + Path.GetExtension(file.FileName);
                    file.SaveAs(Server.MapPath("//Content//ProductImages//") + product.Image);
                }

                // Insert() method was defined by DataAccess.InMemory.ProductRepository class.
                context.Insert(product);
                // Commit() method will store data at cache, which was defined by ProductRepository.
                context.Commit();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(string id)
        {
            Product product = context.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            else
            {
                /* newly added class, ProductManagerViewModel has the property
                    1) Product Product  2) IEnumerable<ProductCategory> ProductCategories */
                ProductManagerViewModel viewModel = new ProductManagerViewModel();

                // the following is refactored.
                viewModel.Product = product;

                /* method Collection() returns IQueryable<ProductCategory>. */
                viewModel.ProductCategories = productCategories.Collection();

                return View(viewModel);
                //return View(product);
            }
        }

        [HttpPost]
        public ActionResult Edit(Product product, string id, HttpPostedFileBase file)
        //public ActionResult Edit(Product product, string id)
        {
            Product productToEdit = context.Find(id);

            if (productToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(product);
                }
                if (file != null)
                {
                    productToEdit.Image = productToEdit.Id + Path.GetExtension(file.FileName);
                    file.SaveAs(Server.MapPath("//Content//ProductImages//") + productToEdit.Image);
                }

                productToEdit.Name = product.Name;
                productToEdit.Description = product.Description;
                productToEdit.Price = product.Price;
                productToEdit.Category = product.Category;
                //productToEdit.Image = product.Image;
                context.Commit();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(string id)
        {

            Product productToDelete = context.Find(id);

            if (productToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productToDelete);
            }
        }

        [HttpPost]
        // rename the action.
        [ActionName("Delete")]
        public ActionResult ConfirmDeleteProduct(string id)
        {

            Product productTodelete = context.Find(id);

            if (productTodelete == null)
            {
                return HttpNotFound();
            }
            context.Delete(id);
            // save the action at cache.
            context.Commit();
            return RedirectToAction("Index");
        }

    }
}
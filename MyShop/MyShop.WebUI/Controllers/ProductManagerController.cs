using MyShop.Core.Models;
using MyShop.DataAccess.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
        ProductRepository context;

        // initialize the repository, which will call the constructor, ProductRepository()
        public ProductManagerController()
        {
            context = new ProductRepository();
        }

        // GET: ProductManager
        public ActionResult Index()
        {
            List<Product> products = context.Collection().ToList();
            return View(products);
        }

        public ActionResult Create()
        {
            Product product = new Product();
            return View(product);
        }
        /* this is overwriting method with the above, Create() 
        we use [HttpPost] when we post & submit a data. */
        [HttpPost]
        public ActionResult Create(Product product)
        {

            if (!ModelState.IsValid)
            {
                return View(product);
            }
            else
            {
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
                return View(product);
            }
        }

        [HttpPost]
        public ActionResult Edit(Product product, string id)
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
                productToEdit.Name = product.Name;
                productToEdit.Description = product.Description;
                productToEdit.Price = product.Price;
                productToEdit.Category = product.Category;
                productToEdit.Image = product.Image;
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
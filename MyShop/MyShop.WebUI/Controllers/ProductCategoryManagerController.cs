using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Models;
using MyShop.DataAccess.InMemory;
using MyShop.Core.Contracts;

namespace MyShop.WebUI.Controllers
{
    public class ProductCategoryManagerController : Controller
    {
        IRepository<ProductCategory> context;
        //InMemoryRepository<ProductCategory> context;
        //ProductCategoryRepository context;

        // we inject 'IRepository<ProductCategory> into constructor.
        public ProductCategoryManagerController(IRepository<ProductCategory> context)
        {
            this.context = context;
            //context = new InMemoryRepository<ProductCategory>();
            //context = new ProductCategoryRepository();
        }

        // GET: ProductCategoryManager
        public ActionResult Index()
        {
            List<ProductCategory> productCategories = context.Collection().ToList();
            return View(productCategories);
        }

        public ActionResult Create()
        {
            ProductCategory productCategory = new ProductCategory();
            return View(productCategory);
        }
        /* this is overwriting method with the above, Create() 
        we use [HttpPost] when we post & submit a data. */
        [HttpPost]
        public ActionResult Create(ProductCategory productCategory)
        {

            if (!ModelState.IsValid)
            {
                return View(productCategory);
            }
            else
            {
                // Insert() method was defined by DataAccess.InMemory.ProductRepository class.
                context.Insert(productCategory);
                // Commit() method will store data at cache, which was defined by ProductRepository.
                context.Commit();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(string id)
        {
            ProductCategory productCategory = context.Find(id);
            if (productCategory == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productCategory);
            }
        }

        [HttpPost]
        public ActionResult Edit(ProductCategory productCategory, string id)
        {
            ProductCategory categoryToEdit = context.Find(id);

            if (categoryToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(productCategory);
                }
                categoryToEdit.Category = productCategory.Category;
                context.Commit();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(string id)
        {

            ProductCategory categoryToDelete = context.Find(id);

            if (categoryToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(categoryToDelete);
            }
        }

        [HttpPost]
        // rename the action.
        [ActionName("Delete")]
        public ActionResult ConfirmDeleteProductCategory(string id)
        {

            ProductCategory categoryToDelete = context.Find(id);

            if (categoryToDelete == null)
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
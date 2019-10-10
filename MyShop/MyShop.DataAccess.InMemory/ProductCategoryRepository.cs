using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using MyShop.Core.Models;

namespace MyShop.DataAccess.InMemory
{
    public class ProductCategoryRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<ProductCategory> productCategories;

        // adding constructor.
        public ProductCategoryRepository()
        {
            productCategories = cache["productCategories"] as List<ProductCategory>;
            if (productCategories == null)
            {
                productCategories = new List<ProductCategory>();
            }
        }
        public void Commit()
        {
            // this will store the data in cache.
            cache["productCategories"] = productCategories;
        }
        public void Insert(ProductCategory c)
        {
            productCategories.Add(c);
        }
        public void Update(ProductCategory productCategory)
        {
            ProductCategory categoryToUpdate = productCategories.Find(c => c.Id == productCategory.Id);

            if (categoryToUpdate != null)
            {
                categoryToUpdate = productCategory;
            }
            else
            {
                throw new Exception("Product Cateogry not found");
            }
        }
        public ProductCategory Find(string Id)
        {
            ProductCategory productCategory = productCategories.Find(c => c.Id == Id);

            if (productCategory != null)
            {
                return productCategory;
            }
            else
            {
                throw new Exception("Product Category not found");
            }
        }
        public IQueryable<ProductCategory> Collection()
        {
            return productCategories.AsQueryable();
        }
        public void Delete(string Id)
        {
            ProductCategory categoryToDelete = productCategories.Find(c => c.Id == Id);

            if (categoryToDelete != null)
            {
                productCategories.Remove(categoryToDelete);
            }
            else
            {
                throw new Exception("Product Category not found");
            }
        }

    }
}

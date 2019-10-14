using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyShop.Core.Models;

namespace MyShop.DataAccess.SQL
{
    public class DataContext : DbContext
    {
        /* DefaultConnection is the name of connectionStrings element of Web.config file on MyShop.WebUI project. */
        public DataContext()
            : base("DefaultConnection") {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
    }
}

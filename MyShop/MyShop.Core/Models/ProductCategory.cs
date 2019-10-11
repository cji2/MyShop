using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.Models
{
    public class ProductCategory: BaseEntity
    {
        // the following will be removed, since BaseEntity already has it.
        // public string Id { get; set; }
        public string Category { get; set; }

        // the following is constructor.
        // the following will be removed, since BaseEntity already has it.
        /*
        public ProductCategory()
        {
            this.Id = Guid.NewGuid().ToString();
        }
        */
    }
}

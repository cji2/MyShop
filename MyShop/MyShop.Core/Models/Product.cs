using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.Models
{
    public class Product : BaseEntity
    {
        // the following will be removed, since BaseEntity already has it.
        //public string Id { get; set; }

        [StringLength(20)]
        [DisplayName("Product Name")]
        public string Name { get; set; }

        public string Description { get; set; }
        public string Specification { get; set; }

        [Range(0, 1000)]
        public decimal Price { get; set; }
        public string Category { get; set; }
        public string Image { get; set; }

        // the following is constructor, which will automatically generate product id.
        // the following will be removed, since BaseEntity already has it.
        /*
        public Product()
        {
            this.Id = Guid.NewGuid().ToString();

        }
        */
    }
}
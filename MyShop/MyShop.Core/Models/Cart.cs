using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.Models
{
    public class Cart : BaseEntity
    {
        /* the keyword, virtual will implement 'lazy loading' */
        public virtual ICollection<CartItem> CartItems { get; set; }

        /* the following builds a constructor. */
        public Cart()
        {
            this.CartItems = new List<CartItem>();
        }
    }
}

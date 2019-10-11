using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.Models
{
    // We will not instantiate BaseEntity, so it should be abstract class.
    public abstract class BaseEntity
    {
        public string Id { get; set; }

        public DateTimeOffset CreateAt { get; set; }

        // the following is constructor.
        public BaseEntity()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreateAt = DateTime.Now;
        }
    }
}

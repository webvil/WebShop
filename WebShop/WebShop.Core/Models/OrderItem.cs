using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Core.Models
{
    public class OrderItem : BaseEntity
    {
        public decimal OrderPrice { get; set; }
        public int Quantity { get; set; }
        public virtual Product Product { get; set; }
    }
}

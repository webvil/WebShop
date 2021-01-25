using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Core.Models
{
    public class Order : BaseEntity
    {
        public string ShippingAddress { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }

    }
}

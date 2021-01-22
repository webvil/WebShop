using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Core.Models
{
    public class ProductOnSale : BaseEntity
    {
        public string ProductId { get; set; }
        public decimal Discount { get; set; }
        public DateTimeOffset Start { get; set; }
        public DateTimeOffset End { get; set; }


    }
}

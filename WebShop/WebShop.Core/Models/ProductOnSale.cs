using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Core.Models
{
    [Table("ProductsOnSale")]
    public class ProductOnSale : BaseEntity
    {
       
        public string ProductId { get; set; }
        public decimal? Discount { get; set; }
        public DateTimeOffset Start { get; set; }
        public DateTimeOffset End { get; set; }
        public virtual Product Product { get; set; }
    }
}

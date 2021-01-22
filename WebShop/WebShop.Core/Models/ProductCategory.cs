using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Core.Models
{
    public class ProductCategory : BaseEntity
    {

        public string Category { get; set; }
        
        public string ParentId { get; set; }
        public virtual ProductCategory Parent { get; set; }
        [ForeignKey("ParentId")]
        public virtual ICollection<ProductCategory> Children { get; set; }
        [ForeignKey("ProductCategoryId")]
        public virtual ICollection<Product> Products { get; set; }


        
    }
}

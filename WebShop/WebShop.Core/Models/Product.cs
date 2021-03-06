﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Core.Models
{
    public class Product : BaseEntity
    {
        [StringLength(20)]
        [DisplayName("Product Name")]
        public string Name { get; set; }
        public string Description { get; set; }
       
        public string ProductCategoryId { get; set; }

        [DisplayName("Brand Name")]
        public string Brand { get; set; }
        public string Image { get; set; }
        [Range(0, 1000)]
        public decimal Price { get; set; }
        [ForeignKey("Id")]
        public virtual ProductCategory Category { get; set; }
        public virtual ICollection<ProductOnSale> SaleInfo { get; set; }
    }
}

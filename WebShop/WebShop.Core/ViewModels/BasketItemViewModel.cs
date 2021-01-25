﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Core.ViewModels
{
    // bring together properties from the 2 tables
    public class BasketItemViewModel
    {
        public string Id { get; set; }
        public int Quantity { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public decimal? DiscountRate { get; set; }
        public decimal? DiscountPrice { get; set; }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Core.Models
{
    public class ProductCategory : BaseEntity
    {

        public string Category { get; set; }

        public string ParentCategory { get; set; }

        
    }
}

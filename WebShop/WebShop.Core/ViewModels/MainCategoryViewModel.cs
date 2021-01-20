using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Core.Models;

namespace WebShop.Core.ViewModels
{
    class MainCategoryViewModel
    {
        public string Name { get; set; }
        public IEnumerable<ProductCategory> Children { get; set; }
    }
}

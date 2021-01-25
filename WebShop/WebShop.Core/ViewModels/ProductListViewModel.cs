using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Core.Models;

namespace WebShop.Core.ViewModels
{
    public class ProductListViewModel
    {
        public Product Product { get; set; }
        public ProductOnSale Sale { get; set; }
      /*  public string Id { get; set; }
        public decimal Discount { get; set; }
        public DateTimeOffset Start { get; set; }
        public DateTimeOffset End { get; set; }*/

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebShop.Core.Models;
using WebShop.WebUI.Models;

namespace WebShop.WebUI.ViewModels
{
    public class ShoppingCartViewModel
    {
        public ShoppingCart Cart{ get; set; }
        public decimal Total { get; set; }
    }
}
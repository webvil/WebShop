using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Core.Models;
using WebShop.DataAccess.SQL;
namespace WebShop.WebUI.Models
{

    public class ShoppingCart
    {
        private readonly DataContext dbContext;

        public string ShoppingCartId { get; set; }

        public List<ShoppingCartItem> ShoppingCartItems { get; set; }
        public decimal Total { get; set; }

        public ShoppingCart(DataContext dbContext)
        {
            this.dbContext = dbContext;

        }

    } 
}

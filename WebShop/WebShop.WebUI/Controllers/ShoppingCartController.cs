using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShop.DataAccess.SQL;
using WebShop.Core.Models;
using System.Data.Entity;
using WebShop.WebUI.Models;
using WebShop.WebUI.ViewModels;

namespace WebShop.WebUI.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly DataContext dataContext;
        public ShoppingCart Cart { get; set; }



        public ShoppingCartController(DataContext dataContext, ShoppingCart cart) 
        {
            this.dataContext = dataContext;
            Cart = cart;
           
        }
       
        // GET: ShoppingCart
        public ActionResult Index()
        {
            string cartId = (string)Session["CartId"] ?? Guid.NewGuid().ToString();

            Session["CartId"] = cartId;
            Cart.ShoppingCartId = cartId;
            Cart.ShoppingCartItems = GetShoppingCartItems();//dataContext.ShoppingCartItems.ToList();
            var model = new ShoppingCartViewModel()
            { 
                Cart = Cart,
                Total = GetShoppingCartItems().Count > 0 ? GetShoppingCartTotal() : 0
            };
            return View(model);
        }
        public ActionResult AddToCart(string id)
        {

            string cartId = (string)Session["CartId"] ?? Guid.NewGuid().ToString();

            Session["CartId"] = cartId;
            Cart.ShoppingCartId = cartId;

            var product = dataContext.Products.Find(id); 
            var shoppingCartItem =
                    dataContext.ShoppingCartItems.SingleOrDefault(
                        s => s.Product.Id == product.Id && s.ShoppingCartId == Cart.ShoppingCartId);

            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem
                {
                    ShoppingCartId = cartId,
                    Product = product,
                    Amount = 1
                };

                dataContext.ShoppingCartItems.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Amount++;

            }
            dataContext.SaveChanges();
            return RedirectToAction("Index", "ShoppingCart");
        }

        public ActionResult RemoveFromCart(Product product)
        {
            string cartId = (string)Session["CartId"] ?? Guid.NewGuid().ToString();

            Session["CartId"] = cartId;
            Cart.ShoppingCartId = cartId;

            var shoppingCartItem =
                    dataContext.ShoppingCartItems.SingleOrDefault(
                        s => s.Product.Id == product.Id && s.ShoppingCartId == Cart.ShoppingCartId);

            var localAmount = 0;

            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                    localAmount = shoppingCartItem.Amount;
                }
                else
                {
                    dataContext.ShoppingCartItems.Remove(shoppingCartItem);
                }
            }

            dataContext.SaveChanges();

            return RedirectToAction("Index");
        }

        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return Cart.ShoppingCartItems ??
                   (Cart.ShoppingCartItems =
                       dataContext.ShoppingCartItems.Where(c => c.ShoppingCartId == Cart.ShoppingCartId)
                           .Include(s => s.Product)
                           .ToList());
        }

        public void ClearCart()
        {
            var cartItems = dataContext
                .ShoppingCartItems
                .Where(c => c.ShoppingCartId == Cart.ShoppingCartId);

            dataContext.ShoppingCartItems.RemoveRange(cartItems);

            dataContext.SaveChanges();
        }

        public decimal GetShoppingCartTotal()
        {
            var total = dataContext.ShoppingCartItems.Where(c => c.ShoppingCartId == Cart.ShoppingCartId)
                .Select(c => c.Product.Price * c.Amount).Sum();
            return total;
        }

       
    }
}
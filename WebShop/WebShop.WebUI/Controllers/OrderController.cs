using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShop.Core.Contracts;
using WebShop.Core.Models;

namespace WebShop.WebUI.Controllers
{
    public class OrderController : Controller
    {
        IRepository<Order> context;
        IRepository<OrderItem> orderItems;
        IRepository<Product> products;
        IBasketService basketService;
        
        public OrderController(IRepository<Order> context, IRepository<OrderItem> orderItems, IRepository<Product> products, IBasketService basketService)
        {
            this.context = context;
            this.orderItems = orderItems;
            this.products = products;
            this.basketService = basketService;
        }
        // GET: Order
        public ActionResult Index()
        {
            var basketItems = basketService.GetBasketItems(this.HttpContext);

            var model = (from p in this.products.Collection().ToList()
                             join b in basketItems.ToList()
                             on p.Id equals b.ProductId
                             select new OrderItem
                             {
                                 OrderedAtPrice = ,
                                 Quantity = b.Quantity,
                                 Product = p
                             }).ToList();


            context.Insert(model);
                orderItems.Insert(orderItem);
            }
            // we need the to create the OrderItems from basketitems
            // and then create the order
            return View();
        }
    }
}
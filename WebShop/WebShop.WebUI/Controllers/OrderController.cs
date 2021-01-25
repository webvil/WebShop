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
//IRepository<OrderItem> orderItems;
        IRepository<Product> products;
        IRepository<BasketItem> basketItems;
        
        public OrderController(IRepository<Order> context, IRepository<Product> products, IRepository<BasketItem> basketItems)
        {
            this.context = context;
            this.basketItems = basketItems;
            this.products = products;
        }
        // GET: Order
        public ActionResult Index()
        {

            var orderItems = (from p in this.products.Collection().ToList()
                             join b in basketItems.Collection()
                             on p.Id equals b.ProductId
                             select new OrderItem
                             {
                                 OrderPrice = 10,
                                 Quantity = b.Quantity,
                                 Product = p
                             }).ToList();

            return View(orderItems);
        }
        [HttpPost]
        public ActionResult Create()
        {
            var order = new Order
            {

            };

            context.Insert(order);
            return HttpNotFound();
        }
    }
}
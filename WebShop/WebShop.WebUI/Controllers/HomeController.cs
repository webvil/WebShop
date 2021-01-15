using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShop.Core.Contracts;
using WebShop.Core.Models;
using WebShop.Core.ViewModels;

namespace WebShop.WebUI.Controllers
{
    public class HomeController : Controller
    {
        readonly IRepository<Product> context;
        readonly IRepository<ProductCategory> productCategories;
        public HomeController(IRepository<Product> productContext,
          IRepository<ProductCategory> productCategories)
        {
            context = productContext;
            this.productCategories = productCategories;
        }
        public ActionResult Index(string Category = null)
        {
            List<Product> products;

            if (Category == null)
            {
                products = context.Collection().ToList();
            }
            else
            {
                products = context.Collection().Where(p => p.Category == Category).ToList();
            }
            var model = new ProductListViewModel
            {
                Products = products,
                ProductCategories = productCategories.Collection().ToList()
            };

            return View(model);
        }

        public ActionResult Details(string Id)
        {
            Session["details"] = "hello session";
            Product product = context.Find(Id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        public ActionResult Cart(string Id)
        {
            
            Product product = context.Find(Id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
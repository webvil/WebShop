using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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
        public ActionResult Index(string Category = null, string MainCategory = null)
        {
            List<Product> products;

            if (Category != null)
            {
                products = context.Collection().Where(p => p.Category.Category == Category).ToList();
            }
            else
            {
                if (MainCategory != null)
                {
                    products = context.Collection().Where(p => p.Category.Parent.Category == MainCategory).ToList();
                }
                else
                {
                    products = context.Collection().ToList();
                }
            }

           

            /* var categories = (from cat in productCategories.Collection().ToList()
                               join child in 
                               on cat.Id equals child.ParentId
                               join prod in context.Collection()
                               on cat.Id equals prod.ProductCategoryId
                               where cat.ParentId == null).ToList();*/
            var cats = new List<ProductCategory>();
            foreach (var cat in productCategories.Collection().ToList())
            {
                foreach (var child in cat.Children)
                {
                    if (child.Products.Count > 0)
                    {
                       
                        cats.Add(cat);
                        break;
                    }
                    
                }
            }
           // var categories = productCategories.Collection()
           // .Where(c => c.ParentId == null)
            
           // .Include(c => c.Children)
           
            
           // .ToList();



            ProductListViewModel model = new ProductListViewModel()
            {
                Products = products,
                ProductCategories = cats

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
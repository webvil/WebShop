using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShop.Core.Models;
using WebShop.DataAccess.InMemory;
using WebShop.Core.ViewModels;
using WebShop.Core.Contracts;
using System.IO;
using System.Data.Entity;

namespace WebShop.WebUI.Controllers
{
    public class SaleManagerController : Controller
    {
        
        IRepository<ProductOnSale> context;
        IRepository<Product> products;
        public SaleManagerController(IRepository<ProductOnSale> productsOnSale,
            IRepository<Product> products)
        {
            context = productsOnSale;
            this.products = products;
        }
        // GET: ProductManager

        public ActionResult Index()
        {
            return HttpNotFound();
        }

        [HttpGet]
        public ActionResult Create()
        {
            return HttpNotFound();
        }
        [HttpPost]
        public ActionResult Create(Product product, HttpPostedFileBase files)
        {
            return HttpNotFound();
        }

        public ActionResult Edit(string Id)
        {


            return HttpNotFound();

        }
        [HttpPost]
        public ActionResult Edit(Product product, string Id, HttpPostedFileBase files)
        {
            return HttpNotFound();
        }

        public ActionResult Delete(string Id)
        {

            return HttpNotFound();
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            return HttpNotFound();

        }
        public ActionResult Details(string Id)
        {
            return HttpNotFound();
        }
        public ActionResult Sale()
        {
            return HttpNotFound();  
        }

    }
}
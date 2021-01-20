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
    public class ProductManagerController : Controller
    {
        IRepository<Product> context;
        IRepository<ProductCategory> productCategories;
        public ProductManagerController(IRepository<Product> productContext,
            IRepository<ProductCategory> productCategories)
        {
            context = productContext;
            this.productCategories = productCategories;
        }
        // GET: ProductManager

        public ActionResult Index()
        {
            List<Product> products = context.Collection().ToList();
            return View(products) ;
        }

        public ActionResult Create()
        {
            var viewModel = new ProductManagerViewModel()
            {
                Product = new Product(),
                ProductCategories = productCategories.Collection().Where(c => c.ParentId == null).Include(c => c.Children).ToList()
            };
            
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult Create(Product product, HttpPostedFileBase files)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            else
            {
                if (files != null && files.ContentLength > 0)
                {
                    
                    var fileName = Path.GetFileName(files.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/img/products"), fileName);
                    files.SaveAs(path);
                    product.Image = fileName;
                }
                context.Insert(product);
                context.Commit();
                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(string Id)
        {
            Product product = context.Find(Id);
            if (product == null)
            {
                return HttpNotFound();
            }
            else
            {
                var viewModel = new ProductManagerViewModel()
                {
                    Product = product,
                    ProductCategories = productCategories.Collection()
                };
                return View(viewModel);
            }
                
              
            
        }
        [HttpPost]
        public ActionResult Edit(Product product, string Id, HttpPostedFileBase files)
        {
            Product productToEdit = context.Find(Id);
            if (productToEdit == null)
            {
                return HttpNotFound();
            }
            
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(product);
                }
                if (files != null && files.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(files.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/img/products"), fileName);
                    files.SaveAs(path);
                    productToEdit.Image = fileName;
                }
                productToEdit.Category = product.Category;
                productToEdit.Description = product.Description;
                productToEdit.Price = product.Price;
                productToEdit.Name = product.Name;
                context.Commit();
                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(string Id)
        {
            Product productToDelete = context.Find(Id);
            if (productToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productToDelete);
            }

        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            Product productToDelete = context.Find(Id);
            if (productToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                context.Delete(Id);
                context.Commit();
                return RedirectToAction("Index");
            }

        }
        public ActionResult Details(string Id)
        {
            Session["details"] = "hello session";
            Product product = context.Find(Id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return RedirectToAction("Details", "Home", product);
            
        }

    }
}
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
using System.Data.Entity.Validation;
using System.Text;

namespace WebShop.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
        
        IRepository<Product> context;
        IRepository<ProductCategory> productCategories;
        IRepository<ProductOnSale> productsOnSale;
        public ProductManagerController(IRepository<Product> productContext,
            IRepository<ProductCategory> productCategories, IRepository<ProductOnSale> productsOnSale)
        {
            context = productContext;
            this.productCategories = productCategories;
            this.productsOnSale = productsOnSale;
        }
        // GET: ProductManager

        public ActionResult Index()
        {
            // make a ProductListViewModel
            var model = (from p in context.Collection().ToList()
                                     join s in productsOnSale.Collection()
                                     on p.Id equals s.ProductId into x
                                     from subnet in x.DefaultIfEmpty()
                                     select new ProductListViewModel 
                                     { 
                                         Product = p,
                                         Sale = subnet ?? new ProductOnSale()
                                         /*Id = subnet?.Id ?? String.Empty,
                                         Discount = (decimal)(subnet?.Discount),
                                         Start = (DateTimeOffset)subnet?.Start,
                                         End = (DateTimeOffset)subnet?.Start */

                                     }).ToList();

            return View(model) ;
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.ProductCategories = productCategories.Collection().Where(c => c.ParentId == null)
                .Include(c => c.Children)
                .Where(c => c.Children.Count > 0).ToList();
            var product = new Product();
            return View(product);
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
        public ActionResult Edit(ProductManagerViewModel viewModel, string Id, HttpPostedFileBase files)
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
                    return View(viewModel);
                }
                if (files != null && files.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(files.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/img/products"), fileName);
                    files.SaveAs(path);
                    productToEdit.Image = fileName;
                }
                productToEdit.Category.Category = viewModel.Product.Category.Category;
                productToEdit.Description = viewModel.Product.Description;
                productToEdit.Price = viewModel.Product.Price;
                productToEdit.Name = viewModel.Product.Name;
                productToEdit.Brand = viewModel.Product.Brand;
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
        [HttpPost]
        public ActionResult AddToSale(ProductOnSale p)
        {

            var productOnSale = productsOnSale.Find(p.Id);
            if (productOnSale == null) // new record
            {
                if (!ModelState.IsValid)
                {
                    return HttpNotFound();
                    //return RedirectToAction("Index");
                }
                else
                {

                    var newSale = new ProductOnSale
                    {

                        ProductId = p.ProductId,
                        Discount = p.Discount,
                        Start = p.Start,
                        End = p.End
                    };
                    productsOnSale.Insert(newSale);
                    productsOnSale.Commit();
                    return RedirectToAction("Index");

                }
            }
            else // Update
            {
                //return HttpNotFound();
                productOnSale.ProductId = p.ProductId;
                productOnSale.Discount = p.Discount;
                productOnSale.Start = p.Start;
                productOnSale.End = p.End;
                productsOnSale.Commit();
                return RedirectToAction("Index");
            }

        }
        public ActionResult RemoveFromSale(string Id)
        {
            var productToRemove = productsOnSale.Find(Id);
            if (productToRemove == null)
            {
                return HttpNotFound();
            }
            else
            {
                productsOnSale.Delete(Id);
                productsOnSale.Commit();
                return RedirectToAction("Index");
            }
        }

    }
}
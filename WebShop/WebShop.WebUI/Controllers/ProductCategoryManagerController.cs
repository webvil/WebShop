﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using WebShop.Core.Contracts;
using WebShop.Core.Models;
using WebShop.DataAccess.InMemory;

namespace WebShop.WebUI.Controllers
{
    public class ProductCategoryManagerController : Controller
    {
        private readonly IRepository<ProductCategory> context;
        public ProductCategoryManagerController(IRepository<ProductCategory> context)
        {
            this.context = context;
        }
        // GET: ProductCategoryManager

        public ActionResult Index()
        {
            List<ProductCategory> productCategories = context.Collection().ToList();
            return View(productCategories);
        }
        //GET
        public ActionResult Create()
        {

            var parentCategories = context.Collection().Where(c => c.ParentId == null).ToList();
            ViewBag.ParentCategories = parentCategories;
            ProductCategory productCategory = new ProductCategory();
            return View(productCategory);
        }
        [HttpPost]
        public ActionResult Create(ProductCategory productCategory)
        {
            if (!ModelState.IsValid)
            {
                return View(productCategory);
            }
            else
            {
                context.Insert(productCategory);
                context.Commit();
                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(string Id)
        {
            ProductCategory productCategory = context.Find(Id);
            var parentCategories = context.Collection().Where(c => c.ParentId == null).ToList();
            ViewBag.ParentCategories = parentCategories;
            if (productCategory == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productCategory);
            }



        }
        [HttpPost]
        public ActionResult Edit(ProductCategory productCategory, string Id)
        {
            ProductCategory productCategoryToEdit = context.Find(Id);
            if (productCategoryToEdit == null)
            {
                return HttpNotFound();
            }

            else
            {
                if (!ModelState.IsValid)
                {
                    return View(productCategory);
                }
                productCategoryToEdit.ParentId = productCategory.ParentId;
                productCategoryToEdit.Category = productCategory.Category;
                context.Commit();
                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(string Id)
        {
            List<ProductCategory> productCategories = context.Collection().Where(a => a.ParentId == Id).ToList();
            ProductCategory productCategoryToDelete = context.Find(Id);
            if (productCategoryToDelete == null)
            {
                return HttpNotFound();
            }
            if (productCategories.Any())
            {
                ViewBag.Message = "Cannot delete parent category";
               
            }
            return View(productCategoryToDelete);


        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            
            ProductCategory productCategoryToDelete = context.Find(Id);
            if (productCategoryToDelete == null)
            {
                return HttpNotFound();
            }

              context.Delete(Id);
                context.Commit();
          
            
            
            return RedirectToAction("Index");

        }

        

    }
}
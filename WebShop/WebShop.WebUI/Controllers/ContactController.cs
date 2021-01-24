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
    public class ContactController : Controller
    {
        IRepository<Contact> context;
        public ContactController(IRepository<Contact> contactContext)
        {
            context = (IRepository<Contact>)contactContext;
        }


        [HttpGet]
        public ActionResult Create()
        {

            var contact = new Contact();
            return View(contact);
        }
        [HttpPost]
        public ActionResult Create(Contact contact, HttpPostedFileBase files)
        {
            if (!ModelState.IsValid)
            {
               
                @ViewBag.Message = "failure";
                return View(contact);
            }
            else
            {

                context.Insert(contact);
                context.Commit();
                @ViewBag.Message = "Details added successfully";
                return RedirectToAction("Index");
                 
            }
        }
    }
}

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
        public ActionResult Create(Contact contact)
        {
            if (!ModelState.IsValid)
            {
               
                TempData["MSG"] = "Oops! Some issue in getting the data.";
                return View(contact);
            }
            else
            {

                context.Insert(contact);
                context.Commit();
                TempData["MSG"] = "Thanks for giving the information!.Will get in touch with you soon";
                return RedirectToAction("Index", "Home", contact);
              

            }
        }


    }
}

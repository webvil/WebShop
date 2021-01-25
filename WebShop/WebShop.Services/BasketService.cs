using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using WebShop.Core.Contracts;
using WebShop.Core.Models;
using WebShop.Core.ViewModels;

namespace WebShop.Services
{
    public class BasketService : IBasketService
    {
        readonly IRepository<Product> productContext;
        readonly IRepository<Basket> basketContext;
        readonly IRepository<BasketItem> basketItemContext;
        public const string BasketSessionName = "eCommerceBasket";
        public BasketService(IRepository<Product> ProductContext, 
            IRepository<Basket> BasketContext,
            IRepository<BasketItem> BasketItemContext)
        {
            productContext = ProductContext;
            basketContext = BasketContext;
            basketItemContext = BasketItemContext;

        }
        
        private Basket GetBasket(HttpContextBase httpContext, bool createIfNull)
        {
            
            HttpCookie cookie = httpContext.Request.Cookies.Get(BasketSessionName);
            // Create a new basket
            Basket basket = new Basket();
            // Does the cookie exist
            if (cookie != null)
            {
                string basketId = cookie.Value;
                if (!string.IsNullOrEmpty(basketId))
                {
                    basket = basketContext.Find(basketId);   
                }
                else // if basketId is null
                {
                    if (createIfNull) 
                    { 
                        basket = CreateNewBasket(httpContext);
                    } 
                }
                    

            }
            else
            {
                if (createIfNull)
                {
                    basket = CreateNewBasket(httpContext);
                }

            }
            return basket;
            
        }
        private Basket CreateNewBasket(HttpContextBase httpContext)
        {
            Basket basket = new Basket();
            basketContext.Insert(basket);
            basketContext.Commit();
            HttpCookie cookie = new HttpCookie(BasketSessionName)
            {
                Value = basket.Id,
                Expires = DateTime.Now.AddDays(1)
            };
            httpContext.Response.Cookies.Add(cookie);
            return basket;
        }

        public void AddToBasket(HttpContextBase httpContext, string productId)
        {
            // create if null
            Basket basket = GetBasket(httpContext, true);
            // Is this looking in db or in memory?
            BasketItem item = basket.BasketItems.FirstOrDefault(i => i.ProductId == productId);
            // does item exist in basket
            if (item == null)
            {
                item = new BasketItem()
                {
                    BasketId = basket.Id,
                    ProductId = productId,
                    Quantity = 1
                };
                basket.BasketItems.Add(item);
            }
            else
            {
                item.Quantity++;
            }
            basketContext.Commit();
        }
        public void RemoveFromBasket(HttpContextBase httpContext, string itemId)
        {
            Basket basket = GetBasket(httpContext, true);
            if (basket != null)
            {
                BasketItem item = basket.BasketItems.FirstOrDefault(i => i.Id == itemId);
                if (item != null)
                {
                    if (item.Quantity > 1)
                    {
                        item.Quantity--;
                    }

                    else
                    {
                        basketItemContext.Delete(itemId);
                        //basketContext.BasketItems.Remove(item);
                        basketItemContext.Commit();
                    }

                }
                    basketContext.Commit();
            }
           
        }

        public List<BasketItemViewModel> GetBasketItems(HttpContextBase httpContext)
        {
            // get basket from db
            // if there are no items return basket in memory
            Basket basket = GetBasket(httpContext, false);
            if (basket != null)
            {
                var results = (from b in basket.BasketItems
                               join p in productContext.Collection()
                               on b.ProductId equals p.Id
                               select new BasketItemViewModel()
                               { 
                                   Id = b.Id,
                                   Quantity = b.Quantity,
                                   ProductName = p.Name,
                                   Image = p.Image,
                                   Price = p.Price,
                                   DiscountRate = p.SaleInfo.Count > 0 ? p.SaleInfo.First().Discount : decimal.Zero,
                                   DiscountPrice = p.SaleInfo.Count > 0 ? p.Price * (100 - p.SaleInfo.First().Discount) / 100 : p.Price
                               }).ToList();
                return results;
            }
            else
            {
                return new List<BasketItemViewModel>();
            }

        }
        public BasketSummaryViewModel GetBasketSummary(HttpContextBase httpContext)
        {
            Basket basket = GetBasket(httpContext, false);
            // Default ViewModel to zero
            BasketSummaryViewModel model = new BasketSummaryViewModel(0,0);
            if (basket != null)
            {
                // Calculate items in basket with Linq
                // int? allow null values
                int? basketCount = (from item in basket.BasketItems
                                    select item.Quantity).Sum();
                decimal? basketTotal = (from item in basket.BasketItems
                                        join p in productContext.Collection()
                                        on item.ProductId equals p.Id
                                        select item.Quantity * p.Price).Sum();
                
                model.BasketCount = basketCount ?? 0;
                model.BasketTotal = basketTotal ?? decimal.Zero; // No error for decimal
            }
            
            return model;
            

        }



    }
}

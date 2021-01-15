using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Core.Models
{
    public class BasketItem : BaseEntity
    {
        public int BasketId { get; set; }
        public string ProductId { get; set; }
        public int Quantity { get; set; }

    }
}

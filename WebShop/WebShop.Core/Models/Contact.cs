using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Core.Models
{
    public class Contact : BaseEntity
    {
        
        
        [StringLength(20)]
        [DisplayName("Name")]
        public string Name { get; set; }
        public string Email { get; set; }
       
        public string PhoneNumber { get; set; }
        public string Message { get; set; }


    }
}

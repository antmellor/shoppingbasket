using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingBasket.Models
{
    public class Product
    {
        public long ProductId { get; set; }
        public long Barcode { get; set; }
        public string ItemName { get; set; }
        public decimal Price { get; set; }
        public bool IsPricedByWeight { get; set; }
        
    }
}

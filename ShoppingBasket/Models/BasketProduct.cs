using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingBasket.Models
{
    public class BasketProduct
    {
        public Product Product { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price => Product.Price * Quantity;
    }
}

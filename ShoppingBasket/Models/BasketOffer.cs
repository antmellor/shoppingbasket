using ShoppingBasket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingBasket
{
    public class BasketOffer
    {
        public Offer Offer { get; set; }
        public int Quantity { get; set; }
        public decimal Price => Offer.Price * Quantity;
    }
}

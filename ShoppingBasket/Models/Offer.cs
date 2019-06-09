using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingBasket.Models
{
    public class Offer
    {
        public long OfferId { get; set; }
        public long ProductId { get; set; }
        public string OfferName { get; set; }
        public int QuantityForOffer { get; set; }
        public decimal Price { get; set; }


        public Product Product { get; set;  }
    }
}

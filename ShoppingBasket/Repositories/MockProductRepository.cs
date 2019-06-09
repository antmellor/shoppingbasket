using ShoppingBasket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingBasket.Repositories
{
    /// <summary>
    /// A mock of data that would otherwise be in a database.
    /// </summary>
    public class MockProductRepository: IProductRepository
    {
        private List<Product> products;
        private List<Offer> offers;

        public MockProductRepository()
        {
            var apple = new Product { ProductId = 1, Barcode = 100000000000, ItemName = "Apple", Price = 0.50m };
            var orange = new Product { ProductId = 2, Barcode = 200000000000, ItemName = "Orange", Price = 0.45m };
            var banana = new Product
            {
                ProductId = 3,
                Barcode = 300000000000,
                ItemName = "Banana",
                Price = 2.00m,
                IsWeightedProduct = true,
                UnitOfWeight = "lb",
                WeightForPrice = 1
            };

            products = new List<Product>
            {
                apple, orange, banana
            };

            offers = new List<Offer>
            {
                new Offer
                {
                    OfferId = 1,
                    OfferName = "Orange 3 for £1",
                    Price = 1.00m,
                    ProductId = 2,
                    Product = orange,
                    QuantityForOffer = 3
                }
            };
        }

        public List<Offer> GetOffers()
        {
            return offers;
        }

        public Product GetProductByBarcode(long barcode)
        {
            return products.Where(p => p.Barcode == barcode).FirstOrDefault();
        }
    }
}

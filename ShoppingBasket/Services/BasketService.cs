using ShoppingBasket.Models;
using ShoppingBasket.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingBasket.Services
{
    public class BasketService
    {

        private IProductRepository _repository;
        private List<BasketProduct> _basketProducts;

        public BasketService(IProductRepository repository)
        {
            _repository = repository;
            _basketProducts = new List<BasketProduct>();
        }

        public decimal GetTotal()
        {
            return _basketProducts.Select(p => p.Product.Price * p.Quantity).Sum();
        }

        public void AddProduct(long barcode)
        {
            var product = _repository.GetProductByBarcode(barcode);
            var basketProduct = new BasketProduct
            {
                Product = product,
                Quantity = 1
            };
            _basketProducts.Add(basketProduct);
        }
    }
}

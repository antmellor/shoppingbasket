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
            // check if product already exists first and increase quantity.
            if (IsProductInBasket(barcode))
            {
                _basketProducts.Find(p => p.Product.Barcode == barcode).Quantity++;
                return;
            }

            var product = _repository.GetProductByBarcode(barcode);
            var basketProduct = new BasketProduct
            {
                Product = product,
                Quantity = 1
            };
            _basketProducts.Add(basketProduct);
        }

        private bool IsProductInBasket(long barcode)
        {
            return _basketProducts.Where(bp => bp.Product.Barcode == barcode).Any();
        }

        public int GetDistinctProductCount()
        {
            return _basketProducts.Count;
        }
    }
}

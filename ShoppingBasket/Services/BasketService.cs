﻿using ShoppingBasket.Models;
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
        private List<BasketOffer> _appliedOffers;

        public BasketService(IProductRepository repository)
        {
            _repository = repository;
            _basketProducts = new List<BasketProduct>();
            _appliedOffers = new List<BasketOffer>();
        }

        public decimal GetTotal()
        {
            ApplyOffers();

            return
                _appliedOffers.Sum(o => o.Price) + _basketProducts.Sum(p => p.Price);
        }

        private void ApplyOffers()
        {
            var offers = _repository.GetOffers();
            foreach(var offer in offers)
            {
                var basketProduct = _basketProducts.Where(b => b.Product == offer.Product).FirstOrDefault();
                if (basketProduct == null || basketProduct.Quantity < offer.QuantityForOffer) continue;

                // divide the quantity in the basket by the quantity required for the offer 
                // to get the total number of times the offer should be applied.
                var quantityOfOfferApplied = (int)basketProduct.Quantity / offer.QuantityForOffer;
                _appliedOffers.Add(new BasketOffer { Offer = offer, Quantity = quantityOfOfferApplied });

                // update the BasketProduct quantity to be the remainder, where an offer has not been applied.
                basketProduct.Quantity = basketProduct.Quantity % offer.QuantityForOffer;
            }
        }

        public void AddProduct(long barcode, int quantity = 1)
        {
            // check if product already exists first and increase quantity.
            if (IsProductInBasket(barcode))
            {
                _basketProducts.Find(p => p.Product.Barcode == barcode).Quantity += quantity;
                return;
            }

            var product = _repository.GetProductByBarcode(barcode);
            var basketProduct = new BasketProduct
            {
                Product = product,
                Quantity = quantity
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

        public void AddWeightedProduct(long barcode, decimal weight)
        {
            if (IsProductInBasket(barcode))
            {
                var bp = _basketProducts.Find(p => p.Product.Barcode == barcode);
                bp.Quantity += GetWeightedQuantity(weight, bp.Product.WeightForPrice);
                return;
            }

            var product = _repository.GetProductByBarcode(barcode);
            var basketProduct = new BasketProduct
            {
                Product = product,
                Quantity = GetWeightedQuantity(weight, product.WeightForPrice)
            };
            _basketProducts.Add(basketProduct);
        }

        private decimal GetWeightedQuantity(decimal scannedWeight, decimal pricedWeight)
        {
            return scannedWeight / pricedWeight;
        }
    }
}

using Moq;
using ShoppingBasket.Models;
using ShoppingBasket.Repositories;
using ShoppingBasket.Services;
using System;
using System.Collections.Generic;
using Xunit;

namespace ShoppingBasket.Tests
{
    public class BasketServiceTests
    {

        private Product apple;
        private Product orange;
        private Product banana;
        private Product sweets;
        private Offer orangeOffer;
        private Mock<IProductRepository> mockProductRepository;

        public BasketServiceTests()
        {
            setupProducts();
            mockProductRepository = new Mock<IProductRepository>();
            mockProductRepository.Setup(m => m.GetProductByBarcode(apple.Barcode)).Returns(apple);
            mockProductRepository.Setup(m => m.GetProductByBarcode(orange.Barcode)).Returns(orange);
            mockProductRepository.Setup(m => m.GetProductByBarcode(banana.Barcode)).Returns(banana);
            mockProductRepository.Setup(m => m.GetProductByBarcode(sweets.Barcode)).Returns(sweets);
            mockProductRepository.Setup(m => m.GetOffers()).Returns(new List<Offer>() { orangeOffer });
        }

        private void setupProducts()
        {
            apple = new Product
            {
                ProductId = 1,
                Barcode = 100000000000,
                ItemName = "Apple",
                Price = (decimal)0.50
            };
            orange = new Product
            {
                ProductId = 2,
                Barcode = 200000000000,
                ItemName = "Orange",
                Price = (decimal)0.45
            };
            banana = new Product
            {
                ProductId = 3,
                Barcode = 300000000000,
                ItemName = "Banana",
                Price = (decimal)2.00,
                UnitOfWeight = "lb",
                WeightForPrice = 1
            };
            sweets = new Product
            {
                ProductId = 4,
                Barcode = 400000000000,
                ItemName = "Sweet Selection",
                Price = (decimal)1.50,
                UnitOfWeight = "g",
                WeightForPrice = 100
            };

            orangeOffer = new Offer
            {
                OfferId = 1,
                ProductId = 2,
                OfferName = "3 for £1 Oranges",
                Price = (decimal)1.00,
                Product = orange,
                QuantityForOffer = 3
            };
        }

        [Fact]
        public void BasketService_WhenBasketIsEmpty_TotalPriceIsZero()
        {
            var basket = new BasketService(mockProductRepository.Object);

            Assert.Equal(0, basket.GetTotal());
        }

        [Fact]
        public void BasketService_WhenSingleItemAddedToBasket_TotalPriceMatchesItemPrice()
        {
            var basket = new BasketService(mockProductRepository.Object);
            basket.AddProduct(apple.Barcode);

            Assert.Equal(apple.Price, basket.GetTotal());
        }

        [Fact]
        public void BasketService_WhenTwoOfSameItemAddedToBasket_TotalPriceIsTwiceTheItemPrice()
        {
            var basket = new BasketService(mockProductRepository.Object);

            // mock concept of scanning an item twice  
            basket.AddProduct(apple.Barcode);
            basket.AddProduct(apple.Barcode);

            var distinctCountOfProductsInBasket = basket.GetDistinctProductCount();

            var expectedResult = apple.Price * 2;
            Assert.Equal(expectedResult, basket.GetTotal());

            Assert.Equal(1, distinctCountOfProductsInBasket);
        }

        [Fact]
        public void BasketService_WhenOfferOnMultipleItemsIsAppliedAndNumberOfItemsMeetsOfferCount_TotalPriceMatchesOfferPrice()
        {
            var basket = new BasketService(mockProductRepository.Object);

            basket.AddProduct(orange.Barcode, 3);

            Assert.Equal(orangeOffer.Price, basket.GetTotal());
        }

        [Fact]
        public void BasketService_WhenOfferOnMultipleItemsIsAppliedButNumberOfItemsDoesNotMeetOfferCount_TotalPriceMatchesItemPrice()
        {
            var basket = new BasketService(mockProductRepository.Object);

            basket.AddProduct(orange.Barcode, 2);

            Assert.Equal(orange.Price * 2, basket.GetTotal());
        }
        
        [Fact]
        public void BasketService_WhenCountOfItemsExceedsOfferCountWithARemainder_TotalPriceIsCombinationOfOfferPlusItemPrice()
        {
            var basket = new BasketService(mockProductRepository.Object);

            basket.AddProduct(orange.Barcode, 5);

            var expectedResult = orangeOffer.Price + (2 * orange.Price);
            Assert.Equal(expectedResult, basket.GetTotal());
        }

        [Fact]
        public void BasketService_WhenWeightedProductAddedToBasketMatchingPriceWeight_TotalPriceEqualsWeightPrice()
        {
            var basket = new BasketService(mockProductRepository.Object);

            basket.AddWeightedProduct(banana.Barcode, 1);

            Assert.Equal(banana.Price, basket.GetTotal());
        }


        [Fact]
        public void BasketService_WhenWeightedProductAddedToBasketLessThanWeightPrice_TotalPriceIsLessThanWeightPrice()
        {
            var basket = new BasketService(mockProductRepository.Object);

            basket.AddWeightedProduct(sweets.Barcode, 50);

            Assert.Equal(sweets.Price / 2, basket.GetTotal());
            Assert.True(sweets.Price > basket.GetTotal());
        }

    }
}

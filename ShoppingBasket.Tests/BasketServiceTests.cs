using Moq;
using ShoppingBasket.Models;
using ShoppingBasket.Repositories;
using ShoppingBasket.Services;
using System;
using Xunit;

namespace ShoppingBasket.Tests
{
    public class BasketServiceTests
    {

        private Product apple;
        private Product orange;
        private Product banana;
        private Mock<IProductRepository> mockProductRepository;

        public BasketServiceTests()
        {
            setupProducts();
            mockProductRepository = new Mock<IProductRepository>();
            mockProductRepository.Setup(m => m.GetProductByBarcode(apple.Barcode)).Returns(apple);
            mockProductRepository.Setup(m => m.GetProductByBarcode(orange.Barcode)).Returns(orange);
            mockProductRepository.Setup(m => m.GetProductByBarcode(banana.Barcode)).Returns(banana);
        }

        private void setupProducts()
        {
            apple = new Product
            {
                Barcode = 100000000000,
                ItemName = "Apple",
                Price = (decimal)0.50
            };
            orange = new Product
            {
                Barcode = 200000000000,
                ItemName = "Orange",
                Price = (decimal)0.45
            };
            banana = new Product
            {
                Barcode = 300000000000,
                ItemName = "Banana",
                Price = (decimal)2.00,
                IsPricedByWeight = true
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

            var expectedResult = apple.Price * 2;
            Assert.Equal(expectedResult, basket.GetTotal());
        }
        
    }
}

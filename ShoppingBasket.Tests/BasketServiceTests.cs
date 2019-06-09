using Moq;
using ShoppingBasket.Models;
using ShoppingBasket.Repositories;
using ShoppingBasket.Services;
using System;
using Xunit;

namespace ShoppingBasket.Tests
{
    public class FixedPriceItemTests
    {
        [Fact]
        public void BasketService_WhenBasketIsEmpty_TotalPriceIsZero()
        {
            var repo = new Mock<IProductRepository>();

            var basket = new BasketService(repo.Object);

            Assert.Equal(0, basket.GetTotal());
        }

        [Fact]
        public void BasketService_WhenSingleItemAddedToBasket_TotalPriceMatchesItemPrice()
        {
            var barcode = 100000000000;
            var mockApple = new Product
            {
                Barcode = barcode,
                ItemName = "Apple",
                Price = (decimal)0.45
            };

            var repo = new Mock<IProductRepository>();

            var basket = new BasketService(repo.Object);
            basket.AddItem(barcode);

            Assert.Equal(mockApple.Price, basket.GetTotal());

        }
    }
}

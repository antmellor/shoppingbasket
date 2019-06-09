using Moq;
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
        }
    }
}

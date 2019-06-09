using ShoppingBasket.Repositories;
using ShoppingBasket.Services;
using System;

namespace ShoppingBasket
{
    public class Program
    {
        public static long appleBarcode =>  100000000000;
        public static long orangeBarcode => 200000000000;
        public static long bananaBarcode => 300000000000;

        static void Main(string[] args)
        {
            var repository = new MockProductRepository();
            var basket = new BasketService(repository);

            Console.WriteLine("Scanning Products");

            Console.WriteLine("Adding 3 apples to basket...");
            basket.AddProduct(appleBarcode, 3);

            Console.WriteLine("Adding 5 oranges to basket...");
            basket.AddProduct(orangeBarcode, 5);

            Console.WriteLine("Adding 2lbs of bananas to basket...");
            basket.AddWeightedProduct(bananaBarcode, 2);

            Console.WriteLine($"Basket Total: £{basket.GetTotal()}");

            Console.WriteLine("Press any key to exit");
            Console.Read();
        }

    }
}

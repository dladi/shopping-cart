using System.Collections.Generic;
using System.Drawing;
using PricingService.Models;

namespace PricingService.Tests
{
    public class DummyDataProvider : IDataProvider
    {
        public ShoppingCart GetShoppingCart()
        {
            return new ShoppingCart
            {
                Id = 1,
                Name = "Best Deals",
                Items = GetItems()
            };
        }

        private List<Item> GetItems()
        {
            return new List<Item>
            {
                GetToys(1, "Tom", 1000),
                GetBooks(2, "Clean Code", 1000),
                GetMobiles(3, "N 220", 3000)
                //GetToy(4, "Agile Principles" , 2000),
                //GetMobile(5, "N 110", 2000)
            };
        }

        private Item GetMobiles(int id, string title, double amount)
        {
            return new Mobile
            {
                Id = id,
                TotalPrice = new Money("INR", amount),
                Title = title,
                Brand = "Nokia",
                Color = Color.Black,
                Quantity = 1,
                WeightInGms = 100
            };
        }

        private Item GetToys(int id, string title, double amount)
        {
            return new Toy
            {
                Id = id,
                TotalPrice = new Money("INR", amount),
                Title = title,
                Quantity = 2,
                AgeGroup = new AgeGroup {Min = 3, Max = 5}
            };
        }

        private Item GetBooks(int id, string title, double amount)
        {
            return new Book
            {
                Id = id,
                TotalPrice = new Money("INR", amount),
                Title = title,
                Author = "Robert C Martin",
                Quantity = 1,
                NoOfPages = 400
            };
        }
    }
}
using System.Collections.Generic;
using Castle.Components.DictionaryAdapter;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PricingService.Interfaces;
using PricingService.Models;
using StructureMap;

namespace PricingService.Tests
{
    [TestClass]
    public partial class PricingEngineTests
    {
        private static IContainer _container;
        private static IPricingService _pricingService;
        private static IDataProvider _dataProvider;

        [ClassInitialize]
        public static void OnClassInitialise(TestContext context)
        {
            _container = BootStrapper.GetContainer();
            _pricingService = _container.GetInstance<IPricingService>();
            _dataProvider = new DummyDataProvider();
        }

        [TestMethod]
        public void Should_Throw_Validation_Error_When_Empty_Cart()
        {
            var response = _pricingService.Price(null);

            CheckForError(response);
            Assert.IsTrue(response.ValidationResult.Errors.Contains("Shopping cart can not be empty."));
        }

        [TestMethod]
        public void Should_Throw_Validation_Error_When_Empty_Cart_Items()
        {
            var response = _pricingService.Price(new ShoppingCart());

            CheckForError(response);
            Assert.IsTrue(response.ValidationResult.Errors.Contains("Shopping cart items can not be empty."));
        }

        [TestMethod]
        public void Should_Throw_Validation_Error_When_Empty_Cart_Currency()
        {
            var response = _pricingService.Price(new ShoppingCart()
            {
                Items = new List<Item>()
                {
                    new Book()
                },
                CartTotal = {Amount = 0, Currency = ""}
            });

            CheckForError(response);
            Assert.IsTrue(response.ValidationResult.Errors.Contains("Shopping cart currency can not be empty"));
        }

        private static Book GetBook()
        {
            return new Book
            {
                Id = 1,
                Title = "CLR Via C#",
                TotalPrice = new Money("USD", 100)
            };
        }


        private static void CheckForError(PricingResponse response)
        {
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.ValidationResult);
            Assert.IsTrue(response.ValidationResult.Errors != null);
        }
    }
}
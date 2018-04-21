using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PricingService.BusinessLogic;
using PricingService.Interfaces;
using PricingService.Models;

namespace PricingService.Tests
{
    public partial class PricingEngineTests
    {
        private double TOLERANCE => 1;

        [TestMethod]
        public void Should_Apply_Discount_Deal_When_ItemId_Based_Rule_Configured()
        {
            var ruleMock = new Mock<IDealsRuleProvider>();
            ruleMock.Setup(provider => provider.GetAll()).Returns(GetItemIdBasedDealRule(1, 1));
            var dealMock = new Mock<IDealsProvider>();
            dealMock.Setup(provider => provider.Get(1)).Returns(GetDiscountedDeal(1));
            var dedupStrategy = new DealPreferenceDedupStrategy(dealMock.Object);
            var requestValidator = _container.GetInstance<IPricingRequestValidator>();
            var dealsEngine = new DealsEngine(dealMock.Object, ruleMock.Object, dedupStrategy);
            var pricingService = new BusinessLogic.PricingService(requestValidator, dealsEngine);

            var response = pricingService.Price(_dataProvider.GetShoppingCart());

            Assert.IsNotNull(response);
            Assert.IsTrue(Math.Abs(response.ShoppingCart.CartTotalAfterDiscount.Amount - 4850) < TOLERANCE);
            Assert.IsTrue(Math.Abs(response.ShoppingCart.CartTotal.Amount - 5000) < TOLERANCE);
        }

        [TestMethod]
        public void Should_Apply_PayItem_Deal_When_ItemId_Based_Rule_Configured()
        {
            var ruleMock = new Mock<IDealsRuleProvider>();
            ruleMock.Setup(provider => provider.GetAll()).Returns(GetItemIdBasedDealRule(1, 1));
            var dealMock = new Mock<IDealsProvider>();
            dealMock.Setup(provider => provider.Get(1)).Returns(GetPayItemDeal(1));
            var dedupStrategy = new DealPreferenceDedupStrategy(dealMock.Object);
            var requestValidator = _container.GetInstance<IPricingRequestValidator>();
            var dealsEngine = new DealsEngine(dealMock.Object, ruleMock.Object, dedupStrategy);
            var pricingService = new BusinessLogic.PricingService(requestValidator, dealsEngine);

            var cart = _dataProvider.GetShoppingCart();
            var response = pricingService.Price(cart);

            Assert.IsNotNull(response);
            Assert.IsTrue(Math.Abs(response.ShoppingCart.CartTotalAfterDiscount.Amount - 4500) < TOLERANCE);
            Assert.IsTrue(Math.Abs(response.ShoppingCart.CartTotal.Amount - 5000) < TOLERANCE);
        }

        [TestMethod]
        public void Should_Apply_Multiple_Deals_When_ItemId_Based_Rule_Configured()
        {
            var ruleMock = new Mock<IDealsRuleProvider>();
            ruleMock.Setup(provider => provider.GetAll()).Returns(GetTwoItemIdBasedDealRule());
            var dealMock = new Mock<IDealsProvider>();
            dealMock.Setup(provider => provider.Get(1)).Returns(GetPayItemDeal(1));
            dealMock.Setup(provider => provider.Get(2)).Returns(GetDiscountedDeal(2));
            var dedupStrategy = new DealPreferenceDedupStrategy(dealMock.Object);
            var requestValidator = _container.GetInstance<IPricingRequestValidator>();
            var dealsEngine = new DealsEngine(dealMock.Object, ruleMock.Object, dedupStrategy);
            var pricingService = new BusinessLogic.PricingService(requestValidator, dealsEngine);

            var cart = _dataProvider.GetShoppingCart();
            var response = pricingService.Price(cart);

            Assert.IsNotNull(response);
            Assert.IsTrue(Math.Abs(response.ShoppingCart.CartTotalAfterDiscount.Amount - 4350) < TOLERANCE);
            Assert.IsTrue(Math.Abs(response.ShoppingCart.CartTotal.Amount - 5000) < TOLERANCE);
        }

        private DiscountDeal GetDiscountedDeal(int dealId)
        {
            return new DiscountDeal
            {
                Id = dealId,
                Preference = 2,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(60),
                Title = "Flat 150 Off",
                Discount = new Money("INR", 150)
            };
        }

        private PayItemDeal GetPayItemDeal(int dealId)
        {
            return new PayItemDeal
            {
                Id = dealId,
                PurchaseUnits = 2,
                FreeUnits = 1,
                Preference = 1,
                StartDate = DateTime.Now,
                IsSoloItemDeal = true,
                EndDate = DateTime.Now.AddDays(60),
                Title = "Buy two, get one free"
            };
        }

        private List<DealRule> GetItemIdBasedDealRule(int dealId, int appliedOnitemid)
        {
            return new List<DealRule>
            {
                new ItemIdBasedDealRule {Id = 1, DealId = dealId, AppliedOnItemIds = new List<int> {appliedOnitemid}}
            };
        }

        private List<DealRule> GetTwoItemIdBasedDealRule()
        {
            return new List<DealRule>
            {
                new ItemIdBasedDealRule {Id = 1, DealId = 1, AppliedOnItemIds = new List<int> {1}},
                new ItemIdBasedDealRule {Id = 2, DealId = 2, AppliedOnItemIds = new List<int> {2}}
            };
        }

        private List<DealRule> GetPayItemDealRule()
        {
            return new List<DealRule>
            {
                new ItemIdBasedDealRule {Id = 1, DealId = 1, AppliedOnItemIds = new List<int> {1}}
            };
        }
    }
}
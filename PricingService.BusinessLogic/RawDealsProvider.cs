using System;
using System.Collections.Generic;
using PricingService.Interfaces;
using PricingService.Models;

namespace PricingService.BusinessLogic
{
    public class RawDealsProvider : IDealsProvider
    {
        private static readonly List<Deal> _deals = new List<Deal>();

        static RawDealsProvider()
        {
            _deals = PopulateDeals();
        }

        public Deal Get(int dealId)
        {
            return _deals.Find(deal => deal.Id.Equals(dealId));
        }

        private static List<Deal> PopulateDeals()
        {
            return new List<Deal>
            {
                GetPayItemDeal(),
                GetDiscountDeal(),
                GetCombiDiscountDeal()
            };
        }

        private static DiscountDeal GetDiscountDeal()
        {
            return new DiscountDeal
            {
                Id = 1,
                Preference = 1,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(60),
                Title = "Flat 100 Off",
                Discount = new Money("INR", 100)
            };
        }

        private static PayItemDeal GetPayItemDeal()
        {
            return new PayItemDeal
            {
                Id = 2,
                FreeUnits = 2,
                Preference = 1,
                PurchaseUnits = 2,
                StartDate = DateTime.Now,
                IsSoloItemDeal = true,
                EndDate = DateTime.Now.AddDays(60),
                Title = "Buy two, get one free"
            };
        }

        private static DiscountDeal GetCombiDiscountDeal()
        {
            return new DiscountDeal
            {
                Id = 3,
                Preference = 2,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(60),
                Title = "Flat 150 Off",
                Discount = new Money("INR", 150)
            };
        }
    }
}
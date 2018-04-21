using System.Collections.Generic;
using PricingService.Interfaces;
using PricingService.Models;

namespace PricingService.BusinessLogic
{
    public class DealsEngine : IDealsEngine
    {
        private readonly IDealDedupStrategy _dealDedupStrategy;
        private readonly IDealsProvider _dealsProvider;
        private readonly IDealsRuleProvider _dealsRuleProvider;

        public DealsEngine(IDealsProvider dealsProvider, IDealsRuleProvider dealsConfigProvider,
            IDealDedupStrategy dealDedupStrategy)
        {
            _dealsProvider = dealsProvider;
            _dealsRuleProvider = dealsConfigProvider;
            _dealDedupStrategy = dealDedupStrategy;
        }

        public List<DealMap> GetAllMatchingDeals(ShoppingCart shoppingCart)
        {
            var dealRules = _dealsRuleProvider.GetAll();
            var dealMaps = new List<DealMap>();
            foreach (var dealRule in dealRules)
            {
                var dealMap = dealRule.GetApplicableDeals(shoppingCart);
                foreach (var dealMapping in dealMap)
                    dealMapping.ApplicableDeal = _dealsProvider.Get(dealMapping.ApplicableDealId);

                dealMaps.AddRange(dealMap);
            }
            return dealMaps;
        }

        public List<DealMap> GetBestDeals(ShoppingCart shoppingCart, List<DealMap> dealMaps)
        {
            return _dealDedupStrategy.Dedup(shoppingCart, dealMaps);
        }

        public Money CalculateDiscount(ShoppingCart shoppingCart, List<DealMap> dealMaps)
        {
            double totalDiscount = 0;

            foreach (var dealMap in dealMaps)
            {
                var discount = dealMap.ApplicableDeal.Apply(shoppingCart, dealMap);
                //var discount = shoppingCart.ApplyDeal(dealMap);

                totalDiscount = totalDiscount + discount.Amount;
            }

            return new Money(shoppingCart.CartTotal.Currency, totalDiscount);
        }

        public Money ApplyDiscount(Money cartCartTotal, Money discount)
        {
            return new Money(cartCartTotal.Currency, cartCartTotal.Amount - discount.Amount);
        }
    }
}
using System.Collections.Generic;

namespace PricingService.Models
{
    public class PriceRangeBasedDealRule : DealRule
    {
        public Money MinimumItemPrice { get; set; }

        public override List<DealMap> GetApplicableDeals(ShoppingCart shoppingCart)
        {
            var deapMaps = new List<DealMap>();

            foreach (var item in shoppingCart.Items)
                if (item.TotalPrice.Amount >= MinimumItemPrice.Amount)
                    deapMaps.Add(CreateDealMap(item));
            return deapMaps;
        }

        private DealMap CreateDealMap(Item item)
        {
            return new DealMap
            {
                ApplicableDealId = DealId,
                DealItemGroups = new List<DealItemsGroup>
                {
                    new DealItemsGroup
                    {
                        ItemIds = new List<int> {item.Id}
                    }
                }
            };
        }
    }

    public class City
    {
        public string Name { get; set; }

        public string State { get; set; }

        public string Country { get; set; }

        public string ZipCode { get; set; }
    }
}
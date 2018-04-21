using System.Collections.Generic;

namespace PricingService.Models
{
    public class ItemIdBasedDealRule : DealRule
    {
        public List<int> AppliedOnItemIds { get; set; }


        public override List<DealMap> GetApplicableDeals(ShoppingCart shoppingCart)
        {
            var deapMaps = new List<DealMap>();
            var noOfmatchingItems = 0;
            foreach (var itemId in AppliedOnItemIds)
                if (shoppingCart.Items.Exists(x => x.Id.Equals(itemId)))
                    noOfmatchingItems++;
            if (noOfmatchingItems == AppliedOnItemIds.Count)
                deapMaps.Add(CreateDealMap());

            return deapMaps;
        }

        private DealMap CreateDealMap()
        {
            return new DealMap
            {
                ApplicableDealId = DealId,
                DealItemGroups = new List<DealItemsGroup>
                {
                    new DealItemsGroup
                    {
                        ItemIds = AppliedOnItemIds
                    }
                }
            };
        }
    }
}
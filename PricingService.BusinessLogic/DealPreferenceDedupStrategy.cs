using System.Collections.Generic;
using System.Linq;
using PricingService.Interfaces;
using PricingService.Models;

namespace PricingService.BusinessLogic
{
    public class DealPreferenceDedupStrategy : IDealDedupStrategy
    {
        private readonly IDealsProvider _dealsProvider;

        public DealPreferenceDedupStrategy(IDealsProvider dealsProvider)
        {
            _dealsProvider = dealsProvider;
        }

        public List<DealMap> Dedup(ShoppingCart cart, List<DealMap> dealMaps)
        {
            foreach (var dealMap in dealMaps)
                dealMap.ApplicableDeal = _dealsProvider.Get(dealMap.ApplicableDealId);

            dealMaps.Sort(new DealComparer());

            var itemToDealMap = new Dictionary<int, DealMap>();
            foreach (var item in cart.Items)
            {
                foreach (var dealMap in dealMaps)
                {
                    var isDealExistsForItem = dealMap.DealItemGroups.Exists(x => x.ItemIds.Contains(item.Id));
                    if (isDealExistsForItem && itemToDealMap.ContainsKey(item.Id) == false)
                        itemToDealMap.Add(item.Id, dealMap);
                }
                if (itemToDealMap.Count.Equals(cart.Items.Count))
                    break;
            }

            return itemToDealMap.Values.ToList();
        }
    }

    public class DealComparer : IComparer<DealMap>
    {
        public int Compare(DealMap x, DealMap y)
        {
            if (y == null || x == null || x.ApplicableDeal?.Preference.CompareTo(y.ApplicableDeal.Preference) == 0)
                return 0;
            if (x.ApplicableDeal != null)
                return x.ApplicableDeal.Preference.CompareTo(y.ApplicableDeal?.Preference);
            return 0;
        }
    }
}
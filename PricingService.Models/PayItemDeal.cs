using System.Linq;

namespace PricingService.Models
{
    public class PayItemDeal : Deal
    {
        public int PurchaseUnits { get; set; }

        public int FreeUnits { get; set; }

        public override Money Apply(ShoppingCart cart, DealMap dealMap)
        {
            double discount = 0;
            if (IsSoloItemDeal)
            {
                var matchingItem = cart.Items.Find(x => x.Id.Equals(dealMap.DealItemGroups.First().ItemIds.First()));

                var dealMultiplier = matchingItem.Quantity / PurchaseUnits;

                discount = dealMultiplier * FreeUnits * (matchingItem.TotalPrice.Amount / matchingItem.Quantity);
            }
            return new Money(cart.CartTotal.Currency, discount);
        }
    }
}
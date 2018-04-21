using System.Collections.Generic;

namespace PricingService.Models
{
    public abstract class DealRule
    {
        public int Id { get; set; }

        public int DealId { get; set; }

        public abstract List<DealMap> GetApplicableDeals(ShoppingCart shoppingCart);
    }
}
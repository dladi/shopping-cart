using System.Collections.Generic;
using PricingService.Models;

namespace PricingService.Interfaces
{
    public interface IDealDedupStrategy
    {
        List<DealMap> Dedup(ShoppingCart cart, List<DealMap> dealMaps);
    }
}
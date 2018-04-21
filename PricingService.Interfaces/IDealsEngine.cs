using System.Collections.Generic;
using PricingService.Models;

namespace PricingService.Interfaces
{
    public interface IDealsEngine
    {
        List<DealMap> GetAllMatchingDeals(ShoppingCart shoppingCart);

        List<DealMap> GetBestDeals(ShoppingCart shoppingCart, List<DealMap> dealMaps);

        Money CalculateDiscount(ShoppingCart shoppingCart, List<DealMap> dealMaps);
        Money ApplyDiscount(Money cartCartTotal, Money discount);
    }
}
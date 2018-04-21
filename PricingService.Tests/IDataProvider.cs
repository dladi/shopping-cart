using PricingService.Models;

namespace PricingService.Tests
{
    public interface IDataProvider
    {
        ShoppingCart GetShoppingCart();
    }
}
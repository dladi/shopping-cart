using PricingService.Models;

namespace PricingService.Interfaces
{
    public interface IPricingService
    {
        PricingResponse Price(ShoppingCart cart);
    }
}
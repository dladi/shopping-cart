using PricingService.Models;

namespace PricingService.Interfaces
{
    public interface IPricingRequestValidator
    {
        ValidatioResult Validate(ShoppingCart cart);
    }
}
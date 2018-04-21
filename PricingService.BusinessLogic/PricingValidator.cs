using PricingService.Interfaces;
using PricingService.Models;

namespace PricingService.BusinessLogic
{
    public class PricingRequestValidator : IPricingRequestValidator
    {
        private readonly ValidatioResult _result = new ValidatioResult();

        public ValidatioResult Validate(ShoppingCart cart)
        {
            if (cart == null)
                _result.Errors.Add("Shopping cart can not be empty.");

            if (cart?.Items == null || cart?.Items.Count <= 0)
                _result.Errors.Add("Shopping cart items can not be empty.");

            ValidateCurrency(cart);

            return _result;
        }

        private void ValidateCurrency(ShoppingCart cart)
        {
            var currency = cart?.CartTotal?.Currency;
            if (!string.IsNullOrWhiteSpace(currency))
            {
                foreach (var item in cart?.Items)
                    if (item?.TotalPrice?.Currency?.Equals(currency) == false)
                        _result.Errors.Add($"Currency can not be different for item {item.Id} : {item.Title}");
            }
            else
                _result.Errors.Add("Shopping cart currency can not be empty");
        }
    }
}
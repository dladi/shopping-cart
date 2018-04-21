using System;
using PricingService.Interfaces;
using PricingService.Models;
using PricingService.Models.ExceptionHandling;

namespace PricingService.BusinessLogic
{
    public class PricingService : IPricingService
    {
        private readonly IDealsEngine _dealsEngine;
        private readonly IPricingRequestValidator _requestValidator;

        public PricingService(IPricingRequestValidator requestValidator, IDealsEngine dealsEngine)
        {
            _requestValidator = requestValidator;
            _dealsEngine = dealsEngine;
        }

        public PricingResponse Price(ShoppingCart cart)
        {
            var result = _requestValidator.Validate(cart);

            if (result.IsValid)
            {
                try
                {
                    var matchingDeals = _dealsEngine.GetAllMatchingDeals(cart);

                    var bestDeals = _dealsEngine.GetBestDeals(cart, matchingDeals);

                    var discount = _dealsEngine.CalculateDiscount(cart, bestDeals);

                    cart.CartTotalAfterDiscount = _dealsEngine.ApplyDiscount(cart.CartTotal, discount);

                    return new PricingResponse {ShoppingCart = cart};
                }
                catch (BaseAppException)
                {
                    throw;
                }
                catch (Exception ex)
                {
                    //TODO: Log exception ex 
                    throw new CalculationException("001", "Unknown error occured");
                }
            }
            return new PricingResponse {ValidationResult = result};
        }
    }
}
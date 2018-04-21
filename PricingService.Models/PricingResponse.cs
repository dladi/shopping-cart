namespace PricingService.Models
{
    public class PricingResponse
    {
        public ShoppingCart ShoppingCart { get; set; }

        public ValidatioResult ValidationResult { get; set; }
    }
}
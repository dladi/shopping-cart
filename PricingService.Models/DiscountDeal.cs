namespace PricingService.Models
{
    public class DiscountDeal : Deal
    {
        public Money Discount { get; set; }

        public override Money Apply(ShoppingCart shoppingCart, DealMap dealMap)
        {
            return Discount;
        }
    }
}
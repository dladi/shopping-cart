namespace PricingService.Models
{
    public class Money
    {
        public Money(string currency, double amount)
        {
            Currency = currency;
            Amount = amount;
        }

        public string Currency { get; set; }

        public double Amount { get; set; }
    }
}
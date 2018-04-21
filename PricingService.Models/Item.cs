namespace PricingService.Models
{
    public abstract class Item
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public Money TotalPrice { get; set; }

        public int Quantity { get; set; }
    }
}
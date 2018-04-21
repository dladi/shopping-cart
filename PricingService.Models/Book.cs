namespace PricingService.Models
{
    public class Book : Item
    {
        public int NoOfPages { get; set; }

        public string Author { get; set; }
    }
}
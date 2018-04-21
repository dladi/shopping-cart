using System.Drawing;

namespace PricingService.Models
{
    public class Mobile : Item
    {
        public string Brand { get; set; }

        public Color Color { get; set; }

        public double WeightInGms { get; set; }
    }
}
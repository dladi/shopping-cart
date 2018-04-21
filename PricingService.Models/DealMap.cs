using System.Collections.Generic;

namespace PricingService.Models
{
    public class DealMap
    {
        public List<DealItemsGroup> DealItemGroups { get; set; }

        public int ApplicableDealId { get; set; }

        public Deal ApplicableDeal { get; set; }
    }
}
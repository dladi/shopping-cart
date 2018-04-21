using System;

namespace PricingService.Models
{
    public abstract class Deal
    {
        public int Id { get; set; }

        //public List<DealItemsGroup> ApplicableItems { get; set; }

        public string Title { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        //public  bool IsPackageDeal{ get; set; }

        public int Preference { get; set; }

        public bool IsSoloItemDeal { get; set; }

        public abstract Money Apply(ShoppingCart shoppingCart, DealMap dealMap);
    }
}
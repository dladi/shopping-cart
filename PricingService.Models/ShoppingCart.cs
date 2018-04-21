using System.Collections.Generic;
using System.Linq;

namespace PricingService.Models
{
    public class ShoppingCart
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Item> Items { get; set; }

        public Money CartTotalAfterDiscount { get; set; }

        public Money CartTotal
        {
            get
            {
                double cartTotal = 0;
                var currency = "";
                if (Items != null)
                {
                    currency = Items?.First()?.TotalPrice?.Currency;
                    foreach (var item in Items)
                    {
                        if (item?.TotalPrice != null)
                        {
                            cartTotal += item.TotalPrice.Amount;
                        }
                    }
                }
                return new Money(currency, cartTotal);
            }
        }

        public List<DealMap> AppliedDeals { get; set; }
    }
}
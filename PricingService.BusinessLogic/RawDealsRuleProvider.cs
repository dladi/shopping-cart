using System.Collections.Generic;
using PricingService.Interfaces;
using PricingService.Models;

namespace PricingService.BusinessLogic
{
    public class RawDealsRuleProvider : IDealsRuleProvider
    {
        public List<DealRule> GetAll()
        {
            return new List<DealRule>();
        }
    }
}
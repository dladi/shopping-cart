using System.Collections.Generic;
using PricingService.Models;

namespace PricingService.Interfaces
{
    public interface IDealsRuleProvider
    {
        List<DealRule> GetAll();
    }
}
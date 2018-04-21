using PricingService.Models;

namespace PricingService.Interfaces
{
    public interface IDealsProvider
    {
        Deal Get(int dealId);
    }
}
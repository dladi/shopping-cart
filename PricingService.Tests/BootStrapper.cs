using PricingService.BusinessLogic;
using PricingService.Interfaces;
using StructureMap;

namespace PricingService.Tests
{
    public class BootStrapper
    {
        public static IContainer GetContainer()
        {
            return new Container(container =>
            {
                container.For<IDealDedupStrategy>().Use<DealPreferenceDedupStrategy>();
                container.For<IDealsProvider>().Use<RawDealsProvider>();
                container.For<IDealsRuleProvider>().Use<RawDealsRuleProvider>();
                container.For<IDealsEngine>().Use<DealsEngine>();
                container.For<IPricingService>().Use<BusinessLogic.PricingService>();
                container.For<IPricingRequestValidator>().Use<PricingRequestValidator>();
            });
        }
    }
}
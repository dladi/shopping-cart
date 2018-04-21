using PricingService.Interfaces;
using StructureMap;

namespace PricingService.BusinessLogic
{
    public class BootStrapper
    {
        public static IContainer Container
        {
            get
            {
                return new Container(container =>
                {
                    container.For<IDealDedupStrategy>().Use<DealPreferenceDedupStrategy>();
                    container.For<IDealsProvider>().Use<RawDealsProvider>();
                    container.For<IDealsRuleProvider>().Use<RawDealsRuleProvider>();
                    container.For<IDealsEngine>().Use<DealsEngine>();
                    container.For<IPricingService>().Use<PricingService>();
                    container.For<IPricingRequestValidator>().Use<PricingRequestValidator>();
                });
            }
        }
    }
}
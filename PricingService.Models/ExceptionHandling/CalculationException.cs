namespace PricingService.Models.ExceptionHandling
{
    public class CalculationException : BaseAppException
    {
        public CalculationException(string code, string message)
            : base(code, message)
        {
        }
    }
}
namespace PricingService.Models.ExceptionHandling
{
    public class LookupException : BaseAppException
    {
        public LookupException(string code, string message)
            : base(code, message)
        {
        }
    }
}
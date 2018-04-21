using System;

namespace PricingService.Models.ExceptionHandling
{
    public abstract class BaseAppException : Exception
    {
        private string _code;
        private string _message;

        public BaseAppException(string code, string message)
        {
            _code = code;
            _message = message;
        }
    }
}
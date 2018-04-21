using System.Collections.Generic;

namespace PricingService.Models
{
    public class ValidatioResult
    {
        public ValidatioResult()
        {
            Errors = new List<string>();
            Warnings = new List<string>();
        }

        public List<string> Warnings { get; set; }

        public List<string> Errors { get; set; }

        public bool IsValid
        {
            get
            {
                if (Errors.Count > 0)
                    return false;

                return true;
            }
        }
    }
}
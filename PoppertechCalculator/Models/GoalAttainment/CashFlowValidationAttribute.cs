using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PoppertechCalculator.Models.GoalAttainment
{
    public class CashFlowValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var cashflows = (IList<decimal>)value;
            if(!cashflows.Any())
                return new ValidationResult("There must be at least 1 cash flow");
            foreach (var cashFlow in cashflows)
            {
                if (cashFlow < -99999999 || cashFlow > 99999999)
                    return new ValidationResult("Cash flows must be between " + -99999999 + " and " + 99999999);
            }
            return ValidationResult.Success;
        }
    }
}
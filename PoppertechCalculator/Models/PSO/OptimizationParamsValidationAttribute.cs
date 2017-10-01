using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PoppertechCalculator.Models.PSO
{
    public class OptimizationParamsValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var errorMessages = new List<string>();
            var optimizationParams = (OptimizationContext)value;
            if (optimizationParams.Interval < 1 || optimizationParams.Interval > 9999999)
                errorMessages.Add("Interval must be between" + 1 + " and " + 9999999);
            if (optimizationParams.LowerBound < 0 || optimizationParams.LowerBound > (9999999 - optimizationParams.Interval))
                errorMessages.Add("The lower bound must be greater than zero and less than the upper bound, and there must be at least one interval for the optimization to be valid.");
            if (optimizationParams.UpperBound < (optimizationParams.LowerBound + optimizationParams.Interval) || optimizationParams.UpperBound > 99999999)
                errorMessages.Add("The upper bound must be greater than the lower bound and less than 99,999,999, and there must be at least one interval for the optimization to be valid.");
            if(errorMessages.Any())
                return new ValidationResult("Optimization parameters are invalid", errorMessages);
            return ValidationResult.Success;
        }
    }
}
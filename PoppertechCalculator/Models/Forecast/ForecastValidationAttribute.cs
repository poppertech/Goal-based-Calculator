using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PoppertechCalculator.Models
{
    public class ForecastValidationAttribute: ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var errorMessages = new List<string>();
            var forecast = (Forecast)value;
            if (forecast.Minimum > forecast.Worst)
                errorMessages.Add("Minimum");
            if(forecast.Worst > forecast.Likely)
                errorMessages.Add("Worst");
            if(forecast.Likely > forecast.Best)
                errorMessages.Add("Likely");
            if(forecast.Best > forecast.Maximum)
                errorMessages.Add("Best");
            if (errorMessages.Any())
                return new ValidationResult("Forecast is invalid", errorMessages);
            return ValidationResult.Success;
        }
    }
}
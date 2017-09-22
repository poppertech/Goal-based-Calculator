using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PoppertechCalculator.Models
{
    public class ForecastRegion
    {
        [Required]
        [StringLength(16)]
        public string Name { get; set; }

        [Required]
        [ForecastValidation]
        public Forecast Forecast { get; set; }
    }
}
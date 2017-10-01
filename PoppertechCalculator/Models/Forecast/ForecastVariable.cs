using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PoppertechCalculator.Models
{
    public class ForecastVariable
    {
        public ForecastVariable(){}

        public ForecastVariable(ForecastVariable other) 
        {
            this.Name = other.Name;
            this.Regions = other.Regions;
            this.Parent = other.Parent;
        }

        [Required]
        [StringLength(16)]
        public string Name { get; set; }

        [Required]
        [MinLength(1)]
        public IList<ForecastRegion> Regions { get; set; }

        [StringLength(16)]
        public string Parent { get; set; }
    }
}
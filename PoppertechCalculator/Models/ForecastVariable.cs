using System;
using System.Collections.Generic;
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

        public string Name { get; set; }
        public IList<ForecastRegion> Regions { get; set; }
        public string Parent { get; set; }
    }
}
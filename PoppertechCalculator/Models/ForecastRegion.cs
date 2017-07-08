using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PoppertechCalculator.Models
{
    public class ForecastRegion
    {
        public RegionName? Name { get; set; }
        public Forecast Forecast { get; set; }
    }
}
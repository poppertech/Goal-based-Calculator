using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PoppertechCalculator.Models
{
    public class ForecastRegion
    {
        public string Name { get; set; }
        public IEnumerable<TextValuePair<decimal>> Forecast { get; set; }
    }
}
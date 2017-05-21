using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PoppertechCalculator.Models
{
    public class ForecastVariable
    {
        public string Name { get; set; }

        public IEnumerable<ForecastRegion> Regions { get; set; }
    }
}
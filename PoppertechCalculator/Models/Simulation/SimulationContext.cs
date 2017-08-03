using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PoppertechCalculator.Models
{
    public class SimulationContext
    {
        public decimal AreaLower { get; set; }
        public decimal Slope { get; set; }
        public decimal Intercept { get; set; }
        public decimal XLower { get; set; }
    }
}
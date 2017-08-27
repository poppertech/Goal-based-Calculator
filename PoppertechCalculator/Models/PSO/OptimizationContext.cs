using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PoppertechCalculator.Models.PSO
{
    public class OptimizationContext
    {
        public decimal LowerBound { get; set; }
        public decimal UpperBound { get; set; }
        public decimal Interval { get; set; }
    }
}
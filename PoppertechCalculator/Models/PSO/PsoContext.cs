using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PoppertechCalculator.Models.PSO
{
    public class PsoContext:GoalAttainmentContext
    {
        public decimal PositionLowerBound { get; set; }
        public decimal PositionUpperBound { get; set; }
        public decimal Interval { get; set; }
    }
}
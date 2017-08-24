using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PoppertechCalculator.Models.PSO
{
    public class PsoResults
    {
        public IDictionary<string, decimal> ChartData { get; set; }
        public IEnumerable<OptimalInvestmentResult> OptimalInvestments { get; set; }
    }
}
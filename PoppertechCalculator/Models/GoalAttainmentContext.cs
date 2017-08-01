using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PoppertechCalculator.Models
{
    public class GoalAttainmentContext
    {
        public IList<decimal> CashFlows { get; set; }
        public IEnumerable<InvestmentContext> InvestmentContexts { get; set; }
    }
}
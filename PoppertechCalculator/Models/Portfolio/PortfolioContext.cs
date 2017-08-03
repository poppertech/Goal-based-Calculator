using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PoppertechCalculator.Models
{
    public class PortfolioContext
    {
        public IList<PortfolioInvestmentContext> InvestmentContexts { get; set; }

        public IList<decimal> CashFlows { get; set; }
    }
}
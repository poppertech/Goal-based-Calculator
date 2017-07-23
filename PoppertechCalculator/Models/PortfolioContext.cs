using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PoppertechCalculator.Models
{
    public class PortfolioContext
    {
        public InvestmentContext[] InvestmentContexts { get; set; }
        public decimal[] CashFlows { get; set; }
    }
}
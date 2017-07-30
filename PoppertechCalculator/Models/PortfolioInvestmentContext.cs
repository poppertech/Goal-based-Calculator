using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PoppertechCalculator.Models
{
    public class PortfolioInvestmentContext: InvestmentContext
    {
        public PortfolioInvestmentContext(){}

        public PortfolioInvestmentContext(InvestmentContext other):base(other){}

        public decimal[,] TimeSeriesReturns { get; set; }
    }
}
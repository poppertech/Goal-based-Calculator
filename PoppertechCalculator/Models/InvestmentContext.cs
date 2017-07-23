using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PoppertechCalculator.Models
{
    public class InvestmentContext: ForecastVariable
    {
        public decimal Amount { get; set; }

        public decimal Weight { get; set; }

        public decimal[][] TimeSeriesReturns { get; set; }
    }
}
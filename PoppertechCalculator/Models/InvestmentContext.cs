using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PoppertechCalculator.Models
{
    public class InvestmentContext: ForecastVariable
    {
        public InvestmentContext(){}

        public InvestmentContext(InvestmentContext other):base(other)
        {
            this.InitialPrice = other.InitialPrice;
            this.Amount = other.Amount;
            this.Weight = other.Weight;
        }

        public decimal InitialPrice { get; set; }
        public decimal Amount { get; set; }
        public decimal Weight { get; set; }

    }
}
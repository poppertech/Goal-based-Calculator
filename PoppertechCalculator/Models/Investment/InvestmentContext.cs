using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PoppertechCalculator.Models
{
    public class InvestmentContext : ForecastVariable
    {
        public InvestmentContext() { }

        public InvestmentContext(InvestmentContext other)
            : base(other)
        {
            this.InitialPrice = other.InitialPrice;
            this.Amount = other.Amount;
        }

        [Range(1, 1000)]
        public decimal InitialPrice { get; set; }

        [Range(0, 99999999)]
        public decimal Amount { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PoppertechCalculator.Models.Portfolio
{
    public class PortfolioResult
    {
        public int Id { get; set; }
        public string Year { get; set; }
        public decimal Probability { get; set; }
    }
}
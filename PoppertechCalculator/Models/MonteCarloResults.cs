using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PoppertechCalculator.Models
{
    public class MonteCarloResults
    {
        public IList<int> AreaNumbers { get; set; }
        public IList<decimal> Simulations { get; set; }
    }
}
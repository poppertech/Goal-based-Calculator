using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PoppertechCalculator.Models
{
    public class InvestmentStatistics
    {
        public string Investment { get; set; }
        public IEnumerable<TextValuePair<decimal>> Statistics { get; set; }
        
    }

}
using PoppertechCalculator.Models.GoalAttainment;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PoppertechCalculator.Models
{
    public class GoalAttainmentContext
    {
        [Required]
        [CashFlowValidation]
        public IList<decimal> CashFlows { get; set; }

        [Required]
        public IEnumerable<InvestmentContext> InvestmentContexts { get; set; }
    }
}
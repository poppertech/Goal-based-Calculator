﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PoppertechCalculator.Models
{
    public class GoalAttainmentContext
    {
        public decimal[] CashFlows { get; set; }
        public InvestmentContext[] InvestmentContexts { get; set; }
    }
}
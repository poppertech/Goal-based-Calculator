﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PoppertechCalculator.Models.PSO
{
    public class PsoContext:GoalAttainmentContext
    {
        public OptimizationContext OptimizationParams { get; set; }
    }
}
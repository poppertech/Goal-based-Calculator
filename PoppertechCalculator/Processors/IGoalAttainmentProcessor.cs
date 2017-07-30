﻿using PoppertechCalculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoppertechCalculator.Processors
{
    public interface IGoalAttainmentProcessor
    {
        Dictionary<string, decimal> CalculateGoalAttainment(GoalAttainmentContext context);
    }
}

using PoppertechCalculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoppertechCalculator.Processors
{
    public interface IGoalAttainmentProcessor
    {
        IDictionary<string, decimal> GetGoalAttainmentChartData();
        IDictionary<string, decimal> CalculateGoalAttainmentChartData(GoalAttainmentContext context);
        IEnumerable<decimal> CalculateGoalAttainment(GoalAttainmentContext context);
    }
}

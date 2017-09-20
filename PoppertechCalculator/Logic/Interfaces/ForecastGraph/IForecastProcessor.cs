using PoppertechCalculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PoppertechCalculator.Logic.Interfaces.ForecastGraph
{
    public interface IForecastProcessor
    {
        IEnumerable<ForecastVariable> GetForecastVariables();
    }
}
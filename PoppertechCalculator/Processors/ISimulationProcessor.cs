using System;
using System.Collections.Generic;
namespace PoppertechCalculator.Processors
{
    public interface ISimulationProcessor
    {
        IEnumerable<PoppertechCalculator.Models.SimulationResults> SimulateInvestments(IEnumerable<PoppertechCalculator.Models.ForecastVariable> request);
    }
}

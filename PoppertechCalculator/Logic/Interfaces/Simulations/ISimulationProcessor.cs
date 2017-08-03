using PoppertechCalculator.Models;
using System;
using System.Collections.Generic;
namespace PoppertechCalculator.Processors
{
    public interface ISimulationProcessor
    {
        IEnumerable<SimulationResults> SimulateInvestments(IEnumerable<ForecastVariable> request);
    }
}

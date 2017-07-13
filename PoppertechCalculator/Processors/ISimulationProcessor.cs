using PoppertechCalculator.Models;
using System;
using System.Collections.Generic;
namespace PoppertechCalculator.Processors
{
    public interface ISimulationProcessor
    {
        SimulationResults[] SimulateInvestments(ForecastVariable[] request);
    }
}

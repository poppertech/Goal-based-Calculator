using PoppertechCalculator.Models;
using PoppertechCalculator.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PoppertechCalculator.Processors
{
    public class SimulationProcessor : ISimulationProcessor
    {
        private IStatisticsCalculations _statisticsCalculations;
        private IHistogramCalculations _histogramCalculations;
        private IJointSimulator _jointSimulator;

        public SimulationProcessor(IStatisticsCalculations statisticsCalculations, 
            IHistogramCalculations histogramCalculations,
            IJointSimulator jointSimulator)
        {
            _statisticsCalculations = statisticsCalculations;
            _histogramCalculations = histogramCalculations;
            _jointSimulator = jointSimulator;
        }


        public SimulationResults[] SimulateInvestments(ForecastVariable[] forecasts)
        {

            var simulationResults = new SimulationResults[forecasts.Length];

            var unConditionalForecast = forecasts.Where(f => string.IsNullOrWhiteSpace(f.Parent)).Single();
            var unConditionalSimulations = _jointSimulator.CalculateUnconditionalSimulations(unConditionalForecast.Name, unConditionalForecast.Regions[0].Forecast);
            var unConditionalAreaNumbers = unConditionalSimulations.AreaNumbers;
            var histogramContext = new HistogramContext{Simulations =  unConditionalSimulations.Simulations, 
                GlobalXMin = unConditionalForecast.Regions[0].Forecast.Minimum, 
                GlobalXMax = unConditionalForecast.Regions[0].Forecast.Maximum
            };
            simulationResults[0] = GetSimulationResults(unConditionalForecast.Name, histogramContext);

            var conditionalForecasts = forecasts.Where(f => !string.IsNullOrWhiteSpace(f.Parent)).ToArray();

            for (int cnt = 0; cnt < conditionalForecasts.Length; cnt++)
			{
                var conditionalForecast = conditionalForecasts[cnt];
                var regions = conditionalForecast.Regions.ToArray();
                var jointSimulations = _jointSimulator.CalculateJointSimulations(unConditionalAreaNumbers, conditionalForecast.Name, regions);

                var simulationResult = GetSimulationResults(conditionalForecast.Name, jointSimulations);

                simulationResults[cnt + 1] = simulationResult;
			}
            return simulationResults;
        }

        private SimulationResults GetSimulationResults(string investmentName, HistogramContext context)
        {
            var investmentStat = _statisticsCalculations.GetStatistics(context.Simulations);
            var globalXMin = context.GlobalXMin;
            var globalXMax = context.GlobalXMax;
            var histogramData = _histogramCalculations.GetHistogramData(context);
            var simulationResult = new SimulationResults();
            simulationResult.InvestmentName = investmentName;
            simulationResult.Statistics = investmentStat;
            simulationResult.HistogramsData = histogramData;
            return simulationResult;
        }

    }
}
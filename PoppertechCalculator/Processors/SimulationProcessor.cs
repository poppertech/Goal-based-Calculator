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

            var unconditionalSimulationResults = CalculateUnConditionalSimulationResults(forecasts);
            simulationResults[0] = unconditionalSimulationResults;
            var conditionalSimulationResults = CalculateConditionalSimulationResults(forecasts, unconditionalSimulationResults.AreaNumbers);

            conditionalSimulationResults.CopyTo(simulationResults, 1);

            return simulationResults;
        }

        private UnconditionalSimulationResults CalculateUnConditionalSimulationResults(ForecastVariable[] forecasts)
        {
            var unConditionalForecast = forecasts.Where(f => string.IsNullOrWhiteSpace(f.Parent)).Single();
            var unConditionalSimulations = _jointSimulator.CalculateUnconditionalSimulations(unConditionalForecast.Name, unConditionalForecast.Regions[0].Forecast);
            var unConditionalAreaNumbers = unConditionalSimulations.AreaNumbers;
            var histogramContext = new HistogramContext{Simulations =  unConditionalSimulations.Simulations, 
                GlobalXMin = unConditionalForecast.Regions[0].Forecast.Minimum, 
                GlobalXMax = unConditionalForecast.Regions[0].Forecast.Maximum
            };
            var simulationResults = GetSimulationResults(unConditionalForecast.Name, histogramContext);
            var unconditionalSimulationResults = new UnconditionalSimulationResults()
            {
                InvestmentName = simulationResults.InvestmentName,
                HistogramsData = simulationResults.HistogramsData,
                Statistics = simulationResults.Statistics,
                AreaNumbers = unConditionalAreaNumbers
            };
            return unconditionalSimulationResults;
        }

        private SimulationResults[] CalculateConditionalSimulationResults(ForecastVariable[] forecasts, int[] unConditionalAreaNumbers)
        {
            var conditionalForecasts = forecasts.Where(f => !string.IsNullOrWhiteSpace(f.Parent)).ToArray();
            var simulationResults = new SimulationResults[conditionalForecasts.Length];

            for (int cnt = 0; cnt < conditionalForecasts.Length; cnt++)
            {
                var histogramContext = new HistogramContext();
                var conditionalForecast = conditionalForecasts[cnt];
                var regions = conditionalForecast.Regions.ToArray();
                var jointSimulations = _jointSimulator.CalculateJointSimulations(unConditionalAreaNumbers, conditionalForecast.Name, regions);
                histogramContext.Simulations = jointSimulations.Simulations;
                histogramContext.GlobalXMin = regions[0].Forecast.Minimum;
                histogramContext.GlobalXMax = regions[regions.Length - 1].Forecast.Maximum;
                var simulationResult = GetSimulationResults(conditionalForecast.Name, histogramContext);

                simulationResults[cnt] = simulationResult;
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
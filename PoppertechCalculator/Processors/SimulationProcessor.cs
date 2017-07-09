using PoppertechCalculator.Models;
using PoppertechCalculator.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PoppertechCalculator.Processors
{
    public class SimulationProcessor : PoppertechCalculator.Processors.ISimulationProcessor
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


        public IEnumerable<SimulationResults> SimulateInvestments(IEnumerable<ForecastVariable> request)
        {
            var forecasts = request.ToArray();

            var simulationResults = new SimulationResults[forecasts.Length];
            
            for (int cnt = 0; cnt < forecasts.Length; cnt++)
			{
			    var forecast = forecasts[cnt];
                var regions = forecast.Regions.ToArray();
                var jointSimulations = _jointSimulator.CalculateJointSimulations(regions);
                var investmentStat = _statisticsCalculations.GetStatistics(jointSimulations);
                var xMinGlobal = _jointSimulator.GetGlobalXMin();
                var xMaxGlobal = _jointSimulator.GetGlobalXMax();
                var histogramData = _histogramCalculations.GetHistogramData(jointSimulations, xMinGlobal, xMaxGlobal);
                var simulationResult = new SimulationResults();
                simulationResult.InvestmentName = forecast.Name; 
                simulationResult.Statistics = investmentStat;
                simulationResult.HistogramsData = histogramData;
                simulationResults[cnt] = simulationResult;
			}
            return simulationResults;
        }



    }
}
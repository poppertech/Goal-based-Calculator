using PoppertechCalculator.Models;
using PoppertechCalculator.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PoppertechCalculator.Processors
{
    public class SimulationProcessor
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


        public SimulationResults SimulateInvestments(IEnumerable<ForecastVariable> request)
        {
            var forecasts = request.ToArray();

            var investmentStats = new InvestmentStatistics[forecasts.Length];
            var histogramsData = new IEnumerable<HistogramData>[forecasts.Length];
            
            var simulationResults = new SimulationResults() { 
                InvestmentsStatistics = investmentStats,
                HistogramsData = histogramsData
            };
            
            for (int cnt = 0; cnt < forecasts.Length; cnt++)
			{
			    var forecast = forecasts[cnt];
                var regions = forecast.Regions.ToArray();
                var jointSimulations = _jointSimulator.CalculateJointSimulations(regions);
                var investmentStat = _statisticsCalculations.GetStatistics(jointSimulations);
                var xMinGlobal = _jointSimulator.GetGlobalXMin();
                var xMaxGlobal = _jointSimulator.GetGlobalXMax();
                var histogramData = _histogramCalculations.GetHistogramData(jointSimulations, xMinGlobal, xMaxGlobal);
                investmentStats[cnt] = investmentStat;
                histogramsData[cnt] = histogramData; 
			}
            return simulationResults;
        }



    }
}
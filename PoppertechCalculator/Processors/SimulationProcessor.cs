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
        private IUniformRandomRepository _repository;
        private IForecastGraphCalculations _forecastGraphCalcs;

        public SimulationProcessor(IUniformRandomRepository repository, IForecastGraphCalculations forecastGraphCalcs)
        {
            _repository = repository;
            _forecastGraphCalcs = forecastGraphCalcs;
        }

        public bool ReturnTrue(string test)
        {
            var rand = _repository.GetUniformRandByTicker(test);
            if (rand.Any())
                return true;
            return false;
        }

        public IEnumerable<InvestmentStatistics> SimulateInvestments(IEnumerable<ForecastVariable> request)
        {
            var forecasts = request.ToArray();
            var investmentStats = new InvestmentStatistics[forecasts.Length];
            for (int cnt = 0; cnt < forecasts.Length; cnt++)
			{
			    var forecast = forecasts[cnt];
                var investmentStat = SimulateInvestment(forecast);
                investmentStats[cnt] = investmentStat;
			}
            return investmentStats;
        }

        private InvestmentStatistics SimulateInvestment(ForecastVariable forecast)
        {
            var regions = forecast.Regions.ToArray();
            for (int cnt = 0; cnt < regions.Length; cnt++)
            {
                var region = regions[cnt];
                var context = _forecastGraphCalcs.GetSimulationContext(region.Forecast);

            }
            return new InvestmentStatistics();
        }



    }
}
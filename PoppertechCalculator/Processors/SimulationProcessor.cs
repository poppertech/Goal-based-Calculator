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
        private UniformRandomRepository _repository;

        public SimulationProcessor()
        {
            _repository = new UniformRandomRepository("ProbicastCalculator");
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
                var context = ForecastGraphCalculations.GetSimulationContext(region.Forecast);

            }
            return new InvestmentStatistics();
        }



    }
}
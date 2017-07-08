using PoppertechCalculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace PoppertechCalculator.Controllers
{
    [RoutePrefix("simulation")]
    public class SimulationController : ApiController
    {
        public SimulationController()
        {

        }

        [HttpGet]
        [Route("")]
        [ResponseType(typeof(string))]
        public IHttpActionResult Get()
        {
            return Ok("Success");
        }


        [HttpPost]
        [Route("")]
        [ResponseType(typeof(Response<IEnumerable<InvestmentStatistics>>))]
        public IHttpActionResult Post([FromBody] IEnumerable<ForecastVariable> request)
        {
            var stocksStats = new InvestmentStatistics
            {
                Investment = InvestmentName.Stocks,
                Statistics = new Statistics { Mean = 5, Stdev = 15, Skew = -.03m, Kurt = 2 }
            };

            var bondsStats = new InvestmentStatistics
            {
                Investment = InvestmentName.Bonds,
                Statistics = new Statistics { Mean = 2, Stdev = 5, Skew = -.05m, Kurt = 3 }
            };

            var investmentStats = new InvestmentStatistics[]{ stocksStats, bondsStats};
            var response = new Response<IEnumerable<InvestmentStatistics>> { Model = investmentStats };

            return Ok(response);
        }
    }
}

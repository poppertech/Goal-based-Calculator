using PoppertechCalculator.Models;
using PoppertechCalculator.Processors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace PoppertechCalculator.Controllers
{
    [RoutePrefix("portfolio")]
    public class PortfolioSimulationController : ApiController
    {
        private readonly IGoalAttainmentProcessor _processor;

        public PortfolioSimulationController(IGoalAttainmentProcessor processor)
        {
            _processor = processor;
        }

        [HttpGet]
        [Route("")]
        [ResponseType(typeof(Response<IDictionary<string, decimal>>))]
        public IHttpActionResult Get()
        {
            var probabilityChartData = _processor.GetGoalAttainmentChartData();
            var response = new Response<IDictionary<string, decimal>> { Model = probabilityChartData };
            return Ok(response);
        }

        [HttpPost]
        [Route("")]
        [ResponseType(typeof(Response<IDictionary<string,decimal>>))]
        public IHttpActionResult Post([FromBody] GoalAttainmentContext request)
        {
            var probabilityChartData = _processor.CalculateGoalAttainmentChartData(request);
            var response = new Response<IDictionary<string, decimal>> { Model = probabilityChartData };
            return Ok(response);
        }
    }
}

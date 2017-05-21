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
        public IHttpActionResult Post([FromBody] ScenarioRequest request)
        {
            var investmentStats = new InvestmentStatistics[]{
                new InvestmentStatistics{
                    Investment = "Stocks", 
                    Statistics =new []{
                        new TextValuePair<decimal>{Text = "Mean", Value = 5}, 
                        new TextValuePair<decimal>{Text = "Stdev",Value = 15}, 
                        new TextValuePair<decimal>{Text = "Skew", Value = -.03m}, 
                        new TextValuePair<decimal>{Text= "Kurt",Value = 2}}},
                new InvestmentStatistics{
                    Investment = "Bonds", 
                    Statistics = new[] {
                        new TextValuePair<decimal>{Text = "Mean", Value = 2}, 
                        new TextValuePair<decimal>{Text = "Stdev", Value = 5}, 
                        new TextValuePair<decimal>{Text = "Skew",Value = -.05m}, 
                        new TextValuePair<decimal>{Text = "Kurt",Value = 3}}},
                new InvestmentStatistics{
                    Investment = "Portfolio", Statistics = new[]{
                    new TextValuePair<decimal>{Text = "Mean", Value = 3}, 
                    new TextValuePair<decimal>{Text = "Stdev",Value = 10}, 
                    new TextValuePair<decimal>{Text = "Skew",Value = -.04m}, 
                    new TextValuePair<decimal>{Text = "Kurt",Value = 2.5m}}}
                };

            var response = new Response<IEnumerable<InvestmentStatistics>>{Model = investmentStats};

            return Ok(response);
        }
    }
}

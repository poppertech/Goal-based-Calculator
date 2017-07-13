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
    [RoutePrefix("forecast")]
    public class ForecastController : ApiController
    {
        public ForecastController()
        {

        }

        [HttpGet]
        [Route("")]
        [ResponseType(typeof(Response<IEnumerable<ForecastVariable>>))]
        public IHttpActionResult Get()
        {
            var unconditionalForecast = new Forecast { Minimum = 40, Worst = 75, Likely = 100, Best = 130, Maximum = 150 };

            var leftTailForecast = new Forecast { Minimum = 20, Worst = 40, Likely = 80, Best = 100, Maximum = 120 };

            var leftNormalForecast = new Forecast { Minimum = 30, Worst = 60, Likely = 90, Best = 110, Maximum = 130 };

            var rightNormalForecast = new Forecast { Minimum = 50, Worst = 90, Likely = 110, Best = 140, Maximum = 160 };

            var rightTailForecast = new Forecast { Minimum = 60, Worst = 100, Likely = 120, Best = 150, Maximum = 170 };

            var gdp = new ForecastVariable
            {
                Name = "GDP",
                Regions = new[]{
                    new ForecastRegion{
                        Forecast = unconditionalForecast}}
            };

            var stocks = new ForecastVariable
            {
                Name = "Stocks",
                Regions = new[]{
                    new ForecastRegion{
                        Name = "LeftTail",
                        Forecast = leftTailForecast
                    },
                    new ForecastRegion{
                        Name = "LeftNormal",
                        Forecast = leftNormalForecast
                    },
                    new ForecastRegion{
                        Name = "RightNormal",
                        Forecast = rightNormalForecast
                    },
                    new ForecastRegion{
                        Name = "RightTail",
                        Forecast = rightTailForecast
                    }
                },
                Parent = "GDP"
            };

            var bonds = new ForecastVariable
            {
                Name = "Bonds",
                Regions = new[]{
                    new ForecastRegion{
                        Name = "LeftTail",
                        Forecast = leftTailForecast
                    },
                    new ForecastRegion{
                        Name = "LeftNormal",
                        Forecast = leftNormalForecast
                    },
                    new ForecastRegion{
                        Name = "RightNormal",
                        Forecast = rightNormalForecast
                    },
                    new ForecastRegion{
                        Name = "RightTail",
                        Forecast = rightTailForecast
                    }
                },
                Parent = "GDP"
            };

            var variables = new[] { gdp, stocks, bonds };
            var response = new Response<IEnumerable<ForecastVariable>> { Model = variables };
            return Ok(response);
        }

    }
}

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
            var unconditionalForecast = new []{
                new TextValuePair<decimal>{Text = "Minimum", Value = 40},
                new TextValuePair<decimal>{Text = "Worst Case", Value = 75},
                new TextValuePair<decimal>{Text = "Most Likely", Value = 100},
                new TextValuePair<decimal>{Text = "Best Case", Value = 130},
                new TextValuePair<decimal>{Text = "Maximum", Value = 150}
            };

            var leftTailForecast = new []{
                new TextValuePair<decimal>{Text = "Minimum", Value = 20},
                new TextValuePair<decimal>{Text = "Worst Case", Value = 40},
                new TextValuePair<decimal>{Text = "Most Likely", Value = 80},
                new TextValuePair<decimal>{Text = "Best Case", Value = 100},
                new TextValuePair<decimal>{Text = "Maximum", Value = 120}
            };

            var leftNormalForecast = new []{
                new TextValuePair<decimal>{Text = "Minimum", Value = 30},
                new TextValuePair<decimal>{Text = "Worst Case", Value = 60},
                new TextValuePair<decimal>{Text = "Most Likely", Value = 90},
                new TextValuePair<decimal>{Text = "Best Case", Value = 110},
                new TextValuePair<decimal>{Text = "Maximum", Value = 130}
            };

            var rightNormalForecast = new []{
                new TextValuePair<decimal>{Text = "Minimum", Value = 50},
                new TextValuePair<decimal>{Text = "Worst Case", Value = 90},
                new TextValuePair<decimal>{Text = "Most Likely", Value = 110},
                new TextValuePair<decimal>{Text = "Best Case", Value = 140},
                new TextValuePair<decimal>{Text = "Maximum", Value = 160}
            };

            var rightTailForecast = new []{
                new TextValuePair<decimal>{Text = "Minimum", Value = 60},
                new TextValuePair<decimal>{Text = "Worst Case", Value = 100},
                new TextValuePair<decimal>{Text = "Most Likely", Value = 120},
                new TextValuePair<decimal>{Text = "Best Case", Value = 150},
                new TextValuePair<decimal>{Text = "Maximum", Value = 170}
            };

            var gdp = new ForecastVariable{
                Name = "gdp",
                Regions = new []{
                    new ForecastRegion{
                        Name = "all",
                        Forecast = unconditionalForecast
                    }
                }
            };

            var stocks = new ForecastVariable{
                Name = "stocks",
                Regions = new[]{
                    new ForecastRegion{
                        Name = "leftTail",
                        Forecast = leftTailForecast
                    },
                    new ForecastRegion{
                        Name = "leftNormal",
                        Forecast = leftNormalForecast
                    },
                    new ForecastRegion{
                        Name = "rightNormal",
                        Forecast = rightNormalForecast
                    },
                    new ForecastRegion{
                        Name = "rightTail",
                        Forecast = rightTailForecast
                    }
                }
            };

            var bonds = new ForecastVariable{
                Name = "bonds",
                Regions = new[]{
                    new ForecastRegion{
                        Name = "leftTail",
                        Forecast = leftTailForecast
                    },
                    new ForecastRegion{
                        Name = "leftNormal",
                        Forecast = leftNormalForecast
                    },
                    new ForecastRegion{
                        Name = "rightNormal",
                        Forecast = rightNormalForecast
                    },
                    new ForecastRegion{
                        Name = "rightTail",
                        Forecast = rightTailForecast
                    }
                }
            };

            var variables = new[]{gdp, stocks, bonds};
            var response = new Response<IEnumerable<ForecastVariable>> { Model = variables };
            return Ok(response);
        }

    }
}

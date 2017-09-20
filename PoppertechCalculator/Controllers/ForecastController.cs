using PoppertechCalculator.Logic.Interfaces.ForecastGraph;
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
        private IForecastProcessor _processor;

        public ForecastController(IForecastProcessor processor)
        {
            _processor = processor;
        }

        [HttpGet]
        [Route("")]
        [ResponseType(typeof(Response<IEnumerable<ForecastVariable>>))]
        public IHttpActionResult Get()
        {
            var variables = _processor.GetForecastVariables();
            var response = new Response<IEnumerable<ForecastVariable>> { Model = variables };
            return Ok(response);
        }

    }
}

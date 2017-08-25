using PoppertechCalculator.Logic.Interfaces.Pso;
using PoppertechCalculator.Models;
using PoppertechCalculator.Models.PSO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace PoppertechCalculator.Controllers
{
    [RoutePrefix("pso")]
    public class PsoController : ApiController
    {
        private readonly IPsoCalculationsProcessor _processor;

        public PsoController(IPsoCalculationsProcessor processor)
        {
            _processor = processor;
        }

        [HttpPost]
        [Route("")]
        [ResponseType(typeof(Response<PsoResults>))]
        public IHttpActionResult Post([FromBody] PsoContext context)
        {
            var results = _processor.OptimizePortfolio(context);
            var response = new Response<PsoResults> { Model = results };
            return Ok(response);
        }

    }
}

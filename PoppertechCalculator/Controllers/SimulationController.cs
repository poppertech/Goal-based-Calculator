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
        [ResponseType(typeof(string))]
        public IHttpActionResult Post([FromBody] ScenarioRequest request)
        {
            return Ok(request.Test);
        } 
    }
}

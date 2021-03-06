﻿using PoppertechCalculator.Models;
using PoppertechCalculator.Processors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.ModelBinding;

namespace PoppertechCalculator.Controllers
{
    [RoutePrefix("investment")]
    public class InvestmentSimulationController : ApiController
    {
        ISimulationProcessor _processor;
        public InvestmentSimulationController(ISimulationProcessor processor)
        {
            _processor = processor;
        }


        [HttpPost]
        [Route("")]
        [ResponseType(typeof(Response<IEnumerable<SimulationResults>>))]
        public IHttpActionResult Post([FromBody] IEnumerable<ForecastVariable> request)
        {
            var simulationsResults = _processor.SimulateInvestments(request);
            var response = new Response<IEnumerable<SimulationResults>> { Model = simulationsResults };
            return Ok(response);
            
        }


    }
}

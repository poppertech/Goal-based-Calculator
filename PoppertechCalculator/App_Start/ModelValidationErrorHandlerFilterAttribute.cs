using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Http.Results;

namespace PoppertechCalculator.App_Start
{
    public class ModelValidationErrorHandlerFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (!actionContext.ModelState.IsValid)
            {
                var errorMessages = new List<string>();
                foreach (var value in actionContext.ModelState.Values)
	            {
                    foreach (var error in value.Errors)
	                {
		                errorMessages.Add(error.ErrorMessage);
	                }
	            }

                var json = JsonConvert.SerializeObject(errorMessages);

                var httpResponseMessage = new HttpResponseMessage(HttpStatusCode.BadRequest);
                var stringContent = new StringContent(json);
                httpResponseMessage.Content = stringContent;
                httpResponseMessage.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                
                actionContext.Response = httpResponseMessage;
            }
        }
    }
}
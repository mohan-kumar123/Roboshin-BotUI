using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Diagnostics;
using System.IO;

namespace RoboschienWeb.Filters

{
    public class CustomActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            Trace.WriteLine(string.Format("Action Method {0} executed at {1} and action context is {2}", actionContext.ActionDescriptor.DisplayName, DateTime.Now.ToShortDateString(), actionContext));
            var ai = new TelemetryClient();
            string rawRequest;
            using (var stream = new StreamReader(actionContext.HttpContext.Request.Body))
            {
                stream.BaseStream.Position = 0;
                rawRequest = stream.ReadToEnd();
            }
            ai.TrackTrace(rawRequest);

        }
    }
}


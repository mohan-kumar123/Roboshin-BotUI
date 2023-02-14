using Microsoft.AspNetCore.Mvc.Filters;
using RoboschienWeb.Models.Entities;
using System;
using System.Net;

namespace RoboschienWebAPI.Filters
{
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {

        private SickLeaveContext context = null;
        public CustomExceptionFilterAttribute(SickLeaveContext context) {

            this.context = context;

        }

        public override void OnException(ExceptionContext actionExecutedContext)
        {

            if (actionExecutedContext.Exception is NotImplementedException)
            {
                actionExecutedContext.HttpContext.Response.StatusCode = Convert.ToInt32(HttpStatusCode.NotImplemented);
            }

            else
            {

                ErrorLog recordError = new ErrorLog();
                recordError.Id = 0;
                recordError.MethodName = actionExecutedContext.RouteData.Values["action"].ToString();
                recordError.ControllerName = actionExecutedContext.RouteData.Values["controller"].ToString();
                recordError.ExceptionDetails = actionExecutedContext.Exception.Message;
                recordError.CreatedDate = DateTime.UtcNow;
                recordError.ModifiedDate = DateTime.UtcNow;

                //Save to DB 

                context.ErrorLogs.Add(recordError);
                context.SaveChangesAsync();
                actionExecutedContext.HttpContext.Response.StatusCode = Convert.ToInt32(HttpStatusCode.InternalServerError);
            }
            
 
        }
    }
}
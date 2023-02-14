using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoboschienWeb.Security
{
    public class CustomResponseHeaderMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomResponseHeaderMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            //To add Headers AFTER everything you need to do this
            context.Response.OnStarting(state =>
            {
                var httpContext = (HttpContext)state;
                //httpContext.Response.Headers.Add("Strict-Transport-Security", "max-age=31536000");
               // httpContext.Response.Headers.Add("X-Content-Type-Options", "nosniff");
                httpContext.Response.Headers.Add("X-Xss-Protection", "1; mode=block");
                //httpContext.Response.Headers.Add("X-Frame-Options", "SAMEORIGIN");
                httpContext.Response.Headers.Add("X-Frame-Options", "DENY");
                httpContext.Response.Headers.Add("Referrer-Policy", "no-referrer");
                httpContext.Response.Headers.Append("Cache-Control", string.Format("public,max-age={0}", TimeSpan.FromHours(12).TotalSeconds));
                // httpContext.Response.Headers.Add(
                // "Content-Security-Policy",
                // "default-src 'self'; " +
                //// "img-src 'self' myblobacc.blob.core.windows.net; " +
                // "font-src 'self'; " +
                // //"style-src 'self'; " +
                // //"script-src 'self' 'nonce-KIBdfgEKjb34ueiw567bfkshbvfi4KhtIUE3IWF' " +
                // " 'nonce-rewgljnOIBU3iu2btli4tbllwwe'; " +
                // "frame-src 'self';" +
                // "connect-src 'self';");


                //... and so on
                return Task.CompletedTask;
            }, context);

            await _next(context);
        }
    }
}

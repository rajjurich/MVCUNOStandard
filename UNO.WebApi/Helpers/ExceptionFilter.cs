using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;

namespace UNO.WebApi.Helpers
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        public ExceptionFilter()
        {

        }
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            var msg = actionExecutedContext.Exception.Message;
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.BadRequest)
            {                
                Content = new StringContent(msg)            
            };

            actionExecutedContext.Response = response;
        }
    }
}
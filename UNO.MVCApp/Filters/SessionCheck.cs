using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UNO.MVCApp.Filters
{
    public class SessionCheck : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (HttpContext.Current.Session["ACTIVE_USER"] == null)
            {
                filterContext.Result = new RedirectResult("~/?msg=session expired");
                return;
            }
        }
    } 
}
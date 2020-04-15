using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using UNO.AppModel;

namespace UNO.MVCApp.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AccessCheck : ActionFilterAttribute, IActionFilter
    {
        string url = WebConfigurationManager.AppSettings["APIUrl"];
        public string IdParamName { get; set; }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var id = IdParamName;
            RoleMenuAccess obj = new RoleMenuAccess();
            var RoleID = filterContext.HttpContext.Session["Role"]; //filterContext.HttpContext.User.Identity.Name;                           
            if (RoleID != null)
            {
                using (WebClient client = new WebClient())
                {
                    string s = client.DownloadString(url + "MenuAccessAPI/GetSingleMenuAccess/" + id + "/" + RoleID);
                    obj = JsonConvert.DeserializeObject<RoleMenuAccess>(s);
                }
                filterContext.Controller.ViewData.Model = obj;
            }
            else
            {

            }
        }
    }
}
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using UNO.AppModel;
using UNO.MVCApp.Common;
using UNO.MVCApp.Filters;
using UNO.WebApi.Helpers;

namespace UNO.MVCApp.Controllers
{
    
    [Authorize]
    [SessionCheck]
    public class ActivityBrowserController : Controller
    {
        string _uri = ConfigurationManager.AppSettings["APIUrl"].ToString();
        string uri = string.Empty;

        // GET: /Menu/
        [AccessCheck(IdParamName = "EventBrowser/index")]
        public async Task<ActionResult> Index(ActivityBrowser activitybrowser)
        {
            var data = ViewData.Model as RoleMenuAccess;
            if (data.CreateAccess == 0)
                ViewBag.CreateAccess = "False";
            if (data.ViewAccess == 0)
                ViewBag.ViewAccess = "False";
            if (data.UpdateAccess == 0)
                ViewBag.EditAccess = "False";
            if (data.DeleteAccess == 0)
                ViewBag.DeleteAccess = "False";
            ViewBag.Message = TempData["Message"];
            ViewBag.Status = TempData["Status"];
            ViewBag.InnerMessage = TempData["InnerMessage"];
            List<ActivityBrowserDetails> ActivityList = new List<ActivityBrowserDetails>();
            using (WebClient client = new WebClient())
            {
                string controllerName = activitybrowser.ControllerID == null ? "0" : activitybrowser.ControllerID;
                string s = client.DownloadString(_uri + "ActivityBrowserAPI/get/" + controllerName);
                ActivityList = JsonConvert.DeserializeObject<List<ActivityBrowserDetails>>(s);
                ActivityList.RemoveAll(item => item == null);
            }
            activitybrowser.ActivityDetails = ActivityList;
            List<AcsControllerInfo> ControllerList = new List<AcsControllerInfo>();
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}controller", _uri);

                var result = await client.GetAsync(uri);

                ControllerList = await result.Content.ReadAsAsync<List<AcsControllerInfo>>();
                ControllerList.RemoveAll(item => item == null);
                var List = ControllerList.Select(m => new SelectListItem
                {
                    Value = Convert.ToString(m.CTLR_ID),
                    Text = m.CTLR_DESCRIPTION
                });
                ViewBag.ListLevel = List;
            }

            return View(activitybrowser);
        }
    }
}



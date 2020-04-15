using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
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
    public class EventBrowserController : Controller
    {
        string url = WebConfigurationManager.AppSettings["APIUrl"];

        // GET: /Menu/
        [AccessCheck(IdParamName = "EventBrowser/index")]
        public ActionResult Index(EventBrowser eventBrowserObj)
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
            var ListLevel = new SelectList(new[] 
            {
                new { ID = "0", Name = "All" },
                new { ID = "1", Name = "Peripheral" },
                new { ID = "2", Name = "Second Level" },
            },
            "ID", "Name", 1);
            ViewBag.ListLevel = ListLevel;
            List<EventBrowserDetails> EventList = new List<EventBrowserDetails>();
            using (WebClient client = new WebClient())
            {

                string s = client.DownloadString(url + "EventBrowserAPI/get/" + eventBrowserObj.Event_Type + "/" + eventBrowserObj.Level_Id);
                EventList = JsonConvert.DeserializeObject<List<EventBrowserDetails>>(s);
                EventList.RemoveAll(item => item == null);
                eventBrowserObj.EventDetails = EventList;
            }
            return View(eventBrowserObj);
        }
    }
}



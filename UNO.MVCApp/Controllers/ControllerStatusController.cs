using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using UNO.AppModel;
using UNO.MVCApp.Filters;

namespace UNO.MVCApp.Controllers
{
    [Authorize]
    [SessionCheck]
    public class ControllerStatusController : Controller
    {
        string _uri = ConfigurationManager.AppSettings["APIUrl"].ToString();
        string uri = string.Empty;
        //
        // GET: /ControllerStatus/
        [AccessCheck(IdParamName = "ControllerStatus/index")]
        public ActionResult Index()
        {
            List<AcsControllerInfo> AcsControllerInfoList = new List<AcsControllerInfo>();
            var data = ViewData.Model as RoleMenuAccess;
            if (data.CreateAccess == 0)
                ViewBag.CreateAccess = "False";
            if (data.ViewAccess == 0)
                ViewBag.ViewAccess = "False";
            if (data.UpdateAccess == 0)
                ViewBag.EditAccess = "False";
            if (data.DeleteAccess == 0)
                ViewBag.DeleteAccess = "False";
            using (WebClient client = new WebClient())
            {
                string s = client.DownloadString(_uri + "Controller");
                AcsControllerInfoList = JsonConvert.DeserializeObject<List<AcsControllerInfo>>(s);
                AcsControllerInfoList.RemoveAll(item => item == null);
            }
                return View(AcsControllerInfoList);
        }

        public string GetList()
        {
            List<AcsControllerInfo> AcsControllerInfoList = new List<AcsControllerInfo>();
            using (WebClient client = new WebClient())
            {
                string s = client.DownloadString(_uri + "Controller");
                AcsControllerInfoList = JsonConvert.DeserializeObject<List<AcsControllerInfo>>(s);
                AcsControllerInfoList.RemoveAll(item => item == null);
            }
            return JsonConvert.SerializeObject(AcsControllerInfoList); ;
        }

    }
}

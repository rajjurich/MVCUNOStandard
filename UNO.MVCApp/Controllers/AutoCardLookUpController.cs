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
    public class AutoCardLookUpController : Controller
    {
        string _uri = ConfigurationManager.AppSettings["APIUrl"].ToString();
        string uri = string.Empty;

        // GET: /Menu/
        [AccessCheck(IdParamName = "AutoCardLookUp/index")]
        public ActionResult Index(AutoCardLookUp locateCardHolder)
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
            List<AutoCardLookUp> ctrlRdrList = new List<AutoCardLookUp>();
            using (WebClient client = new WebClient())
            {
                string s = client.DownloadString(_uri + "AutoCardLookUpAPI");
                ctrlRdrList = JsonConvert.DeserializeObject<List<AutoCardLookUp>>(s);
                ctrlRdrList.RemoveAll(item => item == null);
            }
            var List = ctrlRdrList.Select(m => new SelectListItem
            {
                Value = Convert.ToString(m.CTLR_READER_ID),
                Text = m.READER_DESCRIPTION
            });
            ViewBag.ListLevel = List;
            return View(locateCardHolder);
        }
    }
}



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
    public class LocateCardHolderController : Controller
    {
        string _uri = ConfigurationManager.AppSettings["APIUrl"].ToString();
        string uri = string.Empty;

        // GET: /Menu/
        [AccessCheck(IdParamName = "LocateCardHolder/index")]
        public ActionResult Index(LocateCardHolder locateCardHolder)
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
                new { ID = "-1", Name = "Select Criteria" },
                new { ID = "1", Name = "Employee Code" },
                new { ID = "2", Name = "Card Code" },
            },
          "ID", "Name", 1);
            ViewBag.ListLevel = ListLevel;
            return View(locateCardHolder);
        }
    }
}



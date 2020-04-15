using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UNO.MVCApp.Filters;

namespace UNO.MVCApp.Controllers
{
    [Authorize]
    [SessionCheck]
    public class ManagerDashboardController : Controller
    {
        //
        // GET: /ManagerDashboard/

        public ActionResult Index()
        {
            return View();
        }

    }
}

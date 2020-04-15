using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UNO.MVCApp.Filters;

namespace UNO.MVCApp.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        //
        // GET: /Dashboard/

        [Authorize]
        [SessionCheck]
        public ActionResult Index()
        {
            return View();
        }

    }
}

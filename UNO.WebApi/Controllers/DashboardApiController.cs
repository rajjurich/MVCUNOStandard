using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UNO.WebApi.Models;
using UNO.WebApi.Services;

namespace UNO.WebApi.Controllers
{
    public class DashboardApiController : ApiController
    {

        private IDashboardService _DashboardService;
        public DashboardApiController(IDashboardService DashboardService)
        {
            _DashboardService = DashboardService;
        }
        [HttpGet]
        public List<TDay> get()
        {
            return _DashboardService.Get("");
        }

    }
}

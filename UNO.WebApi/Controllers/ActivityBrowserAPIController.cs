using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using UNO.DAL;
using UNO.WebApi.Models;
using UNO.WebApi.Services;

namespace UNO.WebApi.Controllers
{
    public class ActivityBrowserAPIController : ApiController
    {


        //private UnitOfWork _unitOfWork;
        //private DatabaseHelper _DatabaseHelper;

        private IActivityBrowserService _entActivityBrowserService;

        public ActivityBrowserAPIController(IActivityBrowserService entActivityBrowserService)
        {
            _entActivityBrowserService = entActivityBrowserService;
        }
        // GET api/entparams

        [HttpGet]
        [Route("api/ActivityBrowserAPI/get/{whereclause}")]
        public List<ActivityBrowser> get(string whereclause)
        {
            string _whereCond = whereclause ==""?"":"and C.CTLR_ID=" + whereclause;
            String _tableName = "EVENT_LOG_";
            if (DateTime.Now.Date.Month < 10)
            { _tableName = _tableName + "0" + DateTime.Now.Date.Month + DateTime.Now.Date.Year; }
            else
            { _tableName = _tableName + DateTime.Now.Date.Month + DateTime.Now.Date.Year; }
            return _entActivityBrowserService.GetActivity(_tableName, _whereCond);
        }
    }
}

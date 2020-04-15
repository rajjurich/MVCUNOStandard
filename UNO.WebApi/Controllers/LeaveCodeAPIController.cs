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
    public class LeaveCodeAPIController : ApiController
    {
         private ILeaveCodeService _entLeaveCodeService;
        public LeaveCodeAPIController(ILeaveCodeService entLeaveCodeService)
        {
            _entLeaveCodeService = entLeaveCodeService;
        }
        [HttpGet]
        public List<LeaveCode> get()
        {
            return _entLeaveCodeService.Get();
        }

        [HttpGet]
        public List<LeaveCode> get(int id)
        {
            return _entLeaveCodeService.Get(id);
        }
    }
}

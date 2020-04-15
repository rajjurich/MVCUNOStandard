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
    public class ControlAPIController : ApiController
    {
        private IControlService _entControlService;
        public ControlAPIController(IControlService entControlService)
        {
            _entControlService = entControlService;
        }
        [HttpGet]
        public List<Control> get()
        {
            return _entControlService.Get();
        }
    }
}

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
    public class StateAPIController : ApiController
    {
        private IStateService _entStateService;

        public StateAPIController(IStateService entCompanyTypeService)
        {
            _entStateService = entCompanyTypeService;
        }  

        [HttpGet]
        public List<State> get()
        {
            return _entStateService.Get();
        }
    }
}

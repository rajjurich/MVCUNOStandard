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
    public class ModuleApiController : ApiController
    {

        private IModuleService _entModuleService;

        public ModuleApiController(IModuleService entModuleService)
        {
            _entModuleService = entModuleService;
        }
        
        
        [HttpGet]
        public List<Module> get()
        {
            return _entModuleService.Get();
        }



    }
}

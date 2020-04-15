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
    public class SubModuleApiController : ApiController
    {
        
      private ISubModuleService _entSubModuleService;

      public SubModuleApiController(ISubModuleService entSubModuleService)
        {
            _entSubModuleService = entSubModuleService;
        }
        
        
        [HttpGet]
      public List<SubModule> get()
        {
            return _entSubModuleService.Get();
        }


        [HttpGet]
        public List<SubModule> get(string id)
        {
            return _entSubModuleService.Get(id);
        }
        
    }

}

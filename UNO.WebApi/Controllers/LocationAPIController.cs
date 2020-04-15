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
    public class LocationAPIController : ApiController
    {
        private ILocationService _entLocationService;

        public LocationAPIController(ILocationService entLocationService)
        {
            _entLocationService = entLocationService;
        }
        
        
        [HttpGet]
        public List<Location> get()
        {
            return _entLocationService.Get();
        }

    }
}

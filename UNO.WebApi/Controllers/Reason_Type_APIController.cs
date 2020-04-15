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
    public class Reason_Type_APIController : ApiController
    {
        private IReasonTypeService _entReasonTypeService;

        public Reason_Type_APIController(IReasonTypeService entReasonTypeService)
        {
            _entReasonTypeService = entReasonTypeService;
        }  

        [HttpGet]
        public List<Reason_Type> get()
        {
            return _entReasonTypeService.Get();
        }
    }
}

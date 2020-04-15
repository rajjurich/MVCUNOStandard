using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using UNO.DAL;
using UNO.MVCApp.Helpers;
using UNO.WebApi.Dto;
using UNO.WebApi.Models;
using UNO.WebApi.Services;

namespace UNO.WebApi.Controllers
{
    public class CommonTypesApiController : ApiController
    {
        private int activeuser;
        private ICommonTypesService _CommonService;

        public CommonTypesApiController(ICommonTypesService _common)
        {
            _CommonService = _common;
            var request = HttpContext.Current.Request;
            activeuser = Convert.ToInt32(request.Headers["activeuser"]);
        }
        [Route("api/CommonTypesApi/get")]
        public IQueryable<CommonTypesModel> Get()
        {
            return _CommonService.Get(activeuser).AsQueryable();
        }
    }
}

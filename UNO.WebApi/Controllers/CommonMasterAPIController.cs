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
    public class CommonMasterAPIController : ApiController
    {
        private ICommonMasterService _EntCommonService;

        public CommonMasterAPIController(ICommonMasterService EntCommonService)
        {
            _EntCommonService = EntCommonService;
        }

        [HttpGet]
        public List<CommonMaster> get()
        {
            return _EntCommonService.GetCommon();
        }

        [HttpGet]
        [Route("api/CommonMasterAPI/GetCommonWithEmployee")]
        public List<CommonMaster> GetCommonWithEmployee()
        {
            return _EntCommonService.GetCommonWithEmployee();
        }
        [HttpGet]
        [Route("api/CommonMasterAPI/GetCommonIdByName/{entityName}")]
        public Task<int> GetCommonIdByName(string entityName)
        {
            return _EntCommonService.GetCommonIdByName(entityName);
        }
    
    }
}

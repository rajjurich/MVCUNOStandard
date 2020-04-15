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
    public class CompanyTypeAPIController : ApiController
    {
        private ICompanyTypeService _entCompanyTypeService;

        public CompanyTypeAPIController(ICompanyTypeService entCompanyTypeService)
        {
            _entCompanyTypeService = entCompanyTypeService;
        }  

        [HttpGet]
        public List<CompanyType> get()
        {
            return _entCompanyTypeService.Get();
        }
    }
}

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
    public class Company_Level_MenuAPIController : ApiController
    {
        private UnitOfWork _unitOfWork;
        private DatabaseHelper _DatabaseHelper;
        private ICompany_Level_Service _IcompanyService;
        private int activeuser;
        private Utilities _ipaddressobj;

        public Company_Level_MenuAPIController(IUnitOfWork unitOfWork, DatabaseHelper databaseHelper,
            ICompany_Level_Service Icompanyservicecustum, Utilities _ipaddress)
        {
            var request = HttpContext.Current.Request;
            activeuser = Convert.ToInt32(request.Headers["activeuser"]);
            _ipaddressobj = _ipaddress;
            _unitOfWork = (UnitOfWork)unitOfWork;
            _DatabaseHelper = databaseHelper;
            _IcompanyService = Icompanyservicecustum;
        }
        // GET api/<controller>
        [HttpGet]
        [Route("api/Company_Level_MenuAPI/get")]
        public List<Company_Access_APIModel> get()
        {
            return _IcompanyService.GetCompanyAccessList();
        }

        // GET api/<controller>/5
        public Company_Access_APIModel get(int id)
        {
            return _IcompanyService.GetCompanyAccessOne(id);
        }
        [HttpGet]
        [Route("api/Company_Level_MenuAPI/getAll/{id}")]
        public IQueryable<Company_Access_APIModel> getAll(int id)
        {
            return _IcompanyService.GetListForMenuAccess(id).AsQueryable();
        }

        // POST api/<controller>
        
        public async Task<IHttpActionResult> Post(Company_Access_APIModel collection)
        {
            string ipadress = _ipaddressobj.GetIpAddress();
            if (!(ModelState.IsValid))
            {
                return BadRequest(ModelState);
            }
            _unitOfWork.BeginTransaction();

            await _IcompanyService.Create(collection, ipadress,activeuser);

            return Ok(collection);
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}
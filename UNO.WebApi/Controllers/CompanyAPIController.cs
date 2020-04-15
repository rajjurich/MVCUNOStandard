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
using UNO.WebApi.Models;
using UNO.WebApi.Services;



namespace UNO.WebApi.Controllers
{
    
    public class CompanyAPIController : ApiController
    {
        private ICompanyService _entCompanyService;
        ICompanyLocationService _entCompanyLocationService;
        private UnitOfWork _unitOfWork;
        private int activeuser;
        private Utilities _ipaddressobj;
        public CompanyAPIController(ICompanyService entCompanyService
            , ICompanyLocationService entCompanyLocationService
            , IUnitOfWork unitOfWork, Utilities _ipaddress)
        {
            _ipaddressobj = _ipaddress;
            _entCompanyService = entCompanyService;
            _entCompanyLocationService = entCompanyLocationService;
            _unitOfWork = (UnitOfWork)unitOfWork;

            var request = HttpContext.Current.Request;
            activeuser = Convert.ToInt32(request.Headers["activeuser"]);            
        }

        [HttpGet]
        public List<Company> get()
        {
            return _entCompanyService.GetCompany(activeuser);
        }
        
        [HttpGet]
        [Route("api/CompanyAPI/getcompanybiometric")]
        public List<Company> getcompanybiometric()
        {
            return _entCompanyService.GetCompanybio(activeuser);
        }

        [HttpGet]
        public Company get(int id)
        {
            Company companyObj = new Company();

            companyObj = _entCompanyService.GetSingleCompany(id);
            List<CompanyLocationDetails> locationList = new List<CompanyLocationDetails>();
            locationList = _entCompanyLocationService.GetLocation(id);
            companyObj.CompanyLocationList = locationList;
            return companyObj;
        }

        //[HttpPost]
        public async Task<IHttpActionResult> Post(Company _company)
        {

            string ipadress = _company.ipaddress;
            if (!(ModelState.IsValid))
            {
                return BadRequest(ModelState);
            }

            var x = await _entCompanyService.IsUniqCompanyCode(_company, false);        
            
            if (x)
            {
                return BadRequest("Company Code Already Exists!!");
            }

            _unitOfWork.BeginTransaction();

            int controllerId = await _entCompanyService.Create(_company,ipadress,activeuser);

            var ComDetailId = await _entCompanyLocationService.Create(_company.CompanyLocationList, controllerId,ipadress,activeuser);

            return CreatedAtRoute("", new { id = ComDetailId }, _company);
        }

        //[HttpPost]
        public async Task<IHttpActionResult> Put(Company _company)
        {
            string ipadress = _company.ipaddress;
            //return await _entParamsService.Edit(_company);

            if (!(ModelState.IsValid))
            {
                return BadRequest(ModelState);
            }

            var x = await _entCompanyService.IsUniqCompanyCode(_company, true);

            if (x)
            {
                return BadRequest("Company Code Already Exists For Other!!");
            }

            _unitOfWork.BeginTransaction();

            var controllerId = await _entCompanyService.Edit(_company, ipadress, activeuser);
            var LocCtrollerId = await _entCompanyLocationService.Edit(_company.CompanyLocationList, ipadress, activeuser);

            return CreatedAtRoute("", new { id = LocCtrollerId }, _company);
        }

        public async Task<IHttpActionResult> DELETE(int id, Company _company)
        {
            string ipadress = _company.ipaddress;
            if (!(ModelState.IsValid))
            {
                return BadRequest(ModelState);
            }

            _unitOfWork.BeginTransaction();

            var controllerId = await _entCompanyService.Delete(id, ipadress, activeuser);
            var LocCtrollerId = await _entCompanyLocationService.Delete(id, ipadress, activeuser);

            return CreatedAtRoute("", new { id = controllerId }, _company);
        }
    }
}

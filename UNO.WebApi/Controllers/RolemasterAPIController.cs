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
    public class RolemasterAPIController : ApiController
    {
        private UnitOfWork _unitOfWork;
        private DatabaseHelper _DatabaseHelper;

        private IRolemasterService _RolemasterService;
        private int activeuser;
        private Utilities _ipaddressobj;

        public RolemasterAPIController(IRolemasterService RolemasterService,
            IUnitOfWork unitOfWork, DatabaseHelper databaseHelper, Utilities _ipaddress)
        {
            _ipaddressobj = _ipaddress;
            _RolemasterService = RolemasterService;
            _unitOfWork = (UnitOfWork)unitOfWork;
            _DatabaseHelper = databaseHelper;
            var request = HttpContext.Current.Request;
            activeuser = Convert.ToInt32(request.Headers["activeuser"]);
        }
        [HttpGet]

        public IQueryable<ROLEMASTERMODEL> get()
        {
            return _RolemasterService.GetRolemastersDetails(activeuser).AsQueryable();
        }

        public ROLEMASTERMODEL get(int id)
        {
            return _RolemasterService.GetRolemaster(id);
        }


        public async Task<IHttpActionResult> Post(ROLEMASTERMODEL rolemasterobj)
        {
            string ipadress = _ipaddressobj.GetIpAddress();
            if (!(ModelState.IsValid))
            {
                return BadRequest(ModelState);
            }
            string check=rolemasterobj.ROLE_NAME.ToLower();

            if (check == "admin" || check == "employee" || check == "sysadmin")
            {
                return BadRequest("Please These Name Already Taken Please Try another Role Name");
            }

            _unitOfWork.BeginTransaction();

            ROLEMASTERMODEL rolemaster2 = new ROLEMASTERMODEL();

            rolemaster2.COMPANY_ID = rolemasterobj.COMPANY_ID;
            rolemaster2.Company_name = rolemasterobj.Company_name;
            rolemaster2.ROLE_NAME = rolemasterobj.ROLE_NAME;



            await _RolemasterService.Create(rolemaster2, ipadress,activeuser);

            return Ok(rolemasterobj);



        }


        public async Task<IHttpActionResult> Put(ROLEMASTERMODEL rolemasterobj)
        {
            string ipadress = _ipaddressobj.GetIpAddress();
            if (!(ModelState.IsValid))
            {
                return BadRequest(ModelState);
            }

            string check = rolemasterobj.ROLE_NAME.ToLower();

            if (check == "admin" || check == "employee" || check == "sysadmin")
            {
                return BadRequest("Please These Name Already Taken Please Try another Role Name");
            }

            _unitOfWork.BeginTransaction();

            var rolemasterobj1 = await _RolemasterService.Edit(rolemasterobj, ipadress, activeuser);

            return CreatedAtRoute("", new { id = rolemasterobj1 }, rolemasterobj);
        }


        public async Task<IHttpActionResult> DELETE(int id, ROLEMASTERMODEL rolemasterobj)
        {

            string ipadress = _ipaddressobj.GetIpAddress();
            if (!(ModelState.IsValid))
            {
                return BadRequest(ModelState);
            }

            _unitOfWork.BeginTransaction();

            var rolemasterid = await _RolemasterService.Delete(id, ipadress, activeuser);

            return CreatedAtRoute("", new { id = rolemasterid }, id);
        }
    }
}

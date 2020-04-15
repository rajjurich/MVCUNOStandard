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
    public class EmployeeCardConfigAPIController : ApiController
    {


        private UnitOfWork _unitOfWork;
        private DatabaseHelper _DatabaseHelper;
        private Utilities _ipaddressobj;
        private int activeuser;

        private IEmployeeCardConfigService _entEmployeeCardConfigService;

        public EmployeeCardConfigAPIController(IEmployeeCardConfigService entEmployeeCardConfigService,
            IUnitOfWork unitOfWork, DatabaseHelper databaseHelper, Utilities _ipaddress)
        {
            var request = HttpContext.Current.Request;
            activeuser = Convert.ToInt32(request.Headers["activeuser"]);
            _ipaddressobj = _ipaddress;
            _entEmployeeCardConfigService = entEmployeeCardConfigService;
            _unitOfWork = (UnitOfWork)unitOfWork;
            _DatabaseHelper = databaseHelper;
        }
        // GET api/entparams

        [HttpGet]
        [Route("api/EmployeeCardConfigAPI/get")]
        public List<EmployeeCardConfig> get()
        {
            return _entEmployeeCardConfigService.GetEmpCardConfigDetails();
        }

        public EmployeeCardConfig get(int id)
        {
            return _entEmployeeCardConfigService.Getemployeecard(id);
        }

        
        public async Task<IHttpActionResult> Post(EmployeeCardConfigCreateDto employeeCardconfigdto)
        {
            string ipadress = employeeCardconfigdto.ipaddress;
            if (!(ModelState.IsValid))
            {
                return BadRequest(ModelState);
            }
            _unitOfWork.BeginTransaction();

            foreach (var item in employeeCardconfigdto.Employees)
            {
                EmployeeCardConfig employeeconfig = new EmployeeCardConfig();
                employeeconfig.CC_EMP_ID = item.EMPLOYEE_ID;
                employeeconfig.USE_COUNT = employeeCardconfigdto.USECOUNT;
                employeeconfig.ACTIVATION_DATE = employeeCardconfigdto.ACTIVATION_DATE;
                employeeconfig.EXPIRY_DATE = employeeCardconfigdto.EXPIRY_DATE;
                employeeconfig.Remember = employeeCardconfigdto.Remember;
                await _entEmployeeCardConfigService.Create(employeeconfig, ipadress, activeuser);

                //var emp=_entEmployeeCardConfigService.GetEmpCardConfigDetails

            }
            return Ok(employeeCardconfigdto);
        }



        public async Task<IHttpActionResult> Put(EmployeeCardConfig employeeCardconfigdto)
        {
            string ipadress = employeeCardconfigdto.ipaddress;
            if (!(ModelState.IsValid))
            {
                return BadRequest(ModelState);
            }
            _unitOfWork.BeginTransaction();

            var employeeconfig = await _entEmployeeCardConfigService.Edit(employeeCardconfigdto, ipadress, activeuser);

            return CreatedAtRoute("", new { id = employeeconfig }, employeeCardconfigdto);


        }

        public async Task<IHttpActionResult> DELETE(int id,string ipaddress)
        {
            string ipadress = ipaddress;
            if (!(ModelState.IsValid))
            {
                return BadRequest(ModelState);
            }
            _unitOfWork.BeginTransaction();

            var employeeconfig = await _entEmployeeCardConfigService.Delete(id, ipadress, activeuser);

            return CreatedAtRoute("", new { id = employeeconfig }, id);
        }
        
    }
}

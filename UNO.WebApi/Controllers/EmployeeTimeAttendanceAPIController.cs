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
    public class EmployeeTimeAttendanceAPIController : ApiController
    {
        private UnitOfWork _unitOfWork;
        private DatabaseHelper _DatabaseHelper;
        private Utilities _ipaddressobj;

        private IEmployeeTimeAttendanceService _entEmployeeShiftService;
        private int activeuser;

        public EmployeeTimeAttendanceAPIController(IEmployeeTimeAttendanceService entEmployeeCardConfigService,
            IUnitOfWork unitOfWork, DatabaseHelper databaseHelper, Utilities _ipaddress)
        {
            _ipaddressobj = _ipaddress;
            _entEmployeeShiftService = entEmployeeCardConfigService;
            _unitOfWork = (UnitOfWork)unitOfWork;
            _DatabaseHelper = databaseHelper;
            var request = HttpContext.Current.Request;
            activeuser = Convert.ToInt32(request.Headers["activeuser"]);
        }

        [HttpGet]
        
        public IQueryable<EmployeeTimeAttendance> get()
        {
            return _entEmployeeShiftService.GetEmpShiftDetails(activeuser).AsQueryable();
        }

        public EmployeeTimeAttendance get(int id)
        {
            return _entEmployeeShiftService.GetemployeeShift(id);
        }


        public async Task<IHttpActionResult> Post(EmployeeAttenDto employeeAttendance)
        {
            string ipadress = employeeAttendance.ipaddress;
            if (!(ModelState.IsValid))
            {
                return BadRequest(ModelState);
            }
            _unitOfWork.BeginTransaction();

            foreach (var item in employeeAttendance.Employees)
            {
                EmployeeTimeAttendance employeeconfig = new EmployeeTimeAttendance();
                employeeconfig.ETC_EMP_ID = item.EMPLOYEE_ID;
                employeeconfig.ETC_MINIMUM_SWIPE = employeeAttendance.ETC_MINIMUM_SWIPE;
                employeeconfig.ETC_SHIFTCODE = employeeAttendance.ETC_SHIFTCODE;
                employeeconfig.ETC_WEEKEND = employeeAttendance.ETC_WEEKEND;
                employeeconfig.ETC_WEEKOFF = employeeAttendance.ETC_WEEKOFF;
                employeeconfig.ETC_SHIFT_START_DATE = employeeAttendance.ETC_SHIFT_START_DATE;
                employeeconfig.ScheduleType = employeeAttendance.ScheduleType;
                employeeconfig.ShiftType = employeeAttendance.ShiftType;

                await _entEmployeeShiftService.Create(employeeconfig,ipadress,activeuser);
            }
            return Ok(employeeAttendance);
                


        }


        public async Task<IHttpActionResult> Put(EmployeeTimeAttendance employeeAttendance)
        {
            string ipadress = employeeAttendance.ipaddress;
            if (!(ModelState.IsValid))
            {
                return BadRequest(ModelState);
            }

            _unitOfWork.BeginTransaction();

            var employeeatten = await _entEmployeeShiftService.Edit(employeeAttendance, ipadress, activeuser);

            return CreatedAtRoute("", new { id = employeeatten }, employeeAttendance);
        }


        public async Task<IHttpActionResult> DELETE(int id, string ipaadreess)
        {
            string ipadress = ipaadreess;
            if (!(ModelState.IsValid))
            {
                return BadRequest(ModelState);
            }

            _unitOfWork.BeginTransaction();

            var employeeatten = await _entEmployeeShiftService.Delete(id, ipadress, activeuser);

            return CreatedAtRoute("", new { id = employeeatten }, id);
        }
    }
}

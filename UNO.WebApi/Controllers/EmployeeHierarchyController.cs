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
    public class EmployeeHierarchyController : ApiController
    {
        private IEmployeeHierarchyService _employeeHierarchyService;
        private IEmployeeService _employeeService;

        private UnitOfWork _unitOfWork;
        private int activeuser;
        private Utilities _ipaddressobj;
        public EmployeeHierarchyController(IEmployeeHierarchyService employeeHierarchyService
            , IEmployeeService employeeService
            , IUnitOfWork unitOfWork
            , Utilities _ipaddress)
        {
            _ipaddressobj = _ipaddress;
            _employeeHierarchyService = employeeHierarchyService;
            _employeeService = employeeService;
            _unitOfWork = (UnitOfWork)unitOfWork;
            var request = HttpContext.Current.Request;
            activeuser = Convert.ToInt32(request.Headers["activeuser"]);
        }
        // GET api/employeehierarchy
        public IQueryable<EmployeeHierarchyDto> Get()
        {
            var x = _employeeHierarchyService.Get(activeuser).AsQueryable();
            return x;
        }

        // GET api/employeehierarchy/5
        public async Task<IHttpActionResult> Get(int id)
        {
            var employee = await _employeeHierarchyService.GetSingle(id);

            return Ok(employee);
        }

        // POST api/employeehierarchy
        public async Task<IHttpActionResult> Post([FromBody]EmployeeHierarchyCreateDto employeeHierarchyCreateDto)
        {
            string ipadress = employeeHierarchyCreateDto.ipaddress;
            if (!(ModelState.IsValid))
            {
                return BadRequest(ModelState);
            }

            _unitOfWork.BeginTransaction();

            foreach (var item in employeeHierarchyCreateDto.Employees)
            {
                if (!(item.EMPLOYEE_ID == employeeHierarchyCreateDto.Hier_Mgr_ID))
                {

                    EmployeeHierarchy employeeHierarchy = new EmployeeHierarchy();
                    var emp = _employeeHierarchyService.GetByEmployeeID(item.EMPLOYEE_ID);
                    if (emp == null)
                    {
                        employeeHierarchy.Hier_Emp_ID = item.EMPLOYEE_ID;
                        employeeHierarchy.Hier_Mgr_ID = employeeHierarchyCreateDto.Hier_Mgr_ID;
                        employeeHierarchy.EOD_DESIGNATION_ID = _employeeService.GetSingle(Convert.ToInt32(employeeHierarchyCreateDto.Hier_Mgr_ID)).Result.EOD_DESIGNATION_ID;
                        await _employeeHierarchyService.Create(employeeHierarchy, ipadress, activeuser);
                    }
                    else
                    {
                        foreach (var e in emp)
                        {
                            if (!(e.Hier_Emp_ID == item.EMPLOYEE_ID && e.Hier_Mgr_ID == employeeHierarchyCreateDto.Hier_Mgr_ID))
                            {
                                employeeHierarchy.Hier_Emp_ID = item.EMPLOYEE_ID;
                                employeeHierarchy.Hier_Mgr_ID = employeeHierarchyCreateDto.Hier_Mgr_ID;
                                employeeHierarchy.EOD_DESIGNATION_ID = _employeeService.GetSingle(Convert.ToInt32(employeeHierarchyCreateDto.Hier_Mgr_ID)).Result.EOD_DESIGNATION_ID;
                                await _employeeHierarchyService.Create(employeeHierarchy, ipadress, activeuser);
                            }
                        }
                    }
                }

            }


            return Ok(employeeHierarchyCreateDto);

        }

        // PUT api/employeehierarchy/5
        public void Put(int id, [FromBody]string value,string ipaddress)
        {

        }

        // DELETE api/employeehierarchy/5
        public void Delete(int id,string ipaddress)
        {
        }
    }
}

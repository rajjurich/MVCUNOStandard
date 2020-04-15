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
    public class EmployeeController : ApiController
    {
        private IEmployeeService _employeeService;
        private IEmployeeAddressService _employeeAddressService;
        private IRoleService _roleService;
        private IUserService _userService;
        private IEmployeeHierarchyService _employeeHierarchyService;
        private UnitOfWork _unitOfWork;
        private int activeuser;
        private Utilities _ipaddressobj;
        public EmployeeController(IEmployeeService employeeService
            , IEmployeeAddressService employeeAddressService
            , IRoleService roleService
            , IUserService userService
            , IEmployeeHierarchyService employeeHierarchyService
            , IUnitOfWork unitOfWork
            , Utilities _ipaddress
            )
        {
            _ipaddressobj = _ipaddress;
            _employeeService = employeeService;
            _employeeAddressService = employeeAddressService;
            _roleService = roleService;
            _userService = userService;
            _employeeHierarchyService = employeeHierarchyService;
            _unitOfWork = (UnitOfWork)unitOfWork;
            var request = HttpContext.Current.Request;
            activeuser = Convert.ToInt32(request.Headers["activeuser"]);
        }
        // GET api/employee
        public IQueryable<EmployeeDto> Get()
        {
            var x = _employeeService.Get(activeuser).AsQueryable();
            return x;
        }

        [Route("api/employee/WithNoAccess")]
        public IQueryable<EmployeeDto> GetWithNoAccess()
        {
            var x = _employeeService.GetWithNoAccess(activeuser).AsQueryable();
            return x;
        }

        // GET api/employee/5
        public async Task<IHttpActionResult> Get(int id)
        {
            var employee = await _employeeService.GetSingle(id);

            return Ok(employee);

        }

        [Route("api/employeeDto/{id}")]
        public EmployeeDto GetEmployeeDto(int id)
        {
            var employee = _employeeService.Get(activeuser).AsQueryable().Where(x => x.EMPLOYEE_ID == id).FirstOrDefault();

            return employee;
        }

        [Route("api/employeeDtoattandance")]
        public IQueryable<EmployeeDto> GetEmployeeDtoAttandance()
        {
            var employee = _employeeService.GetEmployeeTimeAttendance(activeuser).AsQueryable();

            return employee;
        }


        [Route("api/employeeDtoCardConfig")]
        public IQueryable<EmployeeDto> GetEmployeeDtoCardConfig()
        {
            var employee = _employeeService.GetEmployeeCardConfig(activeuser).AsQueryable();

            return employee;
        }




        [Route("api/employeeDtoByCompanyId/{id}")]
        public IQueryable<EmployeeDto> GetEmployeeDtoByCompanyId(int id)
        {
            var employee = _employeeService.Get(activeuser).AsQueryable().Where(x => x.EOD_COMPANY_ID == id).ToList().AsQueryable();

            return employee;
        }


        [Route("api/employeeDtoByCompanyIdattandance/{id}")]
        public IQueryable<EmployeeDto> GetEmployeeDtoByCompanyIdAttandance(int id)
        {
            var employee = _employeeService.GetEmployeeTimeAttendance(activeuser).AsQueryable().Where(x => x.EOD_COMPANY_ID == id).ToList().AsQueryable();

            return employee;
        }


        [Route("api/employeeDtoByCompanyIdCardConfig/{id}")]
        public IQueryable<EmployeeDto> GetEmployeeDtoByCompanyIdCardConfig(int id)
        {
            var employee = _employeeService.GetEmployeeCardConfig(activeuser).AsQueryable().Where(x => x.EOD_COMPANY_ID == id).ToList().AsQueryable();

            return employee;
        }





        [Route("api/employeeDtoByLocationId/{id}")]
        public IQueryable<EmployeeDto> GetEmployeeDtoByLocationId(int id)
        {
            var employee = _employeeService.Get(activeuser).AsQueryable().Where(x => x.EOD_LOCATION_ID == id).ToList().AsQueryable();

            return employee;
        }


        [Route("api/employeeDtoByLocationIdattandance/{id}")]
        public IQueryable<EmployeeDto> GetEmployeeDtoByLocationIdAttandance(int id)
        {
            var employee = _employeeService.GetEmployeeTimeAttendance(activeuser).AsQueryable().Where(x => x.EOD_LOCATION_ID == id).ToList().AsQueryable();

            return employee;
        }


        [Route("api/employeeDtoByLocationIdCardConfig/{id}")]
        public IQueryable<EmployeeDto> GetEmployeeDtoByLocationIdCardConfig(int id)
        {
            var employee = _employeeService.GetEmployeeCardConfig(activeuser).AsQueryable().Where(x => x.EOD_LOCATION_ID == id).ToList().AsQueryable();

            return employee;
        }



        [Route("api/employeeDtoByDivisionId/{id}")]
        public IQueryable<EmployeeDto> GetEmployeeDtoByDivisionId(int id)
        {
            var employee = _employeeService.Get(activeuser).AsQueryable().Where(x => x.EOD_DIVISION_ID == id).ToList().AsQueryable();

            return employee;
        }

        [Route("api/employeeDtoByDivisionIdattandance/{id}")]
        public IQueryable<EmployeeDto> GetEmployeeDtoByDivisionIdAttandance(int id)
        {
            var employee = _employeeService.GetEmployeeTimeAttendance(activeuser).AsQueryable().Where(x => x.EOD_DIVISION_ID == id).ToList().AsQueryable();

            return employee;
        }


        [Route("api/employeeDtoByDivisionIdCardConfig/{id}")]
        public IQueryable<EmployeeDto> GetEmployeeDtoByDivisionIdCardConfig(int id)
        {
            var employee = _employeeService.GetEmployeeCardConfig(activeuser).AsQueryable().Where(x => x.EOD_DIVISION_ID == id).ToList().AsQueryable();

            return employee;
        }




        [Route("api/employeeDtoByDepartmentId/{id}")]
        public IQueryable<EmployeeDto> GetEmployeeDtoByDepartmentId(int id)
        {
            var employee = _employeeService.Get(activeuser).AsQueryable().Where(x => x.EOD_DEPARTMENT_ID == id).ToList().AsQueryable();

            return employee;
        }

        [Route("api/employeeDtoByCategoryId/{id}")]
        public IQueryable<EmployeeDto> GetEmployeeDtoByCategoryId(int id)
        {
            var employee = _employeeService.Get(activeuser).AsQueryable().Where(x => x.EOD_CATEGORY_ID == id).ToList().AsQueryable();

            return employee;
        }



        [Route("api/employeeDtoByGroupId/{id}")]
        public IQueryable<EmployeeDto> GetEmployeeDtoByGroupId(int id)
        {
            var employee = _employeeService.Get(activeuser).AsQueryable().Where(x => x.EOD_GROUP_ID == id).ToList().AsQueryable();

            return employee;
        }

        [Route("api/employeeDtoByGradeId/{id}")]
        public IQueryable<EmployeeDto> GetEmployeeDtoByGradeId(int id)
        {
            var employee = _employeeService.Get(activeuser).AsQueryable().Where(x => x.EOD_GRADE_ID == id).ToList().AsQueryable();

            return employee;
        }

        [Route("api/employeeDtoByDesignationId/{id}")]
        public IQueryable<EmployeeDto> GetEmployeeDtoByDesignationId(int id)
        {
            var employee = _employeeService.Get(activeuser).AsQueryable().Where(x => x.EOD_DESIGNATION_ID == id).ToList().AsQueryable();

            return employee;
        }


        [Route("api/employeeDtoByDepartmentIdattandance/{id}")]
        public IQueryable<EmployeeDto> GetEmployeeDtoByDepartmentIdAttandance(int id)
        {
            var employee = _employeeService.GetEmployeeTimeAttendance(activeuser).AsQueryable().Where(x => x.EOD_DEPARTMENT_ID == id).ToList().AsQueryable();

            return employee;
        }



        [Route("api/employeeDtoByDepartmentIdCardConfig/{id}")]
        public IQueryable<EmployeeDto> GetEmployeeDtoByDepartmentIdCardConfig(int id)
        {
            var employee = _employeeService.GetEmployeeCardConfig(activeuser).AsQueryable().Where(x => x.EOD_DEPARTMENT_ID == id).ToList().AsQueryable();

            return employee;
        }



        

        [Route("api/employeeDtoByCategoryIdattandance/{id}")]
        public IQueryable<EmployeeDto> GetEmployeeDtoByCategoryIdAttandance(int id)
        {
            var employee = _employeeService.GetEmployeeTimeAttendance(activeuser).AsQueryable().Where(x => x.EOD_CATEGORY_ID == id).ToList().AsQueryable();
            return employee;
        }


        [Route("api/employeeDtoByCategoryIdCardConfig/{id}")]
        public IQueryable<EmployeeDto> GetEmployeeDtoByCategoryIdCardConfig(int id)
        {
            var employee = _employeeService.GetEmployeeCardConfig(activeuser).AsQueryable().Where(x => x.EOD_CATEGORY_ID == id).ToList().AsQueryable();
            return employee;
        }



        // POST api/employee
        public async Task<IHttpActionResult> Post([FromBody]Employee employee)
        {
            string ipadress = employee.ipaddress;
            if (!(ModelState.IsValid))
            {
                return BadRequest(ModelState);
            }
            
            _unitOfWork.BeginTransaction();

            var employeeId = await _employeeService.Create(employee, ipadress,activeuser);

            foreach (var item in employee.EmployeeAddresses)
            {
                item.EMPLOYEE_ID = employeeId;
                await _employeeAddressService.Create(item,ipadress,activeuser);
            }

            User user = new User();
            user.COMPANY_ID = Convert.ToInt32(employee.EOD_COMPANY_ID);
            user.USER_CODE = employee.EOD_EMPID;
            user.Password = "123456";
            user.ROLE_ID = await _roleService.GetRoleIdByRoleName("Employee");
            user.EssEnabled = employee.EssEnabled;
            user.PASSWORD_RESET = true;
            user.ACTIVE_USER = employee.ACTIVE_USER;

            await _userService.Create(user,ipadress,activeuser);

            foreach (var item in employee.EmployeeHierarchy)
            {
                item.Hier_Emp_ID = employeeId;
                if (item.Hier_Mgr_ID != null)
                {
                    item.Hier_Mgr_ID = item.Hier_Mgr_ID;
                    item.EOD_DESIGNATION_ID = _employeeService.GetSingle(Convert.ToInt32(item.Hier_Mgr_ID)).Result.EOD_DESIGNATION_ID;
                    await _employeeHierarchyService.Create(item,ipadress,activeuser);
                }

            }

            return CreatedAtRoute("", new { id = employeeId }, employee);

        }

        // PUT api/employee/5
        public async Task<IHttpActionResult> Put(int id, [FromBody]Employee employee)
        {
            string ipadress = employee.ipaddress;
            if (!(ModelState.IsValid))
            {
                return BadRequest(ModelState);
            }
            if (id != employee.EMPLOYEE_ID)
            {
                return BadRequest(ModelState);
            }
            Employee getEmployee = await _employeeService.GetSingle(id);
            if (getEmployee == null)
            {
                return NotFound();
            }
            _unitOfWork.BeginTransaction();

            await _employeeService.CreateHistory(getEmployee, activeuser, ipadress,activeuser);
            var employeeId = await _employeeService.Edit(employee, ipadress,activeuser);
            
            
            foreach (var item in employee.EmployeeAddresses)
            {
                await _employeeAddressService.Edit(item,ipadress,activeuser);
            }
            var emp = _employeeHierarchyService.GetByEmployeeID(id);
            if (emp != null)
            {
                foreach (var e in emp)
                {

                    if (employee.EmployeeHierarchy.Select(x => x.ENT_HierarchyDef_ID).FirstOrDefault() == e.ENT_HierarchyDef_ID)
                    {
                        var manager = employee.EmployeeHierarchy.Select(x => x.Hier_Mgr_ID).FirstOrDefault();
                        if (!(id == manager))
                        {
                            e.Hier_Mgr_ID = manager;
                            e.EOD_DESIGNATION_ID = _employeeService.GetSingle(Convert.ToInt32(manager)).Result.EOD_DESIGNATION_ID;
                            await _employeeHierarchyService.Edit(e, ipadress, activeuser);
                        }
                    }
                }
            }
            else
            {
                foreach (var item in employee.EmployeeHierarchy)
                {
                    if (!(id == item.Hier_Mgr_ID))
                    {
                        if (item.Hier_Mgr_ID != null)
                        {
                            item.Hier_Emp_ID = employee.EMPLOYEE_ID;
                            item.EOD_DESIGNATION_ID = _employeeService.GetSingle(Convert.ToInt32(item.Hier_Mgr_ID)).Result.EOD_DESIGNATION_ID;
                            await _employeeHierarchyService.Create(item, ipadress, activeuser);
                        }
                    }
                }
            }
            //foreach (var item in employee.EmployeeHierarchy)
            //{
            //    if (item.Hier_Mgr_ID != null)
            //    {

            //        item.Hier_Emp_ID = employee.EMPLOYEE_ID;
            //        item.EOD_DESIGNATION_ID = _employeeService.GetSingle(Convert.ToInt32(item.Hier_Mgr_ID)).Result.EOD_DESIGNATION_ID;
            //        if (emp != null)
            //        {
            //            foreach (var e in emp)
            //            {

            //                //item.Hier_Mgr_ID = e.Hier_Mgr_ID;

            //                if (e == null)
            //                {
            //                    await _employeeHierarchyService.Create(item);
            //                }
            //                else
            //                {
            //                    await _employeeHierarchyService.Edit(item);
            //                }
            //            }
            //        }
            //        else
            //        {
            //            await _employeeHierarchyService.Create(item);
            //        }
            //    }
            //}


            return Ok(getEmployee);
        }

        // DELETE api/employee/5
        [Route("api/employee/{id}/{user}/{ipaddress}")]
        public async Task<IHttpActionResult> Delete(int id, string user,string ipaddress)
        {
            string ipadress = ipaddress;
            Employee getEmployee = await _employeeService.GetSingle(id);
            if (getEmployee == null)
            {
                return NotFound();
            }
            _unitOfWork.BeginTransaction();
            await _employeeService.Delete(id, user, ipadress, activeuser);
            await _employeeAddressService.Delete(id,ipadress,activeuser);
            var empHierarchy = _employeeHierarchyService.GetByEmployeeID(id);
            foreach (var item in empHierarchy)
            {
                await _employeeHierarchyService.Delete(item.ENT_HierarchyDef_ID, ipadress, activeuser);
            }
            var x = _userService.GetUser(activeuser).ToList().Where(u => u.USER_CODE == getEmployee.EOD_EMPID).Select(e => e.USER_ID).FirstOrDefault();
            await _userService.Delete(x, ipadress, activeuser, user);
            return Ok(getEmployee);
        }


        [Route("api/employee/GetCompanyLength/{id}")]
        public Employee GetCompanyLength(int id)
        {
            return _employeeService.GetEmployeeCodeLength(id);
        }





        
    }
}

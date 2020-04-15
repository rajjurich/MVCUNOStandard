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
    public class EmployeeAccessConfigurationController : ApiController
    {
        private IEalConfigService _ealConfigService;
        private UnitOfWork _unitOfWork;
        private IAccessLevelService _accessLevelService;
        private int activeuser;
        private ICommonMasterService _entCommonService;
        private IEmployeeService _employeeService;
        private IEmployeeAccesssService _employeeAccesssService;
        private Utilities _ipaddressobj;

        public EmployeeAccessConfigurationController(IEmployeeService employeeService
            , IEalConfigService ealConfigService
            , IAccessLevelService accessLevelService
            , ICommonMasterService entCommonService
            , IUnitOfWork unitOfWork
            , IEmployeeAccesssService employeeAccesssService, Utilities _ipaddress)
        {
            _ipaddressobj = _ipaddress;
            _employeeService = employeeService;
            _ealConfigService = ealConfigService;
            _accessLevelService = accessLevelService;
            _unitOfWork = (UnitOfWork)unitOfWork;
            _entCommonService = entCommonService;
            _employeeAccesssService = employeeAccesssService;
            var request = HttpContext.Current.Request;
            activeuser = Convert.ToInt32(request.Headers["activeuser"]);
        }
        // GET api/employeeaccessconfiguration
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/employeeaccessconfiguration/5
        public async Task<EmployeeAccess> Get(int id)
        {
            return await _employeeAccesssService.GetSingle(id);
        }

        [Route("api/employeeaccessconfiguration/GetByEntityId/{id}")]
        public IQueryable<EalConfigDto> GetByEntityId(int id)
        {
            var dto = _ealConfigService.GetByEntityId(id);
            return dto;
        }

        // POST api/employeeaccessconfiguration
        public async Task<IHttpActionResult> Post([FromBody]EmployeeAccess employeeAccess)
        {
            string ipadress = employeeAccess.ipaddress;
            if (!(ModelState.IsValid))
            {
                return BadRequest(ModelState);
            }

            _unitOfWork.BeginTransaction();

            return await InsertEalConfig(employeeAccess);
        }

        private async Task<IHttpActionResult> InsertEalConfig(EmployeeAccess employeeAccess)
        {
            string ipadress = employeeAccess.ipaddress;
            if (employeeAccess.AL_IDs.Count > 0)
            {
                var checkEntityForEmployee = employeeAccess.COMMON_TYPES_ID == await _entCommonService.GetCommonIdByName("Employee");
                if (checkEntityForEmployee)
                {
                    if (employeeAccess.AL_IDs.Count > 4)
                    {
                        return BadRequest("One Employee cannot have more than 4 Access Level");
                    }
                    foreach (var employee in employeeAccess.EntityIds)
                    {
                        if (await _ealConfigService.checkEmployee(activeuser, employee))
                        {
                            return BadRequest("One Employee cannot have more than 4 Access Level");
                        }
                    }
                }
                if (await _ealConfigService.checkEntityAccessPermissionAdd(employeeAccess))
                {
                    foreach (var entityId in employeeAccess.EntityIds)
                    {
                        foreach (var alId in employeeAccess.AL_IDs)
                        {
                            var controllers = _accessLevelService.GetControllersByAlId(alId);
                            foreach (var controller in controllers)
                            {
                                if (checkEntityForEmployee)
                                {
                                    await InsertEal(employeeAccess.COMMON_TYPES_ID, entityId, entityId, alId, controller, ipadress);
                                }
                                else
                                {
                                    var employeeList = getEmployeeList(employeeAccess.COMMON_TYPES_ID, entityId);
                                    if (employeeList.Count > 0)
                                    {
                                        foreach (var item in employeeList)
                                        {
                                            await InsertEal(employeeAccess.COMMON_TYPES_ID, entityId, item, alId, controller, ipadress);
                                        }
                                    }
                                    else
                                    {
                                        return BadRequest("No employees found for this entity");
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    return BadRequest("Cannot add record(s), because employee(s) will exceed no of maximum level attached to them.");
                }
            }

            return Ok();
        }

        private List<int> getEmployeeList(int COMMON_TYPES_ID, int entityId)
        {
            var commonTypesName = _entCommonService.GetCommon().Where(x => x.COMMON_TYPES_ID == COMMON_TYPES_ID).Select(x => x.COMMON_NAME).FirstOrDefault();
            List<int> employeeList = new List<int>();
            if (commonTypesName == "CATEGORY")
            {
                employeeList = _employeeService.Get(activeuser).AsQueryable().Where(x => x.EOD_CATEGORY_ID == entityId).Select(x => x.EMPLOYEE_ID).ToList();
            }
            else if (commonTypesName == "DEPARTMENT")
            {
                employeeList = _employeeService.Get(activeuser).AsQueryable().Where(x => x.EOD_DEPARTMENT_ID == entityId).Select(x => x.EMPLOYEE_ID).ToList();
            }
            else if (commonTypesName == "DESIGNATION")
            {
                employeeList = _employeeService.Get(activeuser).AsQueryable().Where(x => x.EOD_DESIGNATION_ID == entityId).Select(x => x.EMPLOYEE_ID).ToList();
            }
            else if (commonTypesName == "DIVISION")
            {
                employeeList = _employeeService.Get(activeuser).AsQueryable().Where(x => x.EOD_DIVISION_ID == entityId).Select(x => x.EMPLOYEE_ID).ToList();
            }
            else if (commonTypesName == "GRADE")
            {
                employeeList = _employeeService.Get(activeuser).AsQueryable().Where(x => x.EOD_GRADE_ID == entityId).Select(x => x.EMPLOYEE_ID).ToList();
            }
            else if (commonTypesName == "GROUP")
            {
                employeeList = _employeeService.Get(activeuser).AsQueryable().Where(x => x.EOD_GROUP_ID == entityId).Select(x => x.EMPLOYEE_ID).ToList();
            }
            else if (commonTypesName == "LOCATION")
            {
                employeeList = _employeeService.Get(activeuser).AsQueryable().Where(x => x.EOD_LOCATION_ID == entityId).Select(x => x.EMPLOYEE_ID).ToList();
            }

            return employeeList;
        }

        private async Task InsertEal(int COMMON_TYPES_ID, int entityId, int emp, long alId, int controller,string ipaddress)
        {
            string ipadress = ipaddress;
            EalConfig ealConfig = new EalConfig();
            ealConfig.ENTITY_TYPE = COMMON_TYPES_ID;
            ealConfig.ENTITY_ID = entityId.ToString();
            ealConfig.EMPLOYEE_CODE = emp;
            ealConfig.CARD_CODE = emp.ToString();//getcardcode pending
            ealConfig.AL_ID = alId;
            ealConfig.FLAG = await _ealConfigService.GetFlagValue(controller);
            ealConfig.CONTROLLER_ID = controller;
            await _ealConfigService.Create(ealConfig, ipadress, activeuser);
        }

        // PUT api/employeeaccessconfiguration/5
        public async Task<IHttpActionResult> Put(int id, [FromBody]EmployeeAccess employeeAccess)
        {
            string ipadress = employeeAccess.ipaddress;
            if (!(ModelState.IsValid))
            {
                return BadRequest(ModelState);
            }

            _unitOfWork.BeginTransaction();
            
            EmployeeAccess employeeAccessDelete = await _employeeAccesssService.GetSingle(id);

            employeeAccessDelete.AL_IDs = employeeAccessDelete.AL_IDs.Except(employeeAccess.AL_IDs).ToList();

            string alids = string.Empty;
            if (employeeAccessDelete.AL_IDs.Count > 0)
            {
                foreach (var alid in employeeAccessDelete.AL_IDs)
                {
                    alids = alids + (alids.Length > 0 ? "," : "") + "'" + alid + "'";
                }
            }
            else
            {
                alids = "''";
            }

            EmployeeAccess employeeAccessAdd = await _employeeAccesssService.GetSingle(id);

            employeeAccessAdd.AL_IDs = employeeAccess.AL_IDs.Except(employeeAccessAdd.AL_IDs).ToList();

            if (!(await deleteEalConfig(id, alids, ipadress)))
            {
                return BadRequest("Something went wrong please try again");
            }

            
            return await InsertEalConfig(employeeAccessAdd);
        }

        private async Task<bool> deleteEalConfig(int id, string alids,string ipaddress)
        {
            string ipadress = ipaddress;
            return await _ealConfigService.DeleteByEntityIdAndAlids(id, alids, activeuser.ToString(), ipadress, activeuser) > -1;
        }     


        // DELETE api/employeeaccessconfiguration/5
        public void Delete(int id)
        {
        }
    }
}

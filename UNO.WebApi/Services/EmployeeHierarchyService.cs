using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using UNO.DAL;
using UNO.WebApi.Dto;
using UNO.WebApi.Models;

namespace UNO.WebApi.Services
{
    public interface IEmployeeHierarchyService
    {
        Task Create(EmployeeHierarchy entity, string ipaddress, int activeuser);
        Task Edit(EmployeeHierarchy entity, string ipaddress, int activeuser);
        Task Delete(int id, string ipaddress, int activeuser);
        IQueryable<EmployeeHierarchyDto> Get(int id);
        List<EmployeeHierarchy> GetByEmployeeID(int id);
        Task<EmployeeHierarchy> GetSingle(int id);
    }
    public class EmployeeHierarchyService : IEmployeeHierarchyService
    {
        private UnitOfWork _unitOfWork;
        private DatabaseHelper _DatabaseHelper;

        private ICommonService _commonService;
        private IUserService _userService;
        public EmployeeHierarchyService(IUnitOfWork unitOfWork
            , DatabaseHelper databaseHelper
            , ICommonService commonService
            , IUserService userService
            )
        {
            _unitOfWork = (UnitOfWork)unitOfWork;
            _DatabaseHelper = databaseHelper;
            _commonService = commonService;
            _userService = userService;
        }
        public async Task Create(EmployeeHierarchy entity, string ipaddress, int activeuser)
        {
            //var chkManager = Convert.IsDBNull(entity.Hier_Mgr_ID);
            var manager = entity.Hier_Mgr_ID.ToString();
            var designation = entity.EOD_DESIGNATION_ID.ToString();
            if (entity.Hier_Mgr_ID == null)
            {
                manager = "null";
                designation = "null";
            }
            string query = " Insert into ent_hierarchydef (Hier_Emp_ID,Hier_Mgr_ID,EOD_DESIGNATION_ID,Hier_IsDeleted" +
                            ",IS_SYNC) " +
                            " values (" + entity.Hier_Emp_ID + "," + manager + "," + designation + "" +
                            " ,0,0)";

            await _DatabaseHelper.Insert(query, CommandType.Text, ipaddress, activeuser);
        }

        public async Task Edit(EmployeeHierarchy entity, string ipaddress, int activeuser)
        {
            string query = " update ent_hierarchydef set Hier_Mgr_ID = " + entity.Hier_Mgr_ID + ",EOD_DESIGNATION_ID=" + entity.EOD_DESIGNATION_ID + "" +
                           " where ENT_HierarchyDef_ID =  " + entity.ENT_HierarchyDef_ID;

            await _DatabaseHelper.Insert(query, CommandType.Text, ipaddress, activeuser);
        }

        public async Task Delete(int id, string ipaddress, int activeuser)
        {
            string query = " update ent_hierarchydef set Hier_IsDeleted = 1,Hier_DeletedDate='" + DateTime.Now.ToString("dd/MMM/yyyy hh:mm:ss") + "'" +
                           " where ENT_HierarchyDef_ID =  " + id;

            await _DatabaseHelper.Insert(query, CommandType.Text, ipaddress,activeuser);
        }

        public IQueryable<EmployeeHierarchyDto> Get(int id)
        {

            string query = string.Empty;
            User user = _userService.GetSingleUser(id);
            if (user.USER_CODE == "superuser")
            {
                query = " select hier.*,emp.EPD_FIRST_NAME + ' ' +emp.EPD_LAST_NAME as empname,mgr.EPD_FIRST_NAME + ' ' +mgr.EPD_LAST_NAME as mgrname" +
                        " , emp.EOD_EMPID as empcode , mgr.EOD_EMPID as mgrcode " +
                        " from ENT_HierarchyDef hier " +
                        " left join ENT_EMPLOYEE_DTLS emp on hier.Hier_Emp_ID=emp.EMPLOYEE_ID " +
                        " left join ENT_EMPLOYEE_DTLS mgr on hier.Hier_Mgr_ID=mgr.EMPLOYEE_ID where Hier_IsDeleted =0";
            }
            else
            {
                query = " select hier.*,emp.EPD_FIRST_NAME + ' ' +emp.EPD_LAST_NAME as empname,mgr.EPD_FIRST_NAME + ' ' +mgr.EPD_LAST_NAME as mgrname" +
                        " , emp.EOD_EMPID as empcode , mgr.EOD_EMPID as mgrcode " +
                        " from ENT_HierarchyDef hier " +
                        " left join ENT_EMPLOYEE_DTLS emp on hier.Hier_Emp_ID=emp.EMPLOYEE_ID " +
                        " left join ENT_EMPLOYEE_DTLS mgr on hier.Hier_Mgr_ID=mgr.EMPLOYEE_ID where Hier_IsDeleted =0 and emp.EOD_COMPANY_ID in (" +
                        CustomQueryies.Queries.RoleDataAccessQuery(id) + " ) ";
            }

            var dataTable = _DatabaseHelper.GetDataTable(query, CommandType.Text);
            List<EmployeeHierarchyDto> employeeHierarchyDtos = new List<EmployeeHierarchyDto>();

            if (dataTable.Rows.Count > 0)
            {
                foreach (DataRow dr in dataTable.Rows)
                {
                    EmployeeHierarchyDto employeeHierarchyDto = new EmployeeHierarchyDto();
                    employeeHierarchyDto.ENT_HierarchyDef_ID = Convert.ToInt32(dr["ENT_HierarchyDef_ID"]);
                    employeeHierarchyDto.EmployeeName = Convert.ToString(dr["empname"]);
                    employeeHierarchyDto.EmployeeCode = Convert.ToString(dr["empcode"]);
                    var chkManager = Convert.IsDBNull(dr["Hier_Mgr_ID"]);
                    if (chkManager)
                    {
                        employeeHierarchyDto.ReportingName = "";
                        employeeHierarchyDto.ReportingCode = "";
                        employeeHierarchyDto.Designation = "";
                    }
                    else
                    {
                        employeeHierarchyDto.ReportingName = Convert.ToString(dr["mgrname"]);
                        employeeHierarchyDto.ReportingCode = Convert.ToString(dr["mgrcode"]);
                        employeeHierarchyDto.Designation = _commonService.GetCommonSingle(Convert.ToInt32(dr["EOD_DESIGNATION_ID"])).OCE_DESCRIPTION;
                    }
                    employeeHierarchyDtos.Add(employeeHierarchyDto);
                }
            }

            return employeeHierarchyDtos.AsQueryable();
        }


        public List<EmployeeHierarchy> GetByEmployeeID(int id)
        {
            string query = "select * from ENT_HierarchyDef where Hier_Emp_ID = " + id;

            var dataSet = _DatabaseHelper.GetDataSet(query, CommandType.Text);

            var dataTableForEmployeeHierarchy = dataSet.Tables[0];

            List<EmployeeHierarchy> employeeHierarchies = new List<EmployeeHierarchy>();
            EmployeeHierarchy employeeHierarchy = new EmployeeHierarchy();

            if (dataTableForEmployeeHierarchy.Rows.Count > 0)
            {
                foreach (DataRow dr in dataTableForEmployeeHierarchy.Rows)
                {
                    employeeHierarchy.ENT_HierarchyDef_ID = Convert.ToInt32(dr["ENT_HierarchyDef_ID"]);
                    employeeHierarchy.Hier_Emp_ID = Convert.ToInt32(dr["Hier_Emp_ID"]);
                    var chkManager = Convert.IsDBNull(dr["Hier_Mgr_ID"]);
                    if (chkManager)
                    {
                        employeeHierarchy.Hier_Mgr_ID = null;
                        employeeHierarchy.EOD_DESIGNATION_ID = null;
                    }
                    else
                    {
                        employeeHierarchy.Hier_Mgr_ID = Convert.ToInt32(dr["Hier_Mgr_ID"]);
                        employeeHierarchy.EOD_DESIGNATION_ID = Convert.ToInt32(dr["EOD_DESIGNATION_ID"]);
                    }
                    //var employee = _employeeService.GetSingle(Convert.ToInt32(dr["Hier_Emp_ID"])).Result;
                    //employeeHierarchy.EmployeeName = string.Format("{0} {1}", employee.EPD_FIRST_NAME, employee.EPD_LAST_NAME);
                    //var manager = _employeeService.GetSingle(Convert.ToInt32(dr["Hier_Mgr_ID"])).Result;
                    //employeeHierarchy.ReportingName = string.Format("{0} {1}", manager.EPD_FIRST_NAME, manager.EPD_LAST_NAME);
                    //employeeHierarchy.Designation = _commonService.GetCommon(Convert.ToInt32(dr["EOD_DESIGNATION_ID"])).OCE_DESCRIPTION;
                    employeeHierarchies.Add(employeeHierarchy);
                }
            }
            else
            {
                employeeHierarchies = null;
            }


            return employeeHierarchies;
        }


        public Task<EmployeeHierarchy> GetSingle(int id)
        {
            string query = "select * from ENT_HierarchyDef where ENT_HierarchyDef_ID = " + id;

            var dataSet = _DatabaseHelper.GetDataSet(query, CommandType.Text);

            var dataTableForEmployee = dataSet.Tables[0];

            EmployeeHierarchy employeeHierarchy = new EmployeeHierarchy();

            if (dataTableForEmployee.Rows.Count > 0)
            {
                foreach (DataRow dr in dataTableForEmployee.Rows)
                {
                    employeeHierarchy.ENT_HierarchyDef_ID = Convert.ToInt32(dr["ENT_HierarchyDef_ID"]);
                    employeeHierarchy.EOD_DESIGNATION_ID = Convert.ToInt32(dr["EOD_DESIGNATION_ID"]);
                    employeeHierarchy.Hier_Emp_ID = Convert.ToInt32(dr["Hier_Emp_ID"]);
                    employeeHierarchy.Hier_Mgr_ID = Convert.ToInt32(dr["Hier_Mgr_ID"]);
                   
                }
            }           

            return Task.Run(() =>
            {
                return employeeHierarchy;
            });
        }
    }
}
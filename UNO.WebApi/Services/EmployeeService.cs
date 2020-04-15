using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using UNO.DAL;
using UNO.MVCApp.Helpers;
using UNO.WebApi.Dto;
using UNO.WebApi.Models;

namespace UNO.WebApi.Services
{
    public interface IEmployeeService
    {
        Task<int> Create(Employee entity, string ipaddress, int activeuser);
        Task<int> Edit(Employee entity, string ipaddress, int activeuser);
        Task<int> CreateHistory(Employee entity, int id, string ipaddress, int activeuser);
        Task<int> Delete(int id, string user, string ipaddress, int activeuser);
        IQueryable<EmployeeDto> Get(int id);

        IQueryable<EmployeeDto> GetEmployeeTimeAttendance(int id);

        IQueryable<EmployeeDto> GetEmployeeCardConfig(int id);
        IQueryable<EmployeeDto> GetWithNoAccess(int id);
        Task<Employee> GetSingle(int id);

        Employee GetEmployeeCodeLength(int id);
    }
    public class EmployeeService : IEmployeeService
    {
        private UnitOfWork _unitOfWork;
        private DatabaseHelper _DatabaseHelper;
        private IEmployeeAddressService _employeeAddressService;
        private IEmployeeHierarchyService _employeeHierarchyService;
        private ICommonService _commonService;
        private IUserService _userService;
        private int activeuser;
        private Utilities _ipaddressobj;
        public EmployeeService(IUnitOfWork unitOfWork
            , DatabaseHelper databaseHelper
            , IEmployeeAddressService employeeAddressService
            , IEmployeeHierarchyService employeeHierarchyService
            , ICommonService commonService
            , IUserService userService
            , Utilities _ipaddress)
        {
            _ipaddressobj = _ipaddress;
            var request = HttpContext.Current.Request;
            activeuser = Convert.ToInt32(request.Headers["activeuser"]);
            _unitOfWork = (UnitOfWork)unitOfWork;
            _DatabaseHelper = databaseHelper;
            _employeeAddressService = employeeAddressService;
            _employeeHierarchyService = employeeHierarchyService;
            _commonService = commonService;
            _userService = userService;
        }
        public async Task<int> Create(Employee entity, string ipaddress, int activeuser)
        {
            var leftReasonId = entity.EOD_LEFT_REASON_ID.ToString();
            if (entity.EOD_LEFT_REASON_ID == null)
            {
                leftReasonId = "null";
            }

            var previousempid = entity.PREVIOUS_EMPLOYEE_ID.ToString();

            if (entity.PREVIOUS_EMPLOYEE_ID == null)
            {
                previousempid = "null";
            }

            string query = " insert into ENT_EMPLOYEE_DTLS(EOD_EMPID " +
                           " ,EPD_SALUTATION" +
                           " ,EPD_FIRST_NAME" +
                           " ,EPD_MIDDLE_NAME" +
                           " ,EPD_LAST_NAME" +
                           " ,EPD_PERSO_FLAG" +
                           " ,EPD_CARD_ID" +
                           " ,EOD_JOINING_DATE" +
                           " ,EOD_CONFIRM_DATE" +
                           " ,EOD_LEFT_DATE" +
                           " ,EOD_LEFT_REASON_ID" +
                           " ,EOD_COMPANY_ID" +
                           " ,EOD_LOCATION_ID" +
                           " ,EOD_DIVISION_ID" +
                           " ,EOD_DEPARTMENT_ID" +
                           " ,EOD_DESIGNATION_ID" +
                           " ,EOD_CATEGORY_ID" +
                           " ,EOD_GROUP_ID" +
                           " ,EOD_GRADE_ID" +
                           " ,EOD_STATUS" +
                           " ,EOD_ISDELETED" +
                           " ,EOD_TYPE" +
                           " ,EOD_WORKTYPE" +
                           " ,EOD_CARD_PIN" +
                           " ,IS_SYNC" +
                           " ,PREVIOUS_EMPLOYEE_ID) values('" + entity.EOD_EMPID + "'" +
                           ",'" + entity.EPD_SALUTATION + "'" +
                           ",'" + entity.EPD_FIRST_NAME + "'" +
                           ",'" + entity.EPD_MIDDLE_NAME + "'" +
                           ",'" + entity.EPD_LAST_NAME + "'" +
                           ",'" + entity.EPD_PERSO_FLAG + "'" +
                           ",'" + entity.EPD_CARD_ID + "'" +
                           ",'" + entity.EOD_JOINING_DATE + "'" +
                           ",'" + entity.EOD_CONFIRM_DATE + "'" +
                           ",'" + entity.EOD_LEFT_DATE + "'" +
                           "," + leftReasonId + "" +
                           ",'" + entity.EOD_COMPANY_ID + "'" +
                           ",'" + entity.EOD_LOCATION_ID + "'" +
                           ",'" + entity.EOD_DIVISION_ID + "'" +
                           ",'" + entity.EOD_DEPARTMENT_ID + "'" +
                           ",'" + entity.EOD_DESIGNATION_ID + "'" +
                           ",'" + entity.EOD_CATEGORY_ID + "'" +
                           ",'" + entity.EOD_GROUP_ID + "'" +
                            ",'" + entity.EOD_GRADE_ID + "'" +
                           ",1" +
                           ",0" +
                           ",'" + entity.EOD_TYPE + "'" +
                           ",'" + entity.EOD_WORKTYPE + "'" +
                           ",'" + entity.EOD_CARD_PIN + "'" +
                           ",0" +
                           "," + previousempid + "" +
                           ") ; ";
            query += " select @@Identity ;";
            query += " insert into ENT_EMPLOYEE_PERSONAL_DTLS(" +
                     " EMPLOYEE_ID" +
                     " ,EPD_GENDER" +
                     " ,EPD_MARITAL_STATUS" +
                     " ,EPD_DOB" +
                     " ,EPD_DateOFMarriage" +
                     " ,EPD_RELIGION" +
                     " ,EPD_REFERENCE_ONE" +
                     " ,EPD_REFERENCE_TWO" +
                     " ,EPD_DOMICILE" +
                     " ,EPD_BLOODGROUP" +
                     " ,EPD_EMAIL" +
                     " ,EPD_PAN" +
                     " ,EPD_PHOTOURL" +
                     " ,EPD_AADHARCARD" +
                     " ,EPD_UAN" +
                     " ,EPD_ISDELETED" +
                     " ,IS_SYNC ) values(@@Identity" +
                     ",'" + entity.EPD_GENDER + "'" +
                     ",'" + entity.EPD_MARITAL_STATUS + "'" +
                     ",'" + entity.EPD_DOB + "'" +
                     ",'" + entity.EPD_DateOFMarriage + "'" +
                     ",'" + entity.EPD_RELIGION + "'" +
                     ",'" + entity.EPD_REFERENCE_ONE + "'" +
                     ",'" + entity.EPD_REFERENCE_TWO + "'" +
                     ",'" + entity.EPD_DOMICILE + "'" +
                     ",'" + entity.EPD_BLOODGROUP + "'" +
                     ",'" + entity.EPD_EMAIL + "'" +
                     ",'" + entity.EPD_PAN + "'" +
                     ",'" + entity.EPD_PHOTOURL + "'" +
                     ",'" + entity.EPD_AADHARCARD + "'" +
                     ",'" + entity.EPD_UAN + "'" +
                     ",0" +
                     ",0" +
                     ") ;";

            return await _DatabaseHelper.Insert(query, CommandType.Text, ipaddress, activeuser);
        }

        public Task<Employee> GetSingle(int id)
        {
            string query = " select * from ENT_EMPLOYEE_DTLS eod join ENT_EMPLOYEE_PERSONAL_DTLS epd " +
                           " on eod.EMPLOYEE_ID=epd.EMPLOYEE_ID  " +
                           " join ent_user usr on usr.USER_CODE=eod.EOD_EMPID " +
                           " where eod.EMPLOYEE_ID = " + id;
            //query += " ; ";

            // query += " ; select * from ENT_EMPLOYEE_ADDRESS where EMPLOYEE_ID =" + id + "";

            var dataSet = _DatabaseHelper.GetDataSet(query, CommandType.Text);

            var dataTableForEmployee = dataSet.Tables[0];

            Employee employee = new Employee();

            if (dataTableForEmployee.Rows.Count > 0)
            {
                foreach (DataRow dr in dataTableForEmployee.Rows)
                {
                    employee.EMPLOYEE_ID = Convert.ToInt32(dr["EMPLOYEE_ID"]);
                    employee.EssEnabled = Convert.ToBoolean(dr["EssEnabled"]);
                    employee.EOD_EMPID = Convert.ToString(dr["EOD_EMPID"]);
                    employee.EPD_SALUTATION = Convert.ToString(dr["EPD_SALUTATION"]);
                    employee.EPD_FIRST_NAME = Convert.ToString(dr["EPD_FIRST_NAME"]);
                    employee.EPD_MIDDLE_NAME = Convert.ToString(dr["EPD_MIDDLE_NAME"]);
                    employee.EPD_LAST_NAME = Convert.ToString(dr["EPD_LAST_NAME"]);
                    employee.EPD_PERSO_FLAG = Convert.ToString(dr["EPD_PERSO_FLAG"]);
                    employee.EPD_CARD_ID = Convert.ToString(dr["EPD_CARD_ID"]);
                    employee.EOD_JOINING_DATE = Convert.ToDateTime(dr["EOD_JOINING_DATE"]);
                    employee.EOD_CONFIRM_DATE = Convert.ToDateTime(dr["EOD_CONFIRM_DATE"]);
                    employee.EOD_LEFT_DATE = Convert.ToDateTime(dr["EOD_LEFT_DATE"]);
                    var chkLeftReason = Convert.IsDBNull(dr["EOD_LEFT_REASON_ID"]);
                    if (chkLeftReason)
                    {
                        employee.EOD_LEFT_REASON_ID = null;
                        employee.EOD_STATUS = 1;
                    }
                    else
                    {
                        employee.EOD_LEFT_REASON_ID = Convert.ToInt32(dr["EOD_LEFT_REASON_ID"]);
                        employee.EOD_STATUS = 0;
                    }


                    employee.EOD_COMPANY_ID = Convert.ToInt32(dr["EOD_COMPANY_ID"]);
                    employee.EOD_LOCATION_ID = Convert.ToInt32(dr["EOD_LOCATION_ID"]);
                    employee.EOD_DIVISION_ID = Convert.ToInt32(dr["EOD_DIVISION_ID"]);
                    employee.EOD_DEPARTMENT_ID = Convert.ToInt32(dr["EOD_DEPARTMENT_ID"]);
                    employee.EOD_DESIGNATION_ID = Convert.ToInt32(dr["EOD_DESIGNATION_ID"]);
                    employee.EOD_CATEGORY_ID = Convert.ToInt32(dr["EOD_CATEGORY_ID"]);
                    employee.EOD_GROUP_ID = Convert.ToInt32(dr["EOD_GROUP_ID"]);
                    employee.EOD_GRADE_ID = Convert.ToInt32(dr["EOD_GRADE_ID"]);
                    employee.EOD_TYPE = Convert.ToString(dr["EOD_TYPE"]);
                    employee.EOD_WORKTYPE = Convert.ToString(dr["EOD_WORKTYPE"]);
                    employee.EOD_CARD_PIN = Convert.ToString(dr["EOD_CARD_PIN"]);

                    var chkprevioueEmployeeID = Convert.IsDBNull(dr["PREVIOUS_EMPLOYEE_ID"]);
                    if (chkprevioueEmployeeID)
                    {
                        employee.PREVIOUS_EMPLOYEE_ID = null;
                    }
                    else
                    {
                        employee.PREVIOUS_EMPLOYEE_ID = Convert.ToInt32(dr["PREVIOUS_EMPLOYEE_ID"]);
                    }

                    employee.EPD_GENDER = Convert.ToInt32(dr["EPD_GENDER"]);
                    employee.EPD_MARITAL_STATUS = Convert.ToInt32(dr["EPD_MARITAL_STATUS"]);
                    employee.EPD_DOB = Convert.ToDateTime(dr["EPD_DOB"]);
                    employee.EPD_DateOFMarriage = Convert.ToDateTime(dr["EPD_DateOFMarriage"]);
                    employee.EPD_RELIGION = Convert.ToInt32(dr["EPD_RELIGION"]);
                    employee.EPD_REFERENCE_ONE = Convert.ToString(dr["EPD_REFERENCE_ONE"]);
                    employee.EPD_REFERENCE_TWO = Convert.ToString(dr["EPD_REFERENCE_TWO"]);
                    employee.EPD_DOMICILE = Convert.ToString(dr["EPD_DOMICILE"]);
                    employee.EPD_BLOODGROUP = Convert.ToInt32(dr["EPD_BLOODGROUP"]);
                    employee.EPD_EMAIL = Convert.ToString(dr["EPD_EMAIL"]);
                    employee.EPD_PAN = Convert.ToString(dr["EPD_PAN"]);
                    employee.EPD_PHOTOURL = Convert.ToString(dr["EPD_PHOTOURL"]);
                    employee.EPD_AADHARCARD = Convert.ToString(dr["EPD_AADHARCARD"]);
                    employee.EPD_UAN = Convert.ToString(dr["EPD_UAN"]);
                }
            }

            employee.EmployeeAddresses = _employeeAddressService.GetEmployeeAddressByEmployeeId(id);


            employee.EmployeeHierarchy = _employeeHierarchyService.GetByEmployeeID(id);

            return Task.Run(() =>
            {
                return employee;
            });
        }


        public IQueryable<EmployeeDto> Get(int id)
        {

            string query = string.Empty;
            User user = _userService.GetSingleUser(id);
            if (user.USER_CODE == "superuser")
            {
                query = " select * from ENT_EMPLOYEE_DTLS eod join ENT_EMPLOYEE_PERSONAL_DTLS epd on eod.EMPLOYEE_ID=epd.EMPLOYEE_ID where EOD_ISDELETED =0 ";
            }
            else
            {
                query = " select eod.EMPLOYEE_ID,EOD_EMPID,EPD_FIRST_NAME,EPD_LAST_NAME,EOD_JOINING_DATE,EOD_DESIGNATION_ID,EOD_STATUS,EOD_COMPANY_ID " +
                        " ,EOD_LOCATION_ID,EOD_DIVISION_ID,EOD_DEPARTMENT_ID,EOD_CATEGORY_ID,EOD_GROUP_ID,EOD_GRADE_ID,EOD_DESIGNATION_ID" +
                        " from ENT_EMPLOYEE_DTLS eod join ENT_EMPLOYEE_PERSONAL_DTLS epd on eod.EMPLOYEE_ID=epd.EMPLOYEE_ID where EOD_ISDELETED =0 and EOD_COMPANY_ID in( " +
                        CustomQueryies.Queries.RoleDataAccessQuery(id) + " ) ";

                query += " union " +
                        " select eod.EMPLOYEE_ID,eod.EOD_EMPID,eod.EPD_FIRST_NAME,eod.EPD_LAST_NAME,eod.EOD_JOINING_DATE,eod.EOD_DESIGNATION_ID,eod.EOD_STATUS,eod.EOD_COMPANY_ID" +
                        " ,eod.EOD_LOCATION_ID,eod.EOD_DIVISION_ID,eod.EOD_DEPARTMENT_ID,eod.EOD_CATEGORY_ID,eod.EOD_GROUP_ID,eod.EOD_GRADE_ID,eod.EOD_DESIGNATION_ID" +
                        " from ENT_ROLE_DATA_ACCESS da  " +
                        " join ent_org_common_types ct on da.COMMON_TYPES_ID=ct.COMMON_TYPES_ID " +
                        " join ENT_EMPLOYEE_DTLS eod on eod.EMPLOYEE_ID=MAPPED_ENTITY_ID and ct.COMMON_TYPES='Employee' " +
                        " join ent_company com on com.COMPANY_ID=eod.EOD_COMPANY_ID and da.MAPPED_ENTITY_ID=eod.EMPLOYEE_ID  " +
                        " where da.USER_CODE=" + id + "";
            }

            var dataTable = _DatabaseHelper.GetDataTable(query, CommandType.Text);
            List<EmployeeDto> employees = new List<EmployeeDto>();

            if (dataTable.Rows.Count > 0)
            {
                foreach (DataRow dr in dataTable.Rows)
                {
                    var x = dataTable.Rows.IndexOf(dr);
                    if (x == 500)
                    {
                        break;
                    }
                    EmployeeDto employee = new EmployeeDto();

                    employee.EMPLOYEE_ID = Convert.ToInt32(dr["EMPLOYEE_ID"]);
                    employee.EOD_EMPID = Convert.ToString(dr["EOD_EMPID"]);
                    employee.FULL_NAME = string.Format("{0} {1}", Convert.ToString(dr["EPD_FIRST_NAME"]), Convert.ToString(dr["EPD_LAST_NAME"]));
                    employee.EOD_JOINING_DATE = Convert.ToDateTime(dr["EOD_JOINING_DATE"]).ToString("dd MMM yyyy");
                    employee.EOD_DESIGNATION = _commonService.GetCommonSingle(Convert.ToInt32(dr["EOD_DESIGNATION_ID"])).OCE_DESCRIPTION;
                    employee.EOD_STATUS = Convert.ToInt32(dr["EOD_STATUS"]) == 1 ? "active" : "inactive";
                    employee.EOD_COMPANY_ID = Convert.ToInt32(dr["EOD_COMPANY_ID"]);
                    employee.EOD_LOCATION_ID = Convert.ToInt32(dr["EOD_LOCATION_ID"]);
                    employee.EOD_DIVISION_ID = Convert.ToInt32(dr["EOD_DIVISION_ID"]);
                    employee.EOD_DEPARTMENT_ID = Convert.ToInt32(dr["EOD_DEPARTMENT_ID"]);
                    employee.EOD_CATEGORY_ID = Convert.ToInt32(dr["EOD_CATEGORY_ID"]);
                    employee.EOD_GROUP_ID = Convert.ToInt32(dr["EOD_GROUP_ID"]);
                    employee.EOD_GRADE_ID = Convert.ToInt32(dr["EOD_GRADE_ID"]);
                    employee.EOD_DESIGNATION_ID = Convert.ToInt32(dr["EOD_DESIGNATION_ID"]);
                    employees.Add(employee);
                }
            }

            return employees.AsQueryable();
        }


        public IQueryable<EmployeeDto> GetEmployeeTimeAttendance(int id)
        {

            string query = "";
            User user = _userService.GetSingleUser(id);
            if (user.USER_CODE == "superuser")
            {
                query = "select employee.* from ENT_EMPLOYEE_DTLS as employee left join TNA_EMPLOYEE_TA_CONFIG as TNA on employee.EMPLOYEE_ID=TNA.ETC_EMP_ID and TNA.ETC_ISDELETED=0 where TNA.ETC_EMP_ID is null and employee.EOD_ISDELETED=0 ";
            }
            else
            {
                query = "select employee.* from ENT_EMPLOYEE_DTLS as employee left join TNA_EMPLOYEE_TA_CONFIG as TNA on employee.EMPLOYEE_ID=TNA.ETC_EMP_ID and TNA.ETC_ISDELETED=0 where TNA.ETC_EMP_ID is null and employee.EOD_ISDELETED=0 and employee.EOD_COMPANY_ID in(" + CustomQueryies.Queries.RoleDataAccessQuery(id) + ")";
            }




            var dataTable = _DatabaseHelper.GetDataTable(query, CommandType.Text);
            List<EmployeeDto> employees = new List<EmployeeDto>();

            if (dataTable.Rows.Count > 0)
            {
                foreach (DataRow dr in dataTable.Rows)
                {
                    EmployeeDto employee = new EmployeeDto();

                    employee.EMPLOYEE_ID = Convert.ToInt32(dr["EMPLOYEE_ID"]);
                    employee.EOD_EMPID = Convert.ToString(dr["EOD_EMPID"]);
                    employee.FULL_NAME = string.Format("{0} {1}", Convert.ToString(dr["EPD_FIRST_NAME"]), Convert.ToString(dr["EPD_LAST_NAME"]));
                    employee.EOD_JOINING_DATE = Convert.ToDateTime(dr["EOD_JOINING_DATE"]).ToString("dd MMM yyyy");
                    employee.EOD_DESIGNATION = _commonService.GetCommonSingle(Convert.ToInt32(dr["EOD_DESIGNATION_ID"])).OCE_DESCRIPTION;
                    employee.EOD_STATUS = Convert.ToInt32(dr["EOD_STATUS"]) == 1 ? "active" : "inactive";
                    employee.EOD_COMPANY_ID = Convert.ToInt32(dr["EOD_COMPANY_ID"]);
                    employee.EOD_LOCATION_ID = Convert.ToInt32(dr["EOD_LOCATION_ID"]);
                    employee.EOD_DIVISION_ID = Convert.ToInt32(dr["EOD_DIVISION_ID"]);
                    employee.EOD_DEPARTMENT_ID = Convert.ToInt32(dr["EOD_DEPARTMENT_ID"]);
                    employees.Add(employee);
                }
            }

            return employees.AsQueryable();
        }


        public IQueryable<EmployeeDto> GetEmployeeCardConfig(int id)
        {
            string query = "";
            User user = _userService.GetSingleUser(id);
            if (user.USER_CODE == "superuser")
            {
                query = "select employee.* from ENT_EMPLOYEE_DTLS as employee left join ACS_CARD_CONFIG as ACS on employee.EMPLOYEE_ID=ACS.CC_EMP_ID and ACS.ACE_isdeleted=0 where ACS.CC_EMP_ID is null and employee.EOD_ISDELETED=0  ";
            }
            else
            {
                query = "select employee.* from ENT_EMPLOYEE_DTLS as employee left join ACS_CARD_CONFIG as ACS on employee.EMPLOYEE_ID=ACS.CC_EMP_ID and ACS.ACE_isdeleted=0 where ACS.CC_EMP_ID is null and employee.EOD_ISDELETED=0  and employee.EOD_COMPANY_ID in(" + CustomQueryies.Queries.RoleDataAccessQuery(id) + ")";
            }




            var dataTable = _DatabaseHelper.GetDataTable(query, CommandType.Text);
            List<EmployeeDto> employees = new List<EmployeeDto>();

            if (dataTable.Rows.Count > 0)
            {
                foreach (DataRow dr in dataTable.Rows)
                {
                    EmployeeDto employee = new EmployeeDto();

                    employee.EMPLOYEE_ID = Convert.ToInt32(dr["EMPLOYEE_ID"]);
                    employee.EOD_EMPID = Convert.ToString(dr["EOD_EMPID"]);
                    employee.FULL_NAME = string.Format("{0} {1}", Convert.ToString(dr["EPD_FIRST_NAME"]), Convert.ToString(dr["EPD_LAST_NAME"]));
                    employee.EOD_JOINING_DATE = Convert.ToDateTime(dr["EOD_JOINING_DATE"]).ToString("dd MMM yyyy");
                    employee.EOD_DESIGNATION = _commonService.GetCommonSingle(Convert.ToInt32(dr["EOD_DESIGNATION_ID"])).OCE_DESCRIPTION;
                    employee.EOD_STATUS = Convert.ToInt32(dr["EOD_STATUS"]) == 1 ? "active" : "inactive";
                    employee.EOD_COMPANY_ID = Convert.ToInt32(dr["EOD_COMPANY_ID"]);
                    employee.EOD_LOCATION_ID = Convert.ToInt32(dr["EOD_LOCATION_ID"]);
                    employee.EOD_DIVISION_ID = Convert.ToInt32(dr["EOD_DIVISION_ID"]);
                    employee.EOD_DEPARTMENT_ID = Convert.ToInt32(dr["EOD_DEPARTMENT_ID"]);
                    employees.Add(employee);
                }
            }

            return employees.AsQueryable();
        }



        public async Task<int> Edit(Employee entity, string ipaddress, int activeuser)
        {

            var leftReasonId = entity.EOD_LEFT_REASON_ID.ToString();
            var status = 0;
            if (entity.EOD_LEFT_REASON_ID == null)
            {
                leftReasonId = "null";
                status = 1;
            }

            var previousempid = entity.PREVIOUS_EMPLOYEE_ID.ToString();

            if (entity.PREVIOUS_EMPLOYEE_ID == null)
            {
                previousempid = "null";
            }

            string query = " Update ent_user set  USER_CODE = '" + entity.EOD_EMPID + "',EssEnabled = '" + entity.EssEnabled + "',COMPANY_ID='" + entity.EOD_COMPANY_ID + "' where USER_CODE in (select EOD_EMPID from ENT_EMPLOYEE_DTLS where EMPLOYEE_ID =  " + entity.EMPLOYEE_ID + ")";

            query += " update ENT_EMPLOYEE_DTLS set EOD_EMPID = '" + entity.EOD_EMPID + "'" +
                           " ,EPD_SALUTATION = '" + entity.EPD_SALUTATION + "' " +
                           " ,EPD_FIRST_NAME = '" + entity.EPD_FIRST_NAME + "',EPD_MIDDLE_NAME='" + entity.EPD_MIDDLE_NAME + "'" +
                           " ,EPD_LAST_NAME='" + entity.EPD_LAST_NAME + "',EPD_PERSO_FLAG='" + entity.EPD_PERSO_FLAG + "'" +
                           " ,EPD_CARD_ID='" + entity.EPD_CARD_ID + "',EOD_JOINING_DATE='" + entity.EOD_JOINING_DATE + "'" +
                           " ,EOD_CONFIRM_DATE='" + entity.EOD_CONFIRM_DATE + "' " +
                           " ,EOD_LEFT_DATE='" + entity.EOD_LEFT_DATE + "'" +
                           " ,EOD_LEFT_REASON_ID=" + leftReasonId + ",EOD_COMPANY_ID='" + entity.EOD_COMPANY_ID + "'" +
                           " ,EOD_LOCATION_ID='" + entity.EOD_LOCATION_ID + "',EOD_DIVISION_ID='" + entity.EOD_DIVISION_ID + "'" +
                           " ,EOD_DEPARTMENT_ID='" + entity.EOD_DEPARTMENT_ID + "',EOD_DESIGNATION_ID='" + entity.EOD_DESIGNATION_ID + "'" +
                           " ,EOD_CATEGORY_ID='" + entity.EOD_CATEGORY_ID + "',EOD_GROUP_ID='" + entity.EOD_GROUP_ID + "'" +
                           " ,EOD_GRADE_ID='" + entity.EOD_GRADE_ID + "',EOD_STATUS='" + status + "'" +
                           " ,EOD_TYPE='" + entity.EOD_TYPE + "',EOD_WORKTYPE='" + entity.EOD_WORKTYPE + "'" +
                           " ,EOD_CARD_PIN='" + entity.EOD_CARD_PIN + "',PREVIOUS_EMPLOYEE_ID=" + previousempid + "" +
                           " where EMPLOYEE_ID =  " + entity.EMPLOYEE_ID + " ;";

            query += " update ENT_EMPLOYEE_PERSONAL_DTLS set EPD_GENDER = '" + entity.EPD_GENDER + "'" +
                     " ,EPD_MARITAL_STATUS = '" + entity.EPD_MARITAL_STATUS + "',EPD_DOB='" + entity.EPD_DOB + "'" +
                     " ,EPD_DateOFMarriage = '" + entity.EPD_DateOFMarriage + "',EPD_RELIGION='" + entity.EPD_RELIGION + "'" +
                     " ,EPD_REFERENCE_ONE = '" + entity.EPD_REFERENCE_ONE + "',EPD_REFERENCE_TWO='" + entity.EPD_REFERENCE_TWO + "'" +
                     " ,EPD_DOMICILE = '" + entity.EPD_DOMICILE + "',EPD_BLOODGROUP='" + entity.EPD_BLOODGROUP + "'" +
                     " ,EPD_EMAIL = '" + entity.EPD_EMAIL + "',EPD_PAN='" + entity.EPD_PAN + "'" +
                     " ,EPD_PHOTOURL = '" + entity.EPD_PHOTOURL + "',EPD_AADHARCARD='" + entity.EPD_AADHARCARD + "'" +
                     " ,EPD_UAN = '" + entity.EPD_UAN + "' " +
                     " where EMPLOYEE_ID =  " + entity.EMPLOYEE_ID + " ;";


            return await _DatabaseHelper.Insert(query, CommandType.Text, ipaddress, activeuser);
        }


        public async Task<int> Delete(int id, string user, string ipaddress, int activeuser)
        {
            string query = " update ENT_EMPLOYEE_DTLS set EOD_ISDELETED = 1" +
                //" ,CTLR_DELETEDBY='" + user + "'" +
                           " ,EOD_DELETEDDATE= '" + DateTime.Now.ToString("dd/MMM/yyyy hh:mm:ss") + "'" +
                           " where EMPLOYEE_ID =  " + id + " ;";
            query += " update ENT_EMPLOYEE_PERSONAL_DTLS set EPD_ISDELETED = 1" +
                     " ,EPD_DELETEDDATE= '" + DateTime.Now.ToString("dd/MMM/yyyy hh:mm:ss") + "'" +
                     " where EMPLOYEE_ID =  " + id + " ;";
            return await _DatabaseHelper.Insert(query, CommandType.Text, ipaddress, activeuser);
        }


        public IQueryable<EmployeeDto> GetWithNoAccess(int id)
        {
            string query = string.Empty;
            User user = _userService.GetSingleUser(id);
            if (user.USER_CODE == "superuser")
            {
                query = " select * from ENT_EMPLOYEE_DTLS eod join ENT_EMPLOYEE_PERSONAL_DTLS epd on eod.EMPLOYEE_ID=epd.EMPLOYEE_ID where EOD_ISDELETED =0 and eod.EMPLOYEE_ID not in (select EMPLOYEE_CODE from eal_config where ISDELETED=0 and ENTITY_TYPE in (select COMMON_TYPES_ID from ENT_ORG_COMMON_TYPES where COMMON_TYPES='employee'))";
            }
            else
            {
                query = " select eod.EMPLOYEE_ID,EOD_EMPID,EPD_FIRST_NAME,EPD_LAST_NAME,EOD_JOINING_DATE,EOD_DESIGNATION_ID,EOD_STATUS,EOD_COMPANY_ID " +
                        " ,EOD_LOCATION_ID,EOD_DIVISION_ID,EOD_DEPARTMENT_ID" +
                        " from ENT_EMPLOYEE_DTLS eod join ENT_EMPLOYEE_PERSONAL_DTLS epd on eod.EMPLOYEE_ID=epd.EMPLOYEE_ID where EOD_ISDELETED =0 and eod.EMPLOYEE_ID not in (select EMPLOYEE_CODE from eal_config where ISDELETED=0 and ENTITY_TYPE in (select COMMON_TYPES_ID from ENT_ORG_COMMON_TYPES where COMMON_TYPES='employee')) and EOD_COMPANY_ID in( " +
                        CustomQueryies.Queries.RoleDataAccessQuery(id) + " ) ";

                query += " union " +
                        " select eod.EMPLOYEE_ID,eod.EOD_EMPID,eod.EPD_FIRST_NAME,eod.EPD_LAST_NAME,eod.EOD_JOINING_DATE,eod.EOD_DESIGNATION_ID,eod.EOD_STATUS,eod.EOD_COMPANY_ID" +
                        " ,eod.EOD_LOCATION_ID,eod.EOD_DIVISION_ID,eod.EOD_DEPARTMENT_ID" +
                        " from ENT_ROLE_DATA_ACCESS da  " +
                        " join ent_org_common_types ct on da.COMMON_TYPES_ID=ct.COMMON_TYPES_ID " +
                        " join ENT_EMPLOYEE_DTLS eod on eod.EMPLOYEE_ID=MAPPED_ENTITY_ID and ct.COMMON_TYPES='Employee' " +
                        " join ent_company com on com.COMPANY_ID=eod.EOD_COMPANY_ID and da.MAPPED_ENTITY_ID=eod.EMPLOYEE_ID  " +
                        " where da.USER_CODE=" + id + " and eod.EMPLOYEE_ID not in (select EMPLOYEE_CODE from eal_config where ISDELETED=0 and ENTITY_TYPE in (select COMMON_TYPES_ID from ENT_ORG_COMMON_TYPES where COMMON_TYPES='employee'))";
            }

            var dataTable = _DatabaseHelper.GetDataTable(query, CommandType.Text);
            List<EmployeeDto> employees = new List<EmployeeDto>();

            if (dataTable.Rows.Count > 0)
            {
                foreach (DataRow dr in dataTable.Rows)
                {
                    EmployeeDto employee = new EmployeeDto();

                    employee.EMPLOYEE_ID = Convert.ToInt32(dr["EMPLOYEE_ID"]);
                    employee.EOD_EMPID = Convert.ToString(dr["EOD_EMPID"]);
                    employee.FULL_NAME = string.Format("{0} {1}", Convert.ToString(dr["EPD_FIRST_NAME"]), Convert.ToString(dr["EPD_LAST_NAME"]));
                    employee.EOD_JOINING_DATE = Convert.ToDateTime(dr["EOD_JOINING_DATE"]).ToString("dd MMM yyyy");
                    employee.EOD_DESIGNATION = _commonService.GetCommonSingle(Convert.ToInt32(dr["EOD_DESIGNATION_ID"])).OCE_DESCRIPTION;
                    employee.EOD_STATUS = Convert.ToInt32(dr["EOD_STATUS"]) == 1 ? "active" : "inactive";
                    employee.EOD_COMPANY_ID = Convert.ToInt32(dr["EOD_COMPANY_ID"]);
                    employee.EOD_LOCATION_ID = Convert.ToInt32(dr["EOD_LOCATION_ID"]);
                    employee.EOD_DIVISION_ID = Convert.ToInt32(dr["EOD_DIVISION_ID"]);
                    employee.EOD_DEPARTMENT_ID = Convert.ToInt32(dr["EOD_DEPARTMENT_ID"]);
                    employees.Add(employee);
                }
            }

            return employees.AsQueryable();
        }


        public async Task<int> CreateHistory(Employee entity, int id, string ipaddress, int activeuser)
        {
            string query = string.Empty;
            User user = _userService.GetSingleUser(id);
            var leftReasonId = entity.EOD_LEFT_REASON_ID.ToString();
            if (entity.EOD_LEFT_REASON_ID == null)
            {
                leftReasonId = "null";
            }

            var previousempid = entity.PREVIOUS_EMPLOYEE_ID.ToString();

            if (entity.PREVIOUS_EMPLOYEE_ID == null)
            {
                previousempid = "null";
            }

            query = " insert into ENT_EMPLOYEE_DTLS_HISTORY(EMPLOYEE_ID,EOD_EMPID " +
                           " ,EPD_SALUTATION" +
                           " ,EPD_FIRST_NAME" +
                           " ,EPD_MIDDLE_NAME" +
                           " ,EPD_LAST_NAME" +
                           " ,EPD_PERSO_FLAG" +
                           " ,EPD_CARD_ID" +
                           " ,EOD_JOINING_DATE" +
                           " ,EOD_CONFIRM_DATE" +
                           " ,EOD_LEFT_DATE" +
                           " ,EOD_LEFT_REASON_ID" +
                           " ,EOD_COMPANY_ID" +
                           " ,EOD_LOCATION_ID" +
                           " ,EOD_DIVISION_ID" +
                           " ,EOD_DEPARTMENT_ID" +
                           " ,EOD_DESIGNATION_ID" +
                           " ,EOD_CATEGORY_ID" +
                           " ,EOD_GROUP_ID" +
                           " ,EOD_GRADE_ID" +
                           " ,EOD_STATUS" +
                           " ,EOD_ISDELETED" +
                           " ,EOD_TYPE" +
                           " ,EOD_WORKTYPE" +
                           " ,EOD_CARD_PIN" +
                           " ,IS_SYNC" +
                           " ,PREVIOUS_EMPLOYEE_ID,EMP_MODIFIEDDATE,EMP_MODIFIEDBY) values('" + entity.EMPLOYEE_ID + "','" + entity.EOD_EMPID + "'" +
                           ",'" + entity.EPD_SALUTATION + "'" +
                           ",'" + entity.EPD_FIRST_NAME + "'" +
                           ",'" + entity.EPD_MIDDLE_NAME + "'" +
                           ",'" + entity.EPD_LAST_NAME + "'" +
                           ",'" + entity.EPD_PERSO_FLAG + "'" +
                           ",'" + entity.EPD_CARD_ID + "'" +
                           ",'" + entity.EOD_JOINING_DATE + "'" +
                           ",'" + entity.EOD_CONFIRM_DATE + "'" +
                           ",'" + entity.EOD_LEFT_DATE + "'" +
                           "," + leftReasonId + "" +
                           ",'" + entity.EOD_COMPANY_ID + "'" +
                           ",'" + entity.EOD_LOCATION_ID + "'" +
                           ",'" + entity.EOD_DIVISION_ID + "'" +
                           ",'" + entity.EOD_DEPARTMENT_ID + "'" +
                           ",'" + entity.EOD_DESIGNATION_ID + "'" +
                           ",'" + entity.EOD_CATEGORY_ID + "'" +
                           ",'" + entity.EOD_GROUP_ID + "'" +
                            ",'" + entity.EOD_GRADE_ID + "'" +
                           ",1" +
                           ",0" +
                           ",'" + entity.EOD_TYPE + "'" +
                           ",'" + entity.EOD_WORKTYPE + "'" +
                           ",'" + entity.EOD_CARD_PIN + "'" +
                           ",0" +
                           "," + previousempid + "" +
                           ",'" + DateTime.Now.ToString("dd/MMM/yyyy hh:mm:ss") + "'" +
                           ",'" + user.USER_CODE + "'" +
                           ") ; ";



            return await _DatabaseHelper.Insert(query, CommandType.Text, ipaddress, activeuser);
        }


        public Employee GetEmployeeCodeLength(int id)
        {
            string query = "select COMPANY_EMP_CODE_LENGTH from ent_company  where COMPANY_ISDELETED=0 and COMPANY_ID=" + id;

            var x = _DatabaseHelper.GetDataTable(query, CommandType.Text);
            Employee obj = new Employee();
            foreach (DataRow dr in x.Rows)
            {
                obj.CompanyCodeLength = Convert.ToInt32(dr["COMPANY_EMP_CODE_LENGTH"]);
            }
            return obj;
        }
    }
}
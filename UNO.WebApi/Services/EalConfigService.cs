using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using UNO.DAL;
using UNO.WebApi.Models;
using UNO.WebApi.Dto;

namespace UNO.WebApi.Services
{
    public interface IEalConfigService
    {
        Task<int> Create(EalConfig entity, string ipaddress, int activeuser);
        Task<int> DeleteByEntityIdAndAlids(int id, string alids, string user, string ipaddress, int activeuser);
        Task<int> DeleteByAlidAndControllerIds(int alid, string controllerIds, string user, string ipaddress, int activeuser);
        Task<int> DeleteByAlidsAndControllerIds(string alids, string controllerIds, string user, string ipaddress, int activeuser);
        Task<int> AddByAlidAndControllerIds(int alid, string controllerIds, string user, string ipaddress, int activeuser);
        Task<int> AddByAlidsAndControllerIds(string alids, string controllerIds, string user, string ipaddress, int activeuser);
        Task<string> GetFlagValue(int controller);
        IQueryable<EalConfigDto> GetByEntityId(int id);
        Task<bool> checkEntityAccessPermissionAdd(EmployeeAccess employeeAccess);
        Task<bool> checkEmployee(int id, int employee);
    }
    public class EalConfigService : IEalConfigService
    {
        private UnitOfWork _unitOfWork;
        private DatabaseHelper _DatabaseHelper;
        private ICommonMasterService _entCommonService;
        private IUserService _userService;
        public EalConfigService(IUnitOfWork unitOfWork
             , ICommonMasterService entCommonService
            , DatabaseHelper databaseHelper
            , IUserService userService)
        {
            _unitOfWork = (UnitOfWork)unitOfWork;
            _DatabaseHelper = databaseHelper;
            _entCommonService = entCommonService;
            _userService = userService;
        }
        public async Task<int> Create(EalConfig entity, string ipaddress, int activeuser)
        {
            string query = " Insert into eal_config (ENTITY_TYPE,ENTITY_ID,EMPLOYEE_CODE,CARD_CODE" +
                            ",AL_ID,FLAG,ISDELETED,CONTROLLER_ID,TemplateFlag) " +
                           " Values ('" + entity.ENTITY_TYPE + "','" + entity.ENTITY_ID + "','" + entity.EMPLOYEE_CODE + "', " +
                           " '" + entity.CARD_CODE + "' ,'" + entity.AL_ID + "',0,0," +
                           " '" + entity.CONTROLLER_ID + "','1') ";
            query += " select @@Identity ;";
            return await _DatabaseHelper.Insert(query, CommandType.Text,ipaddress,activeuser);
        }


        public async Task<string> GetFlagValue(int controller)
        {
            string query = " select case " +
                           " when exists(select 1 From Eal_config where CONTROLLER_ID=" + controller + " and ISDELETED=0) " +
                           " then 0 " +
                           " else " +
                               " case " +
                               " when exists(select 1 from acs_controller where CTLR_ID=" + controller + " and CTLR_TYPE='BIOEDGE+') " +
                               " then 0 " +
                               " else 3 " +
                               " end " +
                           " end ";
            var x = await _DatabaseHelper.GetScalarValue(query, CommandType.Text);
            return Convert.ToString(x);
        }

        public async Task<bool> checkEntityAccessPermissionAdd(EmployeeAccess employeeAccess)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" select EMPLOYEE_CODE,LevelCount ");
            sb.Append(" from (");
            sb.Append(" select EMPLOYEE_CODE,count(*) LevelCount ");
            sb.Append(" from ( ");
            sb.Append(" select AL_ID,EMPLOYEE_CODE ,count(*)  cnt ");
            sb.Append(" from EAL_CONFIG ");
            sb.Append(" where EMPLOYEE_CODE in ");




            string strList = string.Empty;
            foreach (var item in employeeAccess.EntityIds)
            {
                strList += item + ",";
            }
            strList = strList.Substring(0, (strList.Length - 1));

            string strAlList = string.Empty;
            foreach (var item in employeeAccess.AL_IDs)
            {
                strAlList += item + ",";
            }
            strAlList = strAlList.Substring(0, (strAlList.Length - 1));


            if (employeeAccess.COMMON_TYPES_ID == await _entCommonService.GetCommonIdByName("Employee"))
            {
                sb.Append("(" + strList.Trim() + ") ");
            }
            else if (employeeAccess.COMMON_TYPES_ID == await _entCommonService.GetCommonIdByName("Category"))
            {
                sb.Append("( select EOD_EMPID from ENT_EMPLOYEE_DTLS where EOD_CATEGORY_ID in(" + strList.Trim() + ") and  EOD_ISDELETED='0' ) ");
            }
            else if (employeeAccess.COMMON_TYPES_ID == await _entCommonService.GetCommonIdByName("Department"))
            {
                sb.Append("( select EOD_EMPID from ENT_EMPLOYEE_DTLS where EOD_DEPARTMENT_ID in(" + strList.Trim() + ") and  EOD_ISDELETED='0' ) ");
            }
            else if (employeeAccess.COMMON_TYPES_ID == await _entCommonService.GetCommonIdByName("Designation"))
            {
                sb.Append("( select EOD_EMPID from ENT_EMPLOYEE_DTLS where EOD_DESIGNATION_ID in(" + strList.Trim() + ") and  EOD_ISDELETED='0' ) ");
            }
            else if (employeeAccess.COMMON_TYPES_ID == await _entCommonService.GetCommonIdByName("Division"))
            {
                sb.Append("( select EOD_EMPID from ENT_EMPLOYEE_DTLS where EOD_DIVISION_ID in(" + strList.Trim() + ") and  EOD_ISDELETED='0' ) ");
            }
            else if (employeeAccess.COMMON_TYPES_ID == await _entCommonService.GetCommonIdByName("Grade"))
            {
                sb.Append("( select EOD_EMPID from ENT_EMPLOYEE_DTLS where EOD_GRADE_ID in(" + strList.Trim() + ") and  EOD_ISDELETED='0' ) ");
            }
            else if (employeeAccess.COMMON_TYPES_ID == await _entCommonService.GetCommonIdByName("Group"))
            {
                sb.Append("( select EOD_EMPID from ENT_EMPLOYEE_DTLS where EOD_GROUP_ID in(" + strList.Trim() + ") and  EOD_ISDELETED='0' ) ");
            }
            else if (employeeAccess.COMMON_TYPES_ID == await _entCommonService.GetCommonIdByName("Location"))
            {
                sb.Append("( select EOD_EMPID from ENT_EMPLOYEE_DTLS where EOD_LOCATION_ID in(" + strList.Trim() + ") and  EOD_ISDELETED='0' ) ");
            }




            sb.Append(" and AL_ID not in( " + strAlList + " ) and entity_type<>'" + employeeAccess.COMMON_TYPES_ID + "' and ISDELETED='0' and FLAG<>'3' ");
            sb.Append(" group by AL_ID,EMPLOYEE_CODE ");
            sb.Append(" ) Sel ");
            sb.Append(" group by Sel.EMPLOYEE_CODE ");
            sb.Append(" )Wel ");
            sb.Append(" where Wel.LevelCount + " + (employeeAccess.EntityIds.Count) + ">4 ");
            var str = sb.ToString();
            var dataTable = _DatabaseHelper.GetDataTable(sb.ToString(), CommandType.Text);
            if (dataTable.Rows.Count > 0)
            {
                return false;
            }
            return true;
        }


        public IQueryable<EalConfigDto> GetByEntityId(int id)
        {
            var commonTypesName = _entCommonService.GetCommonWithEmployee().Where(x => x.COMMON_TYPES_ID == id).Select(x => x.COMMON_NAME).FirstOrDefault();
            string query = string.Empty;
            if (commonTypesName == "EMPLOYEE")
            {
                query = " select * from (select * " +
                        " ,row_number() over(partition by DES,ENTITY_TYPE,AL_ID,AL_DESCRIPTION order by DES,ENTITY_TYPE,AL_ID,AL_DESCRIPTION) as rn " +
                        " from ( " +
                        " select distinct E.EAL_ID as ID,EP.EPD_FIRST_NAME +' ' + EP.EPD_LAST_NAME as DES,orgt.COMMON_TYPES " +
                        " as ENTITY_TYPE,E.AL_ID,A.AL_DESCRIPTION " +
                        " from EAL_CONFIG E ,ACS_ACCESSLEVEL A ,ENT_EMPLOYEE_DTLS EP, " +
                        " ENT_ORG_COMMON_TYPES orgt " +
                        " where E.AL_ID=A.AL_ID and isDELETED='0' and EP.EMPLOYEE_ID=E.EMPLOYEE_CODE and orgt.COMMON_TYPES_ID=e.ENTITY_TYPE " +
                        " group by E.EAL_ID,E.AL_ID,A.AL_DESCRIPTION,EP.EPD_FIRST_NAME +' ' + EP.EPD_LAST_NAME,E.ENTITY_TYPE,orgt.COMMON_TYPES " +
                        " )a)b where b.rn=1 ";
            }
            else
            {
                query = " select distinct E.EAL_ID as ID,CE.OCE_DESCRIPTION as DES,orgt.COMMON_TYPES  as ENTITY_TYPE,E.AL_ID,A.AL_DESCRIPTION " +
                        " from EAL_CONFIG E,ACS_ACCESSLEVEL A ,ENT_ORG_COMMON_ENTITIES CE, " +
                        " ENT_ORG_COMMON_TYPES orgt " +
                        " where E.ENTITY_TYPE='" + id + "' and E.AL_ID=A.AL_ID and isDELETED='0' " +
                        " and CE.COMMON_TYPES_ID='" + id + "' and CE.ID=E.ENTITY_ID  and orgt.COMMON_TYPES_ID=e.ENTITY_TYPE " +
                        " group by E.EAL_ID,CE.OCE_DESCRIPTION,E.AL_ID ,A.AL_DESCRIPTION,E.ENTITY_TYPE ,orgt.COMMON_TYPES ";
            }

            var dataTable = _DatabaseHelper.GetDataTable(query, CommandType.Text);
            List<EalConfigDto> ealConfigDtos = new List<EalConfigDto>();

            if (dataTable.Rows.Count > 0)
            {
                foreach (DataRow dr in dataTable.Rows)
                {
                    EalConfigDto ealConfigDto = new EalConfigDto();

                    ealConfigDto.Al_from = Convert.ToString(dr["ENTITY_TYPE"]);
                    ealConfigDto.ENTITY_ID = Convert.ToString(dr["ID"]);
                    ealConfigDto.Entity_Name = Convert.ToString(dr["DES"]);
                    ealConfigDto.Level_Description = Convert.ToString(dr["AL_DESCRIPTION"]);
                    ealConfigDtos.Add(ealConfigDto);
                }
            }

            return ealConfigDtos.AsQueryable();
        }


        public async Task<bool> checkEmployee(int id, int employee)
        {
            string query = string.Empty;
            User user = _userService.GetSingleUser(id);
            if (user.USER_CODE == "superuser")
            {
                query = " select count(distinct(Al_id)) from EAL_CONFIG eal where ISDELETED = 0 and  Employee_code= " + employee;
            }
            else
            {
                query = " select count(distinct(Al_id)) from EAL_CONFIG eal join acs_controller acs on eal.CONTROLLER_ID =acs.CTLR_ID and COMPANY_ID in( " +
                        CustomQueryies.Queries.RoleDataAccessQuery(id) + " ) where ISDELETED = 0 and Employee_code= " + employee + "";
            }

            return await _DatabaseHelper.GetScalarValue(query, CommandType.Text) > 3 ? true : false;
        }


        public async Task<int> DeleteByEntityIdAndAlids(int id, string alids, string user, string ipaddress, int activeuser)
        {
            string query = " update eal_config set FLAG =2, ISDELETED=1 " +
                           " where ENTITY_ID in (select ENTITY_ID from eal_config where eal_id=" + id + ") " +
                           " and al_id in (" + alids + ")";

            return await _DatabaseHelper.Insert(query, CommandType.Text, ipaddress, activeuser);
        }


        public async Task<int> DeleteByAlidAndControllerIds(int alid, string controllerIds, string user, string ipaddress, int activeuser)
        {
            string query = " update eal_config set FLAG =2, ISDELETED=1 " +
                          " where 1=1 " +
                          " and al_id = '" + alid + "' and CONTROLLER_ID in (" + controllerIds + ")";

            return await _DatabaseHelper.Insert(query, CommandType.Text, ipaddress, activeuser);
        }


        public async Task<int> AddByAlidAndControllerIds(int alid, string controllerIds, string user, string ipaddress, int activeuser)
        {
            string[] ctrlids = null;
            if (controllerIds.Length > 0)
            {
                ctrlids = controllerIds.Split(',');
            }
            var _ctrlids = ctrlids.Distinct();
            StringBuilder sb = new StringBuilder();

            insertQueryIntoEalConfig(_ctrlids, sb, alid.ToString());

            return await _DatabaseHelper.Insert(sb.ToString(), CommandType.Text, ipaddress, activeuser);
        }



        public async Task<int> DeleteByAlidsAndControllerIds(string alids, string controllerIds, string user, string ipaddress, int activeuser)
        {
            string query = " update eal_config set FLAG =2, ISDELETED=1 " +
                         " where 1=1 " +
                         " and al_id in (" + alids + ") and CONTROLLER_ID in (" + controllerIds + ")";

            return await _DatabaseHelper.Insert(query, CommandType.Text, ipaddress, activeuser);
        }

        public async Task<int> AddByAlidsAndControllerIds(string alids, string controllerIds, string user, string ipaddress, int activeuser)
        {
            string[] ctrlids = null;
            if (controllerIds.Length > 0)
            {
                ctrlids = controllerIds.Split(',');
            }
            var _ctrlids = ctrlids.Distinct();


            string[] al_ids = null;
            if (alids.Length > 0)
            {
                al_ids = alids.Split(',');
            }
            var _alids = al_ids.Distinct();


            StringBuilder sb = new StringBuilder();
            foreach (var alid in _alids)
            {
                insertQueryIntoEalConfig(_ctrlids, sb, alid);
            }

            return await _DatabaseHelper.Insert(sb.ToString(), CommandType.Text, ipaddress, activeuser);
        }

        private static void insertQueryIntoEalConfig(IEnumerable<string> _ctrlids, StringBuilder sb, string alid)
        {
            foreach (var ctrlid in _ctrlids)
            {
                sb.Append(" insert into eal_config ");
                sb.Append(" (ENTITY_TYPE,ENTITY_ID,EMPLOYEE_CODE,CARD_CODE,AL_ID,FLAG,ISDELETED,DELETEDDATE,CONTROLLER_ID,TemplateFlag) ");
                sb.Append(" select distinct ENTITY_TYPE,ENTITY_ID,EMPLOYEE_CODE,CARD_CODE," + alid + ",0,0,null," + ctrlid + ",1  ");
                sb.Append(" from eal_config where al_id=" + alid + " and isdeleted=0 ");
                sb.Append(" ; ");
            }
        }
    }
}
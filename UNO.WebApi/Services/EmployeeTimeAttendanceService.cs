using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using UNO.DAL;
using UNO.WebApi.Models;

namespace UNO.WebApi.Services
{
    public interface IEmployeeTimeAttendanceService
    {
        List<EmployeeTimeAttendance> GetEmpShiftDetails(int id);

        EmployeeTimeAttendance GetemployeeShift(int id);

        Task<int> Create(EmployeeTimeAttendance entity, string ipaddress, int activeuser);

        Task<int> Edit(EmployeeTimeAttendance entity, string ipaddress, int activeuser);

        Task<int> Delete(int id, string ipaddress, int activeuser);
    }


    public class EmployeeTimeAttendanceService : IEmployeeTimeAttendanceService
    {
        private UnitOfWork _unitOfWork;
        private DatabaseHelper _DatabaseHelper;
        private IUserService _userService;


        public EmployeeTimeAttendanceService(IUnitOfWork unitOfWork, DatabaseHelper databaseHelper
            , IUserService userService)
        {
            _unitOfWork = (UnitOfWork)unitOfWork;
            _DatabaseHelper = databaseHelper;
            _userService = userService;
        }



        public List<EmployeeTimeAttendance> GetEmpShiftDetails(int id)
        {
            string query = string.Empty;
            User user = _userService.GetSingleUser(id);
            if (user.USER_CODE == "superuser")
            {
               query = "select TNA.ETC_EMP_ID,TNA.ETC_MINIMUM_SWIPE, " +
                        "SFT.SHIFT_PATTERN_ID, SFT.SHIFT_PATTERN_CODE + ' - ' + SFT.SHIFT_PATTERN_DESCRIPTION AS ETC_SHIFTCODE, " +
                        "wkend.MWK_DESC as ETC_WEEKEND " +
                        ",wkoff.MWK_DESC as ETC_WEEKOFF " +
                        ",TNA.ETC_SHIFT_START_DATE " +
                        "from TNA_EMPLOYEE_TA_CONFIG as TNA " +
                        "inner join TA_SHIFT_PATTERN SFT on TNA.ETC_SHIFTCODE=SFT.SHIFT_PATTERN_ID and ETC_ISDELETED=0 " +
                        "inner join (select * from TA_WKLYOFF  where MWK_OFF=0 and IsDeleted=0) wkoff on TNA.ETC_WEEKOFF=wkoff.MWK_CD and TNA.ETC_ISDELETED=0 " +
                        "inner join (select * from TA_WKLYOFF  where MWK_OFF=1 and IsDeleted=0) wkend " +
                        "on TNA.ETC_WEEKEND=wkend.MWK_CD and TNA.ETC_ISDELETED=0 ";
            }
            else
            {
                query = "select TNA.ETC_EMP_ID,TNA.ETC_MINIMUM_SWIPE, " +
                        "SFT.SHIFT_PATTERN_ID, SFT.SHIFT_PATTERN_CODE + ' - ' + SFT.SHIFT_PATTERN_DESCRIPTION AS ETC_SHIFTCODE, " +
                        "wkend.MWK_DESC as ETC_WEEKEND " +
                        ",wkoff.MWK_DESC as ETC_WEEKOFF " +
                        ",TNA.ETC_SHIFT_START_DATE " +
                        "from TNA_EMPLOYEE_TA_CONFIG as TNA " +
                        "inner join TA_SHIFT_PATTERN SFT on TNA.ETC_SHIFTCODE=SFT.SHIFT_PATTERN_ID and ETC_ISDELETED=0 " +
                        "inner join (select * from TA_WKLYOFF  where MWK_OFF=0 and IsDeleted=0) wkoff on TNA.ETC_WEEKOFF=wkoff.MWK_CD and TNA.ETC_ISDELETED=0 " +
                        "inner join (select * from TA_WKLYOFF  where MWK_OFF=1 and IsDeleted=0) wkend " +
                        "on TNA.ETC_WEEKEND=wkend.MWK_CD and TNA.ETC_ISDELETED=0 and SFT.COMPANY_ID in( " +
                        CustomQueryies.Queries.RoleDataAccessQuery(id) + " ) ";
            }
            
            var x = _DatabaseHelper.GetDataTable(query, CommandType.Text);
            List<EmployeeTimeAttendance> Employeeshiftlist = new List<EmployeeTimeAttendance>();

            foreach(DataRow dr in x.Rows)
            {
                EmployeeTimeAttendance empshiftss = new EmployeeTimeAttendance();
                empshiftss.ETC_EMP_ID = Convert.ToInt32(dr["ETC_EMP_ID"]);
                empshiftss.ETC_MINIMUM_SWIPE = Convert.ToString(dr["ETC_MINIMUM_SWIPE"]);
                empshiftss.ETC_SHIFTCODE = Convert.ToString(dr["ETC_SHIFTCODE"]);
                empshiftss.ETC_WEEKEND = Convert.ToString(dr["ETC_WEEKEND"]);
                empshiftss.ETC_WEEKOFF = Convert.ToString(dr["ETC_WEEKOFF"]);
                empshiftss.ETC_SHIFT_START_DATE = Convert.ToDateTime(dr["ETC_SHIFT_START_DATE"]);
                
                
                Employeeshiftlist.Add(empshiftss);
            }
            return Employeeshiftlist;


        }

        public EmployeeTimeAttendance GetemployeeShift(int id)
        {
            string query = "select ETC_EMP_ID,ETC_MINIMUM_SWIPE,ETC_SHIFTCODE,ETC_WEEKEND,ETC_WEEKOFF,ETC_SHIFT_START_DATE" +
                        "  from TNA_EMPLOYEE_TA_CONFIG " +
                        " where ETC_EMP_ID="+id;

            var x = _DatabaseHelper.GetDataTable(query, CommandType.Text);

            EmployeeTimeAttendance empshiftss = new EmployeeTimeAttendance();
            foreach (DataRow dr in x.Rows)
            {
                
                empshiftss.ETC_EMP_ID = Convert.ToInt32(dr["ETC_EMP_ID"]);
                empshiftss.ETC_MINIMUM_SWIPE = Convert.ToString(dr["ETC_MINIMUM_SWIPE"]);
                empshiftss.ETC_SHIFTCODE = Convert.ToString(dr["ETC_SHIFTCODE"]);
                empshiftss.ETC_WEEKEND = Convert.ToString(dr["ETC_WEEKEND"]);
                empshiftss.ETC_WEEKOFF = Convert.ToString(dr["ETC_WEEKOFF"]);
                empshiftss.ETC_SHIFT_START_DATE = Convert.ToDateTime(dr["ETC_SHIFT_START_DATE"]);
                
                
            }
            return empshiftss;
        }

        public async Task<int> Create(EmployeeTimeAttendance entity, string ipaddress, int activeuser)
        {
            var ETC_EMP_ID = entity.ETC_EMP_ID;
            var ETC_MINIMUM_SWIPE = entity.ETC_MINIMUM_SWIPE;
            var ETC_SHIFTCODE = entity.ETC_SHIFTCODE;
            var ETC_WEEKEND = "";
            

            var ETC_WEEKOFF = entity.ETC_WEEKOFF;
            var ETC_SHIFT_START_DATE = entity.ETC_SHIFT_START_DATE;
            
            var ETC_DELETEDDATE = entity.ETC_DELETEDDATE;
            var ScheduleType = entity.ScheduleType;
            var ShiftType = entity.ShiftType;
            string query = "";
            if (entity.ETC_WEEKEND != null)
            {
                ETC_WEEKEND = entity.ETC_WEEKEND;
                query = "insert into TNA_EMPLOYEE_TA_CONFIG values(" + ETC_EMP_ID + "," + ETC_MINIMUM_SWIPE + "," +
                ETC_SHIFTCODE + "," + ETC_WEEKEND + "," + ETC_WEEKOFF + ",'" + ETC_SHIFT_START_DATE + "',0,null,null,null,0)";
            }
            else
            {
                query = "insert into TNA_EMPLOYEE_TA_CONFIG values(" + ETC_EMP_ID + "," + ETC_MINIMUM_SWIPE + "," +
                ETC_SHIFTCODE + ",null," + ETC_WEEKOFF + ",'" + ETC_SHIFT_START_DATE + "',0,null,null,null,0)";
            }

           





            return await _DatabaseHelper.Insert(query, CommandType.Text,ipaddress,activeuser);
        }

        public async Task<int> Edit(EmployeeTimeAttendance entity, string ipaddress, int activeuser)
        {
            string query = "update TNA_EMPLOYEE_TA_CONFIG set ETC_WEEKEND=" + entity.ETC_WEEKEND + ", ETC_WEEKOFF=" + entity.ETC_WEEKOFF + ",ETC_MINIMUM_SWIPE=" + entity.ETC_MINIMUM_SWIPE + ",ETC_SHIFTCODE=" + entity.ETC_SHIFTCODE + " where ETC_EMP_ID=" + entity.ETC_EMP_ID;

            return await _DatabaseHelper.Insert(query, CommandType.Text, ipaddress, activeuser);
        }

        public async Task<int> Delete(int id, string ipaddress, int activeuser)
        {
            var isdeleteddate = DateTime.Now;

            string query = "update TNA_EMPLOYEE_TA_CONFIG set ETC_ISDELETED=1 ,ETC_DELETEDDATE = '" + isdeleteddate.ToString("dd/MMM/yyyy hh:mm:ss") + "' where ETC_EMP_ID = " + id;

            return await _DatabaseHelper.Insert(query, CommandType.Text, ipaddress, activeuser);
        }
    }
}
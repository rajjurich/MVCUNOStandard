using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using UNO.DAL;
using UNO.WebApi.Models;
namespace UNO.WebApi.Services
{
    public interface ILeaveCodeService
    {
        List<LeaveCode> Get();
        List<LeaveCode> Get(int id);

    }
    public class LeaveCodeService: ILeaveCodeService
    {
         private UnitOfWork _unitOfWork;
        private DatabaseHelper _DatabaseHelper;

        public LeaveCodeService(IUnitOfWork unitOfWork, DatabaseHelper databaseHelper)
        {
            _unitOfWork = (UnitOfWork)unitOfWork;
            _DatabaseHelper = databaseHelper;
        }


        public List<LeaveCode> Get()
        {

            string query = "SELECT Leave_CODE,replace(convert(char(22),ltrim(Leave_Description))+Leave_CODE,' ',' ' )  as  Leave_Description FROM TA_LEAVE_FILE where leave_ISDELETED='0'";

            var x = _DatabaseHelper.GetDataTable(query, CommandType.Text);
            List<LeaveCode> entLeaveCodeList = new List<LeaveCode>();

            foreach (DataRow dr in x.Rows)
            {
                LeaveCode entLeaveCode = new LeaveCode();
                entLeaveCode.Leave_ID = Convert.ToString(dr["Leave_CODE"]);
                entLeaveCode.Leave_Description = Convert.ToString(dr["Leave_Description"]);

                entLeaveCodeList.Add(entLeaveCode);
            }
            return entLeaveCodeList;
        }

        public List<LeaveCode> Get(int id)
        {

            string query = "SELECT Leave_CODE,replace(convert(char(22),ltrim(Leave_Description))+Leave_CODE,' ',' ' )  as  Leave_Description FROM TA_LEAVE_FILE where leave_ISDELETED='0' and COMPANY_ID='" + id + "'";

            var x = _DatabaseHelper.GetDataTable(query, CommandType.Text);
            List<LeaveCode> entLeaveCodeList = new List<LeaveCode>();

            foreach (DataRow dr in x.Rows)
            {
                LeaveCode entLeaveCode = new LeaveCode();
                entLeaveCode.Leave_ID = Convert.ToString(dr["Leave_CODE"]);
                entLeaveCode.Leave_Description = Convert.ToString(dr["Leave_Description"]);

                entLeaveCodeList.Add(entLeaveCode);
            }
            return entLeaveCodeList;
        }
    }
}
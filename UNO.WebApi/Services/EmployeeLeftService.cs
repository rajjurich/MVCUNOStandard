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
    public interface IEmployeeLeftService
    {
        Task<int> Create(EmployeeLeft entity, string ipaddress, int activeuser);
        Task<int> Edit(EmployeeLeft entity, string ipaddress, int activeuser);
        Task<int> Delete(int id, string ipaddress, int activeuser);
         IQueryable<EmployeeLeft> GetEmployeeLeft();
         EmployeeLeft GetEmployeeLeft(int id);
        string Get(int id);
    }

    public class EmployeeLeftService: IEmployeeLeftService
    {
        private UnitOfWork _unitOfWork;
        private DatabaseHelper _DatabaseHelper;
        public EmployeeLeftService(IUnitOfWork unitOfWork, DatabaseHelper databaseHelper)
        {
            _unitOfWork = (UnitOfWork)unitOfWork;
            _DatabaseHelper = databaseHelper;
        }
        public async Task<int> Create(EmployeeLeft entity, string ipaddress, int activeuser)
        {
            string query = " insert into ENT_EMPLOYEE_LEFT(EL_EMP_ID,EL_LEFT_DATE,EL_REASONID,EL_ISDELETED,EL_CREATEDDATE,EL_CREATEDBY)  ";
            query += " values (" + entity.EL_EMP_ID + ", '" + entity.EL_LEFT_DATE.ToString("dd/MMM/yyyy hh:mm") + "'," + entity.EL_REASONID + ",0,'" + DateTime.Now.ToString("dd/MMM/yyyy hh:mm") + "','admin')";
            query += " select @@Identity ;";
            return await _DatabaseHelper.Insert(query, CommandType.Text,ipaddress,activeuser);
        }

        public async Task<int> Edit(EmployeeLeft entity, string ipaddress, int activeuser)
        {
            string query = "Update ENT_EMPLOYEE_LEFT set EL_LEFT_DATE = '" + entity.EL_LEFT_DATE.ToString("dd/MMM/yyyy hh:mm") + "', EL_REASONID = " + entity.EL_REASONID + ",EL_MODIFIEDDATE ='" + DateTime.Now.ToString("dd/MMM/yyyy hh:mm") + "', EL_MODIFIEDBY ='admin'  where EL_ColumnID =  " + entity.EL_COLUMNID;
            return await _DatabaseHelper.Insert(query, CommandType.Text, ipaddress, activeuser);
        }
        public async Task<int> Delete(int id, string ipaddress, int activeuser)
        {
            string query = "Update ENT_EMPLOYEE_LEFT set EL_ISDELETED = 1 , EL_DELETEDDATE ='" + DateTime.Now.ToString("dd/MMM/yyyy hh:mm") + "', EL_DELETEDBY ='admin' where EL_ColumnID = " + id;
            return await _DatabaseHelper.Insert(query, CommandType.Text, ipaddress, activeuser);
        }
        public  IQueryable<EmployeeLeft> GetEmployeeLeft()
        {
            string query = "Select EEL.EL_ColumnID,EEL.EL_EMP_ID,EEL.EL_LEFT_DATE,EEL.EL_ISDELETED,EEL.EL_DELETEDDATE,EEL.EL_REASONID,EED.EOD_EMPID,ER.REASON_DESC  from ENT_EMPLOYEE_LEFT EEL inner join ENT_EMPLOYEE_DTLS  EED on  EEL.EL_EMP_ID=EED.EMPLOYEE_ID inner join ENT_REASON ER on EEL.EL_REASONID=ER.REASON_ID where EEL.EL_DELETEDDATE IS   NULL AND EEL.EL_DELETEDBY IS NULL  ";
            
            var x = _DatabaseHelper.GetDataTable(query, CommandType.Text);
            List<EmployeeLeft> entParamsList = new List<EmployeeLeft>();

            foreach (DataRow dr in x.Rows)
            {
                EmployeeLeft enEmployeeLeft = new EmployeeLeft();
                enEmployeeLeft.EL_COLUMNID = Convert.ToInt32(dr["EL_ColumnID"]);
                enEmployeeLeft.EL_EMP_ID = Convert.ToInt32(dr["EL_EMP_ID"]);
                enEmployeeLeft.EL_ISDELETED = Convert.ToInt32(dr["EL_ISDELETED"]);
                enEmployeeLeft.EL_LEFT_DATE = Convert.ToDateTime(dr["EL_LEFT_DATE"]);
                enEmployeeLeft.EL_REASONID = Convert.ToInt32(dr["EL_REASONID"]);
                enEmployeeLeft.EOD_EMPID = Convert.ToString(dr["EOD_EMPID"]);
                enEmployeeLeft.REASON_DESC = Convert.ToString(dr["REASON_DESC"]);
                entParamsList.Add(enEmployeeLeft);
            }
            return entParamsList.AsQueryable();
        }

        public EmployeeLeft GetEmployeeLeft(int id)
        {
           EmployeeLeft entParamsList = new EmployeeLeft();
            string query = "Select EEL.EL_ColumnID,EEL.EL_EMP_ID,EEL.EL_LEFT_DATE,EEL.EL_ISDELETED,EEL.EL_DELETEDDATE,EEL.EL_REASONID,EED.EOD_EMPID,ER.REASON_DESC  from ENT_EMPLOYEE_LEFT EEL inner join ENT_EMPLOYEE_DTLS  EED on EEL.EL_EMP_ID=EED.EMPLOYEE_ID inner join ENT_REASON ER on EEL.EL_REASONID=ER.REASON_ID  where EEL.EL_DELETEDDATE IS NULL AND EEL.EL_DELETEDBY IS NULL AND EEL.EL_ColumnID =" + id;

            var x = _DatabaseHelper.GetDataTable(query, CommandType.Text);

            foreach (DataRow dr in x.Rows)
            {
               
                entParamsList.EL_COLUMNID = Convert.ToInt32(dr["EL_ColumnID"]);
                entParamsList.EL_EMP_ID = Convert.ToInt32(dr["EL_EMP_ID"]);
                entParamsList.EL_ISDELETED = Convert.ToInt32(dr["EL_ISDELETED"]);
                entParamsList.EL_LEFT_DATE = Convert.ToDateTime(dr["EL_LEFT_DATE"]);
                entParamsList.EL_REASONID = Convert.ToInt32(dr["EL_REASONID"]);
                entParamsList.EOD_EMPID = Convert.ToString(dr["EOD_EMPID"]);
                entParamsList.REASON_DESC = Convert.ToString(dr["REASON_DESC"]);
            }
            return entParamsList;
        }

        public string Get(int id)
        {
            string query = "select CONVERT(varchar(11),eod_joining_date,105) as eod_joining_date from ENT_EMPLOYEE_DTLS where EMPLOYEE_ID =" + id;

            var x = _DatabaseHelper.GetDataTable(query, CommandType.Text);
            string EmpJoiningDate = "";

            foreach (DataRow dr in x.Rows)
            {
                EmpJoiningDate = Convert.ToString(dr["eod_joining_date"]);
            }
            return EmpJoiningDate;
        }
    }
}
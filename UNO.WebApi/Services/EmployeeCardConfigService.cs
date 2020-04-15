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
    public interface IEmployeeCardConfigService
    {
        List<EmployeeCardConfig> GetEmpCardConfigDetails();

        EmployeeCardConfig Getemployeecard(int id);

        Task Create(EmployeeCardConfig entity, string ipaddress, int activeuser);

        Task<int> Edit(EmployeeCardConfig entity, string ipaddress, int activeuser);

        Task<int> Delete(int id, string ipaddress, int activeuser);

    }


    public class EmployeeCardConfigService : IEmployeeCardConfigService
    {
        private UnitOfWork _unitOfWork;
        private DatabaseHelper _DatabaseHelper;

        public EmployeeCardConfigService(IUnitOfWork unitOfWork, DatabaseHelper databaseHelper)
        {
            _unitOfWork = (UnitOfWork)unitOfWork;
            _DatabaseHelper = databaseHelper;
        }
        public List<EmployeeCardConfig> GetEmpCardConfigDetails()
        {
            string query = "select emp.EMPLOYEE_ID as EMP_ID ,eod.EPD_CARD_ID as CARD_CODE ,PIN,usecount AS USE_COUNT,IGNORE_APB, "+
                            "STATUS,ACTIVATION_DATE,EXPIRY_DATE "+ 
                            "FROM  ENT_EMPLOYEE_PERSONAL_DTLS emp "+
                            "inner join  ACS_CARD_CONFIG on(emp.EMPLOYEE_ID=ACS_CARD_CONFIG.cc_emp_id) "+
                            "inner join ENT_EMPLOYEE_DTLS eod on emp.EMPLOYEE_ID=eod.EMPLOYEE_ID "+
                            "where ACE_isdeleted='false' and eod.EOD_STATUS='1'";

            var x = _DatabaseHelper.GetDataTable(query, CommandType.Text);
            List<EmployeeCardConfig> entEmployeeCardConfigList = new List<EmployeeCardConfig>();

            foreach (DataRow dr in x.Rows)
            {
                EmployeeCardConfig entEmployeeCardConfig = new EmployeeCardConfig();
                entEmployeeCardConfig.CC_EMP_ID = Convert.ToInt32(dr["EMP_ID"]);
                entEmployeeCardConfig.CARD_CODE = Convert.ToString(dr["CARD_CODE"]);
                entEmployeeCardConfig.PIN = Convert.ToString(dr["PIN"]);
                entEmployeeCardConfig.USE_COUNT = Convert.ToInt32(dr["USE_COUNT"]);
                entEmployeeCardConfig.IGNORE_APB = Convert.ToBoolean(dr["IGNORE_APB"]);
                entEmployeeCardConfig.STATUS = Convert.ToBoolean(dr["STATUS"]);
                entEmployeeCardConfig.ACTIVATION_DATE = Convert.ToDateTime(dr["ACTIVATION_DATE"]);
                entEmployeeCardConfig.EXPIRY_DATE = Convert.ToDateTime(dr["EXPIRY_DATE"]);
                entEmployeeCardConfigList.Add(entEmployeeCardConfig);
            }
            return entEmployeeCardConfigList;

        }

        public EmployeeCardConfig Getemployeecard(int id)
        {
            string query = "select CC_EMP_ID,ACTIVATION_DATE,EXPIRY_DATE,USECOUNT,STATUS from ACS_CARD_CONFIG where CC_EMP_ID=" + id;
            var d = _DatabaseHelper.GetDataTable(query, CommandType.Text);
            EmployeeCardConfig entparameter = new EmployeeCardConfig();

            foreach(DataRow dr in d.Rows)
            {
                entparameter.CC_EMP_ID = Convert.ToInt32(dr["CC_EMP_ID"]);
                entparameter.ACTIVATION_DATE = Convert.ToDateTime(dr["ACTIVATION_DATE"]);
                entparameter.EXPIRY_DATE = Convert.ToDateTime(dr["EXPIRY_DATE"]);
                entparameter.USE_COUNT = Convert.ToInt32(dr["USECOUNT"]);
                entparameter.STATUS = Convert.ToBoolean(dr["STATUS"]);
            }
            return entparameter;
        }


        public async Task Create(EmployeeCardConfig entity, string ipaddress, int activeuser)
        {
            var USECOUNT = entity.USE_COUNT;
            var ACTIVATION_DATE = entity.ACTIVATION_DATE;
            var EXPIRY_DATE = entity.EXPIRY_DATE;
            var CC_EMP_ID = entity.CC_EMP_ID;
            var STATUS = 1;
            var ACE_isdeleted = 0;
            var ACE_DELETEDDATE = DateTime.Now;
            var RESET_APB = 0;
            var AUTH_MODE = "null";
            var IGNORE_APB = 1;
            var remember = entity.Remember;

            string querymain="";
            if (remember == true) { 

            if (entity.ACTIVATION_DATE==null || entity.EXPIRY_DATE==null || entity.USE_COUNT == 0)
            {
                USECOUNT = 0;
                ACTIVATION_DATE = DateTime.Now;
                EXPIRY_DATE = DateTime.Now;
            }
            string query = "declare @pin int set @pin = NEXT VALUE FOR autogeneratepin   insert into ACS_CARD_CONFIG  values(" + CC_EMP_ID + ",'0',@pin,'0'," + USECOUNT + "," + IGNORE_APB + "," +
                "" + STATUS + ",'" + ACTIVATION_DATE.ToString("dd/MMM/yyyy hh:mm:ss") + "','" + EXPIRY_DATE.ToString("dd/MMM/yyyy hh:mm:ss") + "'," + ACE_isdeleted + ",null,null," +
                "" + AUTH_MODE + ")";
                querymain=query;
            }
            else
            {
                if (entity.ACTIVATION_DATE==null || entity.EXPIRY_DATE==null || entity.USE_COUNT == null)
            {
                USECOUNT = 0;
                ACTIVATION_DATE = DateTime.Now;
                EXPIRY_DATE = DateTime.Now;
            }
            string query = "insert into ACS_CARD_CONFIG  values(" + CC_EMP_ID + ",null,null,null," + USECOUNT + "," + IGNORE_APB + "," +
                "" + STATUS + ",'" + ACTIVATION_DATE.ToString("dd/MMM/yyyy hh:mm:ss") + "','" + EXPIRY_DATE.ToString("dd/MMM/yyyy hh:mm:ss") + "'," + ACE_isdeleted + ",null,null," +
                "" + AUTH_MODE + ")";
                querymain=query;
            }


            await _DatabaseHelper.Insert(querymain, CommandType.Text, ipaddress, activeuser);

        }

        public async Task<int> Edit(EmployeeCardConfig entity, string ipaddress, int activeuser)
        {
            string querymain = "";
            if (entity.STATUS == true)
            {
                string query = "update ACS_CARD_CONFIG set USECOUNT =" + entity.USE_COUNT + ",ACTIVATION_DATE ='" + entity.ACTIVATION_DATE.ToString("dd/MMM/yyyy hh:mm:ss") + "',EXPIRY_DATE ='" + entity.EXPIRY_DATE.ToString("dd/MMM/yyyy hh:mm:ss") + "',STATUS = 1  where CC_EMP_ID = " + entity.CC_EMP_ID;
                querymain = query;
            }
            else
            {
                string query = "update ACS_CARD_CONFIG set USECOUNT =" + entity.USE_COUNT + ",ACTIVATION_DATE ='" + entity.ACTIVATION_DATE.ToString("dd/MMM/yyyy hh:mm:ss") + "',EXPIRY_DATE ='" + entity.EXPIRY_DATE.ToString("dd/MMM/yyyy hh:mm:ss") + "',STATUS = 0  where CC_EMP_ID = " + entity.CC_EMP_ID;
                querymain = query;
            }
            return await _DatabaseHelper.Insert(querymain, CommandType.Text, ipaddress, activeuser);
        }


        public async Task<int> Delete(int id, string ipaddress, int activeuser)
        {
            var isdeleteddate = DateTime.Now;

            string query = "update ACS_CARD_CONFIG set ACE_isdeleted=1 ,ACE_DELETEDDATE = '" + isdeleteddate.ToString("dd/MMM/yyyy hh:mm:ss") + "' where CC_EMP_ID = " + id;

            return await _DatabaseHelper.Insert(query, CommandType.Text, ipaddress, activeuser);

        }


        
    }
}
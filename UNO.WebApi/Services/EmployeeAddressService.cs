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
    public interface IEmployeeAddressService
    {
        Task Create(EmployeeAddress entity, string ipaddress, int activeuser);
        Task Edit(EmployeeAddress entity, string ipaddress, int activeuser);
        Task Delete(int id, string ipaddress, int activeuser);
        IQueryable<EmployeeAddress> Get();
        List<EmployeeAddress> GetEmployeeAddressByEmployeeId(int id);
    }
    public class EmployeeAddressService : IEmployeeAddressService
    {
        private UnitOfWork _unitOfWork;
        private DatabaseHelper _DatabaseHelper;
        public EmployeeAddressService(IUnitOfWork unitOfWork, DatabaseHelper databaseHelper)
        {
            _unitOfWork = (UnitOfWork)unitOfWork;
            _DatabaseHelper = databaseHelper;
        }
        public async Task Create(EmployeeAddress entity, string ipaddress, int activeuser)
        {

            string query = " Insert into ENT_EMPLOYEE_ADDRESS (EMPLOYEE_ID,EAD_ADDRESS_TYPE,EAD_ADDRESS,EAD_CITY" +
                           ",EAD_PIN,EAD_STATE,EAD_COUNTRY,EAD_PHONE_ONE,EAD_PHONE_TWO,EAD_ISDELETED,IS_SYNC) " +
                           " values (" + entity.EMPLOYEE_ID + ",'" + entity.EAD_ADDRESS_TYPE + "','" + entity.EAD_ADDRESS + "','" + entity.EAD_CITY + "'," +
                           " '" + entity.EAD_PIN + "','" + entity.EAD_STATE + "', '" + entity.EAD_COUNTRY + "','" + entity.EAD_PHONE_ONE + "'" +
                           " ,'" + entity.EAD_PHONE_TWO + "',0,0)";

            await _DatabaseHelper.Insert(query, CommandType.Text, ipaddress, activeuser);
        }


        public List<EmployeeAddress> GetEmployeeAddressByEmployeeId(int id)
        {
            string query = "select * from ENT_EMPLOYEE_ADDRESS where EMPLOYEE_ID ='" + id + "'";
            List<EmployeeAddress> employeeAddresses = new List<EmployeeAddress>();
            var dataTable = _DatabaseHelper.GetDataTable(query, CommandType.Text);
            if (dataTable.Rows.Count > 0)
            {
                foreach (DataRow dr in dataTable.Rows)
                {
                    EmployeeAddress employeeAddress = new EmployeeAddress();
                    employeeAddress.EMPLOYEE_ADDRESS = Convert.ToInt32(dr["EMPLOYEE_ADDRESS"]);
                    employeeAddress.EMPLOYEE_ID = Convert.ToInt32(dr["EMPLOYEE_ID"]);
                    employeeAddress.EAD_ADDRESS_TYPE = Convert.ToString(dr["EAD_ADDRESS_TYPE"]);
                    employeeAddress.EAD_ADDRESS = Convert.ToString(dr["EAD_ADDRESS"]);
                    employeeAddress.EAD_CITY = Convert.ToString(dr["EAD_CITY"]);
                    employeeAddress.EAD_PIN = Convert.ToString(dr["EAD_PIN"]);
                    employeeAddress.EAD_STATE = Convert.ToString(dr["EAD_STATE"]);
                    employeeAddress.EAD_COUNTRY = Convert.ToString(dr["EAD_COUNTRY"]);
                    employeeAddress.EAD_PHONE_ONE = Convert.ToString(dr["EAD_PHONE_ONE"]);
                    employeeAddress.EAD_PHONE_TWO = Convert.ToString(dr["EAD_PHONE_TWO"]);
                    employeeAddresses.Add(employeeAddress);
                }

            }
            return employeeAddresses;
        }


        public async Task Edit(EmployeeAddress entity, string ipaddress, int activeuser)
        {
            string query = " update ENT_EMPLOYEE_ADDRESS set EMPLOYEE_ID = '" + entity.EMPLOYEE_ID + "'" +
                           " ,EAD_ADDRESS_TYPE='" + entity.EAD_ADDRESS_TYPE + "',EAD_ADDRESS='" + entity.EAD_ADDRESS + "' " +
                           " ,EAD_CITY='" + entity.EAD_CITY + "',EAD_PIN='" + entity.EAD_PIN + "' " +
                           " ,EAD_STATE='" + entity.EAD_STATE + "',EAD_COUNTRY='" + entity.EAD_COUNTRY + "' " +
                           " ,EAD_PHONE_ONE='" + entity.EAD_PHONE_ONE + "',EAD_PHONE_TWO='" + entity.EAD_PHONE_TWO + "' " +
                           " where EMPLOYEE_ADDRESS = " + entity.EMPLOYEE_ADDRESS + "";

            await _DatabaseHelper.Insert(query, CommandType.Text, ipaddress, activeuser);
        }


        public async Task Delete(int id, string ipaddress, int activeuser)
        {
            string query = " update ENT_EMPLOYEE_ADDRESS set EAD_ISDELETED = 1,EAD_DELETEDDATE='" + DateTime.Now.ToString("dd/MMM/yyyy hh:mm:ss") + "' " +

                            " where  EMPLOYEE_ADDRESS = " + id + "";

            await _DatabaseHelper.Insert(query, CommandType.Text, ipaddress, activeuser);
        }


        public IQueryable<EmployeeAddress> Get()
        {
            string query = "select RowID,READER_DESCRIPTION from acs_employeeAddress where READER_ISDELETED=0";
            var dataTable = _DatabaseHelper.GetDataTable(query, CommandType.Text);
            List<EmployeeAddress> employeeAddresses = new List<EmployeeAddress>();

            if (dataTable.Rows.Count > 0)
            {
                foreach (DataRow dr in dataTable.Rows)
                {
                    EmployeeAddress employeeAddress = new EmployeeAddress();

                    employeeAddress.EMPLOYEE_ADDRESS = Convert.ToInt32(dr["EMPLOYEE_ADDRESS"]);
                    employeeAddress.EMPLOYEE_ID = Convert.ToInt32(dr["EMPLOYEE_ID"]);

                    employeeAddresses.Add(employeeAddress);
                }
            }

            return employeeAddresses.AsQueryable();
        }
    }
}
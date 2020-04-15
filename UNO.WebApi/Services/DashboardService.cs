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
    public interface IDashboardService
    {
        List<TDay> Get(string empid);
    }

    public class DashboardService : IDashboardService
    {
        private UnitOfWork _unitOfWork;
        private DatabaseHelper _DatabaseHelper;

        public DashboardService(IUnitOfWork unitOfWork, DatabaseHelper databaseHelper)
        {
            _unitOfWork = (UnitOfWork)unitOfWork;
            _DatabaseHelper = databaseHelper;
        }




        public List<TDay> Get(string empId)
        {
            string query = "select tday_date, tday_intime, tday_outime , 'status' as status  from tday inner join [dbo].[ENT_EMPLOYEE_DTLS] EDet on EDet.EMPLOYEE_ID =TDAY_EMPCDE where EOD_EMPID ='AC06927' ";

            var x = _DatabaseHelper.GetDataTable(query, CommandType.Text);
            List<TDay> entParamsList = new List<TDay>();

            foreach (DataRow dr in x.Rows)
            {
                TDay entMenu = new TDay();
                entMenu.TDAY_DATE = Convert.ToDateTime(dr["tday_date"]);
                entMenu.TDAY_INTIME = Convert.ToDateTime(dr["TDAY_INTIME"]);
                entMenu.TDAY_OUTIME = Convert.ToDateTime(dr["TDAY_OUTIME"]);
                entMenu.TDAY_SFTREPO = Convert.ToString(dr["status"]);
                //entMenu.MODULE_ID = Convert.ToInt32(dr["MODULE_ID"]);
                //entMenu.SMODULE_NAME = Convert.ToString(dr["SMODULE_NAME"]);
                //entMenu.SMODULE_ID = Convert.ToInt32(dr["SMODULE_ID"]);
                //entMenu.MENU_ITEMPOSITION = Convert.ToInt32(dr["MENU_ITEMPOSITION"]);
                entParamsList.Add(entMenu);
            }
            return entParamsList;

        }

    }
}
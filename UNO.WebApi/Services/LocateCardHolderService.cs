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
    public interface ILocateCardHolderService
    {
        LocateCardHolder GetCardHolder(int FILTER_CRITERIA, string FILTER);
    }


    public class LocateCardHolderService : ILocateCardHolderService
    {
        private UnitOfWork _unitOfWork;
        private DatabaseHelper _DatabaseHelper;

        public LocateCardHolderService(IUnitOfWork unitOfWork, DatabaseHelper databaseHelper)
        {
            _unitOfWork = (UnitOfWork)unitOfWork;
            _DatabaseHelper = databaseHelper;
        }
        public LocateCardHolder GetCardHolder(int FILTER_CRITERIA, string FILTER)
        {
            string query = "SELECT DISTINCT EVENT_EMPLOYEE_CODE," +
                " EVENT_CARD_CODE,EPD_FIRST_NAME+' '+ ISNUll(EPD_MIDDLE_NAME,'') +' '+ EPD_LAST_NAME AS EMP_NAME," +
                " Parm.VALUE AS MARITAL_STATUS,EAD_PHONE_ONE,AR.READER_DESCRIPTION FROM ACS_EVENTS ACS " +
                " INNER JOIN ENT_EMPLOYEE_DTLS ED ON ACS.Event_Employee_Code = ED.EOD_EMPID" +
                " INNER JOIN ENT_EMPLOYEE_PERSONAL_DTLS EPD ON  ED.EMPLOYEE_ID = EPD.EMPLOYEE_ID" +
                " INNER JOIN ENT_PARAMS Parm ON EPD.EPD_MARITAL_STATUS = Parm.PARAM_ID" +
                " INNER JOIN ENT_EMPLOYEE_ADDRESS EAdd ON EPD.EMPLOYEE_ID = EAdd.EMPLOYEE_ID" +
                " INNER JOIN ACS_READER AR ON  ACS.Event_Reader_ID = AR.READER_ID" +
                " INNER JOIN ACS_CONTROLLER AC ON AR.CTLR_ID = AC.CTLR_ID" +
                " WHERE ACS.Event_Type='1'";
            if (FILTER_CRITERIA == 1)
            {
                query += " AND ACS.Event_Employee_Code =" + "'" + FILTER + "'";
            }
            if (FILTER_CRITERIA == 2)
            {
                query += "AND ACS.EVENT_CARD_CODE =" + "'" + FILTER + "'";
            }


            var x = _DatabaseHelper.GetDataTable(query, CommandType.Text);

            LocateCardHolder entCardHolder = new LocateCardHolder();
            foreach (DataRow dr in x.Rows)
            {
                entCardHolder.EVENT_EMPLOYEE_CODE = Convert.ToString(dr["EVENT_EMPLOYEE_CODE"]);
                entCardHolder.EVENT_CARD_CODE = Convert.ToString(dr["EVENT_CARD_CODE"]);
                entCardHolder.EMP_NAME = Convert.ToString(dr["EMP_NAME"]);
                entCardHolder.EPD_MARITAL_STATUS = Convert.ToString(dr["MARITAL_STATUS"]);
                entCardHolder.EPD_PHONE_ONE = Convert.ToString(dr["EAD_PHONE_ONE"]);
                entCardHolder.READER_DESCRIPTION = Convert.ToString(dr["READER_DESCRIPTION"]);
            }
            return entCardHolder;
        }
    }
}
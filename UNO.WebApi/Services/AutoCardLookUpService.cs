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
    public interface IAutoCardLookUpService
    {
        List<AutoCardLookUp> GetReaders();
        AutoCardLookUp GetRecords(string CTLR_ID,string READER_ID);
    }


    public class AutoCardLookUpService : IAutoCardLookUpService
    {
        private UnitOfWork _unitOfWork;
        private DatabaseHelper _DatabaseHelper;

        public AutoCardLookUpService(IUnitOfWork unitOfWork, DatabaseHelper databaseHelper)
        {
            _unitOfWork = (UnitOfWork)unitOfWork;
            _DatabaseHelper = databaseHelper;
        }
        public List<AutoCardLookUp> GetReaders()
        {
            string query = "SELECT AR.CTLR_ID ,AR.READER_ID, AR.READER_DESCRIPTION FROM ACS_READER AR " +
                            " INNER JOIN ACS_CONTROLLER AC ON AR.CTLR_ID=AC.CTLR_ID" +
                            " INNER JOIN ACS_ACCESSPOINT_RELATION APR ON AR.CTLR_ID = APR.AP_CONTROLLER_ID" +
                            " WHERE AR.READER_ID = APR.READER_ID AND APR.APR_ISDELETED = '0'";


            var x = _DatabaseHelper.GetDataTable(query, CommandType.Text);
            List<AutoCardLookUp> ctrlRdrList = new List<AutoCardLookUp>();
            List<AutoCardLookUp> entDefaultList = new List<AutoCardLookUp>  
            {  
                new AutoCardLookUp {Event_DateTime=DateTime.Now,Event_Card_Code="",Emp_Name="", CTLR_READER_ID="-1",READER_DESCRIPTION= "Select Criteria"},  
                new AutoCardLookUp {Event_DateTime=DateTime.Now,Event_Card_Code="",Emp_Name="", CTLR_READER_ID="All",READER_DESCRIPTION= "All"},  
            };
            ctrlRdrList.AddRange(entDefaultList);
            foreach (DataRow dr in x.Rows)
            {
                AutoCardLookUp entAutoCardLookUp = new AutoCardLookUp();
                entAutoCardLookUp.CTLR_READER_ID = Convert.ToString(dr["CTLR_ID"]) + "-" + Convert.ToString(dr["READER_ID"]);
                entAutoCardLookUp.READER_DESCRIPTION = Convert.ToString(dr["READER_DESCRIPTION"]);
                ctrlRdrList.Add(entAutoCardLookUp);
            }
           
            return ctrlRdrList;
        }

        public AutoCardLookUp GetRecords(string CTLR_ID, string READER_ID)
        {
          //var Event_Reader_ID = CTLR_READER_ID.Split('-')[1];
          //var Event_Controller_ID = CTLR_READER_ID.Split('-')[0];
            string query = "select Event_Datetime,Event_Card_Code, "+
                            "EPD_FIRST_NAME+' '+ cast(EPD_MIDDLE_NAME as varchar)+' '+EPD_LAST_NAME as Emp_Name,[STATUS],EPD_PHOTOURL "+
                            "from ACS_Events AC "+
                            "INNER JOIN ENT_EMPLOYEE_DTLS ED ON  AC.Event_Card_Code = ED.EPD_CARD_ID "+
                            "INNER JOIN ENT_EMPLOYEE_PERSONAL_DTLS EPD ON ED.EMPLOYEE_ID = EPD.EMPLOYEE_ID "+
                            "INNER JOIN ACS_CARD_CONFIG CC ON AC.Event_Card_Code = CC.CARD_CODE "+
                            "WHERE Event_Type =(select CODE from ENT_PARAMS where MODULE='ACS' and IDENTIFIER='EVENTTYPE' and [VALUE]='CARD') ";
                            if (CTLR_ID != "0" && READER_ID != "0")
	                        {
                                query += "AND Event_Reader_ID=" + READER_ID + " AND Event_Controller_ID = " + CTLR_ID + " ";
	                        }
                            query += "AND Event_Datetime between '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "' and getdate() order by Event_Datetime asc ";
            var x = _DatabaseHelper.GetDataTable(query, CommandType.Text);
            AutoCardLookUp autoCardLookUpObj = new AutoCardLookUp();
          
            foreach (DataRow dr in x.Rows)
            {
                autoCardLookUpObj.Event_DateTime = Convert.ToDateTime(dr["Event_Datetime"]);
                autoCardLookUpObj.Event_Card_Code = Convert.ToString(dr["Event_Card_Code"]);
                autoCardLookUpObj.Emp_Name = Convert.ToString(dr["Emp_Name"]);
                autoCardLookUpObj.STATUS = Convert.ToString(dr["STATUS"]);
            }
            return autoCardLookUpObj;
        }
    }
}
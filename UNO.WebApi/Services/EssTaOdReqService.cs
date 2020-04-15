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
    public interface IEssTaOdReqService
    {
        List<EssTaOdReq> GetEssTaOdReq();
        EssTaOdReq GetEssTaOdReq(int id);
        Task<int> Edit(EssTaOdReq entity, string ipaddress, int activeuser);
        Task<int> Delete(int id, string ipaddress, int activeuser);
    }
    public class EssTaOdReqService : IEssTaOdReqService
    {
        private UnitOfWork _unitOfWork;
        private DatabaseHelper _DatabaseHelper;
        public EssTaOdReqService(IUnitOfWork unitOfWork, DatabaseHelper databaseHelper)
        {
            _unitOfWork = (UnitOfWork)unitOfWork;
            _DatabaseHelper = databaseHelper;
        }

        public List<EssTaOdReq> GetEssTaOdReq()
          {
              string query = "select ESS_OD_ID,ent_employee_dtls.Epd_First_Name+ '' +ent_employee_dtls.Epd_Last_Name as Name,ess_OD_empid as EmpCode, " +
                             "convert(VARCHAR(20),Ess_OD_fromdt,103) as FromDate,case when convert(VARCHAR(20),Ess_OD_Todt,103)= convert(VARCHAR(20),Ess_OD_fromdt,103) "+
                             "then convert(VARCHAR(20),Ess_OD_fromdt,103) else convert(VARCHAR(20),Ess_OD_Todt,103)  end as ToDate, ESS_OD_ODCD, "+
                             "Case  ESS_OD_Status WHEN 'N' then 'Pending For Approval' when 'A' then 'Approved' When 'R' then 'Rejected' When 'C' then 'Cancelled' "+
                             "End as ESS_OD_Status, "+
                             "datepart(day,Ess_OD_Todt-Ess_OD_fromdt) as days,ESS_OD_REODRK as Reason,ESS_OD_SANC_REODRK as Remark,ESS_OD_REASON_ID as REASON_ID " +
                             "from ESS_TA_OD,ent_employee_dtls "+
                             "where ess_ta_od.ess_OD_empid=ent_employee_dtls.EMPLOYEE_ID  "+                                                                                    
                             "and  ess_od_isdeleted='0' order by convert(datetime,Ess_OD_fromdt,103)  desc";

              var x = _DatabaseHelper.GetDataTable(query, CommandType.Text);
              List<EssTaOdReq> entEssTaOdReqList = new List<EssTaOdReq>();

              foreach (DataRow dr in x.Rows)
              {
                  EssTaOdReq enEssTaOdReq = new EssTaOdReq();
                  enEssTaOdReq.ESS_OD_ID = Convert.ToInt32(dr["ESS_OD_ID"]);
                  enEssTaOdReq.ESS_OD_EMPID = Convert.ToInt32(dr["EmpCode"]);
                  enEssTaOdReq.NAME = Convert.ToString(dr["NAME"]);
                  enEssTaOdReq.ESS_OD_FROMDT = Convert.ToDateTime(dr["FromDate"]);
                  enEssTaOdReq.ESS_OD_TODT = Convert.ToDateTime(dr["ToDate"]);
                  enEssTaOdReq.ESS_OD_ODCD = Convert.ToString(dr["ESS_OD_ODCD"]);
                  enEssTaOdReq.ESS_OD_STATUS = Convert.ToString(dr["ESS_OD_Status"]);
                  enEssTaOdReq.ESS_OD_LVDAYS = Convert.ToDouble(dr["days"]);
                  enEssTaOdReq.ESS_OD_REODRK = Convert.ToString(dr["Reason"]);
                  enEssTaOdReq.ESS_OD_SANC_REODRK = Convert.ToString(dr["Remark"]);
                  enEssTaOdReq.ESS_OD_REASON_ID = Convert.ToInt32(dr["REASON_ID"]);
                  entEssTaOdReqList.Add(enEssTaOdReq);
              }
              return entEssTaOdReqList;
          }

        public EssTaOdReq GetEssTaOdReq(int id)
        {
            string query = "select ESS_OD_ID,ent_employee_dtls.Epd_First_Name+ '' +ent_employee_dtls.Epd_Last_Name as Name,ess_OD_empid as EmpCode, " +
                           "convert(VARCHAR(20),Ess_OD_fromdt,103) as FromDate,case when convert(VARCHAR(20),Ess_OD_Todt,103)= convert(VARCHAR(20),Ess_OD_fromdt,103) " +
                           "then convert(VARCHAR(20),Ess_OD_fromdt,103) else convert(VARCHAR(20),Ess_OD_Todt,103)  end as ToDate, ESS_OD_ODCD, " +
                           "Case  ESS_OD_Status WHEN 'N' then 'Pending For Approval' when 'A' then 'Approved' When 'R' then 'Rejected' When 'C' then 'Cancelled' " +
                           "End as ESS_OD_Status, " +
                           "datepart(day,Ess_OD_Todt-Ess_OD_fromdt) as days,ESS_OD_REODRK as Reason,ESS_OD_SANC_REODRK as Remark,ESS_OD_REASON_ID as REASON_ID " +
                           "from ESS_TA_OD,ent_employee_dtls " +
                           "where ess_ta_od.ess_OD_empid=ent_employee_dtls.EMPLOYEE_ID  " +
                           "and  ess_od_isdeleted='0' and  ESS_Od_ID  =" + id;
                            

            var x = _DatabaseHelper.GetDataTable(query, CommandType.Text);
            EssTaOdReq entEssTaOdReqList = new EssTaOdReq();

            foreach (DataRow dr in x.Rows)
            {
                entEssTaOdReqList.ESS_OD_ID = Convert.ToInt32(dr["ESS_OD_ID"]);
                entEssTaOdReqList.ESS_OD_EMPID = Convert.ToInt32(dr["EmpCode"]);
                entEssTaOdReqList.NAME = Convert.ToString(dr["NAME"]);
                entEssTaOdReqList.ESS_OD_FROMDT = Convert.ToDateTime(dr["FromDate"]);
                entEssTaOdReqList.ESS_OD_TODT = Convert.ToDateTime(dr["ToDate"]);
                entEssTaOdReqList.ESS_OD_ODCD = Convert.ToString(dr["ESS_OD_ODCD"]);
                entEssTaOdReqList.ESS_OD_STATUS = Convert.ToString(dr["ESS_OD_Status"]);
                entEssTaOdReqList.ESS_OD_LVDAYS = Convert.ToDouble(dr["days"]);
                entEssTaOdReqList.ESS_OD_REODRK = Convert.ToString(dr["Reason"]);
                entEssTaOdReqList.ESS_OD_SANC_REODRK = Convert.ToString(dr["Remark"]);
                entEssTaOdReqList.ESS_OD_REASON_ID = Convert.ToInt32(dr["REASON_ID"]);
            }
            return entEssTaOdReqList;
        }

        public async Task<int> Edit(EssTaOdReq entity, string ipaddress, int activeuser)
        {
            string ErrorMessage = string.Empty;
            string Status = string.Empty;

            //First Condition
              Status = GetTdayStatus(entity.ESS_OD_FROMDT,entity.ESS_OD_EMPID);

            if (Status == "NA")
            {
                ErrorMessage = "Shift not yet created for the selected date(s)";
                throw new Exception(ErrorMessage);
            }


            //Second Condition
            Status = GetTdayStatus(entity.ESS_OD_TODT, entity.ESS_OD_EMPID);

            if (Status == "NA")
            {
                ErrorMessage = "Shift not yet created for the selected date(s)";
                throw new Exception(ErrorMessage);
            }

            //Third Condition

            string strdbFrmdt;
            strdbFrmdt = entity.ESS_OD_FROMDT.ToString("dd/MM/yyyy");
            DateTime dtfromdt = DateTime.ParseExact(strdbFrmdt, "dd/MM/yyyy", null);


            string strdbtodt;
            strdbtodt = entity.ESS_OD_TODT.ToString("dd/MM/yyyy");
            DateTime dtttodt = DateTime.ParseExact(strdbtodt, "dd/MM/yyyy", null);

            int ChkLeaveAppDate = GetLeaveAppDate(strdbFrmdt, strdbtodt, entity.ESS_OD_EMPID,entity.ESS_OD_ID);

            if (ChkLeaveAppDate == 1)
            {
                ErrorMessage = "Outdoor already applied for the date range";
                throw new Exception(ErrorMessage);
            }

            if (ErrorMessage == "")
            {
                try
                {
                    string query = "Update ESS_TA_OD set ESS_OD_FROMDT=Convert(datetime,'" + strdbFrmdt + "',103),ESS_OD_ODCD='OD', " +
                                   "ESS_OD_RSNID='" + entity.ESS_OD_REASON_ID + "', " +
                                   "ESS_OD_TODT=Convert(datetime,'" + strdbtodt + "',103),ESS_OD_Remark='" + entity.ESS_OD_REODRK + "'  " +
                                   "where ESS_OD_ISDELETED='0' And ESS_OD_RowID=" + entity.ESS_OD_ID + "";
                    return await _DatabaseHelper.Insert(query, CommandType.Text,ipaddress,activeuser);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            else
            {
                throw new Exception(ErrorMessage);
            }
        }

        public async Task<int> Delete(int id, string ipaddress, int activeuser)
        {
            string query = "Update ESS_TA_OD set ESS_OD_ISDELETED = '1', ESS_OD_DELETEDDATE='" + DateTime.Now.ToString("dd/MMM/yyyy hh:mm") + "' where ESS_OD_Rowid = " + id;
            return await _DatabaseHelper.Insert(query, CommandType.Text, ipaddress, activeuser);
        }

        public string GetTdayStatus(DateTime date, int tday_empcde)
        {
            //string query = string.Empty;
            string query = "select ts.TDAY_STATUS as TDAY_STATUS from tday_status ts inner join TDAY t on ts.TDAY_STATUS_ID=t.TDAY_STATUS_ID WHERE t.TDAY_DATE = '" + date.ToString("dd/MMM/yyyy hh:mm") + "' AND t.TDAY_EMPCDE =  '" + tday_empcde + "'";
            var x = _DatabaseHelper.GetDataTable(query, CommandType.Text);
            string Status = string.Empty;

            if (x.Rows.Count > 0)
            {
             Status = x.Rows[0][0].ToString();
            }
            else
            {
                Status = "NA";
            }
            return Status;
        }

        public int GetLeaveAppDate(string strdbFrmdt, string strdbtodt, int ESS_Od_EMPID, int ESS_Od_ID)
        {
            string query = string.Empty;
            query = "Select count(ESS_od_empid) FROM ESS_TA_Od Where (ESS_od_FROMDT between convert(datetime,'" + strdbFrmdt + "',103) and Convert(datetime,'" + strdbtodt + "',103)  OR ESS_OD_TODT  between convert(datetime,'" + strdbFrmdt + "',103) and Convert(datetime,'" + strdbtodt + "',103)) and ESS_OD_ISDELETED=0  AND ESS_OD_EMPID= '" + ESS_Od_EMPID + "' AND   ESS_OD_RowId <>  " + ESS_Od_ID + "";

            var x = _DatabaseHelper.GetDataTable(query, CommandType.Text);
            int value = 0;
            if (Convert.ToUInt32(x.Rows[0][0]) > 0)
            {
                value = 1;
            }
            else
            {
                value = 0;
            }
            return value;
        }
    }
}
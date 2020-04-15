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
    public interface IEssTaMaReqService
    {
        IQueryable<EssTaMaReq> GetEssTaMaReq();
        EssTaMaReq GetEssTaMaReq(int id);

        Task<int> Edit(EssTaMaReq entity, string ipaddress, int activeuser);
        Task<int> Delete(int id, string ipaddress, int activeuser);
    }
    public class EssTaMaReqService : IEssTaMaReqService
    {
        private UnitOfWork _unitOfWork;
        private DatabaseHelper _DatabaseHelper;

        public EssTaMaReqService(IUnitOfWork unitOfWork, DatabaseHelper databaseHelper)
        {
            _unitOfWork = (UnitOfWork)unitOfWork;
            _DatabaseHelper = databaseHelper;
        }

        public IQueryable<EssTaMaReq> GetEssTaMaReq()
        {
            string query = " select ESS_Ma_ID,ess_Ma_empid, ent_employee_dtls.Epd_First_Name+ ' ' +ent_employee_dtls.Epd_Last_Name as Name   ,Ess_Ma_fromdt as Ess_Ma_fromdt, " +
                           "case when Ess_Ma_Todt= Ess_Ma_fromdt THEN Ess_Ma_fromdt ELSE ESS_Ma_TODT  END AS ESS_Ma_TODT, " +
                           "Case  ESS_Ma_Status WHEN 'N' then 'Pending For Approval' " +
                           "when 'A' then 'Approved' When 'R' then 'Rejected' When 'C' then 'Cancelled' End as ESS_Ma_Status, ESS_Ma_LVDAYS,ESS_Ma_REMARK,ESS_Ma_SANC_REMARK,ESS_Ma_REASON_ID  from ESS_TA_Ma,ent_employee_dtls " +
                           "where  ess_ta_Ma.ess_Ma_empid=ent_employee_dtls.EMPLOYEE_ID " +
                           "And ess_Ma_isdeleted='0' order by convert(datetime,Ess_Ma_fromdt,103)  desc ";

            var x = _DatabaseHelper.GetDataTable(query, CommandType.Text);
            List<EssTaMaReq> entEssTaMaReqList = new List<EssTaMaReq>();

            foreach (DataRow dr in x.Rows)
            {
                EssTaMaReq enEssTaMaReq = new EssTaMaReq();
                enEssTaMaReq.ESS_MA_ID = Convert.ToInt32(dr["ESS_Ma_ID"]);
                enEssTaMaReq.ESS_MA_EMPID = Convert.ToInt32(dr["ESS_Ma_EMPID"]);
                enEssTaMaReq.NAME = Convert.ToString(dr["NAME"]);
                enEssTaMaReq.ESS_MA_FROMDT = Convert.ToDateTime(dr["ESS_Ma_FROMDT"]);
                enEssTaMaReq.ESS_MA_TODT = Convert.ToDateTime(dr["ESS_Ma_TODT"]);
                enEssTaMaReq.ESS_MA_STATUS = Convert.ToString(dr["ESS_Ma_STATUS"]);
                enEssTaMaReq.ESS_MA_LVDAYS = Convert.ToDouble(dr["ESS_Ma_LVDAYS"]);
                enEssTaMaReq.ESS_MA_REMARK = Convert.ToString(dr["ESS_Ma_REMARK"]);
                enEssTaMaReq.ESS_MA_SANC_REMARK = Convert.ToString(dr["ESS_Ma_SANC_REMARK"]);
                enEssTaMaReq.ESS_MA_REASON_ID = Convert.ToInt32(dr["ESS_Ma_REASON_ID"]);
                entEssTaMaReqList.Add(enEssTaMaReq);
            }
            return entEssTaMaReqList.AsQueryable(); 
        }

        public EssTaMaReq GetEssTaMaReq(int id)
        {
            string query = " select ESS_Ma_ID,ess_Ma_empid, ent_employee_dtls.Epd_First_Name+ ' ' +ent_employee_dtls.Epd_Mast_Name as Name   ,Ess_Ma_fromdt as Ess_Ma_fromdt, " +
                             "case when Ess_Ma_Todt= Ess_Ma_fromdt THEN Ess_Ma_fromdt ELSE ESS_Ma_TODT  END AS ESS_Ma_TODT, " +
                             "Case  ESS_Ma_Status WHEN 'N' then 'Pending For Approval' " +
                             "when 'A' then 'Approved' When 'R' then 'Rejected' When 'C' then 'Cancelled' End as ESS_Ma_Status, ESS_Ma_LVDAYS,ESS_Ma_REMARK,ESS_Ma_SANC_REMARK,ESS_Ma_REASON_ID  from ESS_TA_Ma,ent_employee_dtls " +
                             "where  ess_ta_Ma.ess_Ma_empid=ent_employee_dtls.EMPLOYEE_ID " +
                             "And ess_Ma_isdeleted='0' and  ESS_Ma_ID  =" + id;

            var x = _DatabaseHelper.GetDataTable(query, CommandType.Text);
            EssTaMaReq entEssTaMaReqList = new EssTaMaReq();

            foreach (DataRow dr in x.Rows)
            {
                entEssTaMaReqList.ESS_MA_ID = Convert.ToInt32(dr["ESS_Ma_ID"]);
                entEssTaMaReqList.ESS_MA_EMPID = Convert.ToInt32(dr["ESS_Ma_EMPID"]);
                entEssTaMaReqList.NAME = Convert.ToString(dr["NAME"]);
                entEssTaMaReqList.ESS_MA_FROMDT = Convert.ToDateTime(dr["ESS_Ma_FROMDT"]);
                entEssTaMaReqList.ESS_MA_TODT = Convert.ToDateTime(dr["ESS_Ma_TODT"]);
                entEssTaMaReqList.ESS_MA_STATUS = Convert.ToString(dr["ESS_Ma_STATUS"]);
                entEssTaMaReqList.ESS_MA_LVDAYS = Convert.ToDouble(dr["ESS_Ma_LVDAYS"]);
                entEssTaMaReqList.ESS_MA_REMARK = Convert.ToString(dr["ESS_Ma_REMARK"]);
                entEssTaMaReqList.ESS_MA_SANC_REMARK = Convert.ToString(dr["ESS_Ma_SANC_REMARK"]);
                entEssTaMaReqList.ESS_MA_REASON_ID = Convert.ToInt32(dr["ESS_Ma_REASON_ID"]);
            }
            return entEssTaMaReqList;
        }
        public async Task<int> Edit(EssTaMaReq entity, string ipaddress, int activeuser)
        {
            string ErrorMessage = string.Empty;
            //First Condition
            string Status = GetTdayStatus(entity.ESS_MA_FROMDT, entity.ESS_MA_EMPID);

            if (Status == "NA")
            {
                ErrorMessage = "The shift is already created,cannot Edit holiday for this month";
                throw new Exception(ErrorMessage);
            }
            //Second Condition

            string strdbFrmdt;
            strdbFrmdt = entity.ESS_MA_FROMDT.ToString("dd/MM/yyyy");
            DateTime dtfromdt = DateTime.ParseExact(strdbFrmdt, "dd/MM/yyyy", null);


            string strdbtodt;
            strdbtodt = entity.ESS_MA_TODT.ToString("dd/MM/yyyy");
            DateTime dtttodt = DateTime.ParseExact(strdbtodt, "dd/MM/yyyy", null);
            
            TimeSpan difference = dtttodt - dtfromdt;
            double LvDays = difference.Days + 1;

            if (entity.ESS_MA_LVDAYS == 0.50)
            {
                LvDays = 0.50;

            }

            //int LeaveBalCount = GetLeaveBal(LvDays, entity.ESS_MA_LVCD, entity.ESS_MA_EMPID);

            //if (LeaveBalCount == 0)
            //{
            //    ErrorMessage = "Insufficient Leave Balance";
            //    throw new Exception(ErrorMessage);
            //}

            int ChkLeaveAppDate = GetLeaveAppDate(strdbFrmdt, strdbtodt, entity.ESS_MA_EMPID, entity.ESS_MA_ID);

            if (ChkLeaveAppDate == 1)
            {
                ErrorMessage = "Leave already applied for the date range";
                throw new Exception(ErrorMessage);
            }
            //Fourth Condition

            string StatusMessage = ChkDayStatus(strdbFrmdt, strdbtodt, entity.ESS_MA_EMPID);

            if (StatusMessage == "")
            {
                ErrorMessage = "";
            }
            else
            {
                ErrorMessage = StatusMessage;
                throw new Exception(ErrorMessage);
            }
            //fifth Condition

            //string ValidationMessage = LEAVE_RULE_VALIDATION(LvDays, entity.ESS_MA_LVCD, entity.ESS_MA_EMPID, dtfromdt, dtttodt);

            //if (ValidationMessage == "")
            //{
            //    ErrorMessage = "";
            //}
            //else
            //{
            //    ErrorMessage = ValidationMessage;
            //    throw new Exception(ErrorMessage);
            //}


            if (ErrorMessage == "")
            {
                try
                {

                    string query = "Update ESS_TA_Ma set ESS_Ma_FROMDT = '" + entity.ESS_MA_FROMDT.ToString("dd/MMM/yyyy hh:mm") + "',ESS_Ma_TODT='" + entity.ESS_MA_TODT.ToString("dd/MMM/yyyy hh:mm") + "',ESS_Ma_REASON_ID='" + entity.ESS_MA_REASON_ID + "',ESS_Ma_LVDAYS='" + entity.ESS_MA_LVDAYS + "',ESS_Ma_Remark='" + entity.ESS_MA_REMARK + "'  where ESS_Ma_ID =  " + entity.ESS_MA_ID;
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
            string query = "Update ESS_TA_Ma set ESS_Ma_ISDELETED = '1', ESS_Ma_DELETEDDATE ='" + DateTime.Now.ToString("dd/MMM/yyyy hh:mm") + "' where ESS_Ma_ID = " + id;
            return await _DatabaseHelper.Insert(query, CommandType.Text, ipaddress, activeuser);
        }
        public string GetTdayStatus(DateTime date, int tday_empcde)
        {
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

        public int GetLeaveBal(double LvDays, string ESS_Ma_LVCD, int ESS_Ma_EMPID)
        {
            string query = string.Empty;
            //need to add leaveid ESS_Ma_LVCD
            query = "Select count(*) from ta_leave_summary where Lv_Emp_Id=" + ESS_Ma_EMPID + "  and Lv_Available > " + LvDays + " ";

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

        public int GetLeaveAppDate(string strdbFrmdt, string strdbtodt, int ESS_Ma_EMPID, int ESS_Ma_ID)
        {
            string query = string.Empty;
            query = "Select count(ESS_Ma_EMPID) FROM ESS_TA_Ma Where ESS_Ma_EMPID = '" + ESS_Ma_EMPID + "' and  ESS_Ma_ID <> " + ESS_Ma_ID + " and ess_Ma_isdeleted='0' " +
                                      " And ((ESS_Ma_FROMDT <= Convert(DateTime,'" + strdbFrmdt + "',103) " +
                                       " And ESS_Ma_TODT >= Convert(DateTime,'" + strdbFrmdt + "',103)) " +
                                      " Or (ESS_Ma_FROMDT <= Convert(DateTime,'" + strdbtodt + "',103) " +
                                       " And ESS_Ma_TODT >= Convert(DateTime,'" + strdbtodt + "',103)) " +
                                       " Or (ESS_Ma_FROMDT >= Convert(DateTime,'" + strdbFrmdt + "',103) " +
                                       " And ESS_Ma_FROMDT <= Convert(DateTime,'" + strdbtodt + "',103)) " +
                                     " Or (ESS_Ma_TODT >= Convert(DateTime,'" + strdbFrmdt + "',103) " +
                                     " And ESS_Ma_TODT <= Convert(DateTime,'" + strdbtodt + "',103))) "; ;

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

        public string ChkDayStatus(string strdbFrmdt, string strdbtodt, int ESS_Ma_EMPID)
        {
            string query = string.Empty;

            query = "select ISNULL(ts.TDAY_STATUS,'') as tday_status from TDAY t inner join tday_status ts on ts.TDAY_STATUS_ID=t.TDAY_STATUS_ID  where t.TDAY_DATE  between convert(datetime,'" + strdbFrmdt + "',103) and Convert(datetime,'" + strdbtodt + "',103)  AND t.tday_empcde= '" + ESS_Ma_EMPID + "'"; ;

            var x = _DatabaseHelper.GetDataTable(query, CommandType.Text);

            string strmessage = string.Empty; ;

            for (int i = 0; i <= x.Rows.Count - 1; i++)
            {
                if (x.Rows[i]["tday_status"].ToString() == "PR")
                {
                    strmessage = "Cannot apply for Leave as Employee is already present for the day";
                }
                if (strdbFrmdt == strdbtodt)
                {
                    //if (x.Rows[i]["tday_status"].ToString() == "ABW2" || x.Rows[i]["tday_status"].ToString() == "ABWO")
                    //{
                    //    strmessage = "Cannot apply Leave on weekly off";

                    //}
                    //else if (x.Rows[i]["tday_status"].ToString() == "ABHO")
                    //{
                    //    strmessage = "Cannot apply Leave on holiday";
                    //}
                }
            }
            return strmessage;
        }

        /*--------------------------------LEAVE_RULE_VALIDATION-------------------------------*/

        public string LEAVE_RULE_VALIDATION(double LvDays, string ESS_Ma_LVCD, int ESS_Ma_EMPID, DateTime strdbFrmdt, DateTime strdbtodt)
        {
            string query = string.Empty;
            string pStatus = string.Empty;
            int pPrefixCount = 0;
            int pSuffixCount = 0;
            string LR_DAYS;
            string LEAVE_RULE;
            string LR_GreaterOrLesser;
            double LR_MinDaysAllowed;
            double LR_MaxDaysAllowed;
            
            query = "SELECT LR_DAYS,LEAVE_RULE,LR_GreaterOrLesser,LR_MinDaysAllowed,LR_MaxDaysAllowed " +
                    "FROM TA_LEAVE_RULE_NEW  " +
                    "INNER JOIN  ENT_EMPLOYEE_DTLS ON TA_LEAVE_RULE_NEW.LR_CATEGORYID=ENT_EMPLOYEE_DTLS.EOD_CATEGORY_ID  " +
                    "WHERE ENT_EMPLOYEE_DTLS.EMPLOYEE_ID=" + ESS_Ma_EMPID + "  " +
                    "AND  " +
                    "TA_LEAVE_RULE_NEW.LeaveID='" + ESS_Ma_LVCD + "' "; ;

            var x = _DatabaseHelper.GetDataTable(query, CommandType.Text);

            string strmessage = string.Empty; ;
            
            LR_DAYS = x.Rows[0]["LR_DAYS"].ToString();
            LEAVE_RULE = x.Rows[0]["LEAVE_RULE"].ToString();
            LR_GreaterOrLesser = x.Rows[0]["LR_GreaterOrLesser"].ToString();
            LR_MinDaysAllowed = Convert.ToDouble(x.Rows[0]["LR_MinDaysAllowed"].ToString());
            LR_MaxDaysAllowed = Convert.ToDouble(x.Rows[0]["LR_MaxDaysAllowed"].ToString());


            DataTable TMLEAVE = new DataTable();
            TMLEAVE.Columns.Add("pSOrP", typeof(string));
            TMLEAVE.Columns.Add("pLeaveDate", typeof(DateTime));
            TMLEAVE.Columns.Add("pStatus", typeof(string));

            if (LEAVE_RULE == "P" || LEAVE_RULE == "B" || LEAVE_RULE == "O")
            {
                strdbFrmdt = strdbFrmdt.AddDays(-1);
                pStatus = "ABWO";
                while (pStatus == "ABWO" || pStatus == "ABW2" || pStatus == "ABHO")
                {
                    string queryStatus = "select ts.TDAY_STATUS as TDAY_STATUS from tday_status ts inner join TDAY t on ts.TDAY_STATUS_ID=t.TDAY_STATUS_ID WHERE t.TDAY_DATE = '" + strdbFrmdt + "' AND t.TDAY_EMPCDE =  '" + ESS_Ma_EMPID + "'";
                    var xStatus = _DatabaseHelper.GetDataTable(queryStatus, CommandType.Text);
                    if (xStatus.Rows.Count > 0)
                    {
                        pStatus = xStatus.Rows[0]["TDAY_STATUS"].ToString();
                    }
                    TMLEAVE.Rows.Add("P", strdbFrmdt, pStatus);
                    Convert.ToDateTime(strdbFrmdt).AddDays(-1);
                    pPrefixCount = pPrefixCount + 1;
                }
            }

            //--------------------------------------------------------------------------------

            if (LEAVE_RULE == "S" || LEAVE_RULE == "B" || LEAVE_RULE == "O")
            {
                strdbtodt = strdbtodt.AddDays(-1);
                pStatus = "ABWO";
                while (pStatus == "ABWO" || pStatus == "ABW2" || pStatus == "ABHO")
                {
                    string queryStatus = "select ts.TDAY_STATUS as TDAY_STATUS from tday_status ts inner join TDAY t on ts.TDAY_STATUS_ID=t.TDAY_STATUS_ID WHERE t.TDAY_DATE = '" + strdbtodt + "' AND t.TDAY_EMPCDE =  '" + ESS_Ma_EMPID + "'";
                    var xStatus = _DatabaseHelper.GetDataTable(queryStatus, CommandType.Text);
                    if (xStatus.Rows.Count > 0)
                    {
                        pStatus = xStatus.Rows[0]["TDAY_STATUS"].ToString();
                    }
                    TMLEAVE.Rows.Add("S", strdbtodt, pStatus);
                    strdbtodt.AddDays(-1);
                    pSuffixCount = pSuffixCount + 1;
                }

            }

            if (LEAVE_RULE == "O")
            {
                pPrefixCount = pPrefixCount - 1;
                pSuffixCount = pSuffixCount - 1;

                if (LR_GreaterOrLesser == "G")
                {
                    if (pPrefixCount < pSuffixCount)
                    {
                        var removeList = TMLEAVE.DefaultView;
                        removeList.RowFilter = "pSOrP=P";
                        for (var i = 0; i <= removeList.Count - 1; i++)
                        {
                            TMLEAVE.Rows.Remove(removeList[i].Row);
                        }
                    }
                    else
                    {
                        var removeList = TMLEAVE.DefaultView;
                        removeList.RowFilter = "pSOrP=S";
                        for (var i = 0; i <= removeList.Count - 1; i++)
                        {
                            TMLEAVE.Rows.Remove(removeList[i].Row);
                        }
                    }
                }

                if (LR_GreaterOrLesser == "L")
                {
                    if (pPrefixCount < pSuffixCount)
                    {
                        var removeList = TMLEAVE.DefaultView;
                        removeList.RowFilter = "pSOrP=S";
                        for (var i = 0; i <= removeList.Count - 1; i++)
                        {
                            TMLEAVE.Rows.Remove(removeList[i].Row);
                        }
                    }
                    else
                    {
                        var removeList = TMLEAVE.DefaultView;
                        removeList.RowFilter = "pSOrP=P";
                        for (var i = 0; i <= removeList.Count - 1; i++)
                        {
                            TMLEAVE.Rows.Remove(removeList[i].Row);
                        }
                    }
                }
            }

            //---------------------
            if (LEAVE_RULE == "B")
            {

                string FirstStatus = string.Empty;
                string LastStatus = string.Empty;

                var filteredDataMinRows = TMLEAVE.Select("pLeaveDate= MIN(pLeaveDate)");

                if (filteredDataMinRows.Length != 0)
                {
                    TMLEAVE = filteredDataMinRows.CopyToDataTable();
                    FirstStatus = TMLEAVE.Rows[0]["pStatus"].ToString();
                }

                var filteredDataMaxRows = TMLEAVE.Select("pLeaveDate= MIN(pLeaveDate)");

                if (filteredDataMaxRows.Length != 0)
                {
                    TMLEAVE = filteredDataMaxRows.CopyToDataTable();
                    LastStatus = TMLEAVE.Rows[0]["pStatus"].ToString();
                }

                if (FirstStatus == "AB" || FirstStatus == "PRAB" || FirstStatus == "PR" || FirstStatus == "OD" || FirstStatus == "OP" || FirstStatus == "PRW2" || FirstStatus == "PRWO" || FirstStatus == "PRHO" || FirstStatus == "LWP")
                {
                    var removeList = TMLEAVE.DefaultView;
                    removeList.RowFilter = "pSOrP=P";
                    for (var i = 0; i <= removeList.Count - 1; i++)
                    {
                        TMLEAVE.Rows.Remove(removeList[i].Row);
                    }
                }
                else
                {
                    var removeList = TMLEAVE.DefaultView;
                    removeList.RowFilter = "pLeaveDate= MIN(pLeaveDate)";
                    for (var i = 0; i <= removeList.Count - 1; i++)
                    {
                        TMLEAVE.Rows.Remove(removeList[i].Row);
                    }

                }

                if (LastStatus == "AB" || LastStatus == "PRAB" || LastStatus == "PR" || LastStatus == "OD" || LastStatus == "OP" || LastStatus == "PRW2" || LastStatus == "PRWO" || LastStatus == "PRHO" || LastStatus == "LWP")
                {
                    var removeList = TMLEAVE.DefaultView;
                    removeList.RowFilter = "pSOrP=S";
                    for (var i = 0; i <= removeList.Count - 1; i++)
                    {
                        TMLEAVE.Rows.Remove(removeList[i].Row);
                    }
                }
                else
                {
                    var removeList = TMLEAVE.DefaultView;
                    removeList.RowFilter = "pLeaveDate= MAX(pLeaveDate)";
                    for (var i = 0; i <= removeList.Count - 1; i++)
                    {
                        TMLEAVE.Rows.Remove(removeList[i].Row);
                    }

                }
            }
            //-----------------------------------------------------pending
            while (strdbFrmdt < strdbtodt)
            {
                string queryStatus = "select ts.TDAY_STATUS as TDAY_STATUS from tday_status ts inner join TDAY t on ts.TDAY_STATUS_ID=t.TDAY_STATUS_ID WHERE t.TDAY_DATE = '" + strdbtodt + "' AND t.TDAY_EMPCDE =  '" + ESS_Ma_EMPID + "'";
                var xStatus = _DatabaseHelper.GetDataTable(queryStatus, CommandType.Text);

                if (xStatus.Rows.Count > 0)
                {
                    pStatus = xStatus.Rows[0]["TDAY_STATUS"].ToString();
                }

                TMLEAVE.Rows.Add("W", strdbFrmdt, pStatus);
                strdbFrmdt.AddDays(-1);

            }
            if (Convert.ToDouble(LR_MinDaysAllowed) > LvDays)
            {
                strmessage = "Please apply minimum " + LR_MinDaysAllowed.ToString() + " as per HR policies";
            }


            if (Convert.ToDouble(LR_MaxDaysAllowed) < LvDays)
            {
                strmessage = "Leave days requested should be less than or equal to " + LR_MaxDaysAllowed.ToString() + " as per HR policies";
            }

            if (LvDays < TMLEAVE.Rows.Count)
            {
                strmessage = "The leave dates and count has been changed as per HR policies";
            }
            return strmessage;
        }
    }
}
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
    public interface IEssTaLaReqService
    {
        IQueryable<EssTaLaReq> GetEssTaLaReq();
        EssTaLaReq GetEssTaLaReq(int id);

        Task<int> Edit(EssTaLaReq entity, string ipaddress, int activeuser);
        Task<int> Delete(int id, string ipaddress, int activeuser);
    }
    public class EssTaLaReqService : IEssTaLaReqService
    {
        private UnitOfWork _unitOfWork;
        private DatabaseHelper _DatabaseHelper;

        public EssTaLaReqService(IUnitOfWork unitOfWork, DatabaseHelper databaseHelper)
        {
            _unitOfWork = (UnitOfWork)unitOfWork;
            _DatabaseHelper = databaseHelper;
        }

        public IQueryable<EssTaLaReq> GetEssTaLaReq()
        {
            string query = " select ESS_LA_ID,ess_LA_empid, ent_employee_dtls.Epd_First_Name+ ' ' +ent_employee_dtls.Epd_Last_Name as Name   ,Ess_LA_fromdt as Ess_LA_fromdt, " +
                           "case when Ess_LA_Todt= Ess_LA_fromdt THEN Ess_LA_fromdt ELSE ESS_LA_TODT  END AS ESS_LA_TODT, " +
                           "ESS_LA_LVCD , Case  ESS_LA_Status WHEN 'N' then 'Pending For Approval' " +
                           "when 'A' then 'Approved' When 'R' then 'Rejected' When 'C' then 'Cancelled' End as ESS_LA_Status, ESS_LA_LVDAYS,ESS_LA_REMARK,ESS_LA_SANC_REMARK,ESS_LA_REASON_ID  from ESS_TA_LA,ent_employee_dtls " +
                           "where  ess_ta_la.ess_LA_empid=ent_employee_dtls.EMPLOYEE_ID " +
                           "And ess_la_isdeleted='0' order by convert(datetime,Ess_LA_fromdt,103)  desc ";

            var x = _DatabaseHelper.GetDataTable(query, CommandType.Text);
            List<EssTaLaReq> entEssTaLaReqList = new List<EssTaLaReq>();

            foreach (DataRow dr in x.Rows)
            {
                EssTaLaReq enEssTaLaReq = new EssTaLaReq();
                enEssTaLaReq.ESS_LA_ID = Convert.ToInt32(dr["ESS_LA_ID"]);
                enEssTaLaReq.ESS_LA_EMPID = Convert.ToInt32(dr["ESS_LA_EMPID"]);
                enEssTaLaReq.NAME = Convert.ToString(dr["NAME"]);
                enEssTaLaReq.ESS_LA_FROMDT = Convert.ToDateTime(dr["ESS_LA_FROMDT"]);
                enEssTaLaReq.ESS_LA_TODT = Convert.ToDateTime(dr["ESS_LA_TODT"]);
                enEssTaLaReq.ESS_LA_LVCD = Convert.ToString(dr["ESS_LA_LVCD"]);
                enEssTaLaReq.ESS_LA_STATUS = Convert.ToString(dr["ESS_LA_STATUS"]);
                enEssTaLaReq.ESS_LA_LVDAYS = Convert.ToDouble(dr["ESS_LA_LVDAYS"]);
                enEssTaLaReq.ESS_LA_REMARK = Convert.ToString(dr["ESS_LA_REMARK"]);
                enEssTaLaReq.ESS_LA_SANC_REMARK = Convert.ToString(dr["ESS_LA_SANC_REMARK"]);
                enEssTaLaReq.ESS_LA_REASON_ID = Convert.ToInt32(dr["ESS_LA_REASON_ID"]);
                entEssTaLaReqList.Add(enEssTaLaReq);
            }
            return entEssTaLaReqList.AsQueryable(); 
        }

        public EssTaLaReq GetEssTaLaReq(int id)
        {
            string query = " select ESS_LA_ID,ess_LA_empid, ent_employee_dtls.Epd_First_Name+ ' ' +ent_employee_dtls.Epd_Last_Name as Name   ,Ess_LA_fromdt as Ess_LA_fromdt, " +
                             "case when Ess_LA_Todt= Ess_LA_fromdt THEN Ess_LA_fromdt ELSE ESS_LA_TODT  END AS ESS_LA_TODT, " +
                             "ESS_LA_LVCD , Case  ESS_LA_Status WHEN 'N' then 'Pending For Approval' " +
                             "when 'A' then 'Approved' When 'R' then 'Rejected' When 'C' then 'Cancelled' End as ESS_LA_Status, ESS_LA_LVDAYS,ESS_LA_REMARK,ESS_LA_SANC_REMARK,ESS_LA_REASON_ID  from ESS_TA_LA,ent_employee_dtls " +
                             "where  ess_ta_la.ess_LA_empid=ent_employee_dtls.EMPLOYEE_ID " +
                             "And ess_la_isdeleted='0' and  ESS_LA_ID  =" + id;

            var x = _DatabaseHelper.GetDataTable(query, CommandType.Text);
            EssTaLaReq entEssTaLaReqList = new EssTaLaReq();

            foreach (DataRow dr in x.Rows)
            {
                entEssTaLaReqList.ESS_LA_ID = Convert.ToInt32(dr["ESS_LA_ID"]);
                entEssTaLaReqList.ESS_LA_EMPID = Convert.ToInt32(dr["ESS_LA_EMPID"]);
                entEssTaLaReqList.NAME = Convert.ToString(dr["NAME"]);
                entEssTaLaReqList.ESS_LA_FROMDT = Convert.ToDateTime(dr["ESS_LA_FROMDT"]);
                entEssTaLaReqList.ESS_LA_TODT = Convert.ToDateTime(dr["ESS_LA_TODT"]);
                entEssTaLaReqList.ESS_LA_LVCD = Convert.ToString(dr["ESS_LA_LVCD"]);
                entEssTaLaReqList.ESS_LA_STATUS = Convert.ToString(dr["ESS_LA_STATUS"]);
                entEssTaLaReqList.ESS_LA_LVDAYS = Convert.ToDouble(dr["ESS_LA_LVDAYS"]);
                entEssTaLaReqList.ESS_LA_REMARK = Convert.ToString(dr["ESS_LA_REMARK"]);
                entEssTaLaReqList.ESS_LA_SANC_REMARK = Convert.ToString(dr["ESS_LA_SANC_REMARK"]);
                entEssTaLaReqList.ESS_LA_REASON_ID = Convert.ToInt32(dr["ESS_LA_REASON_ID"]);
            }
            return entEssTaLaReqList;
        }
        public async Task<int> Edit(EssTaLaReq entity, string ipaddress, int activeuser)
        {
            string ErrorMessage = string.Empty;
            //First Condition
            string Status = GetTdayStatus(entity.ESS_LA_FROMDT, entity.ESS_LA_EMPID);

            if (Status == "NA")
            {
                ErrorMessage = "The shift is already created,cannot Edit holiday for this month";
                throw new Exception(ErrorMessage);
            }
            //Second Condition

            string strdbFrmdt;
            strdbFrmdt = entity.ESS_LA_FROMDT.ToString("dd/MM/yyyy");
            DateTime dtfromdt = DateTime.ParseExact(strdbFrmdt, "dd/MM/yyyy", null);


            string strdbtodt;
            strdbtodt = entity.ESS_LA_TODT.ToString("dd/MM/yyyy");
            DateTime dtttodt = DateTime.ParseExact(strdbtodt, "dd/MM/yyyy", null);
            
            TimeSpan difference = dtttodt - dtfromdt;
            double LvDays = difference.Days + 1;

            if (entity.ESS_LA_LVDAYS == 0.50)
            {
                LvDays = 0.50;

            }

            int LeaveBalCount = GetLeaveBal(LvDays, entity.ESS_LA_LVCD, entity.ESS_LA_EMPID);

            if (LeaveBalCount == 0)
            {
                ErrorMessage = "Insufficient Leave Balance";
                throw new Exception(ErrorMessage);
            }

            int ChkLeaveAppDate = GetLeaveAppDate(strdbFrmdt, strdbtodt, entity.ESS_LA_EMPID, entity.ESS_LA_ID);

            if (ChkLeaveAppDate == 1)
            {
                ErrorMessage = "Leave already applied for the date range";
                throw new Exception(ErrorMessage);
            }
            //Fourth Condition

            string StatusMessage = ChkDayStatus(strdbFrmdt, strdbtodt, entity.ESS_LA_EMPID);

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

            string ValidationMessage = LEAVE_RULE_VALIDATION(LvDays, entity.ESS_LA_LVCD, entity.ESS_LA_EMPID, dtfromdt, dtttodt);

            if (ValidationMessage == "")
            {
                ErrorMessage = "";
            }
            else
            {
                ErrorMessage = ValidationMessage;
                throw new Exception(ErrorMessage);
            }


            if (ErrorMessage == "")
            {
                try
                {

                    string query = "Update ESS_TA_LA set ESS_LA_FROMDT = '" + entity.ESS_LA_FROMDT.ToString("dd/MMM/yyyy hh:mm") + "',ESS_LA_TODT='" + entity.ESS_LA_TODT.ToString("dd/MMM/yyyy hh:mm") + "',ESS_LA_lvcd='" + entity.ESS_LA_LVCD + "',ESS_LA_REASON_ID='" + entity.ESS_LA_REASON_ID + "',ESS_LA_LVDAYS='" + entity.ESS_LA_LVDAYS + "',ESS_LA_Remark='" + entity.ESS_LA_REMARK + "'  where ESS_LA_ID =  " + entity.ESS_LA_ID;
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
            string query = "Update ESS_TA_LA set ESS_LA_ISDELETED = '1', ESS_LA_DELETEDDATE ='" + DateTime.Now.ToString("dd/MMM/yyyy hh:mm") + "' where ESS_LA_ID = " + id;
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

        public int GetLeaveBal(double LvDays, string ESS_LA_LVCD, int ESS_LA_EMPID)
        {
            string query = string.Empty;
            //need to add leaveid ESS_LA_LVCD
            query = "Select count(*) from ta_leave_summary where Lv_Emp_Id=" + ESS_LA_EMPID + "  and Lv_Available > " + LvDays + " ";

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

        public int GetLeaveAppDate(string strdbFrmdt, string strdbtodt, int ESS_LA_EMPID, int ESS_LA_ID)
        {
            string query = string.Empty;
            query = "Select count(ESS_LA_EMPID) FROM ESS_TA_LA Where ESS_LA_EMPID = '" + ESS_LA_EMPID + "' and  ESS_LA_ID <> " + ESS_LA_ID + " and ess_la_isdeleted='0' " +
                                      " And ((ESS_LA_FROMDT <= Convert(DateTime,'" + strdbFrmdt + "',103) " +
                                       " And ESS_LA_TODT >= Convert(DateTime,'" + strdbFrmdt + "',103)) " +
                                      " Or (ESS_LA_FROMDT <= Convert(DateTime,'" + strdbtodt + "',103) " +
                                       " And ESS_LA_TODT >= Convert(DateTime,'" + strdbtodt + "',103)) " +
                                       " Or (ESS_LA_FROMDT >= Convert(DateTime,'" + strdbFrmdt + "',103) " +
                                       " And ESS_LA_FROMDT <= Convert(DateTime,'" + strdbtodt + "',103)) " +
                                     " Or (ESS_LA_TODT >= Convert(DateTime,'" + strdbFrmdt + "',103) " +
                                     " And ESS_LA_TODT <= Convert(DateTime,'" + strdbtodt + "',103))) "; ;

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

        public string ChkDayStatus(string strdbFrmdt, string strdbtodt, int ESS_LA_EMPID)
        {
            string query = string.Empty;

            query = "select ISNULL(ts.TDAY_STATUS,'') as tday_status from TDAY t inner join tday_status ts on ts.TDAY_STATUS_ID=t.TDAY_STATUS_ID  where t.TDAY_DATE  between convert(datetime,'" + strdbFrmdt + "',103) and Convert(datetime,'" + strdbtodt + "',103)  AND t.tday_empcde= '" + ESS_LA_EMPID + "'"; ;

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
                    if (x.Rows[i]["tday_status"].ToString() == "ABW2" || x.Rows[i]["tday_status"].ToString() == "ABWO")
                    {
                        strmessage = "Cannot apply Leave on weekly off";

                    }
                    else if (x.Rows[i]["tday_status"].ToString() == "ABHO")
                    {
                        strmessage = "Cannot apply Leave on holiday";
                    }
                }
            }
            return strmessage;
        }

        /*--------------------------------LEAVE_RULE_VALIDATION-------------------------------*/

        public string LEAVE_RULE_VALIDATION(double LvDays, string ESS_LA_LVCD, int ESS_LA_EMPID, DateTime strdbFrmdt, DateTime strdbtodt)
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
                    "WHERE ENT_EMPLOYEE_DTLS.EMPLOYEE_ID=" + ESS_LA_EMPID + "  " +
                    "AND  " +
                    "TA_LEAVE_RULE_NEW.LeaveID='" + ESS_LA_LVCD + "' "; ;

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
                    string queryStatus = "select ts.TDAY_STATUS as TDAY_STATUS from tday_status ts inner join TDAY t on ts.TDAY_STATUS_ID=t.TDAY_STATUS_ID WHERE t.TDAY_DATE = '" + strdbFrmdt + "' AND t.TDAY_EMPCDE =  '" + ESS_LA_EMPID + "'";
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
                    string queryStatus = "select ts.TDAY_STATUS as TDAY_STATUS from tday_status ts inner join TDAY t on ts.TDAY_STATUS_ID=t.TDAY_STATUS_ID WHERE t.TDAY_DATE = '" + strdbtodt + "' AND t.TDAY_EMPCDE =  '" + ESS_LA_EMPID + "'";
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
                string queryStatus = "select ts.TDAY_STATUS as TDAY_STATUS from tday_status ts inner join TDAY t on ts.TDAY_STATUS_ID=t.TDAY_STATUS_ID WHERE t.TDAY_DATE = '" + strdbtodt + "' AND t.TDAY_EMPCDE =  '" + ESS_LA_EMPID + "'";
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
        /*--------------------------------LEAVE_RULE_VALIDATION-------------------------------*/
    }
}
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
    public interface ILeaveRuleService
    {
        Task<int> Create(LeaveRule entity, string ipaddress, int activeuser);
        IQueryable<LeaveRule> GetLeaveRule();
        LeaveRule GetLeaveRule(int id);
        Task<int> Edit(LeaveRule entity, string ipaddress, int activeuser);
        Task<int> Delete(int id, string ipaddress, int activeuser);
    }
    public class LeaveRuleService : ILeaveRuleService
    {
        private UnitOfWork _unitOfWork;
        private DatabaseHelper _DatabaseHelper;
        public LeaveRuleService(IUnitOfWork unitOfWork, DatabaseHelper databaseHelper)
        {
            _unitOfWork = (UnitOfWork)unitOfWork;
            _DatabaseHelper = databaseHelper;
        }

        public LeaveRule GetLeaveRule(int id)
        {
            string query = " select LR_REC_ID,LR_CODE,LR_CATEGORYID,LR_ALLOTMENT,LR_ACCUMULATION,LR_ISDELETED,LR_DELETEDDATE,LeaveID,LR_DAYS,LEAVE_RULE,LR_GreaterOrLesser,LR_MinDaysAllowed,LR_AllotmentType,LR_AllotmentType_YE_PR,LR_MaxDaysAllowed,COMPANY_ID from TA_LEAVE_RULE_NEW where LR_ISDELETED='0' and LR_REC_ID  =" + id;

            var x = _DatabaseHelper.GetDataTable(query, CommandType.Text);
            LeaveRule entParamsList = new LeaveRule();

            foreach (DataRow dr in x.Rows)
            {
                entParamsList.LR_REC_ID = Convert.ToInt32(dr["LR_REC_ID"]);
                entParamsList.LR_CODE = Convert.ToString(dr["LR_CODE"]);
                entParamsList.LR_CATEGORYID = Convert.ToString(dr["LR_CATEGORYID"]);
                entParamsList.LR_ALLOTMENT = Convert.ToDouble(dr["LR_ALLOTMENT"]);
                entParamsList.LR_ACCUMULATION = Convert.ToDouble(dr["LR_ACCUMULATION"]);
                entParamsList.LeaveID = Convert.ToString(dr["LeaveID"]);
                entParamsList.LR_DAYS = Convert.ToString(dr["LR_DAYS"]);
                entParamsList.LEAVE_RULE = Convert.ToString(dr["LEAVE_RULE"]);
                entParamsList.LR_GreaterOrLesser = Convert.ToString(dr["LR_GreaterOrLesser"]);
                entParamsList.LR_MinDaysAllowed = Convert.ToDouble(dr["LR_MinDaysAllowed"]);
                entParamsList.LR_AllotmentType = Convert.ToString(dr["LR_AllotmentType"]);
                entParamsList.LR_AllotmentType_YE_PR = Convert.ToString(dr["LR_AllotmentType_YE_PR"]);
                entParamsList.LR_MaxDaysAllowed = Convert.ToDouble(dr["LR_MaxDaysAllowed"]);
                entParamsList.COMPANY_ID = Convert.ToInt32(dr["COMPANY_ID"]);
            }
            return entParamsList;
        }

        public IQueryable<LeaveRule> GetLeaveRule()
        {
            string query = "select LR_REC_ID,LR_CODE,LR_CATEGORYID,LR_ALLOTMENT,LR_ACCUMULATION,LR_ISDELETED,LR_DELETEDDATE,LeaveID,LR_DAYS,LEAVE_RULE,LR_GreaterOrLesser,LR_MinDaysAllowed,LR_AllotmentType,LR_AllotmentType_YE_PR,LR_MaxDaysAllowed,COMPANY_ID from TA_LEAVE_RULE_NEW where LR_ISDELETED='0'";
            var x = _DatabaseHelper.GetDataTable(query, CommandType.Text);
            List<LeaveRule> entParamsList = new List<LeaveRule>();

            foreach (DataRow dr in x.Rows)
            {
                LeaveRule enLeaveRule = new LeaveRule();
                enLeaveRule.LR_REC_ID = Convert.ToInt32(dr["LR_REC_ID"]);
                enLeaveRule.LR_CODE = Convert.ToString(dr["LR_CODE"]);
                enLeaveRule.LR_CATEGORYID = Convert.ToString(dr["LR_CATEGORYID"]);
                enLeaveRule.LR_ALLOTMENT = Convert.ToDouble(dr["LR_ALLOTMENT"]);
                enLeaveRule.LR_ACCUMULATION = Convert.ToDouble(dr["LR_ACCUMULATION"]);
                enLeaveRule.LeaveID = Convert.ToString(dr["LeaveID"]);
                enLeaveRule.LR_DAYS = Convert.ToString(dr["LR_DAYS"]);
                enLeaveRule.LEAVE_RULE = Convert.ToString(dr["LEAVE_RULE"]);
                enLeaveRule.LR_GreaterOrLesser = Convert.ToString(dr["LR_GreaterOrLesser"]);
                enLeaveRule.LR_MinDaysAllowed = Convert.ToDouble(dr["LR_MinDaysAllowed"]);
                enLeaveRule.LR_AllotmentType = Convert.ToString(dr["LR_AllotmentType"]);
                enLeaveRule.LR_AllotmentType_YE_PR = Convert.ToString(dr["LR_AllotmentType_YE_PR"]);
                enLeaveRule.LR_MaxDaysAllowed = Convert.ToDouble(dr["LR_MaxDaysAllowed"]);
                enLeaveRule.COMPANY_ID = Convert.ToInt32(dr["COMPANY_ID"]);
                entParamsList.Add(enLeaveRule);
            }
            return entParamsList.AsQueryable();
        }

        public async Task<int> Create(LeaveRule entity, string ipaddress, int activeuser)
        {
            string query = " insert into TA_LEAVE_RULE_NEW(LR_CODE,LR_CATEGORYID,LR_ALLOTMENT,LR_ACCUMULATION,LR_ISDELETED,LeaveID,LR_DAYS,LEAVE_RULE,LR_GreaterOrLesser,LR_MinDaysAllowed,LR_AllotmentType,LR_AllotmentType_YE_PR,LR_MaxDaysAllowed,COMPANY_ID)  ";
            query += " values ('" + entity.LR_CATEGORYID + "" + entity.LeaveID + "', '" + entity.LR_CATEGORYID + "'," + entity.LR_ALLOTMENT + "," + entity.LR_ACCUMULATION + ",0,'" + entity.LeaveID + "','" + entity.LR_DAYS + "','" + entity.LEAVE_RULE + "','" + entity.LR_GreaterOrLesser + "'," + entity.LR_MinDaysAllowed + ",'" + entity.LR_AllotmentType + "','" + entity.LR_AllotmentType_YE_PR + "'," + entity.LR_MaxDaysAllowed + "," + entity.COMPANY_ID + ")";
            query += " select @@Identity ;";
            return await _DatabaseHelper.Insert(query, CommandType.Text,ipaddress,activeuser);
        }

        public async Task<int> Edit(LeaveRule entity, string ipaddress, int activeuser)
        {
            string query = "Update TA_LEAVE_RULE_NEW set  LR_CATEGORYID='" + entity.LR_CATEGORYID + "',LR_ALLOTMENT=" + entity.LR_ALLOTMENT + ",LR_ACCUMULATION=" + entity.LR_ACCUMULATION + ",LeaveID='" + entity.LeaveID + "',LR_DAYS='" + entity.LR_DAYS + "',LEAVE_RULE='" + entity.LEAVE_RULE + "',LR_GreaterOrLesser='" + entity.LR_GreaterOrLesser + "',LR_MinDaysAllowed=" + entity.LR_MinDaysAllowed + ",LR_AllotmentType='" + entity.LR_AllotmentType + "',LR_AllotmentType_YE_PR='" + entity.LR_AllotmentType_YE_PR + "',LR_MaxDaysAllowed=" + entity.LR_MaxDaysAllowed + ",COMPANY_ID=" + entity.COMPANY_ID + " where LR_REC_ID =  " + entity.LR_REC_ID;
            return await _DatabaseHelper.Insert(query, CommandType.Text, ipaddress, activeuser);
        }

        public async Task<int> Delete(int id, string ipaddress, int activeuser)
        {
            string query = "Update TA_LEAVE_RULE_NEW set LR_ISDELETED = 1,LR_DELETEDDATE='" + DateTime.Now.ToString("dd/MMM/yyyy hh:mm") + "' where LR_REC_ID = " + id;
            return await _DatabaseHelper.Insert(query, CommandType.Text, ipaddress, activeuser);
        }
    }
}
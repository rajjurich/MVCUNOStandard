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
    public interface ILeaveFileService
    {
        Task<int> Create(LeaveFile entity, string ipaddress, int activeuser);
        IQueryable<LeaveFile> GetLeaveFile();
        LeaveFile GetLeaveFile(int id);
        Task<int> Edit(LeaveFile entity, string ipaddress, int activeuser);
        Task<int> Delete(int id, string ipaddress, int activeuser);
    }
    public class LeaveFileService : ILeaveFileService
    {
        private UnitOfWork _unitOfWork;
        private DatabaseHelper _DatabaseHelper;
        public LeaveFileService(IUnitOfWork unitOfWork, DatabaseHelper databaseHelper)
        {
            _unitOfWork = (UnitOfWork)unitOfWork;
            _DatabaseHelper = databaseHelper;
        }

        public LeaveFile GetLeaveFile(int id)
        {
            string query = " select tl.Leave_File_ID,tl.Leave_CODE,tl.Leave_Description,tl.Leave_IsPaid,case when tl.Leave_IsPaid='1' then 'YES'  when tl.Leave_IsPaid='0' then 'No' end as Leave_IsPaid_desc,tl.Leave_Group,ep.value as Leave_Group_Desc,COMPANY_ID from TA_Leave_File tl inner join ENT_PARAMS ep on tl.Leave_Group=ep.CODE  where tl.Leave_ISDELETED='0' and ep.IDENTIFIER='LEAVE_TYPE' and  tl.Leave_File_ID  =" + id;

            var x = _DatabaseHelper.GetDataTable(query, CommandType.Text);
            LeaveFile entParamsList = new LeaveFile();

            foreach (DataRow dr in x.Rows)
            {
                entParamsList.Leave_File_ID = Convert.ToInt32(dr["Leave_File_ID"]);
                entParamsList.Leave_CODE = Convert.ToString(dr["Leave_CODE"]);
                entParamsList.Leave_Description = Convert.ToString(dr["Leave_Description"]);
                entParamsList.Leave_IsPaid = Convert.ToInt32(dr["Leave_IsPaid"]);
                entParamsList.Leave_Group = Convert.ToString(dr["Leave_Group"]);
                entParamsList.Leave_IsPaid_desc = Convert.ToString(dr["Leave_IsPaid_desc"]);
                entParamsList.Leave_Group_Desc = Convert.ToString(dr["Leave_Group_Desc"]);
                entParamsList.COMPANY_ID = Convert.ToInt32(dr["COMPANY_ID"]);
            }
            return entParamsList;
        }

        public IQueryable<LeaveFile> GetLeaveFile()
        {
            string query = " select tl.Leave_File_ID,tl.Leave_CODE,tl.Leave_Description,tl.Leave_IsPaid,case when tl.Leave_IsPaid='1' then 'YES'  when tl.Leave_IsPaid='0' then 'No' end as Leave_IsPaid_desc,tl.Leave_Group,ep.value as Leave_Group_Desc,COMPANY_ID from TA_Leave_File tl inner join ENT_PARAMS ep on tl.Leave_Group=ep.CODE  where tl.Leave_ISDELETED='0' and ep.IDENTIFIER='LEAVE_TYPE'";
            var x = _DatabaseHelper.GetDataTable(query, CommandType.Text);
            List<LeaveFile> entParamsList = new List<LeaveFile>();

            foreach (DataRow dr in x.Rows)
            {
                LeaveFile enLeaveFile = new LeaveFile();
                enLeaveFile.Leave_File_ID = Convert.ToInt32(dr["Leave_File_ID"]);
                enLeaveFile.Leave_CODE = Convert.ToString(dr["Leave_CODE"]);
                enLeaveFile.Leave_Description = Convert.ToString(dr["Leave_Description"]);
                enLeaveFile.Leave_IsPaid = Convert.ToInt32(dr["Leave_IsPaid"]);
                enLeaveFile.Leave_Group = Convert.ToString(dr["Leave_Group"]);
                enLeaveFile.Leave_IsPaid_desc = Convert.ToString(dr["Leave_IsPaid_desc"]);
                enLeaveFile.Leave_Group_Desc = Convert.ToString(dr["Leave_Group_Desc"]);
                enLeaveFile.COMPANY_ID = Convert.ToInt32(dr["COMPANY_ID"]);
                entParamsList.Add(enLeaveFile);
            }
            return entParamsList.AsQueryable(); 
        }

        public async Task<int> Create(LeaveFile entity, string ipaddress, int activeuser)
        {
            string query = " insert into TA_Leave_File(Leave_CODE,Leave_Description,Leave_IsPaid,Leave_Group,IS_SYNC,COMPANY_ID,Leave_ISDELETED,Leave_IsProDataBasiss)  ";
                   query += " values ('" + entity.Leave_CODE + "', '" + entity.Leave_Description + "', " + entity.Leave_IsPaid + ",'" + entity.Leave_Group + "',0," + entity.COMPANY_ID + ",0,0)";
                   query += " select @@Identity ;";
            return await _DatabaseHelper.Insert(query, CommandType.Text,ipaddress,activeuser);
        }

        public async Task<int> Edit(LeaveFile entity, string ipaddress, int activeuser)
        {
            string query = "Update TA_Leave_File set Leave_CODE = '" + entity.Leave_CODE + "',Leave_Description='" + entity.Leave_Description + "',Leave_IsPaid=" + entity.Leave_IsPaid + ",Leave_Group='" + entity.Leave_Group + "' where Leave_File_ID =  " + entity.Leave_File_ID;
            return await _DatabaseHelper.Insert(query, CommandType.Text, ipaddress, activeuser);
        }

        public async Task<int> Delete(int id, string ipaddress, int activeuser)
        {
            string query = "Update TA_Leave_File set Leave_ISDELETED = 1,Leave_DELETEDDATE='" + DateTime.Now.ToString("dd/MMM/yyyy hh:mm") + "' where Leave_File_ID = " + id;
            return await _DatabaseHelper.Insert(query, CommandType.Text, ipaddress, activeuser);
        }
    }
}
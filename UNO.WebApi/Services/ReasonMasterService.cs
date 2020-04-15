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
    public interface IReasonMasterService
    {
        Task<int> Create(ReasonMaster entity, string ipaddress, int activeuser);
        Task<int> Edit(ReasonMaster entity, string ipaddress, int activeuser);
        Task<int> Delete(int id, string ipaddress, int activeuser);
        List<ReasonMaster> GetReasonMaster();
        ReasonMaster GetReasonMaster(int id);

        List<Reason> GetReasonByType(string LeaveType);
        Task<bool> IsUniqReasonCode(ReasonMaster entity, bool isEdit);
    }
    public class ReasonMasterService : IReasonMasterService
    {
         private UnitOfWork _unitOfWork;
        private DatabaseHelper _DatabaseHelper;

        public ReasonMasterService(IUnitOfWork unitOfWork, DatabaseHelper databaseHelper)
        {
            _unitOfWork = (UnitOfWork)unitOfWork;
            _DatabaseHelper = databaseHelper;
        }

        public async Task<int> Create(ReasonMaster entity, string ipaddress, int activeuser)
        {
            string query = " INSERT INTO ENT_REASON ( REASON_CODE,REASON_DESC,REASON_ISDELETED,REASON_TYPE_ID,COMPANY_ID) "
                            + " VALUES ('" + entity.REASON_CODE + "', '" + entity.REASON_DESC + "',  0, '" + Convert.ToInt32(entity.REASON_TYPE_ID) + "', '" + Convert.ToInt32(entity.COMPANY_ID) + "')";

            query += " select @@Identity";
            return await _DatabaseHelper.Insert(query, CommandType.Text,ipaddress,activeuser);
        }

        public async Task<int> Edit(ReasonMaster entity, string ipaddress, int activeuser)
        {
            string query = " UPDATE ENT_REASON SET REASON_CODE = '" + entity.REASON_CODE + "' , REASON_DESC ='" + entity.REASON_DESC + "' WHERE REASON_ID =  " + entity.REASON_ID;
            return await _DatabaseHelper.Insert(query, CommandType.Text, ipaddress, activeuser);
        }

        public async Task<int> Delete(int id, string ipaddress, int activeuser)
        {
            string query = "UPDATE ENT_REASON SET REASON_ISDELETED = 1  WHERE REASON_ID = " + id;
            return await _DatabaseHelper.Insert(query, CommandType.Text, ipaddress, activeuser);
        }

        public List<ReasonMaster> GetReasonMaster()
        {
            string query = " select REASON_ID,REASON_CODE,REASON_DESC, REASON_TYPE_ID, (select REASON_DESC From ent_reason_type where REASON_TYPE_ID=common_entities.REASON_TYPE_ID)as ent_REASON_DESC, (select COMPANY_NAME From ENT_COMPANY where company_id=common_entities.COMPANY_ID)as COMPANY_NAME  From ENT_REASON common_entities where REASON_ISDELETED=0 ";

            var x = _DatabaseHelper.GetDataTable(query, CommandType.Text);
            List<ReasonMaster> entParamsList = new List<ReasonMaster>();

            foreach (DataRow dr in x.Rows)
            {
                ReasonMaster entity = new ReasonMaster();
                entity.REASON_ID = Convert.ToInt32(dr["REASON_ID"]);
                entity.REASON_CODE = Convert.ToString(dr["REASON_CODE"]);
                entity.REASON_DESC = Convert.ToString(dr["REASON_DESC"]);
                entity.REASON_TYPE_ID = Convert.ToInt32(dr["REASON_TYPE_ID"]);
                entity.REASON_TYPE = Convert.ToString(dr["ent_REASON_DESC"]);
                entity.COMPANY_NAME = Convert.ToString(dr["COMPANY_NAME"]);
                entParamsList.Add(entity);
            }
            return entParamsList;
        }

        public ReasonMaster GetReasonMaster(int id)
        {
            string query = " select REASON_ID,REASON_CODE,REASON_DESC, REASON_TYPE_ID, (select REASON_DESC From ent_reason_type where REASON_TYPE_ID=common_entities.REASON_TYPE_ID)as ent_REASON_DESC, (select COMPANY_NAME From ENT_COMPANY where company_id=common_entities.COMPANY_ID)as COMPANY_NAME,COMPANY_ID  From ENT_REASON common_entities WHERE REASON_ISDELETED =0 and REASON_ID = " + id;

            var x = _DatabaseHelper.GetDataTable(query, CommandType.Text);
            ReasonMaster entity = new ReasonMaster();

            foreach (DataRow dr in x.Rows)
            {
                entity.REASON_ID = Convert.ToInt32(dr["REASON_ID"]);
                entity.REASON_CODE = Convert.ToString(dr["REASON_CODE"]);
                entity.REASON_DESC = Convert.ToString(dr["REASON_DESC"]);
                entity.REASON_TYPE_ID = Convert.ToInt32(dr["REASON_TYPE_ID"]);
                entity.REASON_TYPE = Convert.ToString(dr["ent_REASON_DESC"]);
                entity.COMPANY_NAME = Convert.ToString(dr["COMPANY_NAME"]);
                entity.COMPANY_ID = Convert.ToInt32(dr["COMPANY_ID"]);
            }
            return entity;
        }


        public List<Reason> GetReasonByType(string LeaveType)
        {
            string query = "select ER.REASON_ID as REASON_ID ,ER.REASON_DESC as REASON_DESC from ENT_REASON ER INNER JOIN ENT_REASON_TYPE ERT ON ER.REASON_TYPE_ID=ERT.REASON_TYPE_ID WHERE ERT.REASON_TYPE='" + LeaveType + "'";

            var x = _DatabaseHelper.GetDataTable(query, CommandType.Text);
            List<Reason> entReasonList = new List<Reason>();

            foreach (DataRow dr in x.Rows)
            {
                Reason entReason = new Reason();
                entReason.REASON_ID = Convert.ToString(dr["REASON_ID"]);
                entReason.REASON_DESC = Convert.ToString(dr["REASON_DESC"]);

                entReasonList.Add(entReason);
            }
            return entReasonList;
        }

        public async Task<bool> IsUniqReasonCode(ReasonMaster entity, bool isEdit)
        {
            string query = "SELECT count(REASON_CODE) FROM ENT_REASON WHERE REASON_CODE = '" + entity.REASON_CODE + "' and COMPANY_ID = " + entity.COMPANY_ID + " and REASON_TYPE_ID = "+ entity.REASON_TYPE_ID +" and REASON_ISDELETED = 0";
            if (isEdit)
                query = query + " and REASON_ID <>" + entity.REASON_ID;

            var x = await _DatabaseHelper.GetScalarValue(query, CommandType.Text) == 0 ? false : true;
            return x;

        }
    }
}
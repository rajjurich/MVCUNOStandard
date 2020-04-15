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
    public interface IReaderTemplateService
    {
        Task<int> Create(ReaderTemplate entity, string ipaddress, int activeuser);
        Task<int> Edit(ReaderTemplate entity, string ipaddress, int activeuser);
        Task<int> Delete(int id, string ipaddress, int activeuser);
        IQueryable<ReaderTemplate> GetReaderTemplate();
        ReaderTemplate GetReaderTemplate(int id);
        string GetChkReaderExist(int EventID, int ControllerID);
    }
    public class ReaderTemplateService : IReaderTemplateService
    {
         private UnitOfWork _unitOfWork;
        private DatabaseHelper _DatabaseHelper;
        public ReaderTemplateService(IUnitOfWork unitOfWork, DatabaseHelper databaseHelper)
        {
            _unitOfWork = (UnitOfWork)unitOfWork;
            _DatabaseHelper = databaseHelper;
        }
        public async Task<int> Create(ReaderTemplate entity, string ipaddress, int activeuser)
        {
            string query = " insert into ENT_Reader_MessageTemplate(ControllerID,EventID,EventMessage,isdeleted)  ";
            query += " values (" + entity.ControllerID + ", '" + entity.EventID + "','" + entity.EventMessage + "',0)";
            query += " select @@Identity ;";
            return await _DatabaseHelper.Insert(query, CommandType.Text,ipaddress,activeuser);
        }

        public async Task<int> Edit(ReaderTemplate entity, string ipaddress, int activeuser)
        {
            string query = "Update ENT_Reader_MessageTemplate set EventMessage = '" + entity.EventMessage + "'  where RowID =  " + entity.RowID;
            return await _DatabaseHelper.Insert(query, CommandType.Text, ipaddress, activeuser);
        }
        public async Task<int> Delete(int id, string ipaddress, int activeuser)
        {
            string query = "Update ENT_Reader_MessageTemplate set isdeleted = 1  where RowID = " + id ;
            return await _DatabaseHelper.Insert(query, CommandType.Text, ipaddress, activeuser);
        }
        public IQueryable<ReaderTemplate> GetReaderTemplate()
        {
            string query = " Select ERM.RowID,ERM.ControllerID,ERM.EventID,ERM.EventMessage,AC.CTLR_DESCRIPTION as ControllerName,EP.Value as EventName from ENT_Reader_MessageTemplate ERM inner join ACS_CONTROLLER AC on ERM.ControllerID= AC.ID inner join ENT_PARAMS EP on ERM.EventID=EP.PARAM_ID  where EP.identifier= 'EVENTMASTER' and EP.module='EVNT' and ERM.isdeleted='0'";
            var x = _DatabaseHelper.GetDataTable(query, CommandType.Text);
            List<ReaderTemplate> entParamsList = new List<ReaderTemplate>();

            foreach (DataRow dr in x.Rows)
            {
                ReaderTemplate enReaderTemplate = new ReaderTemplate();
                enReaderTemplate.RowID = Convert.ToInt32(dr["RowID"]);
                enReaderTemplate.EventID = Convert.ToInt32(dr["EventID"]);
                enReaderTemplate.ControllerID = Convert.ToInt32(dr["ControllerID"]);
                enReaderTemplate.EventMessage = Convert.ToString(dr["EventMessage"]);
                enReaderTemplate.ControllerName = Convert.ToString(dr["ControllerName"]);
                enReaderTemplate.EventName = Convert.ToString(dr["EventName"]);
                entParamsList.Add(enReaderTemplate);
            }
            return entParamsList.AsQueryable();
        }

        public ReaderTemplate GetReaderTemplate(int id)
        {
            string query = "Select ERM.RowID,ERM.ControllerID,ERM.EventID,ERM.EventMessage,AC.CTLR_DESCRIPTION as ControllerName,EP.Value as EventName from ENT_Reader_MessageTemplate ERM inner join ACS_CONTROLLER AC on ERM.ControllerID= AC.CTLR_ID inner join ENT_PARAMS EP on ERM.EventID=EP.PARAM_ID where EP.identifier= 'EVENTMASTER' and EP.module='EVNT' and ERM.isdeleted='0' and ERM.RowID =" + id;
            var x = _DatabaseHelper.GetDataTable(query, CommandType.Text);
            ReaderTemplate entParamsList = new ReaderTemplate();

            foreach (DataRow dr in x.Rows)
            {
                entParamsList.RowID = Convert.ToInt32(dr["RowID"]);
                entParamsList.EventID = Convert.ToInt32(dr["EventID"]);
                entParamsList.ControllerID = Convert.ToInt32(dr["ControllerID"]);
                entParamsList.EventMessage = Convert.ToString(dr["EventMessage"]);
                entParamsList.ControllerName = Convert.ToString(dr["ControllerName"]);
                entParamsList.EventName = Convert.ToString(dr["EventMessage"]);
            }
            return entParamsList;
        }

        public string GetChkReaderExist(int EventID, int ControllerID)
        {
            string query = "Select top 1 1 from ENT_Reader_MessageTemplate Where EventID=" + EventID + " and  ControllerID=" + ControllerID + " and isdeleted=0";
            var x = _DatabaseHelper.GetDataTable(query, CommandType.Text);

            string value;
            if (x.Rows.Count > 0)
            {
                value = "True";
            }
            else
            {
                value = "False";
            }
            return value;
        }
    }
}
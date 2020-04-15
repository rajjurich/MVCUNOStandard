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
    public interface IBioMetricTemplateConfigurationService
    {
        List<BioMetricTemplateConfigurationapimodel> GetFingureList();

        BioMetricTemplateConfigurationapimodel GetFingureList(int id);

        Task<int> Create(BioMetricTemplateConfigurationapimodel entity, string ipaddress, int activeuser);

        Task<int> Edit(BioMetricTemplateConfigurationapimodel entity, string ipaddress, int activeuser);

        Task<int> Delete(int id, string ipaddress, int activeuser);
    }
    public class BioMetricTemplateConfigurationService:IBioMetricTemplateConfigurationService
    {
        private UnitOfWork _unitOfWork;
        private DatabaseHelper _DatabaseHelper;


        public BioMetricTemplateConfigurationService(IUnitOfWork unitOfWork, DatabaseHelper databaseHelper)
        {
            _unitOfWork = (UnitOfWork)unitOfWork;
            _DatabaseHelper = databaseHelper;
        }

        public List<BioMetricTemplateConfigurationapimodel> GetFingureList()
        {
            string query = "select bio.id,bio.NoOfFingers,bio.FingureCount,bio.FingureForTA,ent.COMPANY_NAME from BioMetricTemplateConfiguration bio inner join ent_company ent on bio.Companyid=ent.COMPANY_ID where ent.COMPANY_ISDELETED=0 ";
            var x = _DatabaseHelper.GetDataTable(query, CommandType.Text);

            List<BioMetricTemplateConfigurationapimodel> biometriclist = new List<BioMetricTemplateConfigurationapimodel>();

            foreach(DataRow dr in x.Rows)
            {
                BioMetricTemplateConfigurationapimodel biometricobj = new BioMetricTemplateConfigurationapimodel();
                biometricobj.CompanyName = Convert.ToString(dr["COMPANY_NAME"]);
                biometricobj.Mycount = Convert.ToInt32(dr["NoOfFingers"]);
                biometricobj.FingureForAttandance = Convert.ToString(dr["FingureForTA"]);
                biometricobj.FingureForEnroll = Convert.ToString(dr["FingureCount"]);
                biometricobj.bio_metric_id = Convert.ToInt32(dr["id"]);
                biometriclist.Add(biometricobj);
            }
            return biometriclist;
        }

        public BioMetricTemplateConfigurationapimodel GetFingureList(int id)
        {
            string query = "select bio.id,bio.NoOfFingers,bio.FingureCount,bio.FingureForTA,ent.COMPANY_NAME from BioMetricTemplateConfiguration bio inner join ent_company ent on bio.Companyid=ent.COMPANY_ID where ent.COMPANY_ISDELETED=0 and bio.id="+id+"  ";

            var x = _DatabaseHelper.GetDataTable(query, CommandType.Text);
            BioMetricTemplateConfigurationapimodel biometricobj = new BioMetricTemplateConfigurationapimodel();

            foreach (DataRow dr in x.Rows)
            {
                biometricobj.CompanyName = Convert.ToString(dr["COMPANY_NAME"]);
                biometricobj.Mycount = Convert.ToInt32(dr["NoOfFingers"]);
                biometricobj.FingureForAttandance = Convert.ToString(dr["FingureForTA"]);
                biometricobj.FingureForEnroll = Convert.ToString(dr["FingureCount"]);
                biometricobj.bio_metric_id = Convert.ToInt32(dr["id"]);
            }

            return biometricobj;
        }

        public async Task<int> Create(BioMetricTemplateConfigurationapimodel entity, string ipaddress, int activeuser)
        {
            string query = "";
            if(entity.Enroll==false && entity.Time_Attandance==false)
            {
                query="";
            }
            else
            {
                query = "insert into BioMetricTemplateConfiguration values(" + entity.Mycount + ",'" + entity.FingureForEnroll + "','" + entity.FingureForAttandance + "','" + entity.COMPANY_ID + "')";
            }

            return await _DatabaseHelper.Insert(query, CommandType.Text, ipaddress, activeuser);
        }

        public async Task<int> Edit(BioMetricTemplateConfigurationapimodel entity, string ipaddress, int activeuser)
        {
            string query = "update BioMetricTemplateConfiguration set NoOfFingers= "+entity.Mycount+" , FingureCount= '"+entity.FingureForEnroll+"', FingureForTA ='"+entity.FingureForAttandance+"' where id="+entity.bio_metric_id+" ";

            return await _DatabaseHelper.Insert(query, CommandType.Text, ipaddress, activeuser);
           
        }

        public async Task<int> Delete(int id, string ipaddress, int activeuser)
        {

            string query = "DELETE FROM BioMetricTemplateConfiguration WHERE id="+id;

            return await _DatabaseHelper.Insert(query, CommandType.Text, ipaddress, activeuser);
            
        }
    }
}
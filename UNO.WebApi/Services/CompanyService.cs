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
    public interface ICompanyService
    {
        Task<int> Create(Company entity, string ipaddress, int activeuser);
        Task<int> Edit(Company entity, string ipaddress, int activeuser);
        Task<int> Delete(int id, string ipaddress, int activeuser);
        List<Company> GetCompany(int id);

        List<Company> GetCompanybio(int id);
        Company GetSingleCompany(int Cid);

        

        Task<bool> IsUniqCompanyCode(Company entity, bool isEdit);

    }


    public class CompanyService : ICompanyService
    {
        private UnitOfWork _unitOfWork;
        private DatabaseHelper _DatabaseHelper;
        private IUserService _userService;

        public CompanyService(IUnitOfWork unitOfWork
            , DatabaseHelper databaseHelper
            , IUserService userService)
        {
            _unitOfWork = (UnitOfWork)unitOfWork;
            _DatabaseHelper = databaseHelper;
            _userService = userService;
        }

        public async Task<int> Create(Company entity, string ipaddress, int activeuser)
        {
            string query = " INSERT INTO ent_company ( COMPANY_CODE,COMPANY_NAME,COMPANY_CREATEDDATE,COMPANY_ISDELETED,COMPANY_EMP_CODE_LENGTH ) "
                            + " VALUES ('" + entity.COMPANY_CODE + "', '" + entity.COMPANY_NAME + "', '" + DateTime.Now.ToString("dd/MMM/yyyy HH:mm") + "' ,0,"+entity.CompanyCodeLength+")";

            query += " select @@Identity";
            return await _DatabaseHelper.Insert(query, CommandType.Text, ipaddress,activeuser);
        }

        public async Task<int> Edit(Company entity, string ipaddress, int activeuser)
        {
            string query = " UPDATE ent_company SET COMPANY_CODE = '" + entity.COMPANY_CODE + "', COMPANY_NAME = '" + entity.COMPANY_NAME + "' , COMPANY_MODIFIEDDATE = '" + DateTime.Now.ToString("dd/MMM/yyyy HH:mm") + "' ,COMPANY_EMP_CODE_LENGTH="+entity.CompanyCodeLength+" WHERE COMPANY_ID =  " + entity.COMPANY_ID;
            return await _DatabaseHelper.Insert(query, CommandType.Text, ipaddress, activeuser);
        }

        public async Task<int> Delete(int id, string ipaddress, int activeuser)
        {
            string query = "UPDATE ent_company SET COMPANY_ISDELETED = 1 , COMPANY_DELETEDDATE = '" + DateTime.Now.ToString("dd/MMM/yyyy HH:mm") + "' WHERE COMPANY_ID = " + id;
            return await _DatabaseHelper.Insert(query, CommandType.Text, ipaddress, activeuser);
        }

        public List<Company> GetCompany(int id)
        {
            string query = string.Empty;
            User user = _userService.GetSingleUser(id);
            if (user.USER_CODE == "superuser")
            {
                query = " SELECT distinct COMPANY_ID, COMPANY_CODE, COMPANY_NAME FROM ent_company WHERE COMPANY_ISDELETED =0 ";
            }
            else
            {
                query = " SELECT distinct com.COMPANY_ID, COMPANY_CODE, COMPANY_NAME FROM ent_company com" +
                        " join ENT_ROLE_DATA_ACCESS da on com.COMPANY_ID=da.MAPPED_ENTITY_ID" +
                        " join ENT_ORG_COMMON_TYPES ct on ct.COMMON_TYPES_ID=da.COMMON_TYPES_ID and ct.COMMON_TYPES='COMPANY'" +
                        " WHERE COMPANY_ISDELETED =0 and da.USER_CODE=" + id + " ";
            }
            var x = _DatabaseHelper.GetDataTable(query, CommandType.Text);
            List<Company> entParamsList = new List<Company>();

            foreach (DataRow dr in x.Rows)
            {
                Company entCompany = new Company();
                entCompany.COMPANY_ID = Convert.ToInt16(dr["COMPANY_ID"]);
                entCompany.COMPANY_CODE = Convert.ToString(dr["COMPANY_CODE"]);
                entCompany.COMPANY_NAME = Convert.ToString(dr["COMPANY_NAME"]);
                entParamsList.Add(entCompany);
            }
            return entParamsList;
        }

        public Company GetSingleCompany(int Cid)
        {
            string query = " SELECT COMPANY_ID, COMPANY_CODE, COMPANY_NAME,COMPANY_EMP_CODE_LENGTH FROM ent_company WHERE COMPANY_ISDELETED =0 and COMPANY_ID = " + Cid;

            var x = _DatabaseHelper.GetDataTable(query, CommandType.Text);
            Company entParamsList = new Company();

            foreach (DataRow dr in x.Rows)
            {
                entParamsList.COMPANY_ID = Convert.ToInt16(dr["COMPANY_ID"]);
                entParamsList.COMPANY_CODE = Convert.ToString(dr["COMPANY_CODE"]);
                entParamsList.COMPANY_NAME = Convert.ToString(dr["COMPANY_NAME"]);
                entParamsList.CompanyCodeLength = Convert.ToInt32(dr["COMPANY_EMP_CODE_LENGTH"]);
            }
            return entParamsList;
        }



        public async Task<bool> IsUniqCompanyCode(Company entity, bool isEdit)
        {
            string query = "SELECT count(COMPANY_CODE) FROM ent_company WHERE COMPANY_ISDELETED =0 and COMPANY_CODE = '" + entity.COMPANY_CODE + "'";
            if (isEdit)
                query = query + " and COMPANY_ID<>" + entity.COMPANY_ID;

            var x = await _DatabaseHelper.GetScalarValue(query, CommandType.Text) == 0 ? false : true;
            return x;

        }





        public List<Company> GetCompanybio(int id)
        {
            string query = string.Empty;
            User user = _userService.GetSingleUser(id);
            if (user.USER_CODE == "superuser")
            {
                query = " SELECT distinct COMPANY_ID, COMPANY_CODE, COMPANY_NAME FROM ent_company as ent left join BioMetricTemplateConfiguration as bio on ent.COMPANY_ID=bio.Companyid and COMPANY_ISDELETED =0 where bio.Companyid is null ";
            }
            else
            {
                query = " SELECT distinct COMPANY_ID, COMPANY_CODE, COMPANY_NAME FROM ent_company as ent left join BioMetricTemplateConfiguration as bio on ent.COMPANY_ID=bio.Companyid and COMPANY_ISDELETED =0 where bio.Companyid is null and ent.COMPANY_ID="+id+"  ";
            }
            var x = _DatabaseHelper.GetDataTable(query, CommandType.Text);
            List<Company> entParamsList = new List<Company>();

            foreach (DataRow dr in x.Rows)
            {
                Company entCompany = new Company();
                entCompany.COMPANY_ID = Convert.ToInt16(dr["COMPANY_ID"]);
                entCompany.COMPANY_CODE = Convert.ToString(dr["COMPANY_CODE"]);
                entCompany.COMPANY_NAME = Convert.ToString(dr["COMPANY_NAME"]);
                entParamsList.Add(entCompany);
            }
            return entParamsList;
        }
    }
}
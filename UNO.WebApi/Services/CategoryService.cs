using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using UNO.DAL;
using UNO.WebApi.Dto;
using UNO.WebApi.Models;

namespace UNO.WebApi.Services
{
    public interface ICategoryService
    {
        Task<int> Create(Category entity, string ipaddress, int activeuser);
        Task<int> Edit(Category entity, string ipaddress, int activeuser);
        Task<int> Delete(int id, string user, string ipaddress, int activeuser);
        IQueryable<CategoryDto> Get(int id);
        Task<Category> GetSingle(int id);
    }
    public class CategoryService : ICategoryService
    {
        private UnitOfWork _unitOfWork;
        private DatabaseHelper _DatabaseHelper;
        private IUserService _userService;
        public CategoryService(IUnitOfWork unitOfWork
            , DatabaseHelper databaseHelper
            , IUserService userService)
        {
            _unitOfWork = (UnitOfWork)unitOfWork;
            _DatabaseHelper = databaseHelper;
            _userService = userService;
        }
        public async Task<int> Create(Category entity, string ipaddress, int activeuser)
        {

            string query = " Insert into ENT_CATEGORY (ORG_CATEGORY_ID,EARLY_GOING,LATE_COMING,EXTRA_CHECK,EXHRS_BEFORE_SHIFT_HRS," +
                          " EXHRS_AFTER_SHIFT_HRS,COMPENSATORYOFF_CODE,DED_FROM_EXHRS_EARLY_GOING,DED_FROM_EXHRS_LATE_COMING, " +
                          " CREATEDDATE,CREATEDBY,ISDELETED,COMPANY_ID) " +
                          " Values ('" + entity.ORG_CATEGORY_ID + "','" + entity.EARLY_GOING + "','" + entity.LATE_COMING + "', " +
                          " '" + entity.EXTRA_CHECK + "' ,'" + entity.EXHRS_BEFORE_SHIFT_HRS + "','" + entity.EXHRS_AFTER_SHIFT_HRS + "'," +
                          " '" + entity.COMPENSATORYOFF_CODE + "' , '" + entity.DED_FROM_EXHRS_EARLY_GOING + "', '" + entity.DED_FROM_EXHRS_LATE_COMING + "', " +
                          " '" + DateTime.Now.ToString("dd/MMM/yyyy hh:mm:ss") + "','" + entity.CREATEDBY + "',0,'" + entity.COMPANY_ID + "') ";
            query += " select @@Identity ;";
            return await _DatabaseHelper.Insert(query, CommandType.Text, ipaddress,activeuser);
        }

        public async Task<int> Edit(Category entity, string ipaddress, int activeuser)
        {
            string query = " update ENT_CATEGORY set ORG_CATEGORY_ID = '" + entity.ORG_CATEGORY_ID + "'" +
                          " ,EARLY_GOING = '" + entity.EARLY_GOING + "' " +
                          " ,LATE_COMING = '" + entity.LATE_COMING + "',EXTRA_CHECK='" + entity.EXTRA_CHECK + "'" +
                          " ,EXHRS_BEFORE_SHIFT_HRS='" + entity.EXHRS_BEFORE_SHIFT_HRS + "',EXHRS_AFTER_SHIFT_HRS='" + entity.EXHRS_AFTER_SHIFT_HRS + "'" +
                          " ,COMPENSATORYOFF_CODE='" + entity.COMPENSATORYOFF_CODE + "',DED_FROM_EXHRS_EARLY_GOING='" + entity.DED_FROM_EXHRS_EARLY_GOING + "'" +
                          " ,DED_FROM_EXHRS_LATE_COMING='" + entity.DED_FROM_EXHRS_LATE_COMING + "' " +
                          " ,COMPANY_ID = '" + entity.COMPANY_ID + "'" +
                          " ,MODIFIEDDATE='" + DateTime.Now.ToString("dd/MMM/yyyy hh:mm:ss") + "', MODIFIEDBY='" + entity.MODIFIEDBY + "'" +
                          " where CATEGORY_ID =  " + entity.CATEGORY_ID;

            return await _DatabaseHelper.Insert(query, CommandType.Text, ipaddress, activeuser);
        }

        public async Task<int> Delete(int id, string user, string ipaddress, int activeuser)
        {
            string query = " update ENT_CATEGORY set ISDELETED = 1,DELETEDDATE='" + DateTime.Now.ToString("dd/MMM/yyyy hh:mm:ss") + "',DELETEDBY='" + user + "'" +
                           " where CATEGORY_ID =  " + id;

            return await _DatabaseHelper.Insert(query, CommandType.Text, ipaddress, activeuser);
        }

        public IQueryable<CategoryDto> Get(int id)
        {
            string query = string.Empty;
            User user = _userService.GetSingleUser(id);
            if (user.USER_CODE == "superuser")
            {
                query = " select CATEGORY_ID,OCE_DESCRIPTION,ORG_CATEGORY_ID,EARLY_GOING,LATE_COMING"+
                        " ,EXHRS_BEFORE_SHIFT_HRS,EXHRS_AFTER_SHIFT_HRS,COMPENSATORYOFF_CODE,org.COMPANY_ID" +
                        " from ent_category cat join ENT_ORG_COMMON_ENTITIES org " +
                        " on cat.ORG_CATEGORY_ID=org.ID where ISDELETED =0 ";
            }
            else
            {
                query = " select CATEGORY_ID,OCE_DESCRIPTION,ORG_CATEGORY_ID,EARLY_GOING,LATE_COMING" +
                        " ,EXHRS_BEFORE_SHIFT_HRS,EXHRS_AFTER_SHIFT_HRS,COMPENSATORYOFF_CODE,org.COMPANY_ID" +
                        " from ent_category cat join ENT_ORG_COMMON_ENTITIES org " +
                        " on cat.ORG_CATEGORY_ID=org.ID where ISDELETED =0 and cat.COMPANY_ID in( " +
                        CustomQueryies.Queries.RoleDataAccessQuery(id) + " ) ";
            }
            
            var dataTable = _DatabaseHelper.GetDataTable(query, CommandType.Text);
            List<CategoryDto> categoryList = new List<CategoryDto>();

            if (dataTable.Rows.Count > 0)
            {
                foreach (DataRow dr in dataTable.Rows)
                {
                    CategoryDto category = new CategoryDto();
                    category.CATEGORY_ID = Convert.ToInt32(dr["CATEGORY_ID"]);
                    category.OCE_DESCRIPTION = Convert.ToString(dr["OCE_DESCRIPTION"]);                    
                    category.ORG_CATEGORY_ID = Convert.ToInt32(dr["ORG_CATEGORY_ID"]);
                    category.EARLY_GOING = Convert.ToDateTime(dr["EARLY_GOING"]).TimeOfDay;
                    category.LATE_COMING = Convert.ToDateTime(dr["LATE_COMING"]).TimeOfDay;
                    //category.EXTRA_CHECK = Convert.ToBoolean(dr["EXTRA_CHECK"]);
                    category.EXHRS_BEFORE_SHIFT_HRS = Convert.ToDateTime(dr["EXHRS_BEFORE_SHIFT_HRS"]).TimeOfDay;
                    category.EXHRS_AFTER_SHIFT_HRS = Convert.ToDateTime(dr["EXHRS_AFTER_SHIFT_HRS"]).TimeOfDay;
                    category.COMPENSATORYOFF_CODE = Convert.ToString(dr["COMPENSATORYOFF_CODE"]);                  
                    category.COMPANY_ID = Convert.ToInt32(dr["COMPANY_ID"]);
                    categoryList.Add(category);
                }
            }

            return categoryList.AsQueryable();
        }

        public Task<Category> GetSingle(int id)
        {
            string query = "select * from ent_category where CATEGORY_ID = " + id;
          

          
            var dataSet = _DatabaseHelper.GetDataSet(query, CommandType.Text);

            var dataTableForCategory = dataSet.Tables[0];

            Category category = new Category();

            if (dataTableForCategory.Rows.Count > 0)
            {
                foreach (DataRow dr in dataTableForCategory.Rows)
                {

                    category.CATEGORY_ID = Convert.ToInt32(dr["CATEGORY_ID"]);
                    category.ORG_CATEGORY_ID = Convert.ToInt32(dr["ORG_CATEGORY_ID"]);
                    category.EARLY_GOING = Convert.ToDateTime(dr["EARLY_GOING"]).TimeOfDay;
                    category.LATE_COMING = Convert.ToDateTime(dr["LATE_COMING"]).TimeOfDay;
                    category.EXTRA_CHECK = Convert.ToBoolean(dr["EXTRA_CHECK"]);
                    category.EXHRS_BEFORE_SHIFT_HRS = Convert.ToDateTime(dr["EXHRS_BEFORE_SHIFT_HRS"]).TimeOfDay;
                    category.EXHRS_AFTER_SHIFT_HRS = Convert.ToDateTime(dr["EXHRS_AFTER_SHIFT_HRS"]).TimeOfDay;
                    category.COMPENSATORYOFF_CODE = Convert.ToString(dr["COMPENSATORYOFF_CODE"]);
                    category.DED_FROM_EXHRS_EARLY_GOING = Convert.ToBoolean(dr["DED_FROM_EXHRS_EARLY_GOING"]);
                    category.DED_FROM_EXHRS_LATE_COMING = Convert.ToBoolean(dr["DED_FROM_EXHRS_LATE_COMING"]);
                    category.COMPANY_ID = Convert.ToInt32(dr["COMPANY_ID"]);
                    category.ISDELETED = Convert.ToBoolean(dr["ISDELETED"]);

                }
            }

            
            return Task.Run(() =>
            {
                return category;
            });
        }
    }
}
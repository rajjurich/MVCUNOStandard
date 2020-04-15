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
    public interface IWeeklyOffService
    {
        Task<int> Create(WeeklyOff entity, string ipaddress, int activeuser);
        IQueryable<WeeklyOff> GetWeeklyOff(int id);
        WeeklyOff GetWeeklyOffSingle(int id);

        List<WeeklyOff> GetWeekOffAttandance(int id);

        List<WeeklyOff> GetWeekEndAttandance(int id);
        Task<int> Edit(WeeklyOff entity, string ipaddress, int activeuser);
        Task<int> Delete(int id, string ipaddress, int activeuser);
    }
    public class WeeklyOffService : IWeeklyOffService
    {
        private UnitOfWork _unitOfWork;
        private DatabaseHelper _DatabaseHelper;
        private IUserService _userService;
        public WeeklyOffService(IUnitOfWork unitOfWork, DatabaseHelper databaseHelper, IUserService userService)
        {
            _unitOfWork = (UnitOfWork)unitOfWork;
            _DatabaseHelper = databaseHelper;
            _userService = userService;
        }

        public async Task<int> Create(WeeklyOff entity, string ipaddress, int activeuser)
        {
            string MWKPAT="";
            if (entity.FSTMWK == true)
            {
                 MWKPAT =MWKPAT+ "1,";
            }
            if (entity.SECMWK == true)
            {
                MWKPAT = MWKPAT + "2,";
            }
            if (entity.THDMWK == true)
            {
                MWKPAT = MWKPAT + "3,";
            }
            if (entity.FURMWK == true)
            {
                MWKPAT = MWKPAT + "4,";
            }
            if (entity.FIFMWK == true)
            {
                MWKPAT = MWKPAT + "5,";
            }

            MWKPAT = MWKPAT.TrimEnd(',');

            if (MWKPAT != "")
            {
                string query = " insert into TA_WKLYOFF(MWK_DESC,MWK_OFF,MWK_DAY,MWK_PAT,IS_SYNC,COMPANY_ID,CREATEDBY,CREATEDDATE)  ";
                query += " values ('" + entity.MWK_DESC + "', " + entity.MWK_OFF + ", " + entity.MWK_DAY + ",'" + MWKPAT + "',0," + entity.COMPANY_ID + ",'admin','" + DateTime.Now.ToString("dd/MMM/yyyy hh:mm") + "')";
                query += " select @@Identity ;";
                return await _DatabaseHelper.Insert(query, CommandType.Text,ipaddress,activeuser);
            }
            else
            {
                string query = " insert into TA_WKLYOFF(MWK_DESC,MWK_OFF,MWK_DAY,MWK_PAT,IS_SYNC,COMPANY_ID,CREATEDBY,CREATEDDATE)  ";
                query += " values ('" + entity.MWK_DESC + "', " + entity.MWK_OFF + ", " + entity.MWK_DAY + ",'" + entity.MWK_PAT + "',0," + entity.COMPANY_ID + ",'admin','" + DateTime.Now.ToString("dd/MMM/yyyy hh:mm") + "')";
            query += " select @@Identity ;";
            return await _DatabaseHelper.Insert(query, CommandType.Text, ipaddress, activeuser);
            }
        }

          public IQueryable<WeeklyOff> GetWeeklyOff(int id)
          {
              string query = string.Empty;
              User user = _userService.GetSingleUser(id);
              if (user.USER_CODE == "superuser")
              {
                  query = " select MWK_CD,MWK_DESC,MWK_DAY,MWK_OFF,CASE WHEN MWK_DAY='1' THEN 'SUNDAY' WHEN MWK_DAY='2' THEN 'MONDAY' WHEN MWK_DAY='3' THEN 'TUESDAY' WHEN MWK_DAY='4' THEN 'WEDNESDAY'  WHEN MWK_DAY='5' THEN 'THURSDAY'  WHEN MWK_DAY='6' THEN 'FRIDAY'  WHEN MWK_DAY='7' THEN 'SATURDAY' END AS  MWK_DAY_NAME,CASE WHEN MWK_OFF='0' THEN 'WEEKOFF' WHEN MWK_OFF='1' THEN 'WEEKEND' END  AS MWK_OFF_NAME,COMPANY_NAME,MWK_DESC,case when MWK_PAT='1,2,3,4,5' then 'ALL'  when MWK_PAT='2,4' then 'Even' when MWK_PAT='1,3' then 'Odd' else 'UserDefined' end as  pattern  from TA_WKLYOFF inner join ENT_COMPANY on TA_WKLYOFF.COMPANY_ID=ENT_COMPANY.COMPANY_ID   where IsDeleted='0' ";
              }
              else
              {
                  query = " select MWK_CD,MWK_DESC,MWK_DAY,MWK_OFF,CASE WHEN MWK_DAY='1' THEN 'SUNDAY' WHEN MWK_DAY='2' THEN 'MONDAY' WHEN MWK_DAY='3' THEN 'TUESDAY' WHEN MWK_DAY='4' THEN 'WEDNESDAY'  WHEN MWK_DAY='5' THEN 'THURSDAY'  WHEN MWK_DAY='6' THEN 'FRIDAY'  WHEN MWK_DAY='7' THEN 'SATURDAY' END AS  MWK_DAY_NAME,CASE WHEN MWK_OFF='0' THEN 'WEEKOFF' WHEN MWK_OFF='1' THEN 'WEEKEND' END  AS MWK_OFF_NAME,COMPANY_NAME,MWK_DESC,case when MWK_PAT='1,2,3,4,5' then 'ALL'  when MWK_PAT='2,4' then 'Even' when MWK_PAT='1,3' then 'Odd' else 'UserDefined' end as  pattern  from TA_WKLYOFF inner join ENT_COMPANY on TA_WKLYOFF.COMPANY_ID=ENT_COMPANY.COMPANY_ID   where IsDeleted='0' and ENT_COMPANY.COMPANY_ID in( " +
                          CustomQueryies.Queries.RoleDataAccessQuery(id) + " ) ";
              }
              

              var x = _DatabaseHelper.GetDataTable(query, CommandType.Text);
              List<WeeklyOff> entParamsList = new List<WeeklyOff>();

              foreach (DataRow dr in x.Rows)
              {
                  WeeklyOff enWeeklyOff = new WeeklyOff();
                  enWeeklyOff.MWK_CD = Convert.ToInt32(dr["MWK_CD"]);
                  enWeeklyOff.MWK_DESC = Convert.ToString(dr["MWK_DESC"]);
                  enWeeklyOff.MWK_DAY = Convert.ToInt32(dr["MWK_DAY"]);
                  enWeeklyOff.MWK_OFF = Convert.ToInt32(dr["MWK_OFF"]);
                  enWeeklyOff.MWK_DAY_NAME = Convert.ToString(dr["MWK_DAY_NAME"]);
                  enWeeklyOff.MWK_OFF_NAME = Convert.ToString(dr["MWK_OFF_NAME"]);
                  enWeeklyOff.COMPANY_NAME = Convert.ToString(dr["COMPANY_NAME"]);
                  enWeeklyOff.MWK_DESC = Convert.ToString(dr["MWK_DESC"]);
                  enWeeklyOff.MWK_PAT = Convert.ToString(dr["pattern"]);
                  entParamsList.Add(enWeeklyOff);
              }
              return entParamsList.AsQueryable(); ;
          }

          public WeeklyOff GetWeeklyOffSingle(int id)
          {
              string query = " select MWK_CD,MWK_DESC,MWK_DAY,MWK_OFF,CASE WHEN MWK_DAY='1' THEN 'SUNDAY' WHEN MWK_DAY='2' THEN 'MONDAY' WHEN MWK_DAY='3' THEN 'TUESDAY' WHEN MWK_DAY='4' THEN 'WEDNESDAY'  WHEN MWK_DAY='5' THEN 'THURSDAY'  WHEN MWK_DAY='6' THEN 'FRIDAY'  WHEN MWK_DAY='7' THEN 'SATURDAY' END AS  MWK_DAY_NAME,CASE WHEN MWK_OFF='0' THEN 'WEEKOFF' WHEN MWK_OFF='1' THEN 'WEEKEND' END  AS MWK_OFF_NAME,MWK_PAT,COMPANY_ID  from TA_WKLYOFF where IsDeleted='0' and  MWK_CD  =" + id;

              var x = _DatabaseHelper.GetDataTable(query, CommandType.Text);
              WeeklyOff entParamsList = new WeeklyOff();

              foreach (DataRow dr in x.Rows)
              {
                  entParamsList.MWK_CD = Convert.ToInt32(dr["MWK_CD"]);
                  entParamsList.MWK_DESC = Convert.ToString(dr["MWK_DESC"]);
                  entParamsList.MWK_DAY = Convert.ToInt32(dr["MWK_DAY"]);
                  entParamsList.MWK_OFF = Convert.ToInt32(dr["MWK_OFF"]);
                  entParamsList.MWK_DAY_NAME = Convert.ToString(dr["MWK_DAY_NAME"]);
                  entParamsList.MWK_OFF_NAME = Convert.ToString(dr["MWK_OFF_NAME"]);
                  entParamsList.COMPANY_ID = Convert.ToInt32(dr["COMPANY_ID"]);

                  string strcheck = Convert.ToString(dr["MWK_PAT"]);
                  if (strcheck == "1,2,3,4,5" || strcheck == "2,4" || strcheck == "1,3")
                  {
                      entParamsList.MWK_PAT = Convert.ToString(dr["MWK_PAT"]);
                  }
                  else
                  {
                      entParamsList.MWK_PAT = "WeekEnd";


                      string[] values = strcheck.Split(',');
                      for (int i = 0; i < values.Length; i++)
                      {
                          values[i] = values[i].Trim();

                          string val = values[i];

                          if (val == "1")
                          {
                              entParamsList.FSTMWK = true;

                          }

                          if (val == "2")
                          {
                              entParamsList.SECMWK = true;

                          }

                          if (val == "3")
                          {
                              entParamsList.THDMWK = true;

                          }

                          if (val == "4")
                          {
                              entParamsList.FURMWK = true;

                          }

                          if (val == "5")
                          {
                              entParamsList.FIFMWK = true;

                          }
                      }
                  }
              }
              return entParamsList;
          }

          public async Task<int> Edit(WeeklyOff entity, string ipaddress, int activeuser)
          {
            string MWKPAT="";
            if (entity.FSTMWK == true)
            {
                 MWKPAT =MWKPAT+ "1,";
            }
            if (entity.SECMWK == true)
            {
                MWKPAT = MWKPAT + "2,";
            }
            if (entity.THDMWK == true)
            {
                MWKPAT = MWKPAT + "3,";
            }
            if (entity.FURMWK == true)
            {
                MWKPAT = MWKPAT + "4,";
            }
            if (entity.FIFMWK == true)
            {
                MWKPAT = MWKPAT + "5,";
            }

            if (entity.FSTMWK == false && entity.SECMWK == false && entity.THDMWK == false && entity.FURMWK == false && entity.FIFMWK == false)
            {
                MWKPAT = MWKPAT + " ";
            }
            MWKPAT = MWKPAT.TrimEnd(',');

            if (MWKPAT != "")
            {
                string query = "Update TA_WKLYOFF set MWK_DESC = '" + entity.MWK_DESC + "',MWK_OFF=" + entity.MWK_OFF + ",MWK_DAY=" + entity.MWK_DAY + ",MWK_PAT='" + MWKPAT + "',MODIFIEDBY='admin',MODIFIEDDATE='" + DateTime.Now.ToString("dd/MMM/yyyy hh:mm") + "'  where MWK_CD =  " + entity.MWK_CD;
                return await _DatabaseHelper.Insert(query, CommandType.Text, ipaddress, activeuser);
            }
            else
            {
                string query = "Update TA_WKLYOFF set MWK_DESC = '" + entity.MWK_DESC + "',MWK_OFF=" + entity.MWK_OFF + ",MWK_DAY=" + entity.MWK_DAY + ",MWK_PAT='" + entity.MWK_PAT + "',MODIFIEDBY='admin',MODIFIEDDATE='" + DateTime.Now.ToString("dd/MMM/yyyy hh:mm") + "'  where MWK_CD =  " + entity.MWK_CD;
                return await _DatabaseHelper.Insert(query, CommandType.Text, ipaddress, activeuser);
            }
          }

          public async Task<int> Delete(int id, string ipaddress, int activeuser)
          {
              string query = "Update TA_WKLYOFF set IsDeleted = 1,DELETEDBY='admin',DELETEDDATE='" + DateTime.Now.ToString("dd/MMM/yyyy hh:mm") + "' where MWK_CD = " + id;
              return await _DatabaseHelper.Insert(query, CommandType.Text, ipaddress, activeuser);
          }


          public List<WeeklyOff> GetWeekOffAttandance(int id)
          {
            string query = string.Empty;
            User user = _userService.GetSingleUser(id);
            if (user.USER_CODE == "superuser")
            {
                 query = "select mwk_cd,MWK_DESC from TA_WKLYOFF where MWK_OFF=0 and IsDeleted='0' ";
            }
            else
            {
                query = "select mwk_cd,MWK_DESC from TA_WKLYOFF where MWK_OFF=0 and IsDeleted='0' and COMPANY_ID in( " +
                        CustomQueryies.Queries.RoleDataAccessQuery(id) + " ) ";
            }
              var x = _DatabaseHelper.GetDataTable(query, CommandType.Text);

              
              List<WeeklyOff> weekofflist = new List<WeeklyOff>();

              foreach (DataRow dr in x.Rows)
              {
                  WeeklyOff entParamsList = new WeeklyOff();
                  entParamsList.MWK_DESC = Convert.ToString(dr["MWK_DESC"]);
                  entParamsList.MWK_CD = Convert.ToInt32(dr["mwk_cd"]);
                  weekofflist.Add(entParamsList);
              }
              return weekofflist;

          }

          public List<WeeklyOff> GetWeekEndAttandance(int id)
          {
             string query = string.Empty;
            User user = _userService.GetSingleUser(id);
            if (user.USER_CODE == "superuser")
            {
                query = "select mwk_cd,MWK_DESC from TA_WKLYOFF where MWK_OFF=1  and IsDeleted='0' ";
            }
            else
            {
                query = "select mwk_cd,MWK_DESC from TA_WKLYOFF where MWK_OFF=1  and IsDeleted='0' and COMPANY_ID in( " +
                        CustomQueryies.Queries.RoleDataAccessQuery(id) + " ) ";
            }
              var x = _DatabaseHelper.GetDataTable(query, CommandType.Text);

              
              List<WeeklyOff> weekofflist = new List<WeeklyOff>();

              foreach (DataRow dr in x.Rows)
              {
                  WeeklyOff entParamsList = new WeeklyOff();
                  entParamsList.MWK_DESC = Convert.ToString(dr["MWK_DESC"]);
                  entParamsList.MWK_CD = Convert.ToInt32(dr["mwk_cd"]);
                  weekofflist.Add(entParamsList);
              }
              return weekofflist;
          }
    }
}
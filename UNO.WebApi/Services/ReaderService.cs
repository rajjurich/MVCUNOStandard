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
    public interface IReaderService
    {
        Task Create(AcsReader entity, string ipaddress, int activeuser);
        Task Edit(AcsReader entity, string ipaddress, int activeuser);
        Task Delete(int id, string ipaddress, int activeuser);
        IQueryable<AcsReaderDto> Get(int id);
        List<AcsReader> GetReaderByControllerIdAndCompanyId(long id, int cid);
        Task<int> GetControllerIdByReaderId(int id);
        Task<int> GetReaderIdByRowId(int id);
    }
    public class ReaderService : IReaderService
    {
        private UnitOfWork _unitOfWork;
        private DatabaseHelper _DatabaseHelper;
        private IUserService _userService;
        public ReaderService(IUnitOfWork unitOfWork
            , DatabaseHelper databaseHelper
            , IUserService userService)
        {
            _unitOfWork = (UnitOfWork)unitOfWork;
            _DatabaseHelper = databaseHelper;
            _userService = userService;
        }
        public async Task Create(AcsReader entity, string ipaddress, int activeuser)
        {
            var readerActiveStatus = entity.IsActive == true ? 1 : 0;

            string query = " Insert into ACS_READER (READER_ID,READER_DESCRIPTION,CTLR_ID,READER_MODE,READER_TYPE,READER_PASSES_FROM,READER_PASSES_TO,READER_ISDELETED,IsActive,EntryReaderMode,COMPANY_ID) " +
                           " values (" + entity.READER_ID + ",'" + entity.READER_DESCRIPTION + "'," + entity.CTLR_ID + ",'" + entity.READER_MODE + "'," +
                           " '" + entity.READER_TYPE + "','" + entity.READER_PASSES_FROM + "', '" + entity.READER_PASSES_TO + "',0," + readerActiveStatus + ",0," + entity.COMPANY_ID + ") ";

            await _DatabaseHelper.Insert(query, CommandType.Text,ipaddress,activeuser);
        }


        public List<AcsReader> GetReaderByControllerIdAndCompanyId(long id, int cid)
        {
            string query = "select * from acs_reader where COMPANY_ID= '" + cid + "' and CTLR_ID in (select CTLR_ID from acs_controller where ID =" + id + ")";
            List<AcsReader> acsReaders = new List<AcsReader>();
            var dataTable = _DatabaseHelper.GetDataTable(query, CommandType.Text);
            if (dataTable.Rows.Count > 0)
            {
                foreach (DataRow dr in dataTable.Rows)
                {
                    AcsReader acsReader = new AcsReader();
                    acsReader.RowID = Convert.ToInt32(dr["RowID"]);
                    acsReader.READER_ID = Convert.ToInt32(dr["READER_ID"]);
                    acsReader.READER_DESCRIPTION = Convert.ToString(dr["READER_DESCRIPTION"]);
                    acsReader.CTLR_ID = Convert.ToInt32(dr["CTLR_ID"]);
                    acsReader.READER_MODE = Convert.ToString(dr["READER_MODE"]);
                    acsReader.READER_TYPE = Convert.ToString(dr["READER_TYPE"]);
                    //acsReader.READER_PASSES_FROM = Convert.ToString(dr["READER_PASSES_FROM"].ToString());
                    //acsReader.READER_PASSES_TO = Convert.ToString(dr["READER_PASSES_TO"].ToString());
                    acsReader.READER_ISDELETED = Convert.ToBoolean(dr["READER_ISDELETED"]);
                    acsReader.IsActive = Convert.ToBoolean(dr["IsActive"]);
                    acsReaders.Add(acsReader);
                }

            }
            return acsReaders;
        }


        public async Task Edit(AcsReader entity, string ipaddress, int activeuser)
        {
            var readerActiveStatus = entity.IsActive == true ? 1 : 0;

            string query = " update ACS_READER set READER_DESCRIPTION = '" + entity.READER_DESCRIPTION + "'" +
                           " ,READER_MODE='" + entity.READER_MODE + "',READER_TYPE='" + entity.READER_TYPE + "',IsActive= " + readerActiveStatus + " " +
                           " , COMPANY_ID = " + entity.COMPANY_ID + "" +
                           " where READER_ID = " + entity.READER_ID + " and CTLR_ID = " + entity.CTLR_ID + " and RowID= " + entity.RowID + "";

            await _DatabaseHelper.Insert(query, CommandType.Text, ipaddress, activeuser);
        }


        public async Task Delete(int id, string ipaddress, int activeuser)
        {
            string query = " update ACS_READER set READER_ISDELETED = 1 " +

                            " where READER_ISDELETED=0 and CTLR_ID = " + id + "";

            await _DatabaseHelper.Insert(query, CommandType.Text, ipaddress, activeuser);
        }


        public IQueryable<AcsReaderDto> Get(int id)
        {
            string query = string.Empty;
            User user = _userService.GetSingleUser(id);
            if (user.USER_CODE == "superuser")
            {
                query = " select RowID,READER_DESCRIPTION from acs_reader where READER_ISDELETED=0 ";
            }
            else
            {
                query = " select RowID,READER_DESCRIPTION from acs_reader where READER_ISDELETED=0 and COMPANY_ID in( " +
                        " SELECT com.COMPANY_ID FROM ent_company com" +
                        " join ENT_ROLE_DATA_ACCESS da on com.COMPANY_ID=da.MAPPED_ENTITY_ID" +
                        " join ENT_ORG_COMMON_TYPES ct on ct.COMMON_TYPES_ID=da.COMMON_TYPES_ID and ct.COMMON_TYPES='COMPANY'" +
                        " WHERE COMPANY_ISDELETED =0 and da.USER_CODE=" + id + " ) ";
            }
            
            var dataTable = _DatabaseHelper.GetDataTable(query, CommandType.Text);
            List<AcsReaderDto> acsReaders = new List<AcsReaderDto>();

            if (dataTable.Rows.Count > 0)
            {
                foreach (DataRow dr in dataTable.Rows)
                {
                    AcsReaderDto acsReader = new AcsReaderDto();

                    acsReader.RowId = Convert.ToInt32(dr["RowID"]);
                    acsReader.READER_DESCRIPTION = Convert.ToString(dr["READER_DESCRIPTION"]);

                    acsReaders.Add(acsReader);
                }
            }

            return acsReaders.AsQueryable();
        }


        public async Task<int> GetControllerIdByReaderId(int id)
        {
            string query = "select distinct CTLR_ID from acs_reader where RowId= '" + id + "'";
            return await _DatabaseHelper.GetScalarValue(query, CommandType.Text);
        }

        public async Task<int> GetReaderIdByRowId(int id)
        {
            string query = "select Reader_id from acs_reader where RowId= '" + id + "'";
            return await _DatabaseHelper.GetScalarValue(query, CommandType.Text);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using UNO.DAL;
using UNO.MVCApp.Helpers;
using UNO.WebApi.Dto;
using UNO.WebApi.Models;
using UNO.WebApi.Services;

namespace UNO.WebApi.Controllers
{
    public class RoleDataAccessController : ApiController
    {
        private IRoleDataAccessService _roleDataAccessService;
        private UnitOfWork _unitOfWork;
        private ICommonMasterService _commonMasterService;
        private int activeuser;
        private Utilities _ipaddressobj;
        public RoleDataAccessController(IRoleDataAccessService roleDataAccessService
            , IUnitOfWork unitOfWork
            , ICommonMasterService commonMasterService
            , Utilities _ipaddress)
        {
            _ipaddressobj = _ipaddress;
            _roleDataAccessService = roleDataAccessService;
            _unitOfWork = (UnitOfWork)unitOfWork;
            _commonMasterService = commonMasterService;
            var request = HttpContext.Current.Request;
            activeuser = Convert.ToInt32(request.Headers["activeuser"]);
        }
        // GET api/roledataaccess
        public IQueryable<RoleDataAccess> Get()
        {
            var x = _roleDataAccessService.Get(activeuser).AsQueryable();
            return x;
        }

        // GET api/roledataaccess/5
        [Route("api/roledataaccess/GetMappedEntityByUserId/{id}")]
        public IQueryable<MappedEntityId> GetMappedEntityByUserId(int id)
        {
            var x = _roleDataAccessService.GetMappedEntityByUserId(id).AsQueryable();
            return x;
        }

        // POST api/roledataaccess
        public async Task<IHttpActionResult> Post([FromBody]RoleDataAccessDto roleDataAccessDto)
        {
            string ipadress = roleDataAccessDto.ipaddress;
            if (!(ModelState.IsValid))
            {
                return BadRequest(ModelState);
            }

            _unitOfWork.BeginTransaction();

            var x = await _roleDataAccessService.DeleteByUserId(roleDataAccessDto.USER_CODE,ipadress,activeuser);
            int roleDataAccessId = 0;
            if (roleDataAccessDto.MappedEntityIds != null)
            {
                foreach (var item in roleDataAccessDto.MappedEntityIds)
                {
                    RoleDataAccess roleDataAccess = new RoleDataAccess();
                    roleDataAccess.USER_CODE = roleDataAccessDto.USER_CODE;
                    roleDataAccess.MAPPED_ENTITY_ID = item.MAPPED_ENTITY_ID;
                    roleDataAccess.COMMON_TYPES_ID = await _commonMasterService.GetCommonIdByName(item.CommonTypes);
                    roleDataAccessId = await _roleDataAccessService.Create(roleDataAccess, ipadress, activeuser);
                }
            }

            return CreatedAtRoute("", new { id = roleDataAccessId }, roleDataAccessDto);
        }

        // PUT api/roledataaccess/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/roledataaccess/5
        public void Delete(int id)
        {
        }
    }
}

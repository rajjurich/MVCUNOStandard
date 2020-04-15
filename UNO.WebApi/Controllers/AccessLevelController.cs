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
    public class AccessLevelController : ApiController
    {
        private IAccessLevelService _accessLevelService;
        private IAccessLevelRelationService _accessLevelRelationService;
        private UnitOfWork _unitOfWork;
        private IReaderService _readerService;
        private IZoneReaderRelService _zoneReaderRelService;
        private IEalConfigService _ealConfigService;
        private int activeuser;
        private Utilities _ipaddressobj;
        public AccessLevelController(UnitOfWork unitOfWork
            , IEalConfigService ealConfigService
            , IAccessLevelService accessLevelService
            , IAccessLevelRelationService accessLevelRelationService
            , IZoneReaderRelService zoneReaderRelService
            , IReaderService readerService
            , Utilities _ipaddress)
        {
            _ipaddressobj = _ipaddress;
            _unitOfWork = (UnitOfWork)unitOfWork;
            _ealConfigService = ealConfigService;
            _accessLevelService = accessLevelService;
            _accessLevelRelationService = accessLevelRelationService;
            _readerService = readerService;
            _zoneReaderRelService = zoneReaderRelService;
            var request = HttpContext.Current.Request;
            activeuser = Convert.ToInt32(request.Headers["activeuser"]);

        }
        // GET api/accesslevel
        public IQueryable<AccessLevelDto> Get()
        {
            var x = _accessLevelService.Get(activeuser).AsQueryable();
            return x;
        }

        // GET api/accesslevel/5
        public async Task<IHttpActionResult> Get(int id)
        {
            var accessLevel = await _accessLevelService.GetSingle(id);

            return Ok(accessLevel);

        }

        // POST api/accesslevel
        public async Task<IHttpActionResult> Post([FromBody]AccessLevel accessLevel)
        {
            string ipadress = accessLevel.ipaddress;
            if (!(ModelState.IsValid))
            {
                return BadRequest(ModelState);
            }

            _unitOfWork.BeginTransaction();

            var accessLevelId = await _accessLevelService.Create(accessLevel, ipadress,activeuser);

            if (accessLevel.AccessLevelRelation.AccessLevelReaders.Count > 0)
            {
                foreach (var item in accessLevel.AccessLevelRelation.AccessLevelReaders)
                {
                    if (accessLevel.AccessLevelRelation.AL_ENTITY_TYPE == "R")
                    {
                        AccessLevelRelation accessLevelRelation = new AccessLevelRelation();
                        accessLevelRelation.AL_ID = accessLevelId;
                        accessLevelRelation.AL_ENTITY_TYPE = accessLevel.AccessLevelRelation.AL_ENTITY_TYPE;
                        accessLevelRelation.RD_ZN_ID = item;
                        accessLevelRelation.ZoneId = null;
                        accessLevelRelation.CONTROLLER_ID = await _readerService.GetControllerIdByReaderId(item);
                        accessLevelRelation.AccesLevelArray = await _accessLevelRelationService.GetAccessArray(activeuser, accessLevelId, accessLevelRelation.CONTROLLER_ID, await _readerService.GetReaderIdByRowId(item));
                        await _accessLevelRelationService.Create(accessLevelRelation, ipadress,activeuser);
                    }
                    else
                    {
                        var readers = _zoneReaderRelService.GetReadersByZoneId(item);
                        if (readers != null)
                        {
                            foreach (var reader in readers)
                            {
                                AccessLevelRelation accessLevelRelation = new AccessLevelRelation();
                                accessLevelRelation.AL_ID = accessLevelId;
                                accessLevelRelation.AL_ENTITY_TYPE = accessLevel.AccessLevelRelation.AL_ENTITY_TYPE;
                                accessLevelRelation.RD_ZN_ID = reader.RowId;
                                accessLevelRelation.ZoneId = item;
                                accessLevelRelation.CONTROLLER_ID = await _readerService.GetControllerIdByReaderId(reader.RowId);
                                accessLevelRelation.AccesLevelArray = await _accessLevelRelationService.GetAccessArray(activeuser, accessLevelId, accessLevelRelation.CONTROLLER_ID, await _readerService.GetReaderIdByRowId(reader.RowId));
                                await _accessLevelRelationService.Create(accessLevelRelation, ipadress, activeuser);
                            }
                        }
                    }
                }
            }
            return CreatedAtRoute("", new { id = accessLevelId }, accessLevel);
        }

        // PUT api/accesslevel/5
        public async Task<IHttpActionResult> Put(int id, [FromBody]AccessLevel accessLevel)
        {
            string ipadress = accessLevel.ipaddress;
            if (!(ModelState.IsValid))
            {
                return BadRequest(ModelState);
            }
            if (id != accessLevel.AL_ID)
            {
                return BadRequest(ModelState);
            }
            AccessLevel accessLevelDelete = await _accessLevelService.GetSingle(id);
            if (accessLevelDelete == null)
            {
                return NotFound();
            }

            if (accessLevelDelete.AccessLevelRelation.AL_ENTITY_TYPE != accessLevel.AccessLevelRelation.AL_ENTITY_TYPE)
            {
                return BadRequest("Cannot change Entity Type");
            }
            accessLevel.AccessLevelRelation.AccessLevelReaders = accessLevel.AccessLevelRelation.AccessLevelReaders.Distinct().ToList();
            _unitOfWork.BeginTransaction();


            accessLevelDelete.AccessLevelRelation.AccessLevelReaders = accessLevelDelete.AccessLevelRelation.AccessLevelReaders
                                                                        .Except(accessLevel.AccessLevelRelation.AccessLevelReaders)
                                                                        .Distinct()
                                                                        .ToList();
            string controllerIdsForDelete = string.Empty;
            controllerIdsForDelete = await GetControllersToDeleteAccess(accessLevelDelete, controllerIdsForDelete);
            

            AccessLevel accessLevelAdd = await _accessLevelService.GetSingle(id);
            accessLevelAdd.AccessLevelRelation.AccessLevelReaders = accessLevel.AccessLevelRelation.AccessLevelReaders
                                                                    .Except(accessLevelAdd.AccessLevelRelation.AccessLevelReaders)
                                                                    .Distinct()
                                                                    .ToList();

            string controllerIdsForAdd = string.Empty;
            controllerIdsForAdd = await GetControllersToAddAccess(accessLevelAdd, controllerIdsForAdd);
            if (controllerIdsForAdd != string.Empty)
            {
                await _ealConfigService.AddByAlidAndControllerIds(id, controllerIdsForAdd, activeuser.ToString(), ipadress, activeuser);
            }

            if (controllerIdsForDelete != string.Empty)
            {
                await _ealConfigService.DeleteByAlidAndControllerIds(id, controllerIdsForDelete, activeuser.ToString(), ipadress, activeuser);
            }

            var accessLevelId = await _accessLevelService.Edit(accessLevel,ipadress,activeuser);


            if (accessLevel.AccessLevelRelation.AccessLevelReaders.Count > 0)
            {
                
                await _accessLevelRelationService.DeleteByAlId(accessLevel.AL_ID, ipadress, activeuser);
                foreach (var item in accessLevel.AccessLevelRelation.AccessLevelReaders)
                {
                    if (accessLevel.AccessLevelRelation.AL_ENTITY_TYPE == "R")
                    {
                        AccessLevelRelation accessLevelRelation = new AccessLevelRelation();
                        accessLevelRelation.AL_ID = id;
                        accessLevelRelation.AL_ENTITY_TYPE = accessLevel.AccessLevelRelation.AL_ENTITY_TYPE;
                        accessLevelRelation.RD_ZN_ID = item;
                        accessLevelRelation.ZoneId = null;
                        accessLevelRelation.CONTROLLER_ID = await _readerService.GetControllerIdByReaderId(item);
                        accessLevelRelation.AccesLevelArray = await _accessLevelRelationService.GetAccessArray(activeuser, id, accessLevelRelation.CONTROLLER_ID, await _readerService.GetReaderIdByRowId(item));
                        await _accessLevelRelationService.Create(accessLevelRelation, ipadress, activeuser);
                    }
                    else
                    {
                        var readers = _zoneReaderRelService.GetReadersByZoneId(item);
                        if (readers != null)
                        {
                            foreach (var reader in readers)
                            {
                                AccessLevelRelation accessLevelRelation = new AccessLevelRelation();
                                accessLevelRelation.AL_ID = id;
                                accessLevelRelation.AL_ENTITY_TYPE = accessLevel.AccessLevelRelation.AL_ENTITY_TYPE;
                                accessLevelRelation.RD_ZN_ID = reader.RowId;
                                accessLevelRelation.ZoneId = item;
                                accessLevelRelation.CONTROLLER_ID = await _readerService.GetControllerIdByReaderId(reader.RowId);
                                accessLevelRelation.AccesLevelArray = await _accessLevelRelationService.GetAccessArray(activeuser, id, accessLevelRelation.CONTROLLER_ID, await _readerService.GetReaderIdByRowId(reader.RowId));
                                await _accessLevelRelationService.Create(accessLevelRelation, ipadress, activeuser);
                            }
                        }
                    }
                }
            }
            return Ok(accessLevelDelete);
        }

        private async Task<string> GetControllersToAddAccess(AccessLevel accessLevelAdd, string controllerIdsForAdd)
        {
            if (accessLevelAdd.AccessLevelRelation.AccessLevelReaders.Count > 0)
            {
                foreach (var item in accessLevelAdd.AccessLevelRelation.AccessLevelReaders)
                {
                    if (accessLevelAdd.AccessLevelRelation.AL_ENTITY_TYPE == "R")
                    {
                        var controllerIdForAdd = await _readerService.GetControllerIdByReaderId(item);
                        controllerIdsForAdd = controllerIdsForAdd + (controllerIdsForAdd.Length > 0 ? "," : "") + "'" + controllerIdForAdd + "'";
                    }
                    else
                    {
                        var readers = _zoneReaderRelService.GetReadersByZoneId(item);
                        if (readers != null)
                        {
                            foreach (var reader in readers)
                            {
                                var controllerIdForAdd = await _readerService.GetControllerIdByReaderId(reader.RowId);
                                controllerIdsForAdd = controllerIdsForAdd + (controllerIdsForAdd.Length > 0 ? "," : "") + "'" + controllerIdForAdd + "'";
                            }
                        }
                    }
                }
            }
            return controllerIdsForAdd;
        }

        private async Task<string> GetControllersToDeleteAccess(AccessLevel accessLevelDelete, string controllerIdsForDelete)
        {
            if (accessLevelDelete.AccessLevelRelation.AccessLevelReaders.Count > 0)
            {
                foreach (var item in accessLevelDelete.AccessLevelRelation.AccessLevelReaders)
                {
                    if (accessLevelDelete.AccessLevelRelation.AL_ENTITY_TYPE == "R")
                    {
                        var controllerIdFordelete = await _readerService.GetControllerIdByReaderId(item);
                        controllerIdsForDelete = controllerIdsForDelete + (controllerIdsForDelete.Length > 0 ? "," : "") + "'" + controllerIdFordelete + "'";
                    }
                    else
                    {
                        var readers = _zoneReaderRelService.GetReadersByZoneId(item);
                        if (readers != null)
                        {
                            foreach (var reader in readers)
                            {
                                var controllerIdFordelete = await _readerService.GetControllerIdByReaderId(reader.RowId);
                                controllerIdsForDelete = controllerIdsForDelete + (controllerIdsForDelete.Length > 0 ? "," : "") + "'" + controllerIdFordelete + "'";
                            }
                        }
                    }
                }
            }
            return controllerIdsForDelete;
        }

        // DELETE api/accesslevel/5
        [Route("api/accesslevel/{id}/{user}/{ipaddress}")]
        public async Task<IHttpActionResult> Delete(int id, string user, string ipaddress)
        {
            string ipadress = ipaddress;
            AccessLevel getAccessLevel = await _accessLevelService.GetSingle(id);
            if (getAccessLevel == null)
            {
                return NotFound();
            }
            _unitOfWork.BeginTransaction();
            await _accessLevelService.Delete(id, user,ipadress,activeuser);
            await _accessLevelRelationService.DeleteByAlId(id, ipadress, activeuser);
            return Ok(getAccessLevel);
        }
    }
}

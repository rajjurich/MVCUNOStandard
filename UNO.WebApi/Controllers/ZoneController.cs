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
    public class ZoneController : ApiController
    {
        private UnitOfWork _unitOfWork;
        private IZoneService _zoneService;
        private IZoneReaderRelService _zoneReaderRelService;
        private IAccessLevelRelationService _accessLevelRelationService;
        private IReaderService _readerService;
        private IEalConfigService _ealConfigService;
        private int activeuser;
        private Utilities _ipaddressobj;
        public ZoneController(IUnitOfWork unitOfWork, IZoneService zoneService
            , IZoneReaderRelService zoneReaderRelService
            , IAccessLevelRelationService accessLevelRelationService
            , IReaderService readerService
            , IEalConfigService ealConfigService
            , Utilities _ipaddress)
        {
            _ipaddressobj = _ipaddress;
            _unitOfWork = (UnitOfWork)unitOfWork;
            _zoneService = zoneService;
            _zoneReaderRelService = zoneReaderRelService;
            _readerService = readerService;
            _accessLevelRelationService = accessLevelRelationService;
            _ealConfigService = ealConfigService;
            var request = HttpContext.Current.Request;
            activeuser = Convert.ToInt32(request.Headers["activeuser"]);
        }
        // GET api/zone
        public IEnumerable<ZoneDto> Get()
        {
            var x = _zoneService.Get(activeuser).AsQueryable();
            return x;
        }

        // GET api/zone/5
        public async Task<IHttpActionResult> Get(int id)
        {
            var zone = await _zoneService.GetSingle(id);

            return Ok(zone);
        }

        // GET api/zone/abcd
        [Route("api/zone/checkZone/{key}")]
        public bool Get(string key)
        {
            return isZoneExist(key);
        }

        // POST api/zone
        public async Task<IHttpActionResult> Post([FromBody]Zone zone)
        {
            string ipadress = zone.ipaddress;
            if (!(ModelState.IsValid))
            {
                return BadRequest(ModelState);
            }

            _unitOfWork.BeginTransaction();

            var zoneId = await _zoneService.Create(zone,ipadress,activeuser);
            foreach (var item in zone.acsReaderInfos)
            {
                ZoneReaderRel zoneReaderRel = new ZoneReaderRel();
                zoneReaderRel.ZONE_ID = zoneId;
                //zoneReaderRel.CONTROLLER_ID = item.CONTROLLER_ID;
                zoneReaderRel.READER_ID = item.RowId;
                await _zoneReaderRelService.Create(zoneReaderRel,ipadress,activeuser);
            }
            return CreatedAtRoute("", new { id = zoneId }, zone);
        }

        // PUT api/zone/5
        public async Task<IHttpActionResult> Put(int id, [FromBody]Zone zone)
        {
            string ipadress = zone.ipaddress;
            if (!(ModelState.IsValid))
            {
                return BadRequest(ModelState);
            }
            if (id != zone.ZONE_ID)
            {
                return BadRequest(ModelState);
            }
            Zone Zone = await _zoneService.GetSingle(id);
            if (Zone == null)
            {
                return NotFound();
            }

            _unitOfWork.BeginTransaction();

            var zoneId = await _zoneService.Edit(Zone,ipadress,activeuser);

            await _zoneReaderRelService.Delete(id,ipadress,activeuser);
            foreach (var item in zone.acsReaderInfos)
            {
                ZoneReaderRel zoneReaderRel = new ZoneReaderRel();
                zoneReaderRel.ZONE_ID = id;
                zoneReaderRel.READER_ID = item.RowId;
                await _zoneReaderRelService.Create(zoneReaderRel, ipadress, activeuser);
            }

            
            string dt = await _accessLevelRelationService.DeleteByZoneId(id,ipadress,activeuser);
            var alid = Convert.ToInt64(await _accessLevelRelationService.GetAlIdByZoneId(id));
            if (alid != 0)
            {
                var readers = _zoneReaderRelService.GetReadersByZoneId(id);
                if (readers != null)
                {
                    foreach (var reader in readers)
                    {
                        AccessLevelRelation accessLevelRelation = new AccessLevelRelation();

                        accessLevelRelation.AL_ID = alid;
                        accessLevelRelation.AL_ENTITY_TYPE = "Z";
                        accessLevelRelation.RD_ZN_ID = reader.RowId;
                        accessLevelRelation.ZoneId = id;
                        accessLevelRelation.CONTROLLER_ID = await _readerService.GetControllerIdByReaderId(reader.RowId);
                        accessLevelRelation.AccesLevelArray = await _accessLevelRelationService
                            .GetAccessArray(activeuser, alid, accessLevelRelation.CONTROLLER_ID, await _readerService.GetReaderIdByRowId(reader.RowId));
                        await _accessLevelRelationService.Create(accessLevelRelation, ipadress, activeuser);
                    }
                }

                List<AccessLevelRelationEdit> accessLevelRelationDelete = _accessLevelRelationService.GetAccessRelationByZoneId(id, "delete", dt).ToList();
                List<AccessLevelRelationEdit> accessLevelRelationAdd = _accessLevelRelationService.GetAccessRelationByZoneId(id, "add", dt).ToList();
                await add_delete_EalConfig(accessLevelRelationAdd, "add");
                await add_delete_EalConfig(accessLevelRelationDelete, "delete");
            }

            return Ok(Zone);
        }

        // DELETE api/zone/5
        [Route("api/zone/{id}/{user}/{ipaddress}")]
        public async Task<IHttpActionResult> Delete(int id, string user, string ipaddress)
        {
            string ipadress = ipaddress;
            Zone zone = await _zoneService.GetSingle(id);
            if (zone == null)
            {
                return NotFound();
            }
            _unitOfWork.BeginTransaction();
            await _zoneService.Delete(id, user,ipadress,activeuser);
            await _zoneReaderRelService.Delete(id,ipadress,activeuser);

            return Ok(zone);
        }

        private bool isZoneExist(string key)
        {
            var checkZone = _zoneService.Get(activeuser).AsQueryable().Where(x => x.ZONE_DESCRIPTION == key).FirstOrDefault();
            return checkZone == null ? false : true;
        }

        private async Task<bool> add_delete_EalConfig(List<AccessLevelRelationEdit> accessLevelRelationEdits, string activity)
        {
            string ipadress = _ipaddressobj.GetIpAddress();
            if (activity.ToLower() == "delete")
            {
                if (accessLevelRelationEdits.Count > 0)
                {
                    string controllerIds = string.Empty;
                    string alIds = string.Empty;
                    foreach (var accessLevelRelationEdit in accessLevelRelationEdits)
                    {
                        controllerIds = controllerIds + (controllerIds.Length > 0 ? "," : "") + "'" + accessLevelRelationEdit.CONTROLLER_ID + "'";
                        alIds = alIds + (alIds.Length > 0 ? "," : "") + "'" + accessLevelRelationEdit.AL_ID + "'";
                    }
                    return await _ealConfigService.DeleteByAlidsAndControllerIds(alIds, controllerIds, activeuser.ToString(), ipadress, activeuser) > -1;
                }
            }
            else if (activity.ToLower() == "add")
            {
                if (accessLevelRelationEdits.Count > 0)
                {
                    string controllerIds = string.Empty;
                    string alIds = string.Empty;
                    foreach (var accessLevelRelationEdit in accessLevelRelationEdits)
                    {
                        controllerIds = controllerIds + (controllerIds.Length > 0 ? "," : "") + "'" + accessLevelRelationEdit.CONTROLLER_ID + "'";
                        alIds = alIds + (alIds.Length > 0 ? "," : "") + "'" + accessLevelRelationEdit.AL_ID + "'";
                    }
                    return await _ealConfigService.AddByAlidsAndControllerIds(alIds, controllerIds, activeuser.ToString(), ipadress, activeuser) > -1;
                }
            }
            return false;
        }
    }
}

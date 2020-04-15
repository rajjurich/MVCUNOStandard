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
using UNO.WebApi.Helpers;
using UNO.WebApi.Models;
using UNO.WebApi.Services;

namespace UNO.WebApi.Controllers
{
    public class ControllerController : ApiController
    {
        private IControllerService _controllerService;
        private IReaderService _readerService;
        private IDoorService _doorService;
        private IAccessPointRelationService _accessPointRelationService;
        private UnitOfWork _unitOfWork;
        private int activeuser;
        private Utilities _ipaddressobj;
        public ControllerController(IControllerService controllerService
            , IReaderService readerService
            , IDoorService doorService
            , IAccessPointRelationService accessPointRelationService
            , IUnitOfWork unitOfWork
            , Utilities _ipaddress
            )
        {
            _ipaddressobj = _ipaddress;
            _controllerService = controllerService;
            _readerService = readerService;
            _doorService = doorService;
            _accessPointRelationService = accessPointRelationService;
            _unitOfWork = (UnitOfWork)unitOfWork;
            var request = HttpContext.Current.Request;
            activeuser = Convert.ToInt32(request.Headers["activeuser"]);
        }
        // GET api/controller
        public IQueryable<AcsControllerDto> Get()
        {
            var x = _controllerService.Get(activeuser).AsQueryable();
            return x;
        }

        // GET api/controller/5
        [Route("api/controller/GetByControllerId/{controllerId}/{companyId}")]
        public async Task<bool> GetByControllerId(string controllerId, string companyId)
        {
            return await _controllerService.GetControllerByID(controllerId, companyId);
        }

        // GET api/controller/1.1.1.1
        [Route("api/controller/GetByControllerIP/{controllerIp}/{companyId}")]
        public async Task<bool> GetByControllerIP(string controllerIp, string companyId)
        {
            controllerIp = controllerIp.Replace('-', '.');
            return await _controllerService.GetControllerByIP(controllerIp, companyId);
        }
        // GET api/controller/5
        public async Task<IHttpActionResult> Get(int id)
        {
            var controller = await _controllerService.GetSingle(id);

            return Ok(controller);

        }

        // POST api/controller
        public async Task<IHttpActionResult> Post([FromBody]AcsController controller)
        {
            string ipadress = controller.ipaddress;
            if (!(ModelState.IsValid))
            {
                return BadRequest(ModelState);
            }

            _unitOfWork.BeginTransaction();

            var controllerId = await _controllerService.Create(controller, ipadress,activeuser);

            foreach (var item in controller.Readers)
            {
                //AcsReader acsReader = new AcsReader();
                //acsReader.READER_ID = item.READER_ID;
                //acsReader.READER_DESCRIPTION = item.READER_DESCRIPTION;
                //acsReader.CTLR_ID = item.CTLR_ID;
                //acsReader.READER_MODE = item.READER_MODE;
                //acsReader.READER_TYPE = item.READER_TYPE;
                //acsReader.IsActive = item.IsActive;
                item.COMPANY_ID = controller.COMPANY_ID;
                await _readerService.Create(item, ipadress,activeuser);

            }
            int i = 1;
            foreach (var item in controller.AccessPointDetails)
            {
                AcsDoor acsDoor = new AcsDoor();
                acsDoor.DOOR_ID = i;
                acsDoor.CTLR_ID = controllerId;
                acsDoor.DOOR_TYPE = item.DOOR_TYPE;
                acsDoor.DOOR_OPEN_DURATION = item.DOOR_OPEN_DURATION;
                acsDoor.DOOR_FEEDBACK_DURATION = item.DOOR_FEEDBACK_DURATION;
                acsDoor.READER_ID = item.READER_ID;
                await _doorService.Create(acsDoor, ipadress, activeuser);

                AccessPointRelation accessPointRelation = new AccessPointRelation();
                accessPointRelation.AP_ID = item.AP_ID;
                accessPointRelation.READER_ID = item.READER_ID;
                accessPointRelation.DOOR_ID = item.DOOR_ID;
                accessPointRelation.AP_CONTROLLER_ID = controllerId;
                await _accessPointRelationService.Create(accessPointRelation, ipadress,activeuser);

                i++;
            }

            return CreatedAtRoute("", new { id = controllerId }, controller);

        }

        // PUT api/controller/5
        public async Task<IHttpActionResult> Put(int id, [FromBody]AcsController controller)
        {
            string ipadress = controller.ipaddress;
            if (!(ModelState.IsValid))
            {
                return BadRequest(ModelState);
            }
            if (id != controller.ID)
            {
                return BadRequest(ModelState);
            }
            AcsController acsController = await _controllerService.GetSingle(id);
            if (acsController == null)
            {
                return NotFound();
            }
            _unitOfWork.BeginTransaction();
            var controllerId = await _controllerService.Edit(controller, ipadress, activeuser);
            foreach (var item in controller.Readers)
            {
                item.COMPANY_ID = controller.COMPANY_ID;
                await _readerService.Edit(item, ipadress, activeuser);
            }
            foreach (var item in controller.AccessPointDetails)
            {
                AcsDoor acsDoor = new AcsDoor();
                acsDoor.DOOR_ID = item.DOOR_ID;
                acsDoor.CTLR_ID = id;
                acsDoor.DOOR_TYPE = item.DOOR_TYPE;
                acsDoor.DOOR_OPEN_DURATION = item.DOOR_OPEN_DURATION;
                acsDoor.DOOR_FEEDBACK_DURATION = item.DOOR_FEEDBACK_DURATION;
                acsDoor.READER_ID = item.READER_ID;
                await _doorService.Edit(acsDoor, ipadress, activeuser);
            }
            return Ok(acsController);
        }

        // DELETE api/controller/5
        [Route("api/controller/{id}/{user}/{ipaddress}")]
        public async Task<IHttpActionResult> Delete(int id, string user,string ipaddress)
        {
            string ipadress = ipaddress;
            AcsController acsController = await _controllerService.GetSingle(id);
            if (acsController == null)
            {
                return NotFound();
            }
            _unitOfWork.BeginTransaction();
            await _controllerService.Delete(id, user, ipadress, activeuser);
            await _readerService.Delete(acsController.CTLR_ID, ipadress, activeuser);
            await _doorService.Delete(acsController.CTLR_ID, ipadress, activeuser);
            await _accessPointRelationService.Delete(acsController.CTLR_ID, ipadress,activeuser);
            return Ok(acsController);
        }
    }
}

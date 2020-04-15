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
using UNO.WebApi.Models;
using UNO.WebApi.Services;

namespace UNO.WebApi.Controllers
{
    public class ReasonMasterAPIController : ApiController
    {
        private IReasonMasterService _entParamsService;
        private UnitOfWork _unitOfWork;
        private int activeuser;
        private Utilities _ipaddressobj;

        public ReasonMasterAPIController(IReasonMasterService entReasonMasterService, IUnitOfWork unitOfWork
        , Utilities _ipaddress)
        {
            _ipaddressobj = _ipaddress;
            var request = HttpContext.Current.Request;
            activeuser = Convert.ToInt32(request.Headers["activeuser"]);
            _entParamsService = entReasonMasterService;
            _unitOfWork = (UnitOfWork)unitOfWork;
        }

        [HttpGet]
        public List<ReasonMaster> get()
        {
            return _entParamsService.GetReasonMaster();
        }

         [Route("api/ReasonMasterAPI/get/{LeaveType}")]
        public List<Reason> get(string LeaveType)
        {
            return _entParamsService.GetReasonByType(LeaveType);
        }

        [HttpGet]
        public ReasonMaster get(int id)
        {
            return _entParamsService.GetReasonMaster(id);
        }

        [HttpPost]
        public async Task<IHttpActionResult> Post(ReasonMaster _ReasonMaster)
        {
            string ipadress = _ReasonMaster.ipaddress;
            if (!(ModelState.IsValid))
            {
                return BadRequest(ModelState);
            }
            var x = await _entParamsService.IsUniqReasonCode(_ReasonMaster, false);

            if (x)
            {
                return BadRequest("Reason Code Already Exists!!");
            }
            _unitOfWork.BeginTransaction();

            var controllerId = await _entParamsService.Create(_ReasonMaster,ipadress,activeuser);

            return CreatedAtRoute("", new { id = controllerId }, _ReasonMaster);
        }

        [HttpPut]
        public async Task<IHttpActionResult> Put(ReasonMaster _ReasonMaster)
        {
            string ipadress = _ReasonMaster.ipaddress;
            //return await _entParamsService.Edit(_company);

            if (!(ModelState.IsValid))
            {
                return BadRequest(ModelState);
            }
            var x = await _entParamsService.IsUniqReasonCode(_ReasonMaster, true);

            if (x)
            {
                return BadRequest("Reason Code Already Exists for Others!!");
            }
            _unitOfWork.BeginTransaction();

            var controllerId = await _entParamsService.Edit(_ReasonMaster, ipadress, activeuser);

            return CreatedAtRoute("", new { id = controllerId }, _ReasonMaster);
        }

        public async Task<IHttpActionResult> DELETE(int id,string ipaddress)
        {
            string ipadress = ipaddress;
            if (!(ModelState.IsValid))
            {
                return BadRequest(ModelState);
            }

            _unitOfWork.BeginTransaction();

            var controllerId = await _entParamsService.Delete(id, ipadress, activeuser);

            return CreatedAtRoute("", new { id = controllerId }, id);
        }
    }
}

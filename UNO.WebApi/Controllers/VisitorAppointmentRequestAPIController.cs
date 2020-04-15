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
    public class VisitorAppointmentRequestAPIController : ApiController
    {
        private IVisitorAppointmentRequestService _entParamsService;
        private IUnitOfWork _unitOfWork;
        private int activeuser;
        private Utilities _ipaddressobj;
        public VisitorAppointmentRequestAPIController(IVisitorAppointmentRequestService entVisitorAppointmentRequestService, IUnitOfWork unitOfWork, Utilities _ipaddress)
        {
            _ipaddressobj = _ipaddress;
            var request = HttpContext.Current.Request;
            activeuser = Convert.ToInt32(request.Headers["activeuser"]);
            _entParamsService = entVisitorAppointmentRequestService;
            _unitOfWork = unitOfWork;
        }

        //[Route("api/Menu/GetMenuList")]

        public IQueryable<VisitorAppointmentRequest> get()
        {
            var x = _entParamsService.GetVisitorAppointmentRequest().AsQueryable();
            return x;
        }

        [HttpGet]
        [Route("api/VisitorAppointmentRequestAPI/getApproval")]
        public IQueryable<ApprovalAuth> getApproval()
        {
            var x = _entParamsService.Get().AsQueryable();
            return x;
        }

        [HttpGet]
        public VisitorAppointmentRequest get(int id)
        {
            return _entParamsService.GetVisitorAppointmentRequest(id);
        }
       
        public async Task<IHttpActionResult> Post(VisitorAppointmentRequest _VisitorAppointmentRequest)
        {
            string ipadress = _VisitorAppointmentRequest.ipaddress;
            if (!(ModelState.IsValid))
            {
                return BadRequest(ModelState);
            }

            _unitOfWork.BeginTransaction();

            var VisitorAppointmentRequest = await _entParamsService.Create(_VisitorAppointmentRequest,ipadress,activeuser);
            return CreatedAtRoute("", new { id = VisitorAppointmentRequest }, _VisitorAppointmentRequest);
        }

        public async Task<IHttpActionResult> Put(VisitorAppointmentRequest _VisitorAppointmentRequest)
        {
            string ipadress = _VisitorAppointmentRequest.ipaddress;
            if (!(ModelState.IsValid))
            {
                return BadRequest(ModelState);
            }

            _unitOfWork.BeginTransaction();

            var VisitorAppointmentRequest = await _entParamsService.Edit(_VisitorAppointmentRequest, ipadress, activeuser);

            return CreatedAtRoute("", new { id = VisitorAppointmentRequest }, _VisitorAppointmentRequest);
        }

        public async Task<IHttpActionResult> DELETE(int id, string ipaddress)
        {
            string ipadress = ipaddress;
            if (!(ModelState.IsValid))
            {
                return BadRequest(ModelState);
            }
            _unitOfWork.BeginTransaction();

            var VisitorAppointmentRequest = await _entParamsService.Delete(id, ipadress, activeuser);
            return CreatedAtRoute("", new { id = VisitorAppointmentRequest }, id);
        }
    }
}

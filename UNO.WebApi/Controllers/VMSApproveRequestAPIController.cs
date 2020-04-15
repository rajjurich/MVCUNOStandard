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
    public class VMSApproveRequestAPIController : ApiController
    {
        private IVMSApproveRequestService _entParamsService;
        private int activeuser;
        private Utilities _ipaddressobj;
        public VMSApproveRequestAPIController(IVMSApproveRequestService entVMSApproveRequestService, Utilities _ipaddress)
        {
            _ipaddressobj = _ipaddress;
            var request = HttpContext.Current.Request;
            activeuser = Convert.ToInt32(request.Headers["activeuser"]);
            _entParamsService = entVMSApproveRequestService;
        }
        // GET api/entparams

        //[Route("api/Menu/GetMenuList")]

        public List<VisitorAppointmentRequest> get()
        {
            return _entParamsService.GetVMSApproveRequest();
        }

        [HttpGet]
        public VisitorAppointmentRequest get(int id)
        {
            return _entParamsService.GetVMSApproveRequest(id);
        }

        [HttpPut]
        public async Task<int> put(VisitorAppointmentRequest _VisitorAppointmentRequest)
        {
            string ipadress = _ipaddressobj.GetIpAddress();
            return await _entParamsService.ApproveReject(_VisitorAppointmentRequest,ipadress,activeuser);
        }
    }
}

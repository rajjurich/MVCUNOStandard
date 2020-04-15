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
    public class LeaveFileAPIController : ApiController
    {
        private ILeaveFileService _entParamsService;
        private IUnitOfWork _unitOfWork;
        private int activeuser;
        private Utilities _ipaddressobj;
        public LeaveFileAPIController(ILeaveFileService entLeaveFileService, IUnitOfWork unitOfWork, Utilities _ipaddress)
        {
            _ipaddressobj = _ipaddress;
            var request = HttpContext.Current.Request;
            activeuser = Convert.ToInt32(request.Headers["activeuser"]);
            _entParamsService = entLeaveFileService;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public LeaveFile get(int id)
        {
            return _entParamsService.GetLeaveFile(id);
        }

        public IQueryable<LeaveFile> get()
        {
            var x = _entParamsService.GetLeaveFile().AsQueryable();
             return x;
        }

        public async Task<IHttpActionResult> Post(LeaveFile _LeaveFile)
        {
            string ipadress = _LeaveFile.ipaddress;
            if (!(ModelState.IsValid))
            {
                return BadRequest(ModelState);
            }
            _unitOfWork.BeginTransaction();
            var LeaveFile = await _entParamsService.Create(_LeaveFile,ipadress,activeuser);
            return CreatedAtRoute("", new { id = LeaveFile }, _LeaveFile);
        }


        public async Task<IHttpActionResult> Put(LeaveFile _LeaveFile)
        {
            string ipadress = _LeaveFile.ipaddress;
            if (!(ModelState.IsValid))
            {
                return BadRequest(ModelState);
            }

            _unitOfWork.BeginTransaction();

            var LeaveFile = await _entParamsService.Edit(_LeaveFile, ipadress, activeuser);

            return CreatedAtRoute("", new { id = LeaveFile }, _LeaveFile);
            //return await _entParamsService.Edit(_company);
        }

        public async Task<IHttpActionResult> DELETE(int id, string ipaddress)
        {
            string ipadress = ipaddress;
            if (!(ModelState.IsValid))
            {
                return BadRequest(ModelState);
            }

            _unitOfWork.BeginTransaction();

            var LeaveFile = await _entParamsService.Delete(id, ipadress, activeuser);

            return CreatedAtRoute("", new { id = LeaveFile }, id);
        }
    }
}

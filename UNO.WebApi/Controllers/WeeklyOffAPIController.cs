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
    public class WeeklyOffAPIController : ApiController
    {
        private IWeeklyOffService _entParamsService;
        private IUnitOfWork _unitOfWork;
        private int activeuser;
        private Utilities _ipaddressobj;
        public WeeklyOffAPIController(IWeeklyOffService entWeeklyOffService, IUnitOfWork unitOfWork, Utilities _ipaddress)
        {
            _ipaddressobj = _ipaddress;
            _entParamsService = entWeeklyOffService;
            _unitOfWork = unitOfWork;
            var request = HttpContext.Current.Request;
            activeuser = Convert.ToInt32(request.Headers["activeuser"]);
        }

        public IQueryable<WeeklyOff> get()
        {
            var x = _entParamsService.GetWeeklyOff(activeuser).AsQueryable();
            return x;
        }
        
        public async Task<IHttpActionResult> Post(WeeklyOff _WeeklyOff)
        {
            string ipadress = _WeeklyOff.ipaddress;
            if (!(ModelState.IsValid))
            {
                return BadRequest(ModelState);
            }

            _unitOfWork.BeginTransaction();

            var WeeklyOff = await _entParamsService.Create(_WeeklyOff,ipadress,activeuser);

            return CreatedAtRoute("", new { id = WeeklyOff }, _WeeklyOff);
        }

        [HttpGet]
        public WeeklyOff get(int id)
        {
            return _entParamsService.GetWeeklyOffSingle(id);
        }
       
        public async Task<IHttpActionResult> Put(WeeklyOff _WeeklyOff)
        {
            string ipadress = _WeeklyOff.ipaddress;
            if (!(ModelState.IsValid))
            {
                return BadRequest(ModelState);
            }

            _unitOfWork.BeginTransaction();

            var WeeklyOff = await _entParamsService.Edit(_WeeklyOff, ipadress, activeuser);

            return CreatedAtRoute("", new { id = WeeklyOff }, _WeeklyOff);
        }      

        public async Task<IHttpActionResult> DELETE(int id, string ipaddress)
        {
            string ipadress = ipaddress;
            if (!(ModelState.IsValid))
            {
                return BadRequest(ModelState);
            }

            _unitOfWork.BeginTransaction();

            var WeeklyOff = await _entParamsService.Delete(id, ipadress, activeuser);

            return CreatedAtRoute("", new { id = WeeklyOff }, id);
        }


        [HttpGet]
        [Route("api/WeeklyOffAPI/getweekoffattandance")]
        public IQueryable<WeeklyOff> getweekoffattandance()
        {
            return _entParamsService.GetWeekOffAttandance(activeuser).AsQueryable();
        }

        [HttpGet]
        [Route("api/WeeklyOffAPI/getweekendattandance")]
        public IQueryable<WeeklyOff> getweekendattandance()
        {
            return _entParamsService.GetWeekEndAttandance(activeuser).AsQueryable();
        }
    }
}

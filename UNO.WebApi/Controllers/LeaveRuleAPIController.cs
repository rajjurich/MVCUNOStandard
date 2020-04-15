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
    public class LeaveRuleAPIController : ApiController
    {
        private ILeaveRuleService _entParamsService;
        private IUnitOfWork _unitOfWork;
        private int activeuser;
        private Utilities _ipaddressobj;
        public LeaveRuleAPIController(ILeaveRuleService entLeaveRuleService, IUnitOfWork unitOfWork, Utilities _ipaddress)
        {
            _ipaddressobj = _ipaddress;
            var request = HttpContext.Current.Request;
            activeuser = Convert.ToInt32(request.Headers["activeuser"]);
            _entParamsService = entLeaveRuleService;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public LeaveRule get(int id)
        {
            return _entParamsService.GetLeaveRule(id);
        }

        public IQueryable<LeaveRule> get()
        {
            var x = _entParamsService.GetLeaveRule().AsQueryable();
            return x;
        }

        public async Task<IHttpActionResult> Post(LeaveRule _LeaveRule)
        {
            string ipadress = _LeaveRule.ipaddress;
            if (!(ModelState.IsValid))
            {
                return BadRequest(ModelState);
            }

            _unitOfWork.BeginTransaction();

            var LeaveRule = await _entParamsService.Create(_LeaveRule,ipadress,activeuser);
            return CreatedAtRoute("", new { id = LeaveRule }, _LeaveRule);
        }

        public async Task<IHttpActionResult> Put(LeaveRule _LeaveRule)
        {
            string ipadress = _LeaveRule.ipaddress;
            if (!(ModelState.IsValid))
            {
                return BadRequest(ModelState);
            }

            _unitOfWork.BeginTransaction();

            var LeaveRule = await _entParamsService.Edit(_LeaveRule, ipadress, activeuser);

            return CreatedAtRoute("", new { id = LeaveRule }, _LeaveRule);
        }

        public async Task<IHttpActionResult> DELETE(int id, string ipaddress)
        {
            string ipadress = ipaddress;
            if (!(ModelState.IsValid))
            {
                return BadRequest(ModelState);
            }

            _unitOfWork.BeginTransaction();

            var LeaveRule = await _entParamsService.Delete(id, ipadress, activeuser);

            return CreatedAtRoute("", new { id = LeaveRule }, id);
        }
    }
}

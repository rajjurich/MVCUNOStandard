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
    public class EmployeeLeftAPIController : ApiController
    {
        private IEmployeeLeftService _entParamsService;
        private IUnitOfWork _unitOfWork;
        private int activeuser;
        private Utilities _ipaddressobj;

        public EmployeeLeftAPIController(IEmployeeLeftService entEmployeeLeftService, IUnitOfWork unitOfWork, Utilities _ipaddress)
        {
            _entParamsService = entEmployeeLeftService;
            _unitOfWork = unitOfWork;
            var request = HttpContext.Current.Request;
            activeuser = Convert.ToInt32(request.Headers["activeuser"]);
            _ipaddressobj = _ipaddress;
        }

        public IQueryable<EmployeeLeft> get()
        {
            var x = _entParamsService.GetEmployeeLeft().AsQueryable();
            return x;
        }

        [Route("api/EmployeeLeftAPI/getJoiningDate/{id}")]
        public String getJoiningDate(int id)
        {
            return _entParamsService.Get(id);
        }

        [HttpGet]
        public EmployeeLeft get(int id)
        {
            return _entParamsService.GetEmployeeLeft(id);
        }

        public async Task<IHttpActionResult> Post(EmployeeLeft _EmployeeLeft)
        {
            string ipadress = _EmployeeLeft.ipaddress;
            if (!(ModelState.IsValid))
            {
                return BadRequest(ModelState);
            }
            _unitOfWork.BeginTransaction();
            var EmployeeLeft = await _entParamsService.Create(_EmployeeLeft, ipadress, activeuser);
            return CreatedAtRoute("", new { id = EmployeeLeft }, _EmployeeLeft);
        }


        public async Task<IHttpActionResult> Put(EmployeeLeft _EmployeeLeft)
        {
            string ipadress = _EmployeeLeft.ipaddress;
            if (!(ModelState.IsValid))
            {
                return BadRequest(ModelState);
            }

            _unitOfWork.BeginTransaction();

            var EmployeeLeft = await _entParamsService.Edit(_EmployeeLeft, ipadress, activeuser);

            return CreatedAtRoute("", new { id = EmployeeLeft }, _EmployeeLeft);
        }


        public async Task<IHttpActionResult> DELETE(int id,string ipaddress)
        {
            string ipadress = ipaddress;
            if (!(ModelState.IsValid))
            {
                return BadRequest(ModelState);
            }

            _unitOfWork.BeginTransaction();

            var EmployeeLeft = await _entParamsService.Delete(id, ipadress, activeuser);

            return CreatedAtRoute("", new { id = EmployeeLeft }, id);
        }
    }
}

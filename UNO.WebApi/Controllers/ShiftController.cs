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
    public class ShiftController : ApiController
    {
        private UnitOfWork _unitOfWork;
        private IShiftService _shiftService;
        private int activeuser;
        private Utilities _ipaddressobj;
        public ShiftController(IUnitOfWork unitOfWork,
            IShiftService shiftService, Utilities _ipaddress)
        {
            _ipaddressobj = _ipaddress;
            _unitOfWork = (UnitOfWork)unitOfWork;
            _shiftService = shiftService;
            var request = HttpContext.Current.Request;
            activeuser = Convert.ToInt32(request.Headers["activeuser"]);
        }
        // GET api/shift
        public IQueryable<ShiftDto> Get()
        {
            var x = _shiftService.Get(activeuser).AsQueryable();
            return x;
        }

        // GET api/shift/5
        public async Task<IHttpActionResult> Get(int id)
        {
            var shift = await _shiftService.GetSingle(id);

            return Ok(shift);
        }

        // POST api/shift
        public async Task<IHttpActionResult> Post([FromBody]Shift shift)
        {
            string ipadress = shift.ipaddress;
            if (!(ModelState.IsValid))
            {
                return BadRequest(ModelState);
            }

            _unitOfWork.BeginTransaction();

            var shiftId = await _shiftService.Create(shift,ipadress,activeuser);



            return CreatedAtRoute("", new { id = shiftId }, shift);
        }

        // PUT api/shift/5
        public async Task<IHttpActionResult> Put(int id, [FromBody]Shift shift)
        {
            string ipadress = shift.ipaddress;
            if (!(ModelState.IsValid))
            {
                return BadRequest(ModelState);
            }
            if (id != shift.SHIFT_ID)
            {
                return BadRequest(ModelState);
            }
            Shift entShift = await _shiftService.GetSingle(id);
            if (entShift == null)
            {
                return NotFound();
            }
            _unitOfWork.BeginTransaction();
            var controllerId = await _shiftService.Edit(shift, ipadress, activeuser);

            return Ok(entShift);
        }

        // DELETE api/shift/5
        [Route("api/shift/{id}/{user}/{ipaddress}")]
        public async Task<IHttpActionResult> Delete(int id, string user,string ipaddress)
        {
            string ipadress = ipaddress;
            Shift shift = await _shiftService.GetSingle(id);
            if (shift == null)
            {
                return NotFound();
            }
            await _shiftService.Delete(id, user, ipadress, activeuser);
            return Ok(shift);
        }
    }
}

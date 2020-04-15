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
    public class ShiftPatternController : ApiController
    {
        private UnitOfWork _unitOfWork;
        private IShiftPatternService _shiftPatternService;
        private IShiftPatternShiftService _shiftPatternShiftService;
        private int activeuser;
        private Utilities _ipaddressobj;
        public ShiftPatternController(UnitOfWork unitOfWork
            , IShiftPatternService shiftPatternService
            , IShiftPatternShiftService shiftPatternShiftService, Utilities _ipaddress)
        {
            _ipaddressobj = _ipaddress;
            _unitOfWork = (UnitOfWork)unitOfWork;
            _shiftPatternService = shiftPatternService;
            _shiftPatternShiftService = shiftPatternShiftService;
            var request = HttpContext.Current.Request;
            activeuser = Convert.ToInt32(request.Headers["activeuser"]);
        }
        // GET api/shiftpattern
        public IQueryable<ShiftPatternDto> Get()
        {
            var x = _shiftPatternService.Get(activeuser).AsQueryable();
            return x;
        }
        // GET api/shiftpattern/5
        public async Task<IHttpActionResult> Get(int id)
        {
            var shiftPattern = await _shiftPatternService.GetSingle(id);

            return Ok(shiftPattern);
        }

        // POST api/shiftpattern
        public async Task<IHttpActionResult> Post([FromBody]ShiftPattern shiftPattern)
        {
            string ipadress = shiftPattern.ipaddress;
            if (!(ModelState.IsValid))
            {
                return BadRequest(ModelState);
            }

            _unitOfWork.BeginTransaction();

            var shiftPatternId = await _shiftPatternService.Create(shiftPattern,ipadress,activeuser);
            var shifts = shiftPattern.SHIFT_PATTERN.Split(',');
            int i = 1;
            foreach (var shift in shifts)
            {
                await _shiftPatternShiftService.Create(Convert.ToInt32(shift), shiftPatternId, i,ipadress,activeuser);
                i++;
            }


            return CreatedAtRoute("", new { id = shiftPatternId }, shiftPattern);
        }

        // PUT api/shiftpattern/5
        public async Task<IHttpActionResult> Put(int id, [FromBody]ShiftPattern shiftPattern)
        {

            string ipadress = shiftPattern.ipaddress;
            if (!(ModelState.IsValid))
            {
                return BadRequest(ModelState);
            }
            if (id != shiftPattern.SHIFT_PATTERN_ID)
            {
                return BadRequest(ModelState);
            }
            ShiftPattern entShift = await _shiftPatternService.GetSingle(id);
            if (entShift == null)
            {
                return NotFound();
            }
            _unitOfWork.BeginTransaction();
            var controllerId = await _shiftPatternService.Edit(shiftPattern, ipadress, activeuser);
            await _shiftPatternShiftService.Delete(id, ipadress, activeuser);
            var shifts = shiftPattern.SHIFT_PATTERN.Split(',');
            int i = 1;
            foreach (var shift in shifts)
            {
                await _shiftPatternShiftService.Edit(Convert.ToInt32(shift), id, i, ipadress, activeuser);
                i++;
            }

            return Ok(entShift);
        }

        // DELETE api/shiftpattern/5
        [Route("api/shiftpattern/{id}/{user}/{ipaddress}")]
        public async Task<IHttpActionResult> Delete(int id, string user,string ipaddress)
        {
            string ipadress = ipaddress;
            ShiftPattern shiftPattern = await _shiftPatternService.GetSingle(id);
            if (shiftPattern == null)
            {
                return NotFound();
            }
            await _shiftPatternService.Delete(id, user, ipadress, activeuser);
            return Ok(shiftPattern);
        }


        [HttpGet]
        [Route("api/ShiftPattern/getpatterns")]
        public IQueryable<ShiftPattern> getpatterns()
        {
            var x = _shiftPatternService.getshiftpattern(activeuser).AsQueryable();
            return x;
        }
    }
}

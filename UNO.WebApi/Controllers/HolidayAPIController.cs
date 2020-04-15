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
    public class HolidayAPIController : ApiController
    {

        private IHolidayService _entHolidayService;
        private IHolidayLocService _Holiday_loc;
        private int activeuser;
        private Utilities _ipaddressobj;
        public HolidayAPIController(IHolidayService entHolidayService, IHolidayLocService entHoliday_loc, Utilities _ipaddress)
        {
            _ipaddressobj = _ipaddress;
            var request = HttpContext.Current.Request;
            activeuser = Convert.ToInt32(request.Headers["activeuser"]);
            _entHolidayService = entHolidayService;
            _Holiday_loc = entHoliday_loc;
        }
        [HttpGet]
        public List<HolidayDetails> get()
        {
            return _entHolidayService.GetHolidayDetails();
        }
        [HttpGet]
        public HolidayDetails get(int id)
        {
            HolidayDetails objHolidayDetails = new HolidayDetails();
            objHolidayDetails = _entHolidayService.GetHolidayDetails(id);
            List<HolidayLocation> listHolidayLocation = new List<HolidayLocation>();
            listHolidayLocation = _Holiday_loc.GetHolidayLoc(id);
            objHolidayDetails.HolidayLoc = listHolidayLocation;
            return objHolidayDetails;
        }
        [HttpGet]
        [Route("api/HolidayAPI/getID")]
        public List<HolidayLocation> getID()
        {
            return _Holiday_loc.GetHolidayLoc();
        }

        [HttpPost]
        public async Task<IHttpActionResult> post(HolidayDetails entity)
        {
            string ipadress = entity.ipaddress;
            try
            {
                var y = await _entHolidayService.IsUniqHolidayCode(entity, false);

                if (y)
                {
                    return BadRequest("Hoilday Code Already Exists!!");
                }
                var controllerId = await _entHolidayService.Create(entity,ipadress,activeuser);
                int HolidayId = Convert.ToInt32(controllerId);
                for (int i = 0; i < entity.HolidayLoc.Count; i++)
                {
                    entity.HolidayLoc[i].HOLIDAY_ID = HolidayId;
                }
                if (entity.HolidayLoc.Count > 0)
                {
                    var controllerLocId = await _Holiday_loc.Create(entity.HolidayLoc,ipadress,activeuser);                    
                }
                var x = CreatedAtRoute("", new { id = HolidayId }, entity);

                return x;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        public async Task<IHttpActionResult> put(HolidayDetails entity)
        {
            string ipadress = entity.ipaddress;
            try
            {
                var y = await _entHolidayService.IsUniqHolidayCode(entity, true);

                if (y)
                {
                    return BadRequest("Hoilday Code Already Exists For Other!!");
                }
                var controllerId = await _entHolidayService.Edit(entity,ipadress,activeuser);
                for (int i = 0; i < entity.HolidayLoc.Count; i++)
                {
                    entity.HolidayLoc[i].HOLIDAY_ID = entity.HOLIDAY_ID;
                }
                var controllerLocId = await _Holiday_loc.Edit(entity.HolidayLoc, ipadress, activeuser);
                var x = CreatedAtRoute("", new { id = controllerId }, entity);
                return x;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        public async Task<IHttpActionResult> Delete(int id,string ipaddress)
        {
            string ipadress = ipaddress;
            HolidayDetails objHolidayDetails = new HolidayDetails();
            var controllerId = await _entHolidayService.Delete(id,ipadress,activeuser);
            return CreatedAtRoute("", new { id = controllerId }, id);
        }

    }
}

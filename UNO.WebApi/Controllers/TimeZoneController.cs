using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UNO.WebApi.Models;
using UNO.WebApi.Services;

namespace UNO.WebApi.Controllers
{
    public class TimeZoneController : ApiController
    {
        private ITimeZoneService _timeZoneService;

        public TimeZoneController(ITimeZoneService timeZoneService)
        {
            _timeZoneService = timeZoneService;
        }
        // GET api/timezone
        public IQueryable<AcsTimeZone> Get()
        {
            var x = _timeZoneService.Get().AsQueryable();
            return x;
        }

        // GET api/timezone/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/timezone
        public void Post([FromBody]string value)
        {
        }

        // PUT api/timezone/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/timezone/5
        public void Delete(int id)
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using UNO.DAL;
using UNO.WebApi.Dto;
using UNO.WebApi.Services;

namespace UNO.WebApi.Controllers
{
    public class TdayController : ApiController
    {
        private UnitOfWork _unitOfWork;
        private ITDayService _tdayService;
        private int activeuser;
        public TdayController(IUnitOfWork unitOfWork, ITDayService tdayService)
        {
            _unitOfWork = (UnitOfWork)unitOfWork;
            _tdayService = tdayService;
            var request = HttpContext.Current.Request;
            activeuser = Convert.ToInt32(request.Headers["activeuser"]);
        }
        // GET api/tday
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/tday/GetTdayByEmployeeIdMonthWise/12/2019/1
        [Route("api/tday/GetTdayByEmployeeIdMonthWise/{month}/{year}/{id}")]
        public IQueryable<TDayDtoDashboard> GetTdayByEmployeeIdMonthWise(int month, int year, int id)
        {
            var x = _tdayService.GetSingle(activeuser).AsQueryable().Where(t => t.TDAY_DATE.Month == (month + 1) && t.TDAY_DATE.Year == year).ToList();

            var y = (from a in x
                     select new TDayDtoDashboard
                     {
                         title = " <small><b>" + a.Workday + "</b> <br/>" + a.Tday_Status + " <br/><br/><div><small class=pull-left>" + a.InTime + "</small> <small class='pull-right'>" + a.OutTime + "</small></div></small>",
                         start = a.TDAY_DATE,
                         allDay = true,
                         className = a.ClassName
                     }).AsQueryable();
            return y;
        }

        // GET api/tday/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/tday
        public void Post([FromBody]string value)
        {
        }

        // PUT api/tday/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/tday/5
        public void Delete(int id)
        {
        }
    }
}

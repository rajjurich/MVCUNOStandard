using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using UNO.DAL;
using UNO.WebApi.Models;
using UNO.WebApi.Services;

namespace UNO.WebApi.Controllers
{
    public class EventBrowserAPIController : ApiController
    {


        //private UnitOfWork _unitOfWork;
        //private DatabaseHelper _DatabaseHelper;

        private IEventBrowserService _entEventBrowserService;

        public EventBrowserAPIController(IEventBrowserService entEventBrowserService)
        {
            _entEventBrowserService = entEventBrowserService;
        }
        // GET api/entparams

        [Route("api/EventBrowserAPI/get/{Event_Type}/{Level}")]
        [HttpGet]
        public List<EventBrowserDetails> get(int Event_Type, int Level)
        {
            return _entEventBrowserService.GetEvents(Event_Type, Level);
        }
    }
}

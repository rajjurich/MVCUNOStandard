using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UNO.DAL;
using UNO.WebApi.Models;
using UNO.WebApi.Services;

namespace UNO.WebApi.Controllers
{
    public class EntParamsController : ApiController
    {
        //private UnitOfWork _unitOfWork;
        //private DatabaseHelper _DatabaseHelper;
        private IEntParamsService _entParamsService;
        public EntParamsController(IEntParamsService entParamsService)
        {
            _entParamsService = entParamsService;
        }
        // GET api/entparams
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/entparams/{identifier}
        [Route("api/entparams/{identifier}")]
        public IEnumerable<EntParams> Get(string identifier)
        {
            var result = _entParamsService.Get(identifier);
            return result;
        }
        // GET api/entparams/5
        [Route("api/entparams/{id:int}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/entparams
        public void Post([FromBody]string value)
        {
        }

        // PUT api/entparams/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/entparams/5
        public void Delete(int id)
        {
        }
    }
}

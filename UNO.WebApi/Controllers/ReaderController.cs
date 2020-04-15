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
    public class ReaderController : ApiController
    {
        private IReaderService _readerService;
        private UnitOfWork _unitOfWork;
        private int activeuser;
        public ReaderController(IReaderService readerService
            , IUnitOfWork unitOfWork)
        {
            _readerService = readerService;
            _unitOfWork = (UnitOfWork)unitOfWork;
            var request = HttpContext.Current.Request;
            activeuser = Convert.ToInt32(request.Headers["activeuser"]);

        }
        // GET api/reader
        public IQueryable<AcsReaderDto> Get()
        {
            return _readerService.Get(activeuser);
        }

        // GET api/reader/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/reader
        public void Post([FromBody]string value)
        {
        }

        // PUT api/reader/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/reader/5
        public void Delete(int id)
        {
        }
    }
}

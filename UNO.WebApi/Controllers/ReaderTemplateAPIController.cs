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
    public class ReaderTemplateAPIController : ApiController
    {
        private IReaderTemplateService _entParamsService;
        private IUnitOfWork _unitOfWork;
        private int activeuser;
        private Utilities _ipaddressobj;
        public ReaderTemplateAPIController(IReaderTemplateService entReaderTemplateService, IUnitOfWork unitOfWork, Utilities _ipaddress)
        {
            _ipaddressobj = _ipaddress;
            var request = HttpContext.Current.Request;
            activeuser = Convert.ToInt32(request.Headers["activeuser"]);
            _entParamsService = entReaderTemplateService;
            _unitOfWork = unitOfWork;
        }
        // GET api/entparams

        //[Route("api/Menu/GetMenuList")]

        public IQueryable<ReaderTemplate> get()
        {
            var x = _entParamsService.GetReaderTemplate().AsQueryable();
            return x;
        }

        [HttpGet]
        [Route("api/ReaderTemplateAPI/{EventID}/{ControllerID}")]
        public string get(int EventID, int ControllerID)
        {
            return _entParamsService.GetChkReaderExist(EventID, ControllerID);
        }

        [HttpGet]
        public ReaderTemplate get(int id)
        {
            return _entParamsService.GetReaderTemplate(id);
        }

        public async Task<IHttpActionResult> Post(ReaderTemplate _ReaderTemplate)
        {
            string ipadress = _ReaderTemplate.ipaddress;
            if (!(ModelState.IsValid))
            {
                return BadRequest(ModelState);
            }

          string exist= get(_ReaderTemplate.EventID, _ReaderTemplate.ControllerID);

          if (exist == "False")
          {
              _unitOfWork.BeginTransaction();
              var ReaderTemplate = await _entParamsService.Create(_ReaderTemplate,ipadress,activeuser);
              return CreatedAtRoute("", new { id = ReaderTemplate }, _ReaderTemplate);
          }
          else
          {
              return BadRequest("Reader Template Already exist");
          }
        }

        public async Task<IHttpActionResult> Put(ReaderTemplate _ReaderTemplate)
        {
            string ipadress = _ReaderTemplate.ipaddress;
            if (!(ModelState.IsValid))
            {
                return BadRequest(ModelState);
            }

            _unitOfWork.BeginTransaction();

            var ReaderTemplate = await _entParamsService.Edit(_ReaderTemplate, ipadress, activeuser);

            return CreatedAtRoute("", new { id = ReaderTemplate }, _ReaderTemplate);
        }

        public async Task<IHttpActionResult> DELETE(int id, string ipaddress)
        {
            string ipadress = ipaddress;
            if (!(ModelState.IsValid))
            {
                return BadRequest(ModelState);
            }

            _unitOfWork.BeginTransaction();

            var ReaderTemplate = await _entParamsService.Delete(id, ipadress, activeuser);

            return CreatedAtRoute("", new { id = ReaderTemplate }, id);
        }
    }
}

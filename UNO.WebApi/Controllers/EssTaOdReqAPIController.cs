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
    public class EssTaOdReqAPIController : ApiController
    {
         private IEssTaOdReqService _entEssTaOdReqService;
        private IUnitOfWork _unitOfWork;
        private int activeuser;
        private Utilities _ipaddressobj;

        public EssTaOdReqAPIController(IEssTaOdReqService entEssTaOdReqService, IUnitOfWork unitOfWork, Utilities _ipaddress)
        {
            _ipaddressobj = _ipaddress;
            var request = HttpContext.Current.Request;
            activeuser = Convert.ToInt32(request.Headers["activeuser"]);
            _entEssTaOdReqService = entEssTaOdReqService;
            _unitOfWork = unitOfWork;
        }
          public List<EssTaOdReq> get()
        {
            return _entEssTaOdReqService.GetEssTaOdReq();
        }

          [HttpGet]
          public EssTaOdReq get(int id)
          {
              return _entEssTaOdReqService.GetEssTaOdReq(id);
          }

          public async Task<IHttpActionResult> Put(EssTaOdReq _EssTaOdReq)
          {
              string ipadress = _EssTaOdReq.ipaddress;
              if (!(ModelState.IsValid))
              {
                  return BadRequest(ModelState);
              }

              var EssTaOdReq = await _entEssTaOdReqService.Edit(_EssTaOdReq,ipadress,activeuser);

              return CreatedAtRoute("", new { id = EssTaOdReq }, _EssTaOdReq);
          }

          public async Task<IHttpActionResult> DELETE(int id, string ipaddress)
          {
              string ipadress = ipaddress;
              if (!(ModelState.IsValid))
              {
                  return BadRequest(ModelState);
              }

              _unitOfWork.BeginTransaction();

              var EssTaOdReq = await _entEssTaOdReqService.Delete(id, ipadress, activeuser);

              return CreatedAtRoute("", new { id = EssTaOdReq }, id);
          }
    }
}

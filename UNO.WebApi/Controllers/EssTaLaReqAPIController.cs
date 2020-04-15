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
    public class EssTaLaReqAPIController : ApiController
    {
        private IEssTaLaReqService _entEssTaLaReqService;
        private IUnitOfWork _unitOfWork;
        private int activeuser;
        private Utilities _ipaddressobj;
        public EssTaLaReqAPIController(IEssTaLaReqService entEssTaLaReqService, IUnitOfWork unitOfWork, Utilities _ipaddress)
        {
            _ipaddressobj = _ipaddress;
            var request = HttpContext.Current.Request;
            activeuser = Convert.ToInt32(request.Headers["activeuser"]);
            _entEssTaLaReqService = entEssTaLaReqService;
            _unitOfWork = unitOfWork;
        }

        public IQueryable<EssTaLaReq> get()
        {
            var x = _entEssTaLaReqService.GetEssTaLaReq().AsQueryable();
            return x;
        }

          [HttpGet]
          public EssTaLaReq get(int id)
          {
              return _entEssTaLaReqService.GetEssTaLaReq(id);
          }

          public async Task<IHttpActionResult> Put(EssTaLaReq _EssTaLaReq)
          {
              string ipadress = _EssTaLaReq.ipaddress;
              if (!(ModelState.IsValid))
              {
                  return BadRequest(ModelState);
              }

              var EssTaLaReq = await _entEssTaLaReqService.Edit(_EssTaLaReq,ipadress,activeuser);

              return CreatedAtRoute("", new { id = EssTaLaReq }, _EssTaLaReq);
          }

          public async Task<IHttpActionResult> DELETE(int id, string ipaddress)
          {
              string ipadress = ipaddress;
              if (!(ModelState.IsValid))
              {
                  return BadRequest(ModelState);
              }

              _unitOfWork.BeginTransaction();

              var EssTaLaReq = await _entEssTaLaReqService.Delete(id, ipadress, activeuser);

              return CreatedAtRoute("", new { id = EssTaLaReq }, id);
          }
    }
}

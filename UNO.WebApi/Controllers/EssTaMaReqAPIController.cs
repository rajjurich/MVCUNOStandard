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
    public class EssTaMaReqAPIController : ApiController
    {
        private IEssTaMaReqService _entEssTaMaReqService;
        private IUnitOfWork _unitOfWork;
        private int activeuser;
        private Utilities _ipaddressobj;
        public EssTaMaReqAPIController(IEssTaMaReqService entEssTaMaReqService, IUnitOfWork unitOfWork, Utilities _ipaddress)
        {
            _ipaddressobj = _ipaddress;
            var request = HttpContext.Current.Request;
            activeuser = Convert.ToInt32(request.Headers["activeuser"]);
            _entEssTaMaReqService = entEssTaMaReqService;
            _unitOfWork = unitOfWork;
        }

        public IQueryable<EssTaMaReq> get()
        {
            var x = _entEssTaMaReqService.GetEssTaMaReq().AsQueryable();
            return x;
        }

          [HttpGet]
        public EssTaMaReq get(int id)
          {
              return _entEssTaMaReqService.GetEssTaMaReq(id);
          }

          public async Task<IHttpActionResult> Put(EssTaMaReq _EssTaMaReq)
          {
              string ipadress = _EssTaMaReq.ipaddress;
              if (!(ModelState.IsValid))
              {
                  return BadRequest(ModelState);
              }

              var EssTaMaReq = await _entEssTaMaReqService.Edit(_EssTaMaReq,ipadress,activeuser);

              return CreatedAtRoute("", new { id = EssTaMaReq }, _EssTaMaReq);
          }

          public async Task<IHttpActionResult> DELETE(int id,string ipaddress)
          {
              string ipadress = ipaddress;
              if (!(ModelState.IsValid))
              {
                  return BadRequest(ModelState);
              }

              _unitOfWork.BeginTransaction();

              var EssTaMaReq = await _entEssTaMaReqService.Delete(id, ipadress, activeuser);

              return CreatedAtRoute("", new { id = EssTaMaReq }, id);
          }
    }
}

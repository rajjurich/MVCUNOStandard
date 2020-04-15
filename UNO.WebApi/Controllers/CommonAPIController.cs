using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using UNO.DAL;
using UNO.WebApi.Models;
using UNO.WebApi.Services;
using System.Web;
using UNO.MVCApp.Helpers;

namespace UNO.WebApi.Controllers
{
    public class CommonAPIController : ApiController
    {
        private ICommonService _entParamsService;
        private UnitOfWork _unitOfWork;
        private int activeuser;
        private Utilities _ipaddressobj;

        public CommonAPIController(ICommonService entMenuService, IUnitOfWork unitOfWork, Utilities _ipaddress)
        {
            _ipaddressobj = _ipaddress;
            _entParamsService = entMenuService;
            _unitOfWork = (UnitOfWork)unitOfWork;
            var request = HttpContext.Current.Request;
            activeuser = Convert.ToInt32(request.Headers["activeuser"]);
        }

        [HttpGet]
        public List<Common> get()
        {
            return _entParamsService.GetCommon(activeuser);
        }

        [HttpGet]
        [Route("api/CommonAPI/GetByTypes/{types}")]
        public List<Common> getAllCategories(string types)
        {
            var x = _entParamsService.GetCommon(activeuser).Where(a => a.COMMON_NAME.ToLower() == types.ToLower()).ToList();
            return x;
        }

        [HttpGet]
        [Route("api/CommonAPI/GetByTypesWithNoAccess/{types}")]
        public List<Common> getAllCommonWithNoAccess(string types)
        {
            var x = _entParamsService.GetCommonWithNoAccess(activeuser, types).Where(a => a.COMMON_NAME.ToLower() == types.ToLower()).ToList();
            return x;
        }

        [HttpGet]
        public Common get(int id)
        {
            return _entParamsService.GetCommonSingle(id);
        }

        //[HttpPost]
        public async Task<IHttpActionResult> Post(Common _common)
        {
            string ipadress = _ipaddressobj.GetIpAddress();
            if (!(ModelState.IsValid))
            {
                return BadRequest(ModelState);
            }
            var x = await _entParamsService.IsUniqOCECode(_common, false);
            if (x)
            {
                return BadRequest("Oce Code Already Exists!!");
            }
            _unitOfWork.BeginTransaction();

            var controllerId = await _entParamsService.Create(_common, ipadress,activeuser);

            return CreatedAtRoute("", new { id = controllerId }, _common);
        }

        //[HttpPost]
        public async Task<IHttpActionResult> Put(Common _common)
        {
            string ipadress = _ipaddressobj.GetIpAddress();
            //return await _entParamsService.Edit(_company);

            if (!(ModelState.IsValid))
            {
                return BadRequest(ModelState);
            }
            var x = await _entParamsService.IsUniqOCECode(_common, true);
            if (x)
            {
                return BadRequest("Oce Code Already Exists for Other!!");
            }
            _unitOfWork.BeginTransaction();

            var controllerId = await _entParamsService.Edit(_common, ipadress,activeuser);

            return CreatedAtRoute("", new { id = controllerId }, _common);
        }

        public async Task<IHttpActionResult> DELETE(int id,string ipaddress)
        {
            string ipadress = ipaddress;
            if (!(ModelState.IsValid))
            {
                return BadRequest(ModelState);
            }

            _unitOfWork.BeginTransaction();

            var controllerId = await _entParamsService.Delete(id, ipadress,activeuser);

            return CreatedAtRoute("", new { id = controllerId }, id);
        }


        [Route("api/CommonAPI/FilterValuess/{id}/{conditions}")]
        public List<FilterModel> GetFilterValuess(string id, string conditions)
        {
            string coindi=conditions.Replace("-", ".");

            List<FilterModel> lst = _entParamsService.GetFilterValues(id, activeuser, coindi);
            return lst;

        }
    }
}

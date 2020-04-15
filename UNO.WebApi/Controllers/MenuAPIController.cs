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
    public class MenuAPIController : ApiController
    {


        //private UnitOfWork _unitOfWork;
        //private DatabaseHelper _DatabaseHelper;

        private IMenuService _entMenuService;
        private int activeuser;
        private Utilities _ipaddressobj;

        public MenuAPIController(IMenuService entMenuService, Utilities _ipaddress)
        {
            _ipaddressobj = _ipaddress;
            var request = HttpContext.Current.Request;
            activeuser = Convert.ToInt32(request.Headers["activeuser"]);
            _entMenuService = entMenuService;
        }
        // GET api/entparams

        //[Route("api/Menu/GetMenuList")]
        [HttpGet]
        public List<Menu> get()
        {
            return _entMenuService.GetMenu();
        }

        [HttpGet]
        public Menu get(int id)
        {
            return _entMenuService.GetMenu(id);
        }

        [HttpPost]
        public async Task<IHttpActionResult> post(Menu _menu)
        {
            string ipadress = _menu.ipaddress;
            var x = await _entMenuService.IsUniqMenuUrl(_menu, false);

            if (x)
            {
                return BadRequest("Menu Url Already Exists!!");
            }
            var controllerId = await _entMenuService.Create(_menu,ipadress,activeuser);
            return CreatedAtRoute("", new { id = controllerId }, _menu);
        }

        [HttpPut]
        public async Task<IHttpActionResult> put(Menu _menu)
        {
            string ipadress = _menu.ipaddress;
            var x = await _entMenuService.IsUniqMenuUrl(_menu, true);

            if (x)
            {
                return BadRequest("Menu Url Already Exists For Other!!");
            }

            var controllerId = await _entMenuService.Edit(_menu, ipadress, activeuser);

          return CreatedAtRoute("", new { id = controllerId }, _menu);
        }

        [HttpDelete]
        public async Task<IHttpActionResult> Delete(int id,string ipaddress)
        {
            string ipadress = ipaddress;
            var controllerId = await _entMenuService.Delete(id, ipadress, activeuser);

            return CreatedAtRoute("", new { id = controllerId }, id);
        }        
       


    }
}

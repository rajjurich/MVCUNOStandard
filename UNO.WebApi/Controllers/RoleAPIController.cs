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
    public class RoleAPIController : ApiController
    {


        //private UnitOfWork _unitOfWork;
        //private DatabaseHelper _DatabaseHelper;

        private IRoleService _entRoleService;
        private int activeuser;
        private Utilities _ipaddressobj;
        public RoleAPIController(IRoleService entRoleService, Utilities _ipaddress)
        {
            _ipaddressobj = _ipaddress;
            _entRoleService = entRoleService;
            var request = HttpContext.Current.Request;
            activeuser = Convert.ToInt32(request.Headers["activeuser"]);
        }
        // GET api/entparams

        //[Route("api/Menu/GetMenuList")]
        [HttpGet]
        public List<Role> get()
        {
            return _entRoleService.GetRole(activeuser);
        }
        [Route("api/RoleAPI/getByCompanyId/{id}")]
        public List<Role> getByCompanyId(int id)
        {
            return _entRoleService.GetRoleByCompanyId(id);
        }

        [HttpGet]
        public Role get(int id)
        {
            return _entRoleService.GetSingleRole(id);
        }

        [HttpPost]
        public async Task<int> post(Role _role)
        {
            string ipadress = _ipaddressobj.GetIpAddress();
            return await _entRoleService.Create(_role,ipadress,activeuser);          
        }

        [HttpPut]
        public async Task<int> put(Role _role)
        {
            string ipadress = _ipaddressobj.GetIpAddress();
            return await _entRoleService.Edit(_role, ipadress, activeuser);
        }

        [HttpDelete]
        public async Task<int> Delete(int id)
        {
            string ipadress = _ipaddressobj.GetIpAddress();
            return await _entRoleService.Delete(id, ipadress, activeuser);
        }        
       


    }
}

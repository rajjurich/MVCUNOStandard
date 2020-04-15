using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using UNO.MVCApp.Helpers;
using UNO.WebApi.Models;
using UNO.WebApi.Services;

namespace UNO.WebApi.Controllers
{
    public class UserApiController : ApiController
    {

        private IUserService _entUserService;
        private int activeuser;
        private Utilities _ipaddressobj;

        public UserApiController(IUserService entUserService, Utilities _ipaddress)
        {
            _ipaddressobj = _ipaddress;
            _entUserService = entUserService;
            var request = HttpContext.Current.Request;
            activeuser = Convert.ToInt32(request.Headers["activeuser"]);
        }
        // GET api/entparams

        [Route("api/UserApi")]
        [HttpGet]
        public List<User> get()
        {
            return _entUserService.GetUser(activeuser);
        }

        [HttpGet]
        public User get(int id)
        {
            return _entUserService.GetSingleUser(id);
        }

        [HttpPost]
        [Route("api/Userapi/")]
        public async Task<IHttpActionResult> post(User _user)
        {
            string ipadress = _user.ipaddress;
            if (!(ModelState.IsValid))
            {
                return BadRequest(ModelState);
            }
            
            var UserId = await _entUserService.Create(_user,ipadress,activeuser);

            return Ok();// CreatedAtRoute("", new { id = UserId }, _user);
        }

        [HttpPut]
        public async Task<IHttpActionResult> put(User _user)
        {
            string ipadress = _user.ipaddress;
            var UserId = await _entUserService.Edit(_user, ipadress, activeuser);

            return CreatedAtRoute("", new { id = UserId }, _user);
        }

        [HttpDelete]
        public async Task<IHttpActionResult> Delete(int id,string ipaddress)
        {
            string ipadress = ipaddress;
            var UserId = await _entUserService.Delete(id, ipadress, activeuser, "");

            return CreatedAtRoute("", new { id = UserId }, id);
        }

        [HttpPost]
        [Route("api/Userapi/ValidateLogin")]
        public async Task<IHttpActionResult> ValidateLogin(UserLoginApiModel userobj)
        {

            
                string version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
                string versiontrimed = version.Substring(0, 4);
                if (versiontrimed != userobj.verionsofwebapp)
                {
                    //User usersys = new User();

                    return BadRequest("Version Doesnt Match");
                }
                var resultchecker = await _entUserService.IsUserValid(userobj.USER_CODE, userobj.Password);
                return Ok(resultchecker);
                
        }


        

        [HttpPost]
        [Route("api/Userapi/ChangePassword")]
        public async Task<IHttpActionResult> ChangePassword(UserLoginApiModel userobj)
        {
            string ipadress = _ipaddressobj.GetIpAddress();
            var UserId = await _entUserService.ChangePassword(Convert.ToString(userobj.USER_ID), userobj.Password, userobj.isReset, ipadress, activeuser);

            return CreatedAtRoute("", new { id = UserId }, userobj.USER_CODE);
        }

    }
}

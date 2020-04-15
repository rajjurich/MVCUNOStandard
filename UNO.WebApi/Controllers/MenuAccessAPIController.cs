using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using UNO.MVCApp.Helpers;
using UNO.WebApi.Models;
using UNO.WebApi.Services;

namespace UNO.WebApi.Controllers
{
    public class MenuAccessAPIController : ApiController
    {
        private IRoleMenuAccessService _IRoleMenuAccessService;

        private int activeuser;
        private Utilities _ipaddressobj;
        public MenuAccessAPIController(IRoleMenuAccessService entMenuService, Utilities _ipaddress)
        {
            _ipaddressobj = _ipaddress;
            var request = HttpContext.Current.Request;
            activeuser = Convert.ToInt32(request.Headers["activeuser"]);
            _IRoleMenuAccessService = entMenuService;
        }



        [System.Web.Http.HttpGet]
        [Route("api/MenuAccessAPI/GetSingleMenuAccess/{Parameter}/{Parameter2}/{RoleID}")]
        public HttpResponseMessage GetSingleMenuAccess(string Parameter, string Parameter2, string RoleID)
        {
            try
            {
                string link =  Parameter + "/" + Parameter2;
                // Logic :- Get  Menu access Assigne to role
                var data = _IRoleMenuAccessService.GetRoleMenuAccess(link, RoleID);
                if (data != null)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, data);
                    response.Headers.Location = new Uri(this.Request.RequestUri.AbsoluteUri + "/" + data);
                    return response;
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }

                //return null;
            }
            catch (Exception ex)
            {
                //new clsExceptionRepository().DBErrorLog(ex.Message, ex.StackTrace, "MenuAccessAPI");
                Console.WriteLine(ex);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        [System.Web.Http.HttpGet]
        [Route("api/MenuAccessAPI/GetAllMenuRoleWise/{RoleID}")]
        public async Task<HttpResponseMessage> GetAllMenuRoleWise(string RoleID)
        {
            try
            {              

                var data = await _IRoleMenuAccessService.GetAllMenuRoleWise(RoleID);
                if (data != null)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, data);
                    response.Headers.Location = new Uri(this.Request.RequestUri.AbsoluteUri + "/" + data);
                    return response;
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }

               
            }
            catch (Exception ex)
            {
                //new clsExceptionRepository().DBErrorLog(ex.Message, ex.StackTrace, "MenuAccessAPI");
                Console.WriteLine(ex);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }


        }
        [System.Web.Http.HttpPost]

        public async Task<HttpResponseMessage> PostRoleMenuAccess(object[] data)
        {
            //string s = "";
            int Saved = 0;
            string ipadress = _ipaddressobj.GetIpAddress();
                List<RoleMenuAccess> objRMAList = new List<RoleMenuAccess>();
                
                foreach (var objRMA in data)
                {
                    RoleMenuAccess rma = new RoleMenuAccess();   
                    //rma = JsonConvert.DeserializeObject<mstrolemenuaccess>(data[0].ToString());

                    //dynamic al = JValue.Parse(data[0].ToString().Replace(" ",""));

                    dynamic al = JValue.Parse(objRMA.ToString().Replace(" ", ""));

                    //foreach (dynamic zone in al)
                    //{
                    rma.ROLE_ACCESS_ID = al.rmaroleaccessid;
                    rma.CreateAccess = (al.rmacreateaccess == true ? (Int16)1 : (Int16)0);
                    rma.DeleteAccess = (al.rmadeleteaccess == true ? (Int16)1 : (Int16)0);
                    rma.IsDeleted = 0;
                    rma.MENU_ID = al.rmamnuid;
                    //rma.rmarecid = al.rmarecid;
                    rma.ViewAccess = (al.rmareadaccess == true ? (Int16)1 : (Int16)0);
                    //rma.rmarecid = al.rmarecid;
                    rma.ROLE_ID = al.rmaroleid;
                    rma.UpdateAccess = (al.rmaupdateaccess == true ? (Int16)1 : (Int16)0);
                    //}
                    //var x = await _IRoleMenuAccessService.InsertRoleMenuAccess(rma);
                    objRMAList.Add(rma);
                    //rma = objRMA as mstrolemenuaccess;
                }

                var data1 = await _IRoleMenuAccessService.InsertRoleMenuAccessBulk(objRMAList,ipadress,activeuser);

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, data1);
                response.Headers.Location = new Uri(this.Request.RequestUri.AbsoluteUri + "PostRoleMenuAccess/" + data1);
                return response;

        }

        [System.Web.Http.HttpGet]
        [Route("api/MenuAccessAPI/GetMenuRoleWise/{RoleID}")]
        public HttpResponseMessage GetMenuRoleWise(string RoleID)
        {
            try
            {
                // Logic :-  Menu's matching to roles will get selected.

                var data = _IRoleMenuAccessService.GetMenuRoleWise(RoleID);
                if (data != null)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, data);
                    response.Headers.Location = new Uri(this.Request.RequestUri.AbsoluteUri + "/" + data);
                    return response;
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }


        }
    }
}

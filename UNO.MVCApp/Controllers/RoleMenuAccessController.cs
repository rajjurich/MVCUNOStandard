using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using UNO.AppModel;
using UNO.MVCApp.Filters;

namespace UNO.MVCApp.Controllers
{
    [Authorize]
    [SessionCheck]
    public class RoleMenuAccessController : Controller
    {
        string _uri = WebConfigurationManager.AppSettings["APIUrl"];
        string uri = string.Empty;
        //
        //
        // GET: /RoleMenuAccess/
        [AccessCheck(IdParamName = "RoleMenuAccess/index")]
        public async Task<ActionResult> Index()
        {
            List<RoleModule> rolemodule = new List<RoleModule>();
            //List<MenuModel> MenuList = new List<MenuModel>();

            using (HttpClient client = new HttpClient())
            {
                client.AddTokenToHeader("");
                uri = string.Format("{0}RoleAPI", _uri);
                var result = await client.GetAsync(uri);
                var roleContent = await result.Content.ReadAsAsync<List<RoleModule>>();
                var Roles = roleContent.Select(c => new SelectListItem
                {
                    Value = c.ROLE_ID.ToString(),
                    Text = c.ROLE_Name,
                    Selected = false
                });
                ViewBag.RoleId = Roles;
               
            }

            //ViewBag.MenuList = new SelectList(MenuList, "MENU_ID", "MENU_NAME");
            return View();
        }

    }
}

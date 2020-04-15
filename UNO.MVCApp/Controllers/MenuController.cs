using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
using UNO.MVCApp.Common;
using UNO.MVCApp.Filters;
using UNO.WebApi.Helpers;

namespace UNO.MVCApp.Controllers
{
    [Authorize]
    [SessionCheck]
    public class MenuController : Controller
    {
        string url = WebConfigurationManager.AppSettings["APIUrl"];

        // GET: /Menu/
        [AccessCheck(IdParamName = "Menu/index")]
        public ActionResult Index()
        {
            var data = ViewData.Model as RoleMenuAccess;
            if (data.CreateAccess == 0)
                ViewBag.CreateAccess = "False";
            if (data.ViewAccess == 0)
                ViewBag.ViewAccess = "False";
            if (data.UpdateAccess == 0)
                ViewBag.EditAccess = "False";
            if (data.DeleteAccess == 0)
                ViewBag.DeleteAccess = "False";
            ViewBag.Message = TempData["Message"];
            ViewBag.Status = TempData["Status"];
            ViewBag.InnerMessage = TempData["InnerMessage"];

            List<MenuModel> MenuList = new List<MenuModel>();
            using (WebClient client = new WebClient())
            {

                string s = client.DownloadString(url + "MenuAPI");
                MenuList = JsonConvert.DeserializeObject<List<MenuModel>>(s);
                MenuList.RemoveAll(item => item == null);
            }
            return View(MenuList);
        }


        private ActionResult FillData(long id)
        {
            MenuModel menumodel = new MenuModel();

            List<Module> module = new List<Module>();
            List<SubModule> submodule = new List<SubModule>();

            using (WebClient client = new WebClient())
            {
                string mdl = client.DownloadString(url + "ModuleApi");
                string mg = client.DownloadString(url + "SubModuleApi");
                string menu = client.DownloadString(url + "menuApi/" + id);
                module = JsonConvert.DeserializeObject<List<Module>>(mdl);
                submodule = JsonConvert.DeserializeObject<List<SubModule>>(mg);
                menumodel = JsonConvert.DeserializeObject<MenuModel>(menu);

            }
            ViewBag.ModuleId = new SelectList(module, "MODULE_ID", "MODULE_NAME");
            ViewBag.SubModuleId = new SelectList(submodule, "SMODULE_ID", "SMODULE_NAME");

            return View(menumodel);
        }
        //
        // GET: /menu/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /menu/Create
        public ActionResult Create()
        {
            MenuModel menumodel = new MenuModel();
            List<Module> module = new List<Module>();
            List<SubModule> submodule = new List<SubModule>();
            using (WebClient client = new WebClient())
            {
                string mdl = client.DownloadString(url + "ModuleApi");
                module = JsonConvert.DeserializeObject<List<Module>>(mdl);
            }
            ViewBag.ModuleId = new SelectList(module, "MODULE_ID", "MODULE_NAME");
            ViewBag.SubModuleId = new SelectList(submodule, "SMODULE_ID", "SMODULE_NAME");
            return View(menumodel);
        }

        [HttpPost]
        public ActionResult subModuleByID(string id)
        {
            List<SubModule> submodule = new List<SubModule>();
            using (WebClient client = new WebClient())
            {
                string mg = client.DownloadString(url + "SubModuleApi" + "/" + id);
                submodule = JsonConvert.DeserializeObject<List<SubModule>>(mg);

            }
            SelectList objSubModule = new SelectList(submodule, "SMODULE_ID", "SMODULE_NAME");
            return Json(objSubModule);
        }



        //
        // POST: /menu/Create
        [HttpPost]
        public async Task<ActionResult> Create(MenuModel menuModel)
        {
            Utilities _ipaddressobj = new Utilities();
            menuModel.ipaddress = _ipaddressobj.GetIpAddress();
            try
            {
                if (ModelState.IsValid)
                {
                    using (HttpClient client = new HttpClient())
                    {

                        var result = await client.PostAsJsonAsync(url + "menuapi", menuModel);
                        var contents = await result.Content.ReadAsStringAsync();
                        if (result.IsSuccessStatusCode)
                        {
                            TempData["Message"] = MessageConfig.htmlSuccessString;
                            TempData["Status"] = "Success";
                            TempData["InnerMessage"] = "";
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ViewBag.Message = MessageConfig.htmlErrorString;
                            ViewBag.Status = "Failed";
                            ViewBag.InnerMessage = contents;
                            FillData(menuModel.MENU_ID);
                            return View();
                        }
                    }
                }
                else
                {
                    ViewBag.Message = MessageConfig.htmlErrorString;
                    ViewBag.Status = "Failed";
                    ViewBag.InnerMessage = "Model State Failed";
                    FillData(menuModel.MENU_ID);
                    return View();
                }
                //return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Message = MessageConfig.htmlErrorString;
                ViewBag.Status = "Failed";
                ViewBag.InnerMessage = ex.Message;
                FillData(menuModel.MENU_ID);
                return View();
            }
        }

        //
        // GET: /MeterType/Edit/5
        public ActionResult Edit(int id)
        {
            return FillData(id);
        }

        //
        // POST: /MeterType/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(MenuModel menumodel)
        {
            Utilities _ipaddressobj = new Utilities();
            menumodel.ipaddress = _ipaddressobj.GetIpAddress();
            try
            {
                if (ModelState.IsValid)
                {
                    using (HttpClient client = new HttpClient())
                    {

                        var result = await client.PutAsJsonAsync(url + "menuapi", menumodel);
                        var contents = await result.Content.ReadAsStringAsync();
                        if (result.IsSuccessStatusCode)
                        {
                            TempData["Message"] = MessageConfig.htmlSuccessString;
                            TempData["Status"] = "Success";
                            TempData["InnerMessage"] = "";
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ViewBag.Message = MessageConfig.htmlErrorString;
                            ViewBag.Status = "Failed";
                            ViewBag.InnerMessage = contents;
                            FillData(menumodel.MENU_ID);
                            return View();
                        }

                    }
                }
                else
                {
                    ViewBag.Message = MessageConfig.htmlErrorString;
                    ViewBag.Status = "Failed";
                    ViewBag.InnerMessage = "Model State Failed";
                    FillData(menumodel.MENU_ID);
                    return View();
                }
                //return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Message = MessageConfig.htmlErrorString;
                ViewBag.Status = "Failed";
                ViewBag.InnerMessage = ex.Message;
                FillData(menumodel.MENU_ID);
                return View();
            }
        }

        //
        // GET: /MeterType/Delete/5
        [HttpGet]
        [ActionName("Delete")]
        public ActionResult DeleteView(int id)
        {
            return FillData(id);
        }

        //
        // POST: /MeterType/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            Utilities _ipaddressobj = new Utilities();
            string ipaddress= _ipaddressobj.GetIpAddress();
            try
            {
                using (HttpClient client = new HttpClient())
                {

                    var result = await client.DeleteAsync(url + "menuapi/" + id + "/" + ipaddress);
                    var contents = await result.Content.ReadAsStringAsync();
                    if (result.IsSuccessStatusCode)
                    {
                        TempData["Message"] = MessageConfig.htmlSuccessString;
                        TempData["Status"] = "Success";
                        TempData["InnerMessage"] = "";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.Message = MessageConfig.htmlErrorString;
                        ViewBag.Status = "Failed";
                        ViewBag.InnerMessage = contents;
                        FillData(id);
                        return View();
                    }
                }
                //return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Message = MessageConfig.htmlErrorString;
                ViewBag.Status = "Failed";
                ViewBag.InnerMessage = ex.Message;
                FillData(id);
                return View();
            }
        }

        [HttpGet]
        [ActionName("View")]
        public ActionResult ReadView(int id)
        {
            return FillData(id);
        }


    }
}



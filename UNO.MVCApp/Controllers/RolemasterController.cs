using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
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
    public class RolemasterController : Controller
    {
        string _uri = ConfigurationManager.AppSettings["APIUrl"].ToString();
        string uri = string.Empty;
        //
        // GET: /Rolemaster/
        [AccessCheck(IdParamName = "Rolemaster/index")]
        public async Task<ActionResult> Index()
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

            return await GetRoleMasterList();
        }


        private async Task<ActionResult> GetRoleMasterList()
        {
            using (HttpClient client = new HttpClient())
            {
                client.AddTokenToHeader("");
                uri = string.Format("{0}RolemasterAPI", _uri);

                var result = await client.GetAsync(uri);

                var ROLEMASTERobj = await result.Content.ReadAsAsync<List<ROLEMASTERCREATEMODEL>>();
                return View(ROLEMASTERobj);
            }
        }


        private async Task<ROLEMASTERCREATEMODEL> GetEmployeeShift(int id)
        {
            ROLEMASTERCREATEMODEL ROLEMASTERobj;
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}RolemasterAPI/{1}", _uri, id);
                var result = await client.GetAsync(uri);
                ROLEMASTERobj = await result.Content.ReadAsAsync<ROLEMASTERCREATEMODEL>();
            }
            return ROLEMASTERobj;
        }


        private async Task BindDropDown()
        {
            using (HttpClient client = new HttpClient())
            {
                client.AddTokenToHeader("");
                uri = string.Format("{0}CompanyAPI", _uri);
                var result = await client.GetAsync(uri);
                var companyContent = await result.Content.ReadAsAsync<List<CompanyModel>>();
                var Companies = companyContent.Select(c => new SelectListItem
                {
                    Value = c.COMPANY_ID.ToString(),
                    Text = c.COMPANY_NAME
                });




                ViewBag.Companies = Companies;

            }

        }

        [HttpGet]
        public async Task<ActionResult> Create()
        {
            await BindDropDown();
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(ROLEMASTERCREATEMODEL rolemasterobj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (HttpClient client = new HttpClient())
                    {
                        uri = string.Format("{0}RolemasterAPI", _uri);

                        var result = await client.PostAsJsonAsync(uri, rolemasterobj);

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
                            return RedirectToAction("Index");
                        }
                    }
                    
                }
                else
                {
                    ViewBag.message = MessageConfig.htmlErrorString;
                    ViewBag.status = "failed";
                    ViewBag.innermessage = "model state failed";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = MessageConfig.htmlErrorString;
                ViewBag.Status = "Failed";
                ViewBag.InnerMessage = ex.Message;
                return View();
            }
        }


        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            ROLEMASTERCREATEMODEL rolemasterobj = await GetEmployeeShift(id);
            await BindDropDown();
            return View(rolemasterobj);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(int id, ROLEMASTERCREATEMODEL rolemasterobj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (HttpClient client = new HttpClient())
                    {
                        uri = string.Format("{0}RolemasterAPI", _uri);
                        var result = await client.PutAsJsonAsync(uri, rolemasterobj);
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
                            return RedirectToAction("Index");
                        }
                    }
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {
                ViewBag.Message = MessageConfig.htmlErrorString;
                ViewBag.Status = "Failed";
                ViewBag.InnerMessage = "";
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            ROLEMASTERCREATEMODEL rolemasterobj = await GetEmployeeShift(id);
            await BindDropDown();
            return View(rolemasterobj);
        }


        [HttpPost]
        public async Task<ActionResult> Delete(ROLEMASTERCREATEMODEL rolemasterobj)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    uri = string.Format("{0}RolemasterAPI", _uri);
                    var result = await client.DeleteAsync(uri + "/" + rolemasterobj.rolemasterid);
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
                        return RedirectToAction("Index");
                    }
                }
            }
            catch (Exception)
            {
                ViewBag.Message = MessageConfig.htmlErrorString;
                ViewBag.Status = "Failed";
                ViewBag.InnerMessage = "";
                return RedirectToAction("Index");
            }
        }


        [HttpGet]
        public async Task<ActionResult> View(int id)
        {
            ROLEMASTERCREATEMODEL rolemasterobj = await GetEmployeeShift(id);
            await BindDropDown();
            return View(rolemasterobj);
        }

    }
}

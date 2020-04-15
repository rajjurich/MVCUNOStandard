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
    public class EmployeeLeftController : Controller
    {
        //
        // GET: /EmployeeLeft/

        string _uri = ConfigurationManager.AppSettings["APIUrl"].ToString();
        string uri = string.Empty;

        [AccessCheck(IdParamName = "EmployeeLeft/index")]

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
            return await GetEmployeeList();
        }
        private async Task<ActionResult> GetEmployeeList()
        {
            using (HttpClient client = new HttpClient())
            {
                client.AddTokenToHeader("");
                uri = string.Format("{0}EmployeeLeftAPI", _uri);

                var result = await client.GetAsync(uri);

                var EmployeeLeft = await result.Content.ReadAsAsync<List<EmployeeLeftModel>>();
                return View(EmployeeLeft);
            }
        }

        private async Task<EmployeeLeftModel> GetEmployeeLeft(int id)
        {
            EmployeeLeftModel employee;
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}EmployeeLeftAPI/{1}", _uri, id);

                var result = await client.GetAsync(uri);

                employee = await result.Content.ReadAsAsync<EmployeeLeftModel>();
            }
            return employee;
        }
        
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            await BindDropDown();
            return View();

        }
        public async Task<ActionResult> Create(EmployeeLeftModel EmployeeLeft)
        {
            Utilities _ipaddressobj = new Utilities();
            EmployeeLeft.ipaddress = _ipaddressobj.GetIpAddress();
            try
            {
                if (ModelState.IsValid)
                {
                    using (HttpClient client = new HttpClient())
                    {
                        uri = string.Format("{0}EmployeeLeftAPI", _uri);

                        var result = await client.PostAsJsonAsync(uri, EmployeeLeft);

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
                    ViewBag.Message = MessageConfig.htmlErrorString;
                    ViewBag.Status = "Failed";
                    ViewBag.InnerMessage = "Model State Failed";
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
            EmployeeLeftModel EmployeeLeft = await GetEmployeeLeft(id);
            await BindDropDown();
            return View(EmployeeLeft);
        }
        public async Task<ActionResult> Edit(int id, EmployeeLeftModel EmployeeLeft)
        {
            Utilities _ipaddressobj = new Utilities();
            EmployeeLeft.ipaddress = _ipaddressobj.GetIpAddress();
            try
            {
                if (ModelState.IsValid)
                {
                    using (HttpClient client = new HttpClient())
                    {
                        uri = string.Format("{0}EmployeeLeftAPI", _uri);
                        var result = await client.PutAsJsonAsync(uri, EmployeeLeft);
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
        public async Task<ActionResult> DeleteView(int id)
        {
            EmployeeLeftModel EmployeeLeft = await GetEmployeeLeft(id);
            await BindDropDown();
            return View(EmployeeLeft);
        }
        public async Task<ActionResult> DeleteView(EmployeeLeftModel EmployeeLeft)
        {
            Utilities _ipaddressobj = new Utilities();
            EmployeeLeft.ipaddress = _ipaddressobj.GetIpAddress();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    uri = string.Format("{0}EmployeeLeftAPI", _uri);
                    var result = await client.DeleteAsync(uri + "/" + EmployeeLeft.EL_COLUMNID+"/"+EmployeeLeft.ipaddress);
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

        [HttpPost]
        public async Task<ActionResult> GetEmpJoiningDateByID(string id)
        {
            
            using (HttpClient client = new HttpClient())
            {
                 uri = string.Format("{0}EmployeeLeftAPI/getJoiningDate/{1}", _uri, id);

                 var result = await client.GetAsync(uri);

                var task = await result.Content.ReadAsStringAsync();

                var submodule =  task.Replace('\"', ' ').Trim();
               
                return Json(submodule, JsonRequestBehavior.AllowGet);
            }
        }
      
        [HttpGet]
        public async Task<ActionResult> View(int id)
        {
            EmployeeLeftModel EmployeeLeft = await GetEmployeeLeft(id);
            await BindDropDown();
            return View(EmployeeLeft);
        }
        private async Task BindDropDown()
        {
            using (HttpClient client = new HttpClient())
            {
                string Type1 = "EL";
                client.AddTokenToHeader("");
                uri = string.Format("{0}Employee", _uri);
                var result = await client.GetAsync(uri);
                var EmployeeLeftContent = await result.Content.ReadAsAsync<List<EmployeeInfo>>();
                var EmployeeLeft = EmployeeLeftContent.Select(c => new SelectListItem
                {
                    Value = c.EMPLOYEE_ID.ToString(),
                    Text = c.FULL_NAME
                });
                ViewBag.EmployeeId = EmployeeLeft;
                client.AddTokenToHeader("");
                uri = string.Format("{0}ReasonMasterAPI/get/" + Type1, _uri);
                result = await client.GetAsync(uri);
                var ReasonContent = await result.Content.ReadAsAsync<List<Reason>>();
                var Reason = ReasonContent.Select(c => new SelectListItem
                {
                    Value = c.REASON_ID.ToString(),
                    Text = c.REASON_DESC
                });
                ViewBag.ReasonId = Reason;
            }
        }
    }
}

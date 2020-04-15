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
    public class LeaveFileController : Controller
    {
        // GET: /LeaveFile/
        string _uri = ConfigurationManager.AppSettings["APIUrl"].ToString();
        string uri = string.Empty;
       
        [AccessCheck(IdParamName = "LeaveFile/index")]
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

            return await GetLeaveFileList();
        }

        private async Task<ActionResult> GetLeaveFileList()
        {
            using (HttpClient client = new HttpClient())
            {
                client.AddTokenToHeader("");
                uri = string.Format("{0}LeaveFileAPI", _uri);

                var result = await client.GetAsync(uri);
                var LeaveFileModel = await result.Content.ReadAsAsync<List<LeaveFileModel>>();
                return View(LeaveFileModel);
            }
        }

        private async Task<LeaveFileModel> GetLeaveFile(int id)
        {
            LeaveFileModel LeaveFile;
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}LeaveFileAPI/{1}", _uri, id);

                var result = await client.GetAsync(uri);

                LeaveFile = await result.Content.ReadAsAsync<LeaveFileModel>();
            }
            return LeaveFile;
        }

        private async Task BindDropDown()
        {

            using (HttpClient client = new HttpClient())
            {
                string identifer = "LEAVE_TYPE";
                client.AddTokenToHeader("");
                uri = string.Format("{0}CompanyAPI", _uri);
                var result = await client.GetAsync(uri);
                var CompanyContent = await result.Content.ReadAsAsync<List<CompanyModel>>();
                var Company = CompanyContent.Select(c => new SelectListItem
                {
                    Value = c.COMPANY_ID.ToString(),
                    Text = c.COMPANY_NAME
                });
                ViewBag.CompanyId = Company;

                client.AddTokenToHeader("");
                uri = string.Format("{0}EntParams/" + identifer, _uri);
                result = await client.GetAsync(uri);
                var EntParamsContent = await result.Content.ReadAsAsync<List<EntParams>>();
                var EntParams = EntParamsContent.Select(c => new SelectListItem
                {
                    Value = c.CODE.ToString(),
                    Text = c.VALUE
                });
                ViewBag.EntParamsId = EntParams;

                var PaidLeave = new List<SelectListItem>();
                PaidLeave.Add(new SelectListItem() { Text = "Yes", Value = "1", Selected = true });
                PaidLeave.Add(new SelectListItem() { Text = "No", Value = "0" });
                ViewBag.PaidLeave = PaidLeave;
            }
        }

        [HttpGet]
        public async Task<ActionResult> Create()
        {
            await BindDropDown();
            return View();
        }

        public async Task<ActionResult> Create(LeaveFileModel LeaveFile)
        {
            Utilities _ipaddressobj = new Utilities();
            LeaveFile.ipaddress = _ipaddressobj.GetIpAddress();
            try
            {
                if (ModelState.IsValid)
                {
                    using (HttpClient client = new HttpClient())
                    {
                        uri = string.Format("{0}LeaveFileAPI", _uri);

                        var result = await client.PostAsJsonAsync(uri, LeaveFile);

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
                            return View();
                        }
                    }
                }
                else
                {
                    ViewBag.Message = MessageConfig.htmlErrorString;
                    ViewBag.Status = "Failed";
                    ViewBag.InnerMessage = "Model State Failed";
                    return View();
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
            LeaveFileModel LeaveFile = await GetLeaveFile(id);
            await BindDropDown();
            return View(LeaveFile);
        }

        public async Task<ActionResult> Edit(int id, LeaveFileModel LeaveFile)
        {
            Utilities _ipaddressobj = new Utilities();
            LeaveFile.ipaddress = _ipaddressobj.GetIpAddress();
            try
            {
                if (ModelState.IsValid)
                {
                    using (HttpClient client = new HttpClient())
                    {
                        uri = string.Format("{0}LeaveFileAPI", _uri);
                        var result = await client.PutAsJsonAsync(uri, LeaveFile);
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
            LeaveFileModel LeaveFile = await GetLeaveFile(id);
            await BindDropDown();
            return View(LeaveFile);
        }

        public async Task<ActionResult> DeleteView(LeaveFileModel LeaveFileModel)
        {
            Utilities _ipaddressobj = new Utilities();
            LeaveFileModel.ipaddress = _ipaddressobj.GetIpAddress();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    uri = string.Format("{0}LeaveFileAPI", _uri);
                    var result = await client.DeleteAsync(uri + "/" + LeaveFileModel.Leave_File_ID + "/" + LeaveFileModel.ipaddress);
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
    }
}

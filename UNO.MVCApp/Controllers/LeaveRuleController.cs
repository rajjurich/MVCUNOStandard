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
    public class LeaveRuleController : Controller
    {
        //
        // GET: /LeaveRule/ string url = WebConfigurationManager.AppSettings["APIUrl"];
        string _uri = ConfigurationManager.AppSettings["APIUrl"].ToString();
        string uri = string.Empty;
        [AccessCheck(IdParamName = "LeaveRule/index")]
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

            return await GetLeaveRuleList();
        }

        private async Task<ActionResult> GetLeaveRuleList()
        {
            using (HttpClient client = new HttpClient())
            {
                client.AddTokenToHeader("");
                uri = string.Format("{0}LeaveRuleAPI", _uri);

                var result = await client.GetAsync(uri);

                var LeaveRuleModel = await result.Content.ReadAsAsync<List<LeaveRuleModel>>();
                return View(LeaveRuleModel);
            }
        }

        private async Task<LeaveRuleModel> GetLeaveRule(int id)
        {
            LeaveRuleModel LeaveRule;
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}LeaveRuleAPI/{1}", _uri, id);

                var result = await client.GetAsync(uri);

                LeaveRule = await result.Content.ReadAsAsync<LeaveRuleModel>();
            }
            return LeaveRule;
        }


        private async Task BindDropDown()
        {
            using (HttpClient client = new HttpClient())
            {
                string Type = "Category";
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
                uri = string.Format("{0}CommonAPI/GetByTypes/Category", _uri);
                result = await client.GetAsync(uri);
                var CategoriesContent = await result.Content.ReadAsAsync<List<CommonEntitiesModel>>();
                var Categories = CategoriesContent.Select(c => new SelectListItem
                {
                    Value = c.ID.ToString(),
                    Text = c.OCE_DESCRIPTION
                });
                ViewBag.Categories = Categories;

                var Days = new List<SelectListItem>();
                Days.Add(new SelectListItem() { Text = "None", Value = "N", Selected = true });
                Days.Add(new SelectListItem() { Text = "Prefixed Only", Value = "P" });
                Days.Add(new SelectListItem() { Text = "Suffixed Only", Value = "S" });
                Days.Add(new SelectListItem() { Text = "Prefixed & Suffixed", Value = "B" });
                Days.Add(new SelectListItem() { Text = "Prefixed Or Suffixed", Value = "O" });
                ViewBag.Days = Days;

                var AllotmentType = new List<SelectListItem>();
                AllotmentType.Add(new SelectListItem() { Text = "Yearly", Value = "Y" });
                AllotmentType.Add(new SelectListItem() { Text = "Monthly", Value = "M" });
                ViewBag.AllotmentType = AllotmentType;

                client.AddTokenToHeader("");
                uri = string.Format("{0}LeaveCodeAPI", _uri);
                var result1 = await client.GetAsync(uri);
                var LeaveCodeContent = await result1.Content.ReadAsAsync<List<LeaveCode>>();
                var LeaveCode = LeaveCodeContent.Select(c => new SelectListItem
                {
                    Value = c.Leave_ID.ToString(),
                    Text = c.Leave_Description
                });
                ViewBag.LeaveCode = LeaveCode;
            }
        }
      

        [HttpGet]
        public async Task<ActionResult> Create()
        {
            await BindDropDown();
            return View();
        }
        public async Task<ActionResult> Create(LeaveRuleModel LeaveRule)
        {
            Utilities _ipaddressobj = new Utilities();
            LeaveRule.ipaddress = _ipaddressobj.GetIpAddress();
            try
            {
                if (ModelState.IsValid)
                {
                    using (HttpClient client = new HttpClient())
                    {
                        uri = string.Format("{0}LeaveRuleAPI", _uri);

                        var result = await client.PostAsJsonAsync(uri, LeaveRule);

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

        [HttpPost]
        public async Task<ActionResult> LeaveCodeBycompID(int id)
        {
            List<LeaveCode> LeaveCode = new List<LeaveCode>();
            using (HttpClient client = new HttpClient())
            {
                client.AddTokenToHeader("");
                uri = string.Format("{0}LeaveRuleAPI/{1}", _uri, id);

                var result = await client.GetAsync(uri);

                 LeaveCode = await result.Content.ReadAsAsync<List<LeaveCode>>();
                SelectList objLeaveCode = new SelectList(LeaveCode, "Leave_ID", "Leave_Description");
                return Json(objLeaveCode);
            }

        }


        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            LeaveRuleModel LeaveRule = await GetLeaveRule(id);
            await BindDropDown();
            return View(LeaveRule);
        }

        public async Task<ActionResult> Edit(int id, LeaveRuleModel LeaveRule)
        {
            Utilities _ipaddressobj = new Utilities();
            LeaveRule.ipaddress = _ipaddressobj.GetIpAddress();
            try
            {
                    using (HttpClient client = new HttpClient())
                    {
                        uri = string.Format("{0}LeaveRuleAPI", _uri);
                        var result = await client.PutAsJsonAsync(uri, LeaveRule);
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
        public async Task<ActionResult> DeleteView(int id)
        {
            LeaveRuleModel LeaveRule = await GetLeaveRule(id);
            await BindDropDown();
            return View(LeaveRule);

        }

        public async Task<ActionResult> DeleteView(LeaveRuleModel LeaveRuleModel)
        {
            Utilities _ipaddressobj = new Utilities();
            LeaveRuleModel.ipaddress = _ipaddressobj.GetIpAddress();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    uri = string.Format("{0}LeaveRuleAPI", _uri);
                    var result = await client.DeleteAsync(uri + "/" + LeaveRuleModel.LR_REC_ID + "/" + LeaveRuleModel.ipaddress);
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

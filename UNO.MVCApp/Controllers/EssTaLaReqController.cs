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
    public class EssTaLaReqController : Controller
    {
        string _uri = ConfigurationManager.AppSettings["APIUrl"].ToString();
        string uri = string.Empty;    
        //
        // GET: /EssTaLaReq/
        [AccessCheck(IdParamName = "EssTaLaReq/index")]
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

            return await GetEssTaLaReqList();
        }

        private async Task<ActionResult> GetEssTaLaReqList()
        {
            using (HttpClient client = new HttpClient())
            {
                client.AddTokenToHeader("");
                uri = string.Format("{0}EssTaLaReqAPI", _uri);
                var result = await client.GetAsync(uri);
                var EssTaLaReqModel = await result.Content.ReadAsAsync<List<EssTaLaReqModel>>();
                return View(EssTaLaReqModel);
            }
        }

        private async Task<EssTaLaReqModel> GetEssTaLaReq(int id)
        {
            EssTaLaReqModel EssTaLaReq;
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}EssTaLaReqAPI/{1}", _uri, id);
                var result = await client.GetAsync(uri);
                EssTaLaReq = await result.Content.ReadAsAsync<EssTaLaReqModel>();
            }
            return EssTaLaReq;
        }

        private async Task BindDropDown()
        {
            string Type = "LA";
            using (HttpClient client = new HttpClient())
            {
                client.AddTokenToHeader("");
                uri = string.Format("{0}LeaveCodeAPI", _uri);
                var result = await client.GetAsync(uri);
                var LeaveCodeContent = await result.Content.ReadAsAsync<List<LeaveCode>>();
                var LeaveCode = LeaveCodeContent.Select(c => new SelectListItem
                {
                    Value = c.Leave_ID.ToString(),
                    Text = c.Leave_Description
                });
                ViewBag.LeaveCode = LeaveCode;
                client.AddTokenToHeader("");
                uri = string.Format("{0}ReasonMasterAPI/get/" + Type, _uri);
                var result1 = await client.GetAsync(uri);
                var ReasonContent = await result1.Content.ReadAsAsync<List<Reason>>();
                var Reason = ReasonContent.Select(c => new SelectListItem
                {
                    Value = c.REASON_ID.ToString(),
                    Text = c.REASON_DESC
                });
                ViewBag.ReasonId = Reason;
            }
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            EssTaLaReqModel EssTaLaReq = await GetEssTaLaReq(id);
            await BindDropDown();
            return View(EssTaLaReq);
        }

        public async Task<ActionResult> Edit(int id, EssTaLaReqModel EssTaLaReq)
        {
            Utilities _ipaddressobj = new Utilities();
            EssTaLaReq.ipaddress = _ipaddressobj.GetIpAddress();
            try
            {
                if (ModelState.IsValid)
                {
                    using (HttpClient client = new HttpClient())
                    {
                        uri = string.Format("{0}EssTaLaReqAPI", _uri);
                        var result = await client.PutAsJsonAsync(uri, EssTaLaReq);
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
            EssTaLaReqModel EssTaLaReq = await GetEssTaLaReq(id);
            await BindDropDown();
            return View(EssTaLaReq);
        }

        public async Task<ActionResult> DeleteView(EssTaLaReqModel EssTaLaReq)
        {
            Utilities _ipaddressobj = new Utilities();
            EssTaLaReq.ipaddress = _ipaddressobj.GetIpAddress();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    uri = string.Format("{0}EssTaLaReqAPI", _uri);
                    var result = await client.DeleteAsync(uri + "/" + EssTaLaReq.ESS_LA_ID + "/" + EssTaLaReq.ipaddress);
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

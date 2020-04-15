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
    public class EssTaMaReqController : Controller
    {
        string _uri = ConfigurationManager.AppSettings["APIUrl"].ToString();
        string uri = string.Empty;
        //
        // GET: /EssTaMaReq/
        [AccessCheck(IdParamName = "EssTaMaReq/index")]
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

            return await GetEssTaMaReqList();
        }

        private async Task<ActionResult> GetEssTaMaReqList()
        {
            using (HttpClient client = new HttpClient())
            {
                client.AddTokenToHeader("");
                uri = string.Format("{0}EssTaMaReqAPI", _uri);
                var result = await client.GetAsync(uri);
                var EssTaMaReqModel = await result.Content.ReadAsAsync<List<EssTaMaReqModel>>();
                return View(EssTaMaReqModel);
            }
        }

        private async Task<EssTaMaReqModel> GetEssTaMaReq(int id)
        {
            EssTaMaReqModel EssTaMaReq;
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}EssTaMaReqAPI/{1}", _uri, id);
                var result = await client.GetAsync(uri);
                EssTaMaReq = await result.Content.ReadAsAsync<EssTaMaReqModel>();
            }
            return EssTaMaReq;
        }


        private async Task BindDropDown()
        {
            string Type = "MA";
            using (HttpClient client = new HttpClient())
            {
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



        //private ActionResult FillData(long id)
        //{
        //    string Type = "MA";
        //    EssTaMaReqModel EssTaMaReqModel = new EssTaMaReqModel();
        //    List<LeaveCode> LeaveCode = new List<LeaveCode>();
        //    List<Reason> Reason = new List<Reason>();

        //    using (WebClient client = new WebClient())
        //    {
        //        string EssTaMaReq = client.DownloadString(_uri + "EssTaMaReqAPI/" + id);
        //        EssTaMaReqModel = JsonConvert.DeserializeObject<EssTaMaReqModel>(EssTaMaReq);

        //        string mdl = client.DownloadString(_uri + "LeaveCodeAPI");
        //        LeaveCode = JsonConvert.DeserializeObject<List<LeaveCode>>(mdl);

        //        string mdlb = client.DownloadString(_uri + "ReasonMasterAPI/get/" + Type);
        //        Reason = JsonConvert.DeserializeObject<List<Reason>>(mdlb);
        //    }

        //    ViewBag.LeaveCode = new SelectList(LeaveCode, "Leave_ID", "Leave_Description");
        //    ViewBag.ReasonId = new SelectList(Reason, "REASON_ID", "REASON_DESC");
        //    return View(EssTaMaReqModel);
        //}

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            EssTaMaReqModel EssTaMaReq = await GetEssTaMaReq(id);
            await BindDropDown();
            return View(EssTaMaReq);
        }

        public async Task<ActionResult> Edit(int id, EssTaMaReqModel EssTaMaReq)
        {
            Utilities _ipaddressobj = new Utilities();
            EssTaMaReq.ipaddress = _ipaddressobj.GetIpAddress();
            try
            {
                if (ModelState.IsValid)
                {
                    using (HttpClient client = new HttpClient())
                    {
                        uri = string.Format("{0}EssTaMaReqAPI", _uri);
                        var result = await client.PutAsJsonAsync(uri, EssTaMaReq);
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
            EssTaMaReqModel EssTaMaReq = await GetEssTaMaReq(id);
            await BindDropDown();
            return View(EssTaMaReq);
        }

        public async Task<ActionResult> DeleteView(EssTaMaReqModel EssTaMaReq)
        {
            Utilities _ipaddressobj = new Utilities();
            EssTaMaReq.ipaddress = _ipaddressobj.GetIpAddress();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    uri = string.Format("{0}EssTaMaReqAPI", _uri);
                    var result = await client.DeleteAsync(uri + "/" + EssTaMaReq.ESS_MA_ID + "/" + EssTaMaReq.ipaddress);
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

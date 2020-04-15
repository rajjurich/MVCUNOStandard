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
    public class EssTaOdReqController : Controller
    {
        string _uri = ConfigurationManager.AppSettings["APIUrl"].ToString();
        string uri = string.Empty;
        //
        // GET: /EssTaOdReq/
        [AccessCheck(IdParamName = "EssTaLaReq/index")]

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

            List<EssTaOdReqModel> EssTaOdReq = new List<EssTaOdReqModel>();
            using (WebClient client = new WebClient())
            {
                string s = client.DownloadString(_uri + "EssTaOdReqAPI");
                EssTaOdReq = JsonConvert.DeserializeObject<List<EssTaOdReqModel>>(s);
                EssTaOdReq.RemoveAll(item => item == null);
            }
            return View(EssTaOdReq);
        }

        private ActionResult FillData(long id)
        {
            string Type = "OD";
            EssTaOdReqModel EssTaOdReqModel = new EssTaOdReqModel();
          
            List<Reason> Reason = new List<Reason>();

            using (WebClient client = new WebClient())
            {
                string EssTaOdReq = client.DownloadString(_uri + "EssTaOdReqAPI/" + id);
                EssTaOdReqModel = JsonConvert.DeserializeObject<EssTaOdReqModel>(EssTaOdReq);

                string mdlb = client.DownloadString(_uri + "ReasonMasterAPI/get/" + Type);
                Reason = JsonConvert.DeserializeObject<List<Reason>>(mdlb);
            }
            ViewBag.ReasonId = new SelectList(Reason, "REASON_ID", "REASON_DESC");
            return View(EssTaOdReqModel);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            return FillData(id);
        }

        public async Task<ActionResult> Edit(int id, EssTaOdReqModel EssTaOdReq)
        {
            Utilities _ipaddressobj = new Utilities();
            EssTaOdReq.ipaddress = _ipaddressobj.GetIpAddress();
            try
            {
                if (ModelState.IsValid)
                {
                    using (HttpClient client = new HttpClient())
                    {
                        uri = string.Format("{0}EssTaOdReqAPI", _uri);
                        var result = await client.PutAsJsonAsync(uri, EssTaOdReq);
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
                            return FillData(id);
                        }
                    }
                }
                else
                {
                    return FillData(id);
                }
            }
            catch (Exception)
            {
                ViewBag.Message = MessageConfig.htmlErrorString;
                ViewBag.Status = "Failed";
                ViewBag.InnerMessage = "";
                return FillData(id);
            }
        }

        [HttpGet]
        public ActionResult DeleteView(int id)
        {
            return FillData(id);
        }

        public async Task<ActionResult> DeleteView(EssTaOdReqModel EssTaOdReq)
        {
            Utilities _ipaddressobj = new Utilities();
            EssTaOdReq.ipaddress = _ipaddressobj.GetIpAddress();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    uri = string.Format("{0}EssTaOdReqAPI", _uri);
                    var result = await client.DeleteAsync(uri + "/" + EssTaOdReq.ESS_OD_ID + "/" + EssTaOdReq.ipaddress);
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
                        return FillData(EssTaOdReq.ESS_OD_ID);
                    }
                }
            }
            catch (Exception)
            {
                ViewBag.Message = MessageConfig.htmlErrorString;
                ViewBag.Status = "Failed";
                ViewBag.InnerMessage = "";
                return FillData(EssTaOdReq.ESS_OD_ID);
            }
        }
    }
}

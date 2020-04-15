using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using UNO.AppModel;
using UNO.MVCApp.Common;
using UNO.MVCApp.Filters;
using UNO.WebApi.Helpers;

namespace UNO.MVCApp.Controllers
{
    [Authorize]
    [SessionCheck]
    public class ReasonMasterController : Controller
    {
        string _uri = ConfigurationManager.AppSettings["APIUrl"].ToString();
        string uri = string.Empty;
        //
        // GET: /ReasonMaster/

        [AccessCheck(IdParamName = "ReasonMaster/index")]
        public async Task<ActionResult> Index()
        {
            var acs = new List<ReasonMaster>();
            try
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
                using (HttpClient client = new HttpClient())
                {
                    client.AddTokenToHeader("");
                    uri = string.Format("{0}ReasonMasterAPI", _uri);
                    var result = await client.GetAsync(uri);
                    acs = await result.Content.ReadAsAsync<List<ReasonMaster>>();

                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = MessageConfig.htmlErrorString;
                ViewBag.Status = "Failed";
                ViewBag.InnerMessage = ex.Message;
            }
            return View(acs);
        }

        [HttpGet]
        public async Task<ActionResult> Create()
        {
            //FillData();
            await BindDropDown();
            return View();
        }

        public async Task<ActionResult> Create(ReasonMaster resonMaster)
        {
            Utilities _ipaddressobj = new Utilities();
            resonMaster.ipaddress = _ipaddressobj.GetIpAddress();
            try
            {
                if (ModelState.IsValid)
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.AddTokenToHeader("");
                        uri = string.Format("{0}ReasonMasterAPI", _uri);

                        var result = await client.PostAsJsonAsync(uri, resonMaster);

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
                            //FillData();
                            //return View();
                        }
                    }
                }
                else
                {
                    ViewBag.Message = MessageConfig.htmlErrorString;
                    ViewBag.Status = "Failed";
                    ViewBag.InnerMessage = "Model State Failed";
                    //FillData();
                    //return View(resonMaster);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = MessageConfig.htmlErrorString;
                ViewBag.Status = "Failed";
                ViewBag.InnerMessage = ex.Message;
                //FillData();
                //return View(resonMaster);
            }
            await BindDropDown();
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {

            ReasonMaster reasonMaster;
            using (HttpClient client = new HttpClient())
            {
                client.AddTokenToHeader("");
                uri = string.Format("{0}ReasonMasterAPI/{1}", _uri, id);

                var result = await client.GetAsync(uri);

                reasonMaster = await result.Content.ReadAsAsync<ReasonMaster>();
            }
            await BindDropDown();
            ViewBag.Title = "Reason View";
            return View(reasonMaster);
            
        }

        public async Task<ActionResult> Edit(ReasonMaster cominfo)
        {
            Utilities _ipaddressobj = new Utilities();
            cominfo.ipaddress = _ipaddressobj.GetIpAddress();
            try
            {
                if (ModelState.IsValid)
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.AddTokenToHeader("");
                        uri = string.Format("{0}ReasonMasterAPI", _uri);

                        var result = await client.PutAsJsonAsync(uri, cominfo);

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
                            //FillData();
                            //return View();
                        }
                    }
                }
                else
                {
                    ViewBag.Message = MessageConfig.htmlErrorString;
                    ViewBag.Status = "Failed";
                    ViewBag.InnerMessage = "Model State Failed";
                    //FillData();
                    //return View(cominfo);
                }
            }
            catch (Exception)
            {
                ViewBag.Message = MessageConfig.htmlErrorString;
                ViewBag.Status = "Failed";
                ViewBag.InnerMessage = "";
                //FillData();
                //return View(cominfo);
            }
            await BindDropDown();
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> View(int id)
        {
            ReasonMaster reasonMaster;
            using (HttpClient client = new HttpClient())
            {
                client.AddTokenToHeader("");
                uri = string.Format("{0}ReasonMasterAPI/{1}", _uri, id);

                var result = await client.GetAsync(uri);

                reasonMaster = await result.Content.ReadAsAsync<ReasonMaster>();
            }
            await BindDropDown();
            ViewBag.Title = "Reason View";
            return View(reasonMaster);


            //ReasonMaster commonModel = new ReasonMaster();

            //using (WebClient client = new WebClient())
            //{
            //    string common = client.DownloadString(_uri + "ReasonMasterAPI/" + id);
            //    commonModel = JsonConvert.DeserializeObject<ReasonMaster>(common);
            //}
            //FillData();
            //ViewBag.Title = "Reason View";
            //return View(commonModel);
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            ReasonMaster commonModel = new ReasonMaster();

            using (WebClient client = new WebClient())
            {
                string common = client.DownloadString(_uri + "ReasonMasterAPI/" + id);
                commonModel = JsonConvert.DeserializeObject<ReasonMaster>(common);
            }
            //FillData();
            await BindDropDown();            
            ViewBag.Title = "Reason View";
            return View(commonModel);
        }

        public async Task<ActionResult> Delete(int id, ReasonMaster cominfo)
        {
            Utilities _ipaddressobj = new Utilities();
            cominfo.ipaddress = _ipaddressobj.GetIpAddress();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.AddTokenToHeader("");
                    uri = string.Format("{0}ReasonMasterAPI/" + id + "/" + cominfo.ipaddress, _uri);

                    var result = await client.DeleteAsync(uri);

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
                        //FillData();
                        //return View();
                    }
                }
            }
            catch (Exception)
            {
                ViewBag.Message = MessageConfig.htmlErrorString;
                ViewBag.Status = "Failed";
                ViewBag.InnerMessage = "";
                //FillData();
                //return View(cominfo);
            }
            await BindDropDown();
            return View();
        }

        private ActionResult FillData()
        {
            List<Reason_Type> cmodule = new List<Reason_Type>();
            List<CompanyModel> lstCompany = new List<CompanyModel>();

            using (WebClient client = new WebClient())
            {
                string common = client.DownloadString(_uri + "Reason_Type_API");
                cmodule = JsonConvert.DeserializeObject<List<Reason_Type>>(common);
                string company = client.DownloadString(_uri + "CompanyAPI");
                lstCompany = JsonConvert.DeserializeObject<List<CompanyModel>>(company);
            }

            ViewBag.REASON_TYPES = new SelectList(cmodule, "REASON_TYPE_ID", "REASON_DESC");
            ViewBag.COMPANY_LIST = new SelectList(lstCompany, "COMPANY_ID", "COMPANY_NAME");

            return View();
        }

        private async Task BindDropDown()
        {
            List<Reason_Type> cmodule = new List<Reason_Type>();
            List<CompanyModel> companymodel = new List<CompanyModel>();

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

                uri = string.Format("{0}Reason_Type_API", _uri);
                result = await client.GetAsync(uri);
                var reasonContent = await result.Content.ReadAsAsync<List<Reason_Type>>();
                var Reasons = reasonContent.Select(c => new SelectListItem
                {
                    Value = c.REASON_TYPE_ID.ToString(),
                    Text = c.REASON_DESC
                });

                ViewBag.COMPANY_LIST = Companies;
                ViewBag.REASON_TYPES = Reasons;
            }
        }

    }
}

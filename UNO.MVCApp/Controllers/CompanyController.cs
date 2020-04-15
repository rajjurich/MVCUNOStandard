using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using UNO.AppModel;
using UNO.MVCApp.Common;
using UNO.MVCApp.Filters;
using UNO.WebApi.Helpers;

namespace UNO.MVCApp.Controllers
{
    [Authorize]
    [SessionCheck]
    public class CompanyController : Controller
    {
        // GET: /Company/

       
        string _uri = ConfigurationManager.AppSettings["APIUrl"].ToString();
        string uri = string.Empty;

        [AccessCheck(IdParamName = "Company/index")]
        public async Task<ActionResult> Index()
        {
            List<CompanyModel> CompanyList = new List<CompanyModel>();
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
                    uri = string.Format("{0}CompanyAPI", _uri);
                    var result = await client.GetAsync(uri);
                    CompanyList = await result.Content.ReadAsAsync<List<CompanyModel>>();
                    
                }

                //using (WebClient client = new WebClient())
                //{
                //    string s = client.DownloadString(_uri + "CompanyAPI");
                //    CompanyList = JsonConvert.DeserializeObject<List<CompanyModel>>(s);
                //    CompanyList.RemoveAll(item => item == null);
                //}
            }
            catch (Exception ex)
            {

                ViewBag.Message = MessageConfig.htmlErrorString;
                ViewBag.Status = "Failed";
                ViewBag.InnerMessage = ex.Message;
            }
            return View(CompanyList);
        }

        [HttpGet]
        public ActionResult Create()
        {
            CompanyModel companymodel = new CompanyModel();
            FillData(companymodel);
            return View();
        }

        public async Task<ActionResult> Create(CompanyModel compinfo)
        {
            Utilities _ipaddressobj = new Utilities();
            compinfo.ipaddress = _ipaddressobj.GetIpAddress();
            try
            {
                if (ModelState.IsValid)
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.AddTokenToHeader("");
                        uri = string.Format("{0}CompanyAPI", _uri);

                        var result = await client.PostAsJsonAsync(uri, compinfo);
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
                            FillData(compinfo);
                            return View();
                        }
                    }
                }
                else
                {
                    ViewBag.Message = MessageConfig.htmlErrorString;
                    ViewBag.Status = "Failed";
                    ViewBag.InnerMessage = "Model State Failed";
                    FillData(compinfo);
                    return View(compinfo);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = MessageConfig.htmlErrorString;
                ViewBag.Status = "Failed";
                ViewBag.InnerMessage = ex.Message;
                return View(compinfo);
            }
        }

        [HttpGet]
        public ActionResult View(int id)
        {
            CompanyModel companymodel = new CompanyModel();
            using (WebClient client = new WebClient())
            {
                string company = client.DownloadString(_uri + "CompanyAPI/" + id + "?aid=" + id);
                companymodel = JsonConvert.DeserializeObject<CompanyModel>(company);
            }
            FillData(companymodel);
            return View(companymodel);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            CompanyModel companymodel = new CompanyModel();
            using (WebClient client = new WebClient())
            {
                string company = client.DownloadString(_uri + "CompanyAPI/" + id + "?aid=" + id);
                companymodel = JsonConvert.DeserializeObject<CompanyModel>(company);
            }
            FillData(companymodel);
            return View(companymodel);
        }
        public async Task<ActionResult> Edit(int id, CompanyModel compinfo)
        {
            Utilities _ipaddressobj = new Utilities();
            compinfo.ipaddress = _ipaddressobj.GetIpAddress();
            try
            {
                if (ModelState.IsValid)
                {
                    using (HttpClient client = new HttpClient())
                    {
                        uri = string.Format("{0}CompanyAPI", _uri);
                        compinfo.COMPANY_ID = id;
                        var result = await client.PutAsJsonAsync(uri, compinfo);
                        
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
                    FillData(compinfo);
                    return View(compinfo);
                }
            }
            catch (Exception)
            {
                ViewBag.Message = MessageConfig.htmlErrorString;
                ViewBag.Status = "Failed";
                ViewBag.InnerMessage = "";
                return View(compinfo);
            }
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            CompanyModel companymodel = new CompanyModel();

            using (WebClient client = new WebClient())
            {
                string company = client.DownloadString(_uri + "CompanyAPI/" + id + "?aid=" + id);
                companymodel = JsonConvert.DeserializeObject<CompanyModel>(company);
            }
            FillData(companymodel);
            return View(companymodel);
        }

        public async Task<ActionResult> Delete(int id,CompanyLocationDetails compinfo)
        {
            Utilities _ipaddressobj = new Utilities();
            compinfo.ipaddress = _ipaddressobj.GetIpAddress();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    uri = string.Format("{0}CompanyAPI/" + compinfo.ipaddress, _uri);

                    var result = await client.DeleteAsync(uri + "/" + id);

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
            catch (Exception)
            {
                ViewBag.Message = MessageConfig.htmlErrorString;
                ViewBag.Status = "Failed";
                ViewBag.InnerMessage = "";
                return View();
            }
        }

        private ActionResult FillData(CompanyModel companymodel)
        {
            CompanyLocationDetails comlocDetails = new CompanyLocationDetails();
            if (companymodel.CompanyLocationList != null && companymodel.CompanyLocationList.Count!=0)
            {
                comlocDetails = companymodel.CompanyLocationList[0];    
            }
            List<CompanyLocationDetails> cmodule = new List<CompanyLocationDetails>();
            List<StateModule> smodule = new List<StateModule>();
            using (WebClient client = new WebClient())
            {
                string company = client.DownloadString(_uri + "CompanyTypeAPI");
                cmodule = JsonConvert.DeserializeObject<List<CompanyLocationDetails>>(company);
                string states = client.DownloadString(_uri + "StateAPI");
                smodule = JsonConvert.DeserializeObject<List<StateModule>>(states);
            }
            ViewBag.ADDRESSTYPEID = new SelectList(cmodule, "ADDRESS_TYPE_ID", "ADDRESS_TYPE", comlocDetails.ADDRESS_TYPE_ID);
            ViewBag.State = new SelectList(smodule, "STATE_CODE", "STATE_NAME", comlocDetails.STATE_CODE);
            return View(companymodel);
        }

    }
}
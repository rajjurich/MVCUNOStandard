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
    public class BioMetricTemplateConfigurationController : Controller
    {
        string _uri = ConfigurationManager.AppSettings["APIUrl"].ToString();
        string uri = string.Empty;
        
        
        [AccessCheck(IdParamName = "BioMetricTemplateConfiguration/index")]
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

            return await GetBiometerkist();
        }

        private async Task<BioMetricTemplateConfigurationCreateModel> GetBiometerkist(int id)
        {
            BioMetricTemplateConfigurationCreateModel biometrics;
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}BioMetricTemplateConfigurationAPI/{1}", _uri, id);
                var result = await client.GetAsync(uri);
                biometrics = await result.Content.ReadAsAsync<BioMetricTemplateConfigurationCreateModel>();
            }
            return biometrics;
        }


        private async Task<ActionResult> GetBiometerkist()
        {
            using (HttpClient client = new HttpClient())
            {
                client.AddTokenToHeader("");
                uri = string.Format("{0}BioMetricTemplateConfigurationAPI", _uri);

                var result = await client.GetAsync(uri);

                var biometrics = await result.Content.ReadAsAsync<List<BioMetricTemplateConfigurationCreateModel>>();
                return View(biometrics);
            }
        }


        

        private async Task BindDropDown()
        {
            using (HttpClient client = new HttpClient())
            {
                client.AddTokenToHeader("");
                uri = string.Format("{0}CompanyAPI/getcompanybiometric", _uri);
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
        public async Task<ActionResult> Create(BioMetricTemplateConfigurationCreateModel biometrictemp)
        {
            Utilities _ipaddressobj = new Utilities();
            biometrictemp.ipaddress = _ipaddressobj.GetIpAddress();
            try
            {
                if (ModelState.IsValid)
                {
                    using (HttpClient client = new HttpClient())
                    {
                        uri = string.Format("{0}BioMetricTemplateConfigurationAPI", _uri);

                        var result = await client.PostAsJsonAsync(uri, biometrictemp);

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
            BioMetricTemplateConfigurationCreateModel biometrictemp = await GetBiometerkist(id);
            await BindDropDown();
            return View(biometrictemp);
        }


        public async Task<ActionResult> Edit(int id, BioMetricTemplateConfigurationCreateModel biometrictemp)
        {
            Utilities _ipaddressobj = new Utilities();
            biometrictemp.ipaddress = _ipaddressobj.GetIpAddress();
            try
            {
                if (ModelState.IsValid)
                {
                    using (HttpClient client = new HttpClient())
                    {
                        uri = string.Format("{0}BioMetricTemplateConfigurationAPI", _uri);
                        var result = await client.PutAsJsonAsync(uri, biometrictemp);
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
            BioMetricTemplateConfigurationCreateModel biometrictemp = await GetBiometerkist(id);
            await BindDropDown();
            return View(biometrictemp);
        }


        [HttpPost]
        public async Task<ActionResult> Delete(BioMetricTemplateConfigurationCreateModel biometrictemp)
        {
            Utilities _ipaddressobj = new Utilities();
            biometrictemp.ipaddress = _ipaddressobj.GetIpAddress();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    uri = string.Format("{0}BioMetricTemplateConfigurationAPI", _uri);
                    var result = await client.DeleteAsync(uri + "/" + biometrictemp.bio_metric_id);
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

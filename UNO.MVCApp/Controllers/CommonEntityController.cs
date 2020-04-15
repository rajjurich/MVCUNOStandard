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
    public class CommonEntityController : Controller
    {
        string _uri = ConfigurationManager.AppSettings["APIUrl"].ToString();
        string uri = string.Empty;
        //
        // GET: /CommonMaster/

         [AccessCheck(IdParamName = "CommonEntity/index")]
        public async Task<ActionResult> Index()
        {
            List<CommonEntitiesModel> CommonList = new List<CommonEntitiesModel>();
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
                    uri = string.Format("{0}CommonAPI", _uri);
                    var result = await client.GetAsync(uri);
                    var commonEntitiesContent = await result.Content.ReadAsAsync<List<CommonEntitiesModel>>();                    
                    CommonList = commonEntitiesContent;                    
                }
                //using (WebClient client = new WebClient())
                //{
                //    string s = client.DownloadString(_uri + "CommonAPI");
                //    CommonList = JsonConvert.DeserializeObject<List<CommonEntitiesModel>>(s);
                //    CommonList.RemoveAll(item => item == null);
                //}
            }
            catch (Exception ex)
            {

                ViewBag.Message = MessageConfig.htmlErrorString;
                ViewBag.Status = "Failed";
                ViewBag.InnerMessage = ex.Message;
            }
        
            return View(CommonList);
        }        

        [HttpGet]
        public async Task<ActionResult> Create()
        {
            await FillData();
            return View();
        }

        public async Task<ActionResult> Create(CommonEntitiesModel cominfo)
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
                        uri = string.Format("{0}CommonAPI", _uri);

                        var result = await client.PostAsJsonAsync(uri, cominfo);

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
                           await FillData();
                            return View();
                        }
                    }
                }
                else
                {
                    ViewBag.Message = MessageConfig.htmlErrorString;
                    ViewBag.Status = "Failed";
                    ViewBag.InnerMessage = "Model State Failed";
                    await FillData();
                    return View(cominfo);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = MessageConfig.htmlErrorString;
                ViewBag.Status = "Failed";
                ViewBag.InnerMessage = ex.Message;
               
            }
            await FillData();
            return View(cominfo);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            CommonEntitiesModel commonModel = new CommonEntitiesModel();

            using (WebClient client = new WebClient())
            {
                string common = client.DownloadString(_uri + "CommonAPI/" + id);
                commonModel = JsonConvert.DeserializeObject<CommonEntitiesModel>(common);
            }
            await FillData();
            return View(commonModel);
        }

        public async Task<ActionResult> Edit(CommonEntitiesModel cominfo)
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
                        uri = string.Format("{0}CommonAPI", _uri);

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
                            await FillData();
                            return View();
                        }
                    }
                }
                else
                {
                    ViewBag.Message = MessageConfig.htmlErrorString;
                    ViewBag.Status = "Failed";
                    ViewBag.InnerMessage = "Model State Failed";
                    await FillData();
                    return View(cominfo);
                }
            }
            catch (Exception)
            {
                ViewBag.Message = MessageConfig.htmlErrorString;
                ViewBag.Status = "Failed";
                ViewBag.InnerMessage = "";
                
            }
            await FillData();
            return View(cominfo);
        }

        [HttpGet]
        public async Task<ActionResult> View(int id)
        {
            CommonEntitiesModel commonModel = new CommonEntitiesModel();

            using (WebClient client = new WebClient())
            {
                string common = client.DownloadString(_uri + "CommonAPI/" + id);
                commonModel = JsonConvert.DeserializeObject<CommonEntitiesModel>(common);
            }
            await FillData();
            return View(commonModel);
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            CommonEntitiesModel commonModel = new CommonEntitiesModel();

            using (WebClient client = new WebClient())
            {
                string common = client.DownloadString(_uri + "CommonAPI/" + id);
                commonModel = JsonConvert.DeserializeObject<CommonEntitiesModel>(common);
            }
            await FillData();
            return View(commonModel);
        }

        public async Task<ActionResult> Delete(CommonEntitiesModel cominfo)
        {
            Utilities _ipaddressobj = new Utilities();
            cominfo.ipaddress = _ipaddressobj.GetIpAddress();
            try
            {
                    using (HttpClient client = new HttpClient())
                    {
                        uri = string.Format("{0}CommonAPI/" + cominfo.ID + "/" + cominfo.ipaddress, _uri);

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
                            await FillData();
                            return View(cominfo);
                        }
                    }
            }
            catch (Exception)
            {
                ViewBag.Message = MessageConfig.htmlErrorString;
                ViewBag.Status = "Failed";
                ViewBag.InnerMessage = "";
                
            }
            await FillData();
            return View(cominfo);
        }

        private async Task<ActionResult> FillData()
        {

            List<CommonMasterModel> cmodule = new List<CommonMasterModel>();
            List<CompanyModel> lstCompany = new List<CompanyModel>();
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

                uri = string.Format("{0}CommonMasterAPI", _uri);
                result = await client.GetAsync(uri);
                var commonMasterContent = await result.Content.ReadAsAsync<List<CommonMasterModel>>();
                var CommonMasters = commonMasterContent.Select(c => new SelectListItem
                {
                    Value = c.COMMON_TYPES_ID.ToString(),
                    Text = c.COMMON_NAME
                });

                ViewBag.COMMON_TYPES = CommonMasters;
                ViewBag.COMPANY_LIST = Companies;
            }
            //using (WebClient client = new WebClient())
            //{
            //    string common = client.DownloadString(_uri + "CommonMasterAPI");
            //    cmodule = JsonConvert.DeserializeObject<List<CommonMasterModel>>(common);
            //    string company = client.DownloadString(_uri + "CompanyAPI");
            //    lstCompany = JsonConvert.DeserializeObject<List<CompanyModel>>(company);
            //}

            //ViewBag.COMMON_TYPES = new SelectList(cmodule, "COMMON_TYPES_ID", "COMMON_NAME");
            //ViewBag.COMPANY_LIST = Companies;

            return View();
        }
    }
}

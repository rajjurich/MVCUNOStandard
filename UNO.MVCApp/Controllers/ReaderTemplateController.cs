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
    public class ReaderTemplateController : Controller
    {
        //
        // GET: /ReaderTemplate/       
        string _uri = ConfigurationManager.AppSettings["APIUrl"].ToString();
        string uri = string.Empty;

        [AccessCheck(IdParamName = "ReaderTemplate/index")]
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

            return await GetLReaderTemplateList();
        }

        private async Task<ActionResult> GetLReaderTemplateList()
        {
            using (HttpClient client = new HttpClient())
            {
                client.AddTokenToHeader("");
                uri = string.Format("{0}ReaderTemplateAPI", _uri);
                var result = await client.GetAsync(uri);
                var ReaderTemplateModel = await result.Content.ReadAsAsync<List<ReaderTemplateModel>>();
                return View(ReaderTemplateModel);
            }
        }

        private async Task<ReaderTemplateModel> GetReaderTemplate(int id)
        {
            ReaderTemplateModel ReaderTemplate;
            using (HttpClient client = new HttpClient())
            {
                client.AddTokenToHeader("");
                uri = string.Format("{0}ReaderTemplateAPI/{1}", _uri, id);
                var result = await client.GetAsync(uri);
                ReaderTemplate = await result.Content.ReadAsAsync<ReaderTemplateModel>();
            }
            return ReaderTemplate;
        }

        private async Task BindDropDown()
        {
            using (HttpClient client = new HttpClient())
            {
                client.AddTokenToHeader("");
                uri = string.Format("{0}ControlApi", _uri);
                var result = await client.GetAsync(uri);
                var ControlContent = await result.Content.ReadAsAsync<List<Control>>();
                var Control = ControlContent.Select(c => new SelectListItem
                {
                    Value = c.ControllerID.ToString(),
                    Text = c.ControllerName
                });
                ViewBag.ControlId = Control;

                
                uri = string.Format("{0}CompanyAPI", _uri);
                var result1 = await client.GetAsync(uri);
                var CompanyContent = await result1.Content.ReadAsAsync<List<CompanyModel>>();
                var Company = CompanyContent.Select(c => new SelectListItem
                {
                    Value = c.COMPANY_ID.ToString(),
                    Text = c.COMPANY_NAME
                });
                ViewBag.CompanyId = Company;

               
                uri = string.Format("{0}entparams/EVENTMASTER", _uri);
                var result2 = await client.GetAsync(uri);
                var EntParamsContent = await result2.Content.ReadAsAsync<List<EntParams>>();
                var EntParams = EntParamsContent.Select(c => new SelectListItem
                {
                    Value = c.PARAM_ID.ToString(),
                    Text = c.VALUE
                });
                ViewBag.EventId = EntParams;
            }
        }


        [HttpGet]
        public async Task<ActionResult> Create()
        {
            await BindDropDown();
            return View();
        }
        public async Task<ActionResult> Create(ReaderTemplateModel Readertemplate)
        {
            Utilities _ipaddressobj = new Utilities();
            Readertemplate.ipaddress = _ipaddressobj.GetIpAddress();
            try
            {
                if (ModelState.IsValid)
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.AddTokenToHeader("");
                        uri = string.Format("{0}ReaderTemplateAPI", _uri);

                        var result = await client.PostAsJsonAsync(uri, Readertemplate);

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
            ReaderTemplateModel ReaderTemplate = await GetReaderTemplate(id);
            await BindDropDown();
            return View(ReaderTemplate);
        }

        public async Task<ActionResult> Edit(int id, ReaderTemplateModel Readertemplate)
        {
            Utilities _ipaddressobj = new Utilities();
            Readertemplate.ipaddress = _ipaddressobj.GetIpAddress();
            try
            {
                if (ModelState.IsValid)
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.AddTokenToHeader("");
                        uri = string.Format("{0}ReaderTemplateAPI", _uri);
                        var result = await client.PutAsJsonAsync(uri, Readertemplate);
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
            ReaderTemplateModel ReaderTemplate = await GetReaderTemplate(id);
            await BindDropDown();
            return View(ReaderTemplate);
        }

        [HttpPost]
        public async Task<ActionResult> DeleteView( ReaderTemplateModel ReaderTemplate)
        {
            Utilities _ipaddressobj = new Utilities();
            ReaderTemplate.ipaddress = _ipaddressobj.GetIpAddress();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.AddTokenToHeader("");
                    uri = string.Format("{0}ReaderTemplateAPI", _uri);
                    var result = await client.DeleteAsync(uri + "/" + ReaderTemplate.RowID + "/" + ReaderTemplate.ipaddress);
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

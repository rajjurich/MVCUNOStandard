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
    public class WeeklyOffController : Controller
    {
        string _uri = ConfigurationManager.AppSettings["APIUrl"].ToString();
        string uri = string.Empty;    
        //
        // GET: /WeeklyOff/
        [AccessCheck(IdParamName = "WeeklyOff/index")]
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

            return await GetWeeklyOffList();
        }

        private async Task<ActionResult> GetWeeklyOffList()
        {
            using (HttpClient client = new HttpClient())
            {
                client.AddTokenToHeader("");
                uri = string.Format("{0}WeeklyOffAPI", _uri);
                var result = await client.GetAsync(uri);
                var WeeklyOff = await result.Content.ReadAsAsync<List<WeeklyOff>>();
                return View(WeeklyOff);
            }
        }


        private async Task<WeeklyOff> GetWeeklyOff(int id)
        {
            WeeklyOff WeeklyOff;
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}WeeklyOffAPI/{1}", _uri, id);
                var result = await client.GetAsync(uri);
                WeeklyOff = await result.Content.ReadAsAsync<WeeklyOff>();

                if (WeeklyOff.MWK_PAT != null)
                {
                    if (WeeklyOff.MWK_PAT.ToString() == "WeekEnd")
                    {
                        ViewBag.Number = 10;
                    }
                }
            }
            return WeeklyOff;
        }

        private async Task BindDropDown()
        {
            using (HttpClient client = new HttpClient())
            {
                client.AddTokenToHeader("");
                uri = string.Format("{0}CompanyAPI", _uri);
                var result1 = await client.GetAsync(uri);
                var CompanyContent = await result1.Content.ReadAsAsync<List<CompanyModel>>();
                var Company = CompanyContent.Select(c => new SelectListItem
                {
                    Value = c.COMPANY_ID.ToString(),
                    Text = c.COMPANY_NAME
                });
                ViewBag.CompanyId = Company;

                var Day = new List<SelectListItem>();
                Day.Add(new SelectListItem() { Text = "Sunday", Value = "1", Selected = true });
                Day.Add(new SelectListItem() { Text = "Monday", Value = "2" });
                Day.Add(new SelectListItem() { Text = "Tuesday", Value = "3" });
                Day.Add(new SelectListItem() { Text = "Wednesday", Value = "4" });
                Day.Add(new SelectListItem() { Text = "Thursday", Value = "5" });
                Day.Add(new SelectListItem() { Text = "Friday", Value = "6" });
                Day.Add(new SelectListItem() { Text = "Saturday", Value = "7" });
                ViewBag.Day = Day;
            }
        }

        [HttpGet]
        public async Task<ActionResult> Create()
        {
            await BindDropDown();
            return View();
        }

        public async Task<ActionResult> Create(WeeklyOff WeeklyOff)
        {
            Utilities _ipaddressobj = new Utilities();
            WeeklyOff.ipaddress = _ipaddressobj.GetIpAddress();
            try
            {
                if (ModelState.IsValid)
                {
                    using (HttpClient client = new HttpClient())
                    {
                        uri = string.Format("{0}WeeklyOffAPI", _uri);

                        var result = await client.PostAsJsonAsync(uri, WeeklyOff);

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
            WeeklyOff WeeklyOff = await GetWeeklyOff(id);
            await BindDropDown();
            return View(WeeklyOff);
        }

        public async Task<ActionResult> View(int id)
        {
            WeeklyOff WeeklyOff = await GetWeeklyOff(id);
            await BindDropDown();
            return View(WeeklyOff);
        }

        public async Task<ActionResult> Edit(int id, WeeklyOff WeeklyOff)
        {
            Utilities _ipaddressobj = new Utilities();
            WeeklyOff.ipaddress = _ipaddressobj.GetIpAddress();
            try
            {
                if (ModelState.IsValid)
                {
                    using (HttpClient client = new HttpClient())
                    {
                        uri = string.Format("{0}WeeklyOffAPI", _uri);
                        var result = await client.PutAsJsonAsync(uri, WeeklyOff);
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
            WeeklyOff WeeklyOff = await GetWeeklyOff(id);
            await BindDropDown();
            return View(WeeklyOff);
        }

        public async Task<ActionResult> DeleteView(WeeklyOff WeeklyOff)
        {
            Utilities _ipaddressobj = new Utilities();
            WeeklyOff.ipaddress = _ipaddressobj.GetIpAddress();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    uri = string.Format("{0}WeeklyOffAPI", _uri);
                    var result = await client.DeleteAsync(uri + "/" + WeeklyOff.MWK_CD + "/" + WeeklyOff.ipaddress);
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

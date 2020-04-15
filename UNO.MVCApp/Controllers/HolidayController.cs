using Newtonsoft.Json;
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
    public class HolidayController : Controller
    {
        //
        // GET: /Holiday/
        string url = WebConfigurationManager.AppSettings["APIUrl"];
        string uri = string.Empty;
        string _uri = ConfigurationManager.AppSettings["APIUrl"].ToString();

        //
        // GET: /Menu/
        [AccessCheck(IdParamName = "Holiday/index")]
        public ActionResult Index()
        {
            List<HolidayVm> HolidayList = new List<HolidayVm>();
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

            using (WebClient client = new WebClient())
            {
                string s = client.DownloadString(url + "HolidayAPI");
                HolidayList = JsonConvert.DeserializeObject<List<HolidayVm>>(s);
                HolidayList.RemoveAll(item => item == null);
            }
            return View(HolidayList);
        }

        [HttpGet]
        public async Task<ActionResult> Create()
        {

            await BindDropDown();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(HolidayVm holiday)
        {
            Utilities _ipaddressobj = new Utilities();
            holiday.ipaddress = _ipaddressobj.GetIpAddress();
            
            try
            {
                holiday.ACTIVE_USER = Convert.ToString(Session["user"]);
                if (ModelState.IsValid)
                {
                    using (HttpClient client = new HttpClient())
                    {
                        if (holiday.HolidayLoc !=null) 
                        { 
                            for (var i = 0; i < holiday.HolidayLoc.Count; i++)
                            {
                                if (holiday.HolidayLoc[i].IS_bool_OPTIONAL == true)
                                {
                                    holiday.HolidayLoc[i].IS_OPTIONAL = 1;
                                }
                                else
                                {
                                    holiday.HolidayLoc[i].IS_OPTIONAL = 0;
                                }
                            }
                        }
                        var result = await client.PostAsJsonAsync(url + "HolidayAPI/post", holiday);
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
                        }
                    }
                }
                else
                {
                    ViewBag.Message = MessageConfig.htmlErrorString;
                    ViewBag.Status = "Failed";
                    ViewBag.InnerMessage = "Model State Failed";
                }

            }
            catch (Exception ex)
            {
                ViewBag.Message = MessageConfig.htmlErrorString;
                ViewBag.Status = "Failed";
                ViewBag.InnerMessage = ex.Message;
            }
            await BindDropDown();
            return View();
        }
        public async Task BindDropDown()
        {
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

                uri = string.Format("{0}RoleAPI", _uri);


                ViewBag.CompanyId = Companies;

            }
            var holidayType = new List<SelectListItem>();
            holidayType.Add(new SelectListItem() { Text = "NATIONAL", Value = "N" });
            holidayType.Add(new SelectListItem() { Text = "OPTIONAL", Value = "O" });
            holidayType.Add(new SelectListItem() { Text = "REGIONAL", Value = "R" });
            ViewBag.holidayid = holidayType;
        }

        public HolidayVm BindGrid(int id)
        {
            HolidayVm HolidayObj = new HolidayVm();
            using (WebClient client = new WebClient())
            {
                string s = client.DownloadString(url + "HolidayAPI/" + id);
                HolidayObj = JsonConvert.DeserializeObject<HolidayVm>(s);
            }
            if (HolidayObj.HolidayLoc.Count > 0)
            {
                HolidayObj.LocationWise = "S";
                for (int i = 0; i < HolidayObj.HolidayLoc.Count; i++)
                {
                    if (HolidayObj.HolidayLoc[i].HOLIDAY_ID == id)
                    {
                        HolidayObj.HolidayLoc[i].IS_bool_HOLIDAYLOC_ID = true;
                        if (HolidayObj.HolidayLoc[i].IS_OPTIONAL == 1)
                        {
                            HolidayObj.HolidayLoc[i].IS_bool_OPTIONAL = true;
                        }
                        else
                        {
                            HolidayObj.HolidayLoc[i].IS_bool_OPTIONAL = false;
                        }
                    }
                    else
                    {
                        HolidayObj.HolidayLoc[i].IS_bool_HOLIDAYLOC_ID = false;
                        HolidayObj.HolidayLoc[i].IS_bool_OPTIONAL = false;
                    }
                }
            }
            else
            {
                for (int i = 0; i < HolidayObj.HolidayLoc.Count; i++)
                {
                    HolidayObj.HolidayLoc[i].IS_bool_HOLIDAYLOC_ID = false;
                }
            }
            if (HolidayObj.HOLIDAY_DATE != null)
                HolidayObj.HOLIDAY_DATE = Convert.ToDateTime(HolidayObj.HOLIDAY_DATE.GetDateTimeFormats()[5]);
            return HolidayObj;
        }

        public async Task<ActionResult> Edit(int id)
        {

            //HolidayVm HolidayObj = new HolidayVm();
            //HolidayObj = BindGrid(id);
            await BindDropDown();
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Edit(HolidayVm holiday)
        {
            Utilities _ipaddressobj = new Utilities();
            holiday.ipaddress = _ipaddressobj.GetIpAddress();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    for (var i = 0; i < holiday.HolidayLoc.Count; i++)
                    {
                        if (holiday.HolidayLoc[i].IS_bool_OPTIONAL == true)
                        {
                            holiday.HolidayLoc[i].IS_OPTIONAL = 1;
                        }
                        else
                        {
                            holiday.HolidayLoc[i].IS_OPTIONAL = 0;
                        }
                    }
                    var result = await client.PutAsJsonAsync(url + "HolidayAPI/put", holiday);
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
                        BindDropDown();
                        return View();
                    }
                }


            }
            catch (Exception ex)
            {
                ViewBag.Message = MessageConfig.htmlErrorString;
                ViewBag.Status = "Failed";
                ViewBag.InnerMessage = ex.Message;
                BindDropDown();
                return View();
            }
        }
        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            HolidayVm HolidayObj = new HolidayVm();
            HolidayObj = BindGrid(id);
            await BindDropDown();
            return View(HolidayObj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(HolidayVm holiday)
        {
            Utilities _ipaddressobj = new Utilities();
            holiday.ipaddress = _ipaddressobj.GetIpAddress();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    uri = string.Format("{0}HolidayAPI", url);

                    var result = await client.DeleteAsync(uri + "/" + holiday.HOLIDAY_ID + "/" + holiday.ipaddress);
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
                        BindDropDown();
                        return View();
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = MessageConfig.htmlErrorString;
                ViewBag.Status = "Failed";
                ViewBag.InnerMessage = ex.Message;
                BindDropDown();
                return View();
            }
        }
        public async Task<ActionResult> View(int id)
        {
            HolidayVm HolidayObj = new HolidayVm();
            HolidayObj = BindGrid(id);
            await BindDropDown();
            return View(HolidayObj);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
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
    public class AccessLevelController : Controller
    {
        string _uri = ConfigurationManager.AppSettings["APIUrl"].ToString();
        string uri = string.Empty;
        //
        // GET: /AccessLevel/
        [AccessCheck(IdParamName = "AccessLevel/index")]
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
            return await GetAccessLevelList();
        }

        private async Task<ActionResult> GetAccessLevelList()
        {
            using (HttpClient client = new HttpClient())
            {
                client.AddTokenToHeader("");
                uri = string.Format("{0}accesslevel", _uri);

                var result = await client.GetAsync(uri);

                var accessLevelList = await result.Content.ReadAsAsync<List<AccessLevelInfo>>();
                return View(accessLevelList);
            }
        }
        //
        // GET: /AccessLevel/Details/5

        public async Task<ActionResult> Details(int id)
        {
            AccessLevel acs = await GetAccessLevel(id);
            await BindDropDown();
            return View(acs);
        }

        private async Task<AccessLevel> GetAccessLevel(int id)
        {
            AccessLevel accessLevel;
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}accesslevel/{1}", _uri, id);

                var result = await client.GetAsync(uri);

                accessLevel = await result.Content.ReadAsAsync<AccessLevel>();
            }
            return accessLevel;
        }

        //
        // GET: /AccessLevel/Create

        public async Task<ActionResult> Create()
        {
            await BindDropDown();
            return View();
        }

        private async Task BindDropDown()
        {
            using (HttpClient client = new HttpClient())
            {
                client.AddTokenToHeader("");
                uri = string.Format("{0}reader", _uri);
                var result = await client.GetAsync(uri);
                var content = await result.Content.ReadAsAsync<List<AcsReaderInfo>>();
                var readers = content.Select(d => new SelectListItem
                {
                    Value = d.RowId.ToString(),
                    Text = d.READER_DESCRIPTION
                });
                ViewBag.Readers = readers;

                uri = string.Format("{0}zone", _uri);
                result = await client.GetAsync(uri);
                var zoneContent = await result.Content.ReadAsAsync<List<Zone>>();
                var zones = zoneContent.Select(c => new SelectListItem
                {
                    Value = c.ZONE_ID.ToString(),
                    Text = c.ZONE_DESCRIPTION
                });
                ViewBag.Zones = zones;

                uri = string.Format("{0}timezone", _uri);
                result = await client.GetAsync(uri);
                var timezoneContent = await result.Content.ReadAsAsync<List<AcsTimeZone>>();
                var timezones = timezoneContent.Select(c => new SelectListItem
                {
                    Value = c.TZ_ID.ToString(),
                    Text = c.TZ_DESCRIPTION
                });
                ViewBag.TimeZones = timezones;
            }
        }
        //
        // POST: /AccessLevel/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AccessLevel collection)
        {
            Utilities _ipaddressobj = new Utilities();
            collection.ipaddress = _ipaddressobj.GetIpAddress();

            try
            {
                if (ModelState.IsValid)
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.AddTokenToHeader("");
                        uri = string.Format("{0}accesslevel", _uri);

                        var result = await client.PostAsJsonAsync(uri, collection);
                        var contents = await result.Content.ReadAsStringAsync();
                        if (result.IsSuccessStatusCode)
                        {
                            TempData["Message"] = MessageConfig.htmlSuccessString;
                            TempData["Status"] = "Success";
                            TempData["InnerMessage"] = "";
                            //BindDropDown();
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ViewBag.Message = MessageConfig.htmlErrorString;
                            ViewBag.Status = "Failed";
                            ViewBag.InnerMessage = contents;
                            await BindDropDown();
                            return View();
                        }
                    }
                }
                else
                {
                    // TODO: Add insert logic here
                    ViewBag.Message = MessageConfig.htmlErrorString;
                    ViewBag.Status = "Failed";
                    ViewBag.InnerMessage = "Model State Failed";
                    //return RedirectToAction("Index");
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

        //
        // GET: /AccessLevel/Edit/5

        public async Task<ActionResult> Edit(int id)
        {            
            AccessLevel acs = await GetAccessLevel(id);
            await BindDropDown();
            return View(acs);
        }


        //
        // POST: /AccessLevel/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, AccessLevel collection)
        {
            Utilities _ipaddressobj = new Utilities();
            collection.ipaddress = _ipaddressobj.GetIpAddress();
            AccessLevel acs = await GetAccessLevel(id);
            try
            {
                if (id == collection.AL_ID)
                {
                    // TODO: Add update logic here
                    if (ModelState.IsValid)
                    {
                        using (HttpClient client = new HttpClient())
                        {
                            uri = string.Format("{0}accesslevel/{1}", _uri, id);
                            var result = await client.PutAsJsonAsync(uri, collection);
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
                                await BindDropDown();
                                return View(acs);
                            }
                        }
                    }
                    else
                    {
                        // TODO: Add insert logic here
                        ViewBag.Message = MessageConfig.htmlErrorString;
                        ViewBag.Status = "Failed";
                        ViewBag.InnerMessage = "Model State Failed";

                        //return RedirectToAction("Index");
                    }

                    //return RedirectToAction("Index");
                }
                else
                {
                    throw new Exception("Invalid Model");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = MessageConfig.htmlErrorString;
                ViewBag.Status = "Failed";
                ViewBag.InnerMessage = ex.Message;

            }
            await BindDropDown();
            return View(acs);
        }

        //
        // GET: /AccessLevel/Delete/5

        public async Task<ActionResult> Delete(int id)
        {
            AccessLevel acs = await GetAccessLevel(id);
            await BindDropDown();
            return View(acs);
        }

        //
        // POST: /AccessLevel/Delete/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, AccessLevel collection)
        {
            Utilities _ipaddressobj = new Utilities();
            collection.ipaddress = _ipaddressobj.GetIpAddress();
            try
            {
                // TODO: Add delete logic here
                if (id == collection.AL_ID)
                {
                    using (HttpClient client = new HttpClient())
                    {

                        uri = string.Format("{0}accesslevel/{1}/{2}/{3}", _uri, id, Session["User"].ToString(), collection.ipaddress);

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
                            TempData["Message"] = MessageConfig.htmlErrorString;
                            TempData["Status"] = "Failed";
                            TempData["InnerMessage"] = contents;
                            return RedirectToAction("Index");
                        }
                    }


                }
                else
                {
                    TempData["Message"] = MessageConfig.htmlErrorString;
                    TempData["Status"] = "Failed";
                    TempData["InnerMessage"] = "Invalid";
                    return RedirectToAction("Index");

                }
                //return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Message"] = MessageConfig.htmlErrorString;
                TempData["InnerMessage"] = ex.Message;
                TempData["Status"] = "Failed";
                return RedirectToAction("Index");
            }
        }
    }
}

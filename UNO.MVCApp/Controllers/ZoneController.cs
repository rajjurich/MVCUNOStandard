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
    public class ZoneController : Controller
    {
        string _uri = ConfigurationManager.AppSettings["APIUrl"].ToString();
        string uri = string.Empty;
        //
        // GET: /Zone/
        [AccessCheck(IdParamName = "Zone/index")]
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

            return await GetZoneList();
        }

        private async Task<ActionResult> GetZoneList()
        {
            using (HttpClient client = new HttpClient())
            {
                client.AddTokenToHeader("");
                uri = string.Format("{0}zone", _uri);

                var result = await client.GetAsync(uri);

                var zones = await result.Content.ReadAsAsync<List<Zone>>();
                return View(zones);
            }
        }

        //
        // GET: /Zone/Details/5

        public async Task<ActionResult> Details(int id)
        {
            Zone zone = await GetZone(id);
            await BindDropDown();
            return View(zone);
        }

        //
        // GET: /Zone/Create

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

                uri = string.Format("{0}CompanyAPI", _uri);
                result = await client.GetAsync(uri);
                var companyContent = await result.Content.ReadAsAsync<List<CompanyModel>>();
                var companies = companyContent.Select(c => new SelectListItem
                {
                    Value = c.COMPANY_ID.ToString(),
                    Text = c.COMPANY_NAME
                });
                ViewBag.Companies = companies;
            }
        }

        //
        // POST: /Zone/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Zone collection)
        {
            Utilities _ipaddressobj = new Utilities();
            collection.ipaddress = _ipaddressobj.GetIpAddress();
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    using (HttpClient client = new HttpClient())
                    {
                        uri = string.Format("{0}zone", _uri);

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
                    ViewBag.Message = MessageConfig.htmlErrorString;
                    ViewBag.Status = "Failed";
                    ViewBag.InnerMessage = "Model State Failed";
                }
                //return RedirectToAction("Index");

            }
            catch
            {
                ViewBag.Message = MessageConfig.htmlErrorString;
                ViewBag.Status = "Failed";
                ViewBag.InnerMessage = "Model State Failed";

            }
            await BindDropDown();
            return View();
        }

        //
        // GET: /Zone/Edit/5

        public async Task<ActionResult> Edit(int id)
        {
            Zone zone = await GetZone(id);
            await BindDropDown();
            return View(zone);
        }

        private async Task<Zone> GetZone(int id)
        {
            Zone zone;
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}zone/{1}", _uri, id);

                var result = await client.GetAsync(uri);

                zone = await result.Content.ReadAsAsync<Zone>();
            }
            return zone;
        }
        //
        // POST: /Zone/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Zone collection)
        {
            Utilities _ipaddressobj = new Utilities();
            collection.ipaddress = _ipaddressobj.GetIpAddress();
            Zone zone = await GetZone(id);
            try
            {
                if (id == collection.ZONE_ID)
                {
                    // TODO: Add update logic here
                    if (ModelState.IsValid)
                    {
                        using (HttpClient client = new HttpClient())
                        {
                            uri = string.Format("{0}zone/{1}", _uri, id);
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
                                return View(zone);
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
            return View(zone);
        }

        //
        // GET: /Zone/Delete/5

        public async Task<ActionResult> Delete(int id)
        {
            Zone zone = await GetZone(id);
            await BindDropDown();
            return View(zone);
        }

        //
        // POST: /Zone/Delete/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, Zone collection)
        {
            Utilities _ipaddressobj = new Utilities();
            collection.ipaddress = _ipaddressobj.GetIpAddress();
            try
            {
                // TODO: Add delete logic here

                using (HttpClient client = new HttpClient())
                {
                    uri = string.Format("{0}zone/{1}/{2}/{3}", _uri, id, Session["User"].ToString(), collection.ipaddress);

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

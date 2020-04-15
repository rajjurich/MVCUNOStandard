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
    public class ControllerController : Controller
    {
        string _uri = ConfigurationManager.AppSettings["APIUrl"].ToString();
        string uri = string.Empty;
        //
        // GET: /Controller/
        [AccessCheck(IdParamName = "Controller/index")]
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

            return await GetControllerList();

        }

        private async Task<ActionResult> GetControllerList()
        {
            using (HttpClient client = new HttpClient())
            {
                client.AddTokenToHeader("");
                uri = string.Format("{0}controller", _uri);

                var result = await client.GetAsync(uri);

                var acs = await result.Content.ReadAsAsync<List<AcsControllerInfo>>();
                return View(acs);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Index(FormCollection collection)
        {
            if (collection != null)
            {
                string[] ids = collection["ID"].Split(new char[] { ',' });
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        foreach (var id in ids)
                        {
                            uri = string.Format("{0}controller/{1}", _uri, id);

                            var result = await client.DeleteAsync(uri);
                            var contents = await result.Content.ReadAsStringAsync();
                        }
                        ViewBag.Message = MessageConfig.htmlErrorString;
                        ViewBag.Status = "Success";
                        ViewBag.InnerMessage = string.Format("{0} row(s) deleted", ids.Length);
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = MessageConfig.htmlErrorString;
                    ViewBag.Status = "Failed";
                    ViewBag.InnerMessage = ex.Message;
                }
            }
            return await GetControllerList();
        }
        //
        // GET: /Controller/Details/5

        public async Task<ActionResult> Details(int id)
        {
            AcsController acs = await GetController(id);
            await BindDropDown();
            return View(acs);
        }

        //
        // GET: /Controller/Create

        public async Task<ActionResult> Create()
        {
            //ViewBag.Message = TempData["Message"];
            //ViewBag.InnerMessage = TempData["InnerMessage"];
            //ViewBag.Status = TempData["Status"];
            await BindDropDown();
            return View();
        }
        private async Task BindDropDown()
        {
            IEnumerable<SelectListItem> entParams;
            IEnumerable<SelectListItem> Authentications;
            IEnumerable<SelectListItem> ReaderModes;
            IEnumerable<SelectListItem> ReaderTypes;
            IEnumerable<SelectListItem> DoorTypes;
            IEnumerable<SelectListItem> Companies;

            string s = string.Empty;


            using (HttpClient client = new HttpClient())
            {
                client.AddTokenToHeader("");
                uri = string.Format("{0}entparams/CONTROLLERTYPE", _uri);
                var result = await client.GetAsync(uri);
                var entparams = await result.Content.ReadAsAsync<List<dynamic>>();
                entParams = entparams.Select(m => new SelectListItem
                {
                    Value = m.CODE,
                    Text = m.VALUE
                });

                uri = string.Format("{0}entparams/AUTHENTICATIONMODE", _uri);
                result = await client.GetAsync(uri);
                var authentications = await result.Content.ReadAsAsync<List<dynamic>>();
                Authentications = authentications.Select(m => new SelectListItem
                {
                    Value = m.CODE,
                    Text = m.VALUE,
                    Selected = m.CODE == "C" ? true : false
                });

                uri = string.Format("{0}entparams/READERMODE", _uri);
                result = await client.GetAsync(uri);
                var readerModes = await result.Content.ReadAsAsync<List<dynamic>>();
                ReaderModes = readerModes.Select(m => new SelectListItem
                {
                    Value = m.CODE,
                    Text = m.VALUE
                });

                uri = string.Format("{0}entparams/READERTYPE", _uri);
                result = await client.GetAsync(uri);
                var readerTypes = await result.Content.ReadAsAsync<List<dynamic>>();
                ReaderTypes = readerTypes.Select(m => new SelectListItem
                {
                    Value = m.CODE,
                    Text = m.VALUE
                });

                uri = string.Format("{0}entparams/DOORTYPE", _uri);
                result = await client.GetAsync(uri);
                var doorTypes = await result.Content.ReadAsAsync<List<dynamic>>();
                DoorTypes = doorTypes.Select(m => new SelectListItem
                {
                    Value = m.CODE,
                    Text = m.VALUE
                });

                uri = string.Format("{0}CompanyAPI", _uri);
                result = await client.GetAsync(uri);
                var companyContent = await result.Content.ReadAsAsync<List<CompanyModel>>();
                Companies = companyContent.Select(c => new SelectListItem
                {
                    Value = c.COMPANY_ID.ToString(),
                    Text = c.COMPANY_NAME
                });

            }


            ViewBag.EntParams = entParams;
            ViewBag.Authentication = Authentications;
            ViewBag.ReaderModes = ReaderModes;
            ViewBag.ReaderTypes = ReaderTypes;
            ViewBag.DoorTypes = DoorTypes;
            ViewBag.Companies = Companies;

            var Schedule = new List<SelectListItem>();
            Schedule.Add(new SelectListItem() { Text = "TIMED", Value = "T", Selected = true });
            Schedule.Add(new SelectListItem() { Text = "MIDNIGHT", Value = "M" });
            Schedule.Add(new SelectListItem() { Text = "HARD", Value = "H" });
            ViewBag.Schedule = Schedule;

            var Antipassback = new List<SelectListItem>();
            Antipassback.Add(new SelectListItem() { Text = "YES", Value = "Y" });
            Antipassback.Add(new SelectListItem() { Text = "NO", Value = "N", Selected = true });
            ViewBag.Antipassback = Antipassback;
        }

        //
        // POST: /Controller/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AcsController collection)
        {

            Utilities _ipaddressobj = new Utilities();
            collection.ipaddress = _ipaddressobj.GetIpAddress();
            try
            {
                if (ModelState.IsValid)
                {
                    using (HttpClient client = new HttpClient())
                    {
                        uri = string.Format("{0}controller", _uri);

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
        // GET: /Controller/Edit/5

        public async Task<ActionResult> Edit(int id)
        {
            AcsController acs = await GetController(id);
            await BindDropDown();
            return View(acs);
        }

        private async Task<AcsController> GetController(int id)
        {
            AcsController acs;
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}controller/{1}", _uri, id);

                var result = await client.GetAsync(uri);

                acs = await result.Content.ReadAsAsync<AcsController>();
            }
            return acs;
        }

        //
        // POST: /Controller/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, AcsController collection)
        {
            Utilities _ipaddressobj = new Utilities();
            collection.ipaddress = _ipaddressobj.GetIpAddress();
            AcsController acs = await GetController(id);
            try
            {
                if (id == collection.ID)
                {
                    // TODO: Add update logic here
                    if (ModelState.IsValid)
                    {
                        using (HttpClient client = new HttpClient())
                        {
                            uri = string.Format("{0}controller/{1}", _uri, id);
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
        // GET: /Controller/Delete/5

        public async Task<ActionResult> Delete(int id)
        {
            AcsController acs = await GetController(id);
            await BindDropDown();
            return View(acs);
        }

        //
        // POST: /Controller/Delete/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, AcsController collection)
        {
            Utilities _ipaddressobj = new Utilities();
            collection.ipaddress = _ipaddressobj.GetIpAddress();
            try
            {
                // TODO: Add delete logic here
                if (id == collection.ID)
                {
                    using (HttpClient client = new HttpClient())
                    {

                        uri = string.Format("{0}controller/{1}/{2}/{3}", _uri, id, Session["User"].ToString(), collection.ipaddress);

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

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
    public class ShiftPatternController : Controller
    {
        string _uri = ConfigurationManager.AppSettings["APIUrl"].ToString();
        string uri = string.Empty;
        //
        // GET: /ShiftPattern/
         [AccessCheck(IdParamName = "Shift/index")]
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

            return await GetShiftPatternList();
        }
         private async Task<ActionResult> GetShiftPatternList()
         {
             using (HttpClient client = new HttpClient())
             {
                 client.AddTokenToHeader("");
                 uri = string.Format("{0}shiftpattern", _uri);

                 var result = await client.GetAsync(uri);

                 var shiftPattern = await result.Content.ReadAsAsync<List<ShiftPattern>>();
                 return View(shiftPattern);
             }
         }
        //
        // GET: /ShiftPattern/Details/5

         public async Task<ActionResult> Details(int id)
         {
             ShiftPattern shiftPattern = await GetShiftPattern(id);
             await BindDropDown();
             return View(shiftPattern);
         }

        //
        // GET: /ShiftPattern/Create

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
                uri = string.Format("{0}CompanyAPI", _uri);
                var result = await client.GetAsync(uri);
                var companyContent = await result.Content.ReadAsAsync<List<CompanyModel>>();
                var Companies = companyContent.Select(c => new SelectListItem
                {
                    Value = c.COMPANY_ID.ToString(),
                    Text = c.COMPANY_NAME
                });
                ViewBag.Companies = Companies;

                uri = string.Format("{0}entparams/SHIFT_PATTERN_TYPE", _uri);
                result = await client.GetAsync(uri);
                var shiftPatternTypes = await result.Content.ReadAsAsync<List<dynamic>>();
                var ShiftPatternTypes = shiftPatternTypes.Select(m => new SelectListItem
                {
                    Value = m.CODE,
                    Text = m.VALUE
                });

                uri = string.Format("{0}shift", _uri);
                result = await client.GetAsync(uri);
                var shifts = await result.Content.ReadAsAsync<List<Shift>>();
                var Shifts = shifts.Select(m => new SelectListItem
                {
                    Value = m.SHIFT_ID.ToString(),
                    Text = m.SHIFT_CODE + " - " + m.SHIFT_DESCRIPTION
                });
                ViewBag.ShiftPatternTypes = ShiftPatternTypes;
                ViewBag.Shifts = Shifts;
            }

            //var ShiftType = new List<SelectListItem>();
            //ShiftType.Add(new SelectListItem() { Text = "General", Value = "General" });
            //ShiftType.Add(new SelectListItem() { Text = "Morning", Value = "Morning" });
            //ShiftType.Add(new SelectListItem() { Text = "Afternoon", Value = "Afternoon" });
            //ShiftType.Add(new SelectListItem() { Text = "Night", Value = "Night" });
            //ViewBag.ShiftType = ShiftType;

        }

        //
        // POST: /ShiftPattern/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ShiftPattern collection)
        {
            Utilities _ipaddressobj = new Utilities();
            collection.ipaddress = _ipaddressobj.GetIpAddress();
            try
            {
                if (ModelState.IsValid)
                {
                    using (HttpClient client = new HttpClient())
                    {
                        uri = string.Format("{0}shiftpattern", _uri);

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
        // GET: /ShiftPattern/Edit/5

        public async Task<ActionResult> Edit(int id)
        {
            ShiftPattern shiftPattern = await GetShiftPattern(id);
            await BindDropDown();
            return View(shiftPattern);
        }
        private async Task<ShiftPattern> GetShiftPattern(int id)
        {
            ShiftPattern shiftPattern;
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}shiftpattern/{1}", _uri, id);

                var result = await client.GetAsync(uri);

                shiftPattern = await result.Content.ReadAsAsync<ShiftPattern>();
            }
            return shiftPattern;
        }
        //
        // POST: /ShiftPattern/Edit/5

        [HttpPost]
        public async Task<ActionResult> Edit(int id, ShiftPattern collection)
        {
            Utilities _ipaddressobj = new Utilities();
            collection.ipaddress = _ipaddressobj.GetIpAddress();
            ShiftPattern shiftPattern = await GetShiftPattern(id);
            try
            {
                if (id == collection.SHIFT_PATTERN_ID)
                {
                    // TODO: Add update logic here
                    if (ModelState.IsValid)
                    {
                        using (HttpClient client = new HttpClient())
                        {
                            uri = string.Format("{0}shiftpattern/{1}", _uri, id);
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
                                return View(shiftPattern);
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
            return View(shiftPattern);
        }

        //
        // GET: /ShiftPattern/Delete/5

        public async Task<ActionResult> Delete(int id)
        {
            ShiftPattern shiftPattern = await GetShiftPattern(id);
            await BindDropDown();
            return View(shiftPattern);
        }

        //
        // POST: /ShiftPattern/Delete/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, ShiftPattern collection)
        {
            Utilities _ipaddressobj = new Utilities();
            collection.ipaddress = _ipaddressobj.GetIpAddress();
            try
            {
                // TODO: Add delete logic here
                if (id == collection.SHIFT_PATTERN_ID)
                {
                    using (HttpClient client = new HttpClient())
                    {

                        uri = string.Format("{0}shiftpattern/{1}/{2}/{3}", _uri, id, Session["User"].ToString(), collection.ipaddress);

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

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
    public class VisitorAppointmentRequestController : Controller
    {
        string _uri = ConfigurationManager.AppSettings["APIUrl"].ToString();
        string uri = string.Empty;   
       
        [AccessCheck(IdParamName = "VisitorAppointmentRequest/index")]

        public ActionResult Index()
        {
            var data = ViewData.Model as RoleMenuAccess;
            if (data.CreateAccess == 0)
                ViewBag.CreateAccess = "False";
            if (data.DeleteAccess == 0)
                ViewBag.DeleteAccess = "False";
            if (data.UpdateAccess == 0)
                ViewBag.EditAccess = "False";

            ViewBag.Message = TempData["Message"];
            ViewBag.Status = TempData["Status"];
            ViewBag.InnerMessage = TempData["InnerMessage"];

            List<VisitorAppointmentRequestModel> VisitorList = new List<VisitorAppointmentRequestModel>();
            using (WebClient client = new WebClient())
            {
                string s = client.DownloadString(_uri + "VisitorAppointmentRequestAPI");
                VisitorList = JsonConvert.DeserializeObject<List<VisitorAppointmentRequestModel>>(s);
                VisitorList.RemoveAll(item => item == null);
            }
            return View(VisitorList);
        }

        private async Task<ActionResult> GetVisitorAppointmentRequestList()
        {
            using (HttpClient client = new HttpClient())
            {
                client.AddTokenToHeader("");
                uri = string.Format("{0}VisitorAppointmentRequestAPI", _uri);
                var result = await client.GetAsync(uri);
                var VisitorAppointmentRequestModel = await result.Content.ReadAsAsync<List<VisitorAppointmentRequestModel>>();
                return View(VisitorAppointmentRequestModel);
            }
        }

        private async Task<VisitorAppointmentRequestModel> GetVisitorAppointmentRequest(int id)
        {
            VisitorAppointmentRequestModel VisitorAppointmentRequest;
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}VisitorAppointmentRequestAPI/{1}", _uri, id);
                var result = await client.GetAsync(uri);
                VisitorAppointmentRequest = await result.Content.ReadAsAsync<VisitorAppointmentRequestModel>();
            }
            return VisitorAppointmentRequest;
        }

        private ActionResult FillData(long id)
        {
            VisitorAppointmentRequestModel visitorrequestmodel = new VisitorAppointmentRequestModel();

            List<ApprovalAuth> Approval = new List<ApprovalAuth>();
            List<Location> Location = new List<Location>();
            using (WebClient client = new WebClient())
            {
                string mdl = client.DownloadString(_uri + "VisitorAppointmentRequestAPI/getApproval");
                Approval = JsonConvert.DeserializeObject<List<ApprovalAuth>>(mdl);

                string mdlb = client.DownloadString(_uri + "LocationApi");
                Location = JsonConvert.DeserializeObject<List<Location>>(mdlb);

                string VisitorAppointment = client.DownloadString(_uri + "VisitorAppointmentRequestAPI/" + id);
                visitorrequestmodel = JsonConvert.DeserializeObject<VisitorAppointmentRequestModel>(VisitorAppointment);
            }
            ViewBag.ApprovalId = new SelectList(Approval, "APPROVAL_ID", "APPROVAL_NAME");
            ViewBag.LocationId = new SelectList(Location, "LOCATION_ID", "LOCATION_NAME");

            var VisitorNationality = new List<SelectListItem>();
            VisitorNationality.Add(new SelectListItem() { Text = "Indian", Value = "Indian", Selected = true });
            VisitorNationality.Add(new SelectListItem() { Text = "Foreigner", Value = "Foreigner" });
            ViewBag.VisitorNationality = VisitorNationality;

            var NatureOfWork = new List<SelectListItem>();
            NatureOfWork.Add(new SelectListItem() { Text = "Official", Value = "Official", Selected = true });
            NatureOfWork.Add(new SelectListItem() { Text = "Interview", Value = "Interview" });
            ViewBag.NatureOfWork = NatureOfWork;
            return View(visitorrequestmodel);
        }

        private async Task BindDropDown()
        {
            using (HttpClient client = new HttpClient())
            {
                client.AddTokenToHeader("");
                uri = string.Format("{0}VisitorAppointmentRequestAPI/getApproval", _uri);
                var result = await client.GetAsync(uri);
                var VisitorAppointmentContent = await result.Content.ReadAsAsync<List<ApprovalAuth>>();
                var VisitorAppointment = VisitorAppointmentContent.Select(c => new SelectListItem
                {
                    Value = c.APPROVAL_ID.ToString(),
                    Text = c.APPROVAL_NAME
                });
                ViewBag.ApprovalId = VisitorAppointment;

                client.AddTokenToHeader("");
                uri = string.Format("{0}LocationApi", _uri);
                var result1 = await client.GetAsync(uri);
                var LocationContent = await result1.Content.ReadAsAsync<List<Location>>();
                var Location = LocationContent.Select(c => new SelectListItem
                {
                    Value = c.LOCATION_ID.ToString(),
                    Text = c.LOCATION_NAME
                });
                ViewBag.LocationId = Location;

                var VisitorNationality = new List<SelectListItem>();
                VisitorNationality.Add(new SelectListItem() { Text = "Indian", Value = "Indian", Selected = true });
                VisitorNationality.Add(new SelectListItem() { Text = "Foreigner", Value = "Foreigner" });
                ViewBag.VisitorNationality = VisitorNationality;

                var NatureOfWork = new List<SelectListItem>();
                NatureOfWork.Add(new SelectListItem() { Text = "Official", Value = "Official", Selected = true });
                NatureOfWork.Add(new SelectListItem() { Text = "Interview", Value = "Interview" });
                ViewBag.NatureOfWork = NatureOfWork;
            }
        }

        // GET: /menu/Create
        public async Task<ActionResult> Create()
        {
            await BindDropDown();
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(VisitorAppointmentRequestModel VisitorAppointmentRequest)
        {
            Utilities _ipaddressobj = new Utilities();
            VisitorAppointmentRequest.ipaddress = _ipaddressobj.GetIpAddress();
            try
            {
                if (ModelState.IsValid)
                {
                    using (HttpClient client = new HttpClient())
                    {
                        uri = string.Format("{0}visitorappointmentrequestapi", _uri);

                        var result = await client.PostAsJsonAsync(uri, VisitorAppointmentRequest);

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


        public async Task<ActionResult> Edit(int id)
        {
            VisitorAppointmentRequestModel VisitorAppointmentRequest = await GetVisitorAppointmentRequest(id);
            await BindDropDown();
            return View(VisitorAppointmentRequest);
        }

        //
        // POST: /MeterType/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(VisitorAppointmentRequestModel visitormodel)
        {
            Utilities _ipaddressobj = new Utilities();
            visitormodel.ipaddress = _ipaddressobj.GetIpAddress();
            try
            {
                if (ModelState.IsValid)
                {
                    using (HttpClient client = new HttpClient())
                    {
                        uri = string.Format("{0}VisitorAppointmentRequestAPI", _uri);
                        var result = await client.PutAsJsonAsync(uri, visitormodel);
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
            VisitorAppointmentRequestModel VisitorAppointmentRequest = await GetVisitorAppointmentRequest(id);
            await BindDropDown();
            return View(VisitorAppointmentRequest);
        }

        //
        // POST: /MeterType/Delete/5
       [HttpPost]
        public async Task<ActionResult> DeleteView(int id, VisitorAppointmentRequestModel visitormodel)
        {
            Utilities _ipaddressobj = new Utilities();
            visitormodel.ipaddress = _ipaddressobj.GetIpAddress();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    uri = string.Format("{0}VisitorAppointmentRequestAPI", _uri);
                    var result = await client.DeleteAsync(uri + "/" + visitormodel.REQUEST_ID + "/" + visitormodel.ipaddress);
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

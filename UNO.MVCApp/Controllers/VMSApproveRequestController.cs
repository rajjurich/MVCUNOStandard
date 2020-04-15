using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using UNO.AppModel;
using UNO.MVCApp.Filters;
using UNO.WebApi.Helpers;
using System.Configuration;
using UNO.MVCApp.Common;

namespace UNO.MVCApp.Controllers
{
    [Authorize]
    [SessionCheck]
    public class VMSApproveRequestController : Controller
    {
        string _uri = ConfigurationManager.AppSettings["APIUrl"].ToString();
        string uri = string.Empty;

         [AccessCheck(IdParamName = "VMSApproveRequest/index")]

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

            return await GetVMSApproveRequestList();
        }
        private async Task<ActionResult> GetVMSApproveRequestList()
        {
            using (HttpClient client = new HttpClient())
            {
                client.AddTokenToHeader("");
                uri = string.Format("{0}VMSApproveRequestAPI", _uri);
                var result = await client.GetAsync(uri);
                var VisitorAppointmentRequestModel = await result.Content.ReadAsAsync<List<VisitorAppointmentRequestModel>>();
                return View(VisitorAppointmentRequestModel);
            }
        }

        private async Task<VisitorAppointmentRequestModel> GetVMSApproveRequest(int id)
        {
            VisitorAppointmentRequestModel VisitorAppointmentRequest;
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}VMSApproveRequestAPI/{1}", _uri, id);
                var result = await client.GetAsync(uri);
                VisitorAppointmentRequest = await result.Content.ReadAsAsync<VisitorAppointmentRequestModel>();
            }
            return VisitorAppointmentRequest;
        }

        private async Task BindDropDown()
        {
            using (HttpClient client = new HttpClient())
            {
                var RequestType = new List<SelectListItem>();
                RequestType.Add(new SelectListItem() { Text = "Pending", Value = "N", Selected = true });
                RequestType.Add(new SelectListItem() { Text = "Approved", Value = "A" });
                RequestType.Add(new SelectListItem() { Text = "Rejected", Value = "R" });
                ViewBag.RequestType = RequestType;

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
            }
        }

        public async Task<ActionResult> ApproveReject(int id)
        {
            VisitorAppointmentRequestModel VisitorAppointmentRequest = await GetVMSApproveRequest(id);
            await BindDropDown();
            return View(VisitorAppointmentRequest);
        }

        [HttpPost]
        public async Task<ActionResult> ApproveReject(VisitorAppointmentRequestModel visitormodel, string submit)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    switch (submit)
                    {
                        case "Approve":
                            visitormodel.APPROVAL_STATUS="A";
                            break;
                        case "Reject":
                            visitormodel.APPROVAL_STATUS = "R";
                            break;
                    }

                    var result = await client.PutAsJsonAsync(_uri + "VMSApproveRequestAPI", visitormodel);
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
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                ViewBag.Message = MessageConfig.htmlErrorString;
                ViewBag.Status = "Failed";
                ViewBag.InnerMessage = "";
                return RedirectToAction("Index");
            }
        }
    }
}
